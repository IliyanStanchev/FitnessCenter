using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FitnessCenter.Models
{
    public partial class FitnessContext : DbContext
    {
        public FitnessContext()
            : base("name=FitnessContext")
        {
        }

        public virtual DbSet<Balance> BALANCES { get; set; }
        public virtual DbSet<CustomerSubscriptions> CUSTOMER_SUBSCRIPTIONS { get; set; }
        public virtual DbSet<CustomerVisit> CUSTOMER_VISITS { get; set; }
        public virtual DbSet<Customer> CUSTOMERS { get; set; }
        public virtual DbSet<Delivery> DELIVERIES { get; set; }
        public virtual DbSet<EmployeeSchedule> EMPLOYEE_SCHEDULES { get; set; }
        public virtual DbSet<Employee> EMPLOYEES { get; set; }
        public virtual DbSet<Equipment> EQUIPMENTS { get; set; }
        public virtual DbSet<InstructorSchedule> INSTRUCTOR_SCHEDULES { get; set; }
        public virtual DbSet<Product> PRODUCTS { get; set; }
        public virtual DbSet<Provider> PROVIDERS { get; set; }
        public virtual DbSet<Role> ROLES { get; set; }
        public virtual DbSet<Salary> SALARIES { get; set; }
        public virtual DbSet<Subscription> SUBSCRIPTIONS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Balance>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerVisit>()
                .Property(e => e.CustomerName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.CUSTOMER_SUBSCRIPTIONS)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.CustomerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EMPLOYEE_SCHEDULES)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.INSTRUCTOR_SCHEDULES)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.SALARIES)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Barcode)
                .IsUnicode(false);

            modelBuilder.Entity<Provider>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Provider>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Provider>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Provider>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Provider>()
                .HasMany(e => e.DELIVERIES)
                .WithRequired(e => e.Provider)
                .HasForeignKey(e => e.ProviderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.EMPLOYEES)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subscription>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
