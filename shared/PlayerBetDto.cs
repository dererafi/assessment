namespace shared;

public class PlayerBetDto
{
    public string PlayerId { get; set; } = String.Empty;
    public string BoardArea { get; set; } = String.Empty;
    
    public bool Winning { get; set; } = false;
    public Dictionary<string, string?> PayOut { get; set; } = new Dictionary<string, string>();
    public Dictionary<string, List<string>>  Moves { get; set; } = new Dictionary<string, List<string>>();
}