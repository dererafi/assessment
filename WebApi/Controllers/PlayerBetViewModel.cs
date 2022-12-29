namespace WebApi.Controllers;

public class PlayerBetViewModel
{
   public string PlayerId { get; set; } 
   public string BoardArea { get; set; }
   public string Bet { get; set; } 
   public List<string> Moves { get; set; } 
}