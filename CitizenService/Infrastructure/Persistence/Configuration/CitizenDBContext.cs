using Domain.Aggregate;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

public class CitizenDBContext : DbContext
{
    public CitizenDBContext(DbContextOptions<CitizenDBContext> options)
        : base(options) { }

    public DbSet<CitizenProfile> CitizenProfiles { get; set; }
    public DbSet<CitizenArea> CitizenAreas { get; set; }

    public DbSet<CollectionReport> CollectionReports { get; set; }
    public DbSet<ComplaintReport> ComplaintReports { get; set; }
    public DbSet<RewardHistory> RewardHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --------------------
        // CitizenProfile
        // --------------------
        modelBuilder.Entity<CitizenProfile>(entity =>
        {
            entity.HasKey(c => c.CitizenProfileID);

            entity.Property(c => c.UserID).IsRequired();
            entity.Property(c => c.DisplayName).IsRequired();
            entity.Property(c => c.AvatarName);
            entity.Property(c => c.PointBalance).IsRequired();
            entity.Property(c => c.JoinedAt).IsRequired();
            entity.Property(c => c.IsActive).HasDefaultValue(true);

            entity.HasMany(c => c.CollectionReports)
                  .WithOne()
                  .HasForeignKey(r => r.CitizenProfileID)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(c => c.ComplaintReports)
                  .WithOne()
                  .HasForeignKey(r => r.CitizenProfileID)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(c => c.RewardHistories)
                  .WithOne()
                  .HasForeignKey(r => r.CitizenProfileID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // --------------------
        // CitizenArea
        // --------------------
        modelBuilder.Entity<CitizenArea>(entity =>
        {
            entity.HasKey(a => a.CitizenAreaID);

            entity.Property(a => a.Name).IsRequired();
            entity.Property(a => a.RegionCode).IsRequired();
            entity.Property(a => a.MinLat).IsRequired();
            entity.Property(a => a.MaxLat).IsRequired();
            entity.Property(a => a.MinLng).IsRequired();
            entity.Property(a => a.MaxLng).IsRequired();
            entity.Property(a => a.IsActive).HasDefaultValue(true);
        });

        // --------------------
        // CollectionReport
        // --------------------
        modelBuilder.Entity<CollectionReport>(entity =>
        {
            entity.HasKey(r => r.CollectionReportID);

            entity.Property(r => r.CitizenProfileID).IsRequired();

            entity.Property(r => r.WasteType).IsRequired();
            entity.Property(r => r.Description);
            entity.Property(r => r.ImageName);
            entity.Property(r => r.Status).IsRequired();
            entity.Property(r => r.ReportAt).IsRequired();
            entity.Property(r => r.RegionCode).IsRequired();
            entity.OwnsOne(r => r.GPS);
        });

        // --------------------
        // ComplaintReport
        // --------------------
        modelBuilder.Entity<ComplaintReport>(entity =>
        {
            entity.HasKey(r => r.ComplaintReportID);

            entity.Property(r => r.CitizenProfileID).IsRequired();
            entity.Property(r => r.CitizenAreaID).IsRequired();

            entity.Property(r => r.Title).IsRequired();
            entity.Property(r => r.Description).IsRequired();
            entity.Property(r => r.ImageName);
            entity.Property(r => r.Status).IsRequired();
            entity.Property(r => r.ReportAt).IsRequired();

            entity.HasOne(r => r.CitizenArea)
                  .WithMany()
                  .HasForeignKey(r => r.CitizenAreaID)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // --------------------
        // RewardHistory
        // --------------------
        modelBuilder.Entity<RewardHistory>(entity =>
        {
            entity.HasKey(r => r.RewardHistoryID);

            entity.Property(r => r.CitizenProfileID).IsRequired();
            entity.Property(r => r.CitizenAreaID).IsRequired();

            entity.Property(r => r.Point).IsRequired();
            entity.Property(r => r.Reason).IsRequired();
            entity.Property(r => r.OccurredAt).IsRequired();

            entity.HasOne(r => r.CitizenArea)
                  .WithMany()
                  .HasForeignKey(r => r.CitizenAreaID)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // --------------------
        // AuditLog configuration
        // --------------------
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(a => a.AuditLogId);

            entity.Property(a => a.EntityName)
                  .IsRequired();

            entity.Property(a => a.Action)
                  .IsRequired();

            entity.Property(a => a.PerformedBy)
                  .HasMaxLength(100);

            entity.Property(a => a.OldValue);

            entity.Property(a => a.NewValue);

            entity.Property(a => a.Timestamp)
                  .HasDefaultValueSql("GETUTCDATE()");
        });
    }
}
