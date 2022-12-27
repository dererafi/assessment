namespace repository;

public class Payout
{
    public int Id { get; set; }
    public string? Ratio { get; set; }
    public Move Move { get; set; }
    public int MoveId { get; set; }
}