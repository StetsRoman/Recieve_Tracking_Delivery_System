namespace RTDS.Domain.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RTDSModel : DbContext
    {
        public RTDSModel()
            : base("name=RTDSContext")
        {
        }

        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<BranchType> BranchTypes { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Package_Statuses> Package_Statuses { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>()
                .Property(e => e.Street)
                .IsUnicode(false);

            modelBuilder.Entity<Branch>()
                .Property(e => e.House)
                .IsUnicode(false);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Packages)
                .WithRequired(e => e.Branch)
                .HasForeignKey(e => e.CurrentLocation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Packages1)
                .WithRequired(e => e.Branch1)
                .HasForeignKey(e => e.DestinationLocation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Packages2)
                .WithRequired(e => e.Branch2)
                .HasForeignKey(e => e.SourceLocation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Branch)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BranchType>()
                .Property(e => e.BranchName)
                .IsUnicode(false);

            modelBuilder.Entity<BranchType>()
                .HasMany(e => e.Branches)
                .WithRequired(e => e.BranchType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<City>()
                .Property(e => e.CityName)
                .IsUnicode(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.Branches)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Phone_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Packages)
                .WithRequired(e => e.Client)
                .HasForeignKey(e => e.ReceiverID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Packages1)
                .WithRequired(e => e.Client1)
                .HasForeignKey(e => e.SenderID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Package_Statuses>()
                .Property(e => e.Status_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Package_Statuses>()
                .HasMany(e => e.Packages)
                .WithRequired(e => e.Package_Statuses)
                .HasForeignKey(e => e.StatusID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Package>()
                .Property(e => e.Package_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Package>()
                .Property(e => e.Size)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone_Number)
                .IsUnicode(false);
        }
    }
}
