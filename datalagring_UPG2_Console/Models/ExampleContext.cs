using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace datalagring_UPG2_Console.Models
{
    public partial class ExampleContext : DbContext
    {
        public ExampleContext()
        {
        }

        public ExampleContext(DbContextOptions<ExampleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<SupportCases> SupportCases { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:ec-server-aj.database.windows.net,1433;Initial Catalog=UPG2-Datalagringdb;Persist Security Info=False;User ID=sqlAdmin;Password=MyLife2020;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.Ssnumber)
                    .HasName("PK__Customer__D52535032868D246");
            });

            modelBuilder.Entity<SupportCases>(entity =>
            {
                entity.HasKey(e => e.CaseNumber)
                    .HasName("PK__SupportC__103BB8D974776C10");

                entity.HasOne(d => d.CustomerNumberNavigation)
                    .WithMany(p => p.SupportCases)
                    .HasForeignKey(d => d.CustomerNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SupportCa__Custo__66603565");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
