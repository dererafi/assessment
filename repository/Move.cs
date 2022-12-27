namespace repository;

public class Move
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Bet Bet { get; set; }
    public int BetId { get; set; }
    public List<MoveNumbers> MoveNumbers { get; set; }
    public List<Payout> Payout { get; set; }
}