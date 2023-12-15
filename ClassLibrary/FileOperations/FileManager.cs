using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace ClassLibrary.FileOperations;

public class FileManager<T> where T : class
{
    private readonly string _root;
    private readonly PropertyInfo _property;
    private readonly ReaderWriterLockSlim _readerWriterLock;
    private readonly string _sequencePath;
    private readonly JsonSerializerOptions _options;

    public FileManager()
    {
        var property = typeof(T).GetProperty("Id");
        if (property is null || property.PropertyType != typeof(int))
        {
            throw new Exception("Class doesn't have Id property.");
        }

        _property = property;
        _readerWriterLock = new ReaderWriterLockSlim();
        DirectoryInfo directoryInfo =
            new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "DataBase", typeof(T).Name, "data"));
        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }

        _root = directoryInfo.FullName;
        _sequencePath = Path.Combine(directoryInfo.Parent.FullName, "sequence.txt");
        FileInfo fileInfo = new FileInfo(_sequencePath);
        if (!fileInfo.Exists)
        {
            BinaryWriter binaryWriter = new BinaryWriter(fileInfo.Create());
            binaryWriter.Write(0);
            binaryWriter.Close();
        }
        
        _options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
    }

    public Task Insert(T data)
    {
        int id;

        try
        {
            _readerWriterLock.EnterWriteLock();
            using (FileStream fileStream = new FileStream(_sequencePath, FileMode.Open, FileAccess.ReadWrite))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream, Encoding.UTF8, true))
                {
                    id = binaryReader.ReadInt32();
                }

                fileStream.Position = 0;
                
                using (BinaryWriter binaryWriter = new BinaryWriter(fileStream, Encoding.UTF8, false))
                {
                    binaryWriter.Write(++id);
                }
            }
        }
        finally
        {
            _readerWriterLock.ExitWriteLock();
        }
        
        _property.SetValue(data, id);

        try
        {
            _readerWriterLock.EnterWriteLock();
            using (FileStream fileStream = new FileStream(Path.Combine(_root, id.ToString()), FileMode.CreateNew))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(JsonSerializer.Serialize(data, _options));
                }
            }
        }
        finally
        {
            _readerWriterLock.ExitWriteLock();
        }
        
        return Task.CompletedTask;
    }
}