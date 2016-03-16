using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using KonigLabs.LevisJeans.Models.Entities;

namespace KonigLabs.LevisJeans.Models.Contexts
{
    public class MainDataContext : DbContext
    {
        public MainDataContext()
            : base("DefaultConnection")
        {
        }

        public new Database Database => base.Database;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Customer

            modelBuilder.Entity<Customer>()
                .ToTable("Customers")
                .HasKey(p => p.Id)
                .Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Customer>()
                .Property(p => p.FirstName)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(p => p.LastName)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(p => p.Email)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(p => p.Phone)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .HasOptional(p => p.Answers)
                .WithRequired(p => p.Customer);

            #endregion

            #region Test

            modelBuilder.Entity<Test>()
                .ToTable("Tests")
                .HasKey(p => p.Id);

            modelBuilder.Entity<Test>()
                .Property(p => p.Answer1)
                .IsRequired();

            modelBuilder.Entity<Test>()
                .Property(p => p.Answer2)
                .IsRequired();

            modelBuilder.Entity<Test>()
                .Property(p => p.Answer3)
                .IsRequired();

            modelBuilder.Entity<Test>()
                .Property(p => p.Answer4)
                .IsRequired();

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}