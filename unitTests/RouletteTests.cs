using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using repository;
using shared;

namespace unitTests;

[TestFixture]
public class RouletteTests
{
    private BetMoveService _betMoveService;

    private string _outsideBoardArea;
    private string _insideBoardArea;
    
    private string _playerId;
    private string _evenBet;
    private string _splitBet;
    private List<string> _EvenBetMoves;
    private List<string> _splitBetMoves;
    private PlayerBetDto _playerBetDto;
    private PlayerBetDto _playeSplitBetsDto;
    private List<string> _boardNumbers;
    
    [SetUp]
    public void Init()
    {
        _betMoveService = new BetMoveService();

        _playerId = "Player1";
        
        _outsideBoardArea = Constants.BoardArea.Outside;
        _evenBet = Constants.MoveType.Keys["Even"];
        _EvenBetMoves =  Constants.Moves.Out[_evenBet];
        _playerBetDto = new PlayerBetDto()
        {
            PlayerId = "Player4",
            BoardArea = _outsideBoardArea,
            Moves = new Dictionary<string, List<string>>(){ {_evenBet, _EvenBetMoves} },
            PayOut = new Dictionary<string, string> { { _evenBet, Constants.PayOut.Move[_evenBet] } }
        };
        
        
        _insideBoardArea = Constants.BoardArea.Inside;
        _splitBet = Constants.MoveType.Keys["Split"];
        _splitBetMoves = new List<string>() {  "1" , "2"  };
        _playeSplitBetsDto = new PlayerBetDto()
        {
            PlayerId = "Player1",
            BoardArea = _insideBoardArea,
            PayOut =  new Dictionary<string, string> { {_splitBet, Constants.PayOut.Move[_evenBet]} },
            Moves = new Dictionary<string, List<string>>(){
            {
                _splitBet, _splitBetMoves
            }},
        };
        
    }


    [Test]
    public void GetBoardAreaBetDetails_GivenOutsideBoardAreaReturnOutsideDetails()
    {
        //Arrange 

        //Act
        var bet = _betMoveService.GetBoardAreaBetDetails(_outsideBoardArea, _playerId, _evenBet, new List<string>());
        
        //Assert  
        Assert.AreEqual(_playerBetDto.PlayerId, bet.PlayerId);
    } 
    
    [Test]
    public void GetBoardAreaBetDetails_GivenInsideBoardAreaReturnInsideDetails()
    {
        //Arrange 

        //Act
        var bet = _betMoveService.GetBoardAreaBetDetails(_insideBoardArea, _playerId, _splitBet, _splitBetMoves);
        
        //Assert  
        Assert.That(bet.PayOut,  Is.Not.EqualTo(string.Empty));
    }

    [Test]
    
    public void GetBoardAreaBetDetails_GivenInvalidBoardAreaThrowException()
    {
        //Arrange 
        var func = TestGetBoardAreaBetDetails;

        //Act

        //Assert  
        Assert.That(TestGetBoardAreaBetDetails, Throws.TypeOf<ArgumentException>());
    }

    private void TestGetBoardAreaBetDetails()
    {
        var bet = _betMoveService.GetBoardAreaBetDetails("", _playerId, _evenBet, _splitBetMoves);
    }

    [Test]
    public void GetOutsideBetDetails_GivenEvenPopulatePayerBetWithEvenBoardNumbers()
    {
        //Arrange 
        var boardArea = Constants.BoardArea.Outside;
        var playerId = "Player1";
        var bet = Constants.MoveType.Keys["Even"];
        var moves = new List<string>();
        var evenBets = new List<string> {"2", "4", "6", "8", "10", "12", "14", "16", "18", "20", "22", "24", "26", "28", "30", "32", "34", "36"};
        
        //Act
        var playerBet = _betMoveService.GetBoardAreaBetDetails(boardArea, playerId, bet, moves);
        
        //Assert  
        var playerNumbers = playerBet.Moves["Even"];
        Assert.IsTrue(playerNumbers.Any(evenBets.Contains));
    }
    
    [Test]
    public void GetInsideBetDetails_GivenPayerDetailPopulatePayerBet()
    {
        //Arrange
        
        
        //Act
        var playerBet = _betMoveService.GetBoardAreaBetDetails(_insideBoardArea, _playerId, _splitBet, _splitBetMoves);
        
        //Assert  
        Assert.That(playerBet.PayOut, Is.Not.EqualTo(string.Empty));
        
    }

    [Test]
    public void Spin_GivenRequestToSpinReturnRandomBoardNumber()
    {
        //Arrange
        _boardNumbers = new List<string>{"0", "00", "1", "2","3" ,"4", "5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20","21","22","23","24","25","26","27","28","29","30","31","32","33","34","35","36"};

        //Act
        var result = _betMoveService.Spin();
        var found = _boardNumbers.Any(x => x == result);
        
        //Assert 
        Assert.IsTrue(found);
    }
    
    [Test]
    public void Payout_GivenAllPossiblePlayerBets_CheckIfWinningBetExists_ReturnsPayRatioIfCheckIfWinningBetExist()
    {
        //Arrange 
        var winningNumber = _betMoveService.Spin();
        
        //Act
        _playerBetDto.PayOut = _betMoveService.Payout(winningNumber, _playerBetDto.Moves);
        
        //Assert
        Assert.That(winningNumber, Is.Not.EqualTo(string.Empty));
    }

    [Test]
    public async Task GivenPlayBet_SaveRecordToSqlLiteDb()
    {
        var dbName = "RouletteDb.db";
        if(File.Exists(dbName))
            File.Delete(dbName);
        
        await using var dbContext = new SqlLiteDbContext();
        await dbContext.Database.EnsureCreatedAsync();
        await dbContext.Bets.AddRangeAsync(new[]
        {
            new Bet()
            {
                BoardArea = Constants.BoardArea.Inside,
                PlayerId = "Player1",
                Moves = new List<Move>()
                {
                    new Move()
                    {
                       Name = "Even",
                       Payout = new List<Payout>()
                       {
                           new Payout()
                           {
                               Ratio = "2:1"
                           }
                       },
                       MoveNumbers = new List<MoveNumbers>()
                       {
                           new MoveNumbers()
                           {
                               Number = "2"
                           },
                           new MoveNumbers()
                           {
                               Number = "4"
                           },
                           new MoveNumbers()
                           {
                               Number = "6"
                           },
                           new MoveNumbers()
                           {
                               Number = "8"
                           },
                           new MoveNumbers()
                           {
                               Number = "10"
                           },
                           new MoveNumbers()
                           {
                               Number = "12"
                           }
                       }
                    }
                }
            },
            new Bet()
            {
                BoardArea = Constants.BoardArea.Outside,
                PlayerId = "Player1",
                Moves = new List<Move>()
                {
                    new Move()
                    {
                        Name = "Odd",
                        Payout = new List<Payout>()
                        {
                            new Payout()
                            {
                                Ratio = "2:1"
                            }
                        },
                        MoveNumbers = new List<MoveNumbers>()
                        {
                            new MoveNumbers()
                            {
                                Number = "1"
                            },
                            new MoveNumbers()
                            {
                                Number = "3"
                            },
                            new MoveNumbers()
                            {
                                Number = "5"
                            },
                            new MoveNumbers()
                            {
                                Number = "7"
                            }
                        }
                    },
                    
                }
            },
            new Bet()
            {
                BoardArea = Constants.BoardArea.Inside,
                PlayerId = "Player1",
                Moves = new List<Move>()
                {
                    new Move()
                    {
                        Name = "Split",
                        Payout = new List<Payout>()
                        {
                            new Payout()
                            {
                                Ratio = "2:1"
                            }
                        },
                        MoveNumbers = new List<MoveNumbers>()
                        {
                            new MoveNumbers()
                            {
                                Number = "2"
                            },
                            new MoveNumbers()
                            {
                                Number = "4"
                            }
                        }
                    }
                }
            }
        });
        await dbContext.SaveChangesAsync();

        var bets = dbContext?.Bets.ToList();
        
        Assert.IsTrue(bets?.Count > 0);
    }
    
    [Test]
    public async Task GivenPlayBet_SaveRecordToSqlLiteDbUsingEfCore()
    {
        //Arrange
        var mapper = new Mapper();
       
        //Act
        await mapper.Save(new List<PlayerBetDto>(){ _playerBetDto });
        
        //Assert
        await using var dbContext = new SqlLiteDbContext();
        var bets = dbContext.Bets.Where(p => p.PlayerId == _playerBetDto.PlayerId).ToList();
        Assert.IsTrue(bets?.Count > 0);
    }

    [Test]
    public async Task GivenPlayId_GetSavedRecordToSqlLiteDb()
    {
       //Arrange
       var mapper = new Mapper();
       
       //Act
       var playerBetDtos = await mapper.GetSavedPlayerBetFromDb();
        
       //Assert
       Assert.IsTrue(playerBetDtos?.Count > 0);
    }

   
}