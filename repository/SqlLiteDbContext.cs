using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace repository;

public class SqlLiteDbContext: DbContext
{
    public DbSet<Bet> Bets { get; set; }
    
    public string FilePath { get; set; }  = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"RouletteDb.db");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("FileName=" + FilePath, option =>
        {
            option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        });
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bet>().ToTable("Bet", "test");
        modelBuilder.Entity<Bet>(entity =>
        {
            entity.HasKey(k => k.Id);
        });
        
        modelBuilder.Entity<Move>().ToTable("Move", "test");
        modelBuilder.Entity<Move>(entity =>
        {
            entity.HasKey(k => k.Id);
        });
        
        modelBuilder.Entity<MoveNumbers>().ToTable("MoveNumbers", "test");
        modelBuilder.Entity<MoveNumbers>(entity =>
        {
            entity.HasKey(k => k.Id);
        });
        
        modelBuilder.Entity<Payout>().ToTable("Payout", "test");
        modelBuilder.Entity<Payout>(entity =>
        {
            entity.HasKey(k => k.Id);
        });
        
        
        base.OnModelCreating(modelBuilder);
    }
}