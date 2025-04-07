using System;
using System.Collections.Generic;
using EventEase.Models;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Data;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:st10441724.database.windows.net,1433;Initial Catalog=eventeasekit;Persist Security Info=False;User ID=aadim;Password=Aadi@18!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951AED37B083B4");

            entity.ToTable("Booking");

            entity.HasIndex(e => new { e.VenueId, e.BookingDate }, "UQ_Booking").IsUnique();

            entity.Property(e => e.BookingDate).HasColumnType("datetime");

            entity.HasOne(d => d.Event).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__EventId__6383C8BA");

            entity.HasOne(d => d.Venue).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.VenueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__VenueId__6477ECF3");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Event__7944C8104DC8F3BD");

            entity.ToTable("Event");

            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.EventName).HasMaxLength(100);

        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(e => e.VenueId).HasName("PK__Venue__3C57E5F25F43DEB9");

            entity.ToTable("Venue");

            entity.HasIndex(e => e.VenueName, "UQ__Venue__A40F8D126D34C9A5").IsUnique();

            entity.Property(e => e.ImageUrl).IsUnicode(false);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.VenueName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
