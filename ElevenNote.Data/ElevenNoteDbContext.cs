using Microsoft.EntityFrameworkCore;
public class ElevenNoteDbContext : DbContext
{
    public ElevenNoteDbContext(DbContextOptions<ElevenNoteDbContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
}
    
