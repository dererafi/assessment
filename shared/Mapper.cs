using Microsoft.EntityFrameworkCore;
using repository;

namespace shared;

public interface IMapper
{
    Task<int>  Save(List<PlayerBetDto> playerBetDtos);
    Task<List<PlayerBetDto>> GetSavedPlayerBetFromDb();
}

public class Mapper : IMapper
{
    public  async Task<int>  Save(List<PlayerBetDto> playerBetDtos)
    {
        var newBets = new List<Bet>();
        
        await using var dbContext = new SqlLiteDbContext();
        await dbContext.Database.EnsureCreatedAsync();
        
        playerBetDtos.ForEach(dto =>
        {
            newBets.Add(new Bet()
            {
                PlayerId = dto.PlayerId,
                BoardArea = dto.BoardArea,
                Moves = MapMoves(dto.Moves, dto.PayOut),
            }); 
        });

        await dbContext.Bets.AddRangeAsync(newBets);
        return await dbContext.SaveChangesAsync();
    }

    private List<Move> MapMoves(Dictionary<string, List<string>> moves, Dictionary<string, string?> payouts)
    {
        var mappedMoves = new List<Move>();
        var ratios = new List<Payout>();
        foreach (var move in moves)
        {
            var numbers = move.Value.Select(num => new MoveNumbers()
            {
                Number = num
            }).ToList();

            ratios.Add( new Payout() {Ratio = payouts[move.Key]});

            mappedMoves.Add(new Move()
            {
                Name = move.Key,
                MoveNumbers = numbers,
                Payout = ratios
            });
        }

        return mappedMoves;
    }

    public async Task<List<PlayerBetDto>> GetSavedPlayerBetFromDb()
    {
        var playerBetDtos = new List<PlayerBetDto>();
        await using var dbContext = new SqlLiteDbContext();
        var bets = dbContext?.Bets
            .Include(m => m.Moves).ThenInclude(n => n.MoveNumbers)
            .Include(m => m.Moves).ThenInclude(p => p.Payout)
            .ToList();
        bets?.ForEach(bet =>
        {
            var moves = bet.Moves;
            playerBetDtos.Add(new PlayerBetDto()
            {
                PlayerId =  bet.PlayerId,
                BoardArea = bet.BoardArea,
                Moves = GetPlayMoves(moves),
                PayOut = GetPlayPayout(moves)
            });
        });
        
        return playerBetDtos;
    }
    
    private Dictionary<string, List<string>> GetPlayMoves(List<Move> playerMoves)
    {
        var sanitized = new Dictionary<string, List<string>>();
        playerMoves.ForEach(move =>
        {
            sanitized[move.Name] = move.MoveNumbers.Select(x => x.Number).ToList();
        });
        return sanitized;
    }

    private Dictionary<string, string?> GetPlayPayout(List<Move> playerMoves)
    {
        var sanitized = new Dictionary<string, string?>();
        playerMoves.ForEach(move =>
        {
            sanitized[move.Name] =  move?.Payout.Select(x => x.Ratio)?.FirstOrDefault();
        });
        
        return sanitized;
    }
}