using MiApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MiApi.Persistence;

public class FernandoDbContext : DbContext
{
    public FernandoDbContext(DbContextOptions<FernandoDbContext> options) : base(options)
    {
    }


    public DbSet<User> Usuarios { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
}