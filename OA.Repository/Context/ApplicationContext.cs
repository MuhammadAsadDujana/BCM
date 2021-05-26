using OA.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Repository.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("BCMConnectionString")
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<ForgotPasswordLinks> ForgotPasswordLinks { get; set; }

        public DbSet<Physician> Physician { get; set; }
        public DbSet<PhysicianCredentials> PhysicianCredentials { get; set; }
        public DbSet<PhysicianDiscipline> PhysicianDiscipline { get; set; }
        public DbSet<PhysicianTreatmentTeam> PhysicianTreatmentTeam { get; set; }
        public DbSet<PhysicianSpecialties> PhysicianSpecialties { get; set; }
        public DbSet<PhysicianLanguage> PhysicianLanguage { get; set; }

        public DbSet<AreaOfInterest> AreaOfInterests { get; set; }
        public DbSet<Discipline> Discipline { get; set; }
        public DbSet<TreatmentTeam> TreatmentTeam { get; set; }
        public DbSet<Credentials> Credentials { get; set; }
        public DbSet<Specialties> Specialties { get; set; }
        public DbSet<Language> Language { get; set; }


        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        //public DbSet<City> City { get; set; }

        public DbSet<UserAreaOfInterest> UserAreaOfInterest { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasRequired(m => m.Country).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasRequired(m => m.State).WithMany().WillCascadeOnDelete(false);
            //modelBuilder.Entity<User>().HasRequired(m => m.City).WithMany().WillCascadeOnDelete(false);
        }
    }
}
