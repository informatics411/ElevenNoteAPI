using Microsoft.EntityFrameworkCore;
public class ElevenNoteDbContext : DbContext
{
    public ElevenNoteDbContext(DbContextOptions<ElevenNoteDbContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<NoteEntity> Notes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NoteEntity>()
        .HasOne(n => n.Owner)
        .WithMany(p => p.Notes)
        .HasForeignKey(n => n.OwnerId);
    }
}
    
