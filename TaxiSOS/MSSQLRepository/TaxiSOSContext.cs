using Microsoft.EntityFrameworkCore;
using DataModel;

namespace MSSQLRepository
{
    public partial class TaxiSOSContext : DbContext
    {
        public virtual DbSet<Cards> Cards { get; set; }
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Drivers> Drivers { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PersonalAccount> PersonalAccount { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=LENOVO;Database=TaxiSOS;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cards>(entity =>
            {
                entity.HasKey(e => new { e.IdClient, e.CardNumber });

                entity.Property(e => e.IdClient).HasColumnName("Id_Client");

                entity.Property(e => e.CardNumber)
                    .HasColumnName("Card_Number")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Cvv)
                    .IsRequired()
                    .HasColumnName("CVV")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.ExpireDate)
                    .HasColumnName("Expire_Date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cards_Clients");
            });

            modelBuilder.Entity<Cars>(entity =>
            {
                entity.HasKey(e => e.IdCar);

                entity.Property(e => e.IdCar)
                    .HasColumnName("Id_Car")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdDriver).HasColumnName("Id_Driver");

                entity.Property(e => e.Mark)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationNumber)
                    .IsRequired()
                    .HasColumnName("Registration_Number")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceClass)
                    .IsRequired()
                    .HasColumnName("Service_Class")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDriverNavigation)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.IdDriver)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cars_Drivers");
            });

            modelBuilder.Entity<Clients>(entity =>
            {
                entity.HasKey(e => e.IdClient);

                entity.Property(e => e.IdClient)
                    .HasColumnName("Id_Client")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TelephoneNumber)
                    .IsRequired()
                    .HasColumnName("Telephone_Number")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Drivers>(entity =>
            {
                entity.HasKey(e => e.IdDriver);

                entity.Property(e => e.IdDriver)
                    .HasColumnName("Id_Driver")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.LicenseNumber)
                    .IsRequired()
                    .HasColumnName("License_Number")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDriverNavigation)
                    .WithOne(p => p.Drivers)
                    .HasForeignKey<Drivers>(d => d.IdDriver)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Drivers_Personal_Account");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.IdOrder);

                entity.Property(e => e.IdOrder)
                    .HasColumnName("Id_Order")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ArrivalPoint)
                    .IsRequired()
                    .HasColumnName("Arrival_Point")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DestinationPoint)
                    .IsRequired()
                    .HasColumnName("Destination_Point")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdClient).HasColumnName("Id_Client");

                entity.Property(e => e.IdDriver).HasColumnName("Id_Driver");

                entity.Property(e => e.OrderTime)
                    .HasColumnName("Order_Time")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Clients");

                entity.HasOne(d => d.IdDriverNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdDriver)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Drivers");
            });

            modelBuilder.Entity<PersonalAccount>(entity =>
            {
                entity.HasKey(e => e.IdDriver);

                entity.ToTable("Personal_Account");

                entity.Property(e => e.IdDriver)
                    .HasColumnName("Id_Driver")
                    .ValueGeneratedNever();

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasColumnName("Account_Number")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
