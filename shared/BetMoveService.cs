using System.Linq;

namespace shared;

public interface IBetMoveService
{
    PlayerBetDto GetOutsideBetDetails(string playerId, string bet);
    PlayerBetDto GetInsideBetDetails(string playerId, List<string> moves);
    PlayerBetDto GetBoardAreaBetDetails(string boardArea, string playerId, string bet, List<string> moves);
    string Spin();
    Dictionary<string, string?> Payout(string winningNumber, Dictionary<string, List<string>> moves);
}

public class BetMoveService : IBetMoveService
{
    public PlayerBetDto GetOutsideBetDetails(string playerId, string bet)
    {
        var key = Constants.MoveType.Keys[bet];
        var evenMoves = Constants.Moves.Out[bet];
        return new PlayerBetDto()
        {
            BoardArea = Constants.BoardArea.Outside,
            PlayerId = playerId,
            Moves = new Dictionary<string, List<string>>(){ {key, evenMoves} },
            PayOut =  new Dictionary<string, string> { {key, Constants.PayOut.Move[key] } }
        };
    }

    public PlayerBetDto GetInsideBetDetails(string playerId, List<string> moves)
    {
        var numberOfMoves = moves.Count;
        var key = Constants.Moves.In[numberOfMoves].FirstOrDefault();
        return new PlayerBetDto()
        {
            BoardArea = Constants.BoardArea.Inside,
            PlayerId = playerId,
            Moves = new Dictionary<string, List<string>>(){ {key, moves} },
            PayOut =  new Dictionary<string, string>{ {key, Constants.PayOut.Move[key]} }
        };
    }

    public PlayerBetDto GetBoardAreaBetDetails(string boardArea, string playerId, string bet, List<string> moves)
    {
        return boardArea switch
        {
            Constants.BoardArea.Outside => GetOutsideBetDetails(playerId, bet),
            Constants.BoardArea.Inside => GetInsideBetDetails(playerId, moves),
            _ => throw new ArgumentException("invalid arguments", nameof(boardArea))
        };
    }

    public string Spin()
    {
        var random = new Random();
        var list = new List<string>{"0", "00", "1", "2","3" ,"4", "5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20","21","22","23","24","25","26","27","28","29","30","31","32","33","34","35","36"};
        var index = random.Next(list.Count);
        return list[index];
    }

    public Dictionary<string, string?> Payout(string winningNumber, Dictionary<string, List<string>> moves)
    {
       var ratios = new Dictionary<string, string>();
       foreach (var move in moves)
       {
           if (move.Value.Any(n => n == winningNumber))
           {
               ratios[move.Key] = Constants.PayOut.Move[move.Key];
           }
       }
        
       return ratios;
    }
    
}