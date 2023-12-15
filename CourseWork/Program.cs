using System.Text;
using ClassLibrary.Entity;
using ClassLibrary.FileOperations;

internal class Program
{
    public static async Task Main(string[] args)
    {
        // Generate test data
        /*Client[] clients = new Client[100];
        for (var i = 0; i < clients.Length; i++)
        {
            clients[i] = new Client()
            {
                Lastname = GenerateLine(),
                Firstname = GenerateLine(),
                Middlename = GenerateLine(),
                PhoneNumber = GeneratePhoneNumber()
            };
        }
        
        
        FileManager<Client> fileManage = new FileManager<Client>();
        foreach (var client in clients)
        {
            await fileManage.Insert(client);
        }*/

        /*string[] filmNames = new string[] { "Форсаж", "Dumb & Dumber", "Lion King", "Spiderman: No way home" };
        Ticket[] tickets = new Ticket[250];
        for (var i = 0; i < tickets.Length; i++)
        {
            tickets[i] = new Ticket()
            {
                ClientId = null,
                FilmName = filmNames[Random.Shared.Next(0, filmNames.Length)],
                Place = Random.Shared.Next(1, 26),
                Price = Random.Shared.Next(500, 2001),
                Seance = i % 2 == 0 ? SeanceEnum.Day : SeanceEnum.Night,
                Start = new DateTime(2023, 12, 15, Random.Shared.Next(8, 21), 0, 0)
            };
        }
        
        FileManager<Ticket> fileManager = new FileManager<Ticket>();
        foreach (var ticket in tickets)
        {
            await fileManager.Insert(ticket);
        }*/
        
        
    }

    /*private static string GenerateLine()
    {
        StringBuilder builder = new StringBuilder();
        int count = Random.Shared.Next(1, 11);
        for (int i = 0; i < count; i++)
        {
            builder.Append((char)Random.Shared.Next(65, 91));
        }

        return builder.ToString();
    }
    
    private static string GeneratePhoneNumber()
    {
        StringBuilder builder = new StringBuilder();
        int count = 9;
        for (int i = 0; i < count; i++)
        {
            builder.Append(Random.Shared.Next(0, 10));
        }

        return "+380" + builder;
    }*/
}