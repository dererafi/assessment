namespace repository;

public class Bet
{
    public int Id { get; set; }
    public string PlayerId { get; set; } = string.Empty;
    public string BoardArea { get; set; } = string.Empty;
    public List<Move> Moves { get; set; } = new List<Move>();
}