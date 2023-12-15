namespace ClassLibrary.Entity;

public class Ticket
{
    public int Id { get; set; }
    public SeanceEnum Seance { get; set; }
    public string FilmName { get; set; }
    public DateTime Start { get; set; }
    public int Place { get; set; }
    public decimal Price { get; set; }

    public int? ClientId { get; set; }
}