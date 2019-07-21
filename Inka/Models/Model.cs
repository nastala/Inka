namespace Inka.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=ModelConnectionString")
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<EducationLevel> EducationLevels { get; set; }
        public virtual DbSet<ForeignLanguage> ForeignLanguages { get; set; }
        public virtual DbSet<Licence> Licences { get; set; }
        public virtual DbSet<Nation> Nations { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<University> Universities { get; set; }
        public virtual DbSet<UniversityDepartment> UniversityDepartments { get; set; }
        public virtual DbSet<UserForeignLanguage> UserForeignLanguages { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasMany(e => e.Districts)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<District>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.District)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EducationLevel>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.EducationLevel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ForeignLanguage>()
                .HasMany(e => e.UserForeignLanguages)
                .WithRequired(e => e.ForeignLanguage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Licence>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Licence)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Nation>()
                .HasMany(e => e.Cities)
                .WithRequired(e => e.Nation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Nation>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Nation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<University>()
                .HasMany(e => e.UniversityDepartments)
                .WithRequired(e => e.University)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.IdentityNumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.TelephoneNumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserForeignLanguages)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
