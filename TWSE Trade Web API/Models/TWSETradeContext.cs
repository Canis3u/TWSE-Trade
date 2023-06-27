using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TWSE_Trade_Web_API.Models
{
    public partial class TWSETradeContext : DbContext
    {
        public TWSETradeContext()
        {
        }

        public TWSETradeContext(DbContextOptions<TWSETradeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClosingPrice> ClosingPrices { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<Trade> Trades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<ClosingPrice>(entity =>
            {
                entity.HasKey(e => new { e.StockId, e.TradeDate });

                entity.ToTable("ClosingPrice");

                entity.Property(e => e.StockId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TradeDate).HasColumnType("date");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.ClosingPrices)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClsingPrice_Stock");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("Stock");

                entity.Property(e => e.StockId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Trade>(entity =>
            {
                entity.ToTable("Trade");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StockId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TradeDate).HasColumnType("date");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.Trades)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trade_Stock");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
