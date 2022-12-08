namespace FitnessCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EMPLOYEES")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            EMPLOYEE_SCHEDULES = new HashSet<EmployeeSchedule>();
            INSTRUCTOR_SCHEDULES = new HashSet<InstructorSchedule>();
            SALARIES = new HashSet<Salary>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Username { get; set; }

        [Required]
        [StringLength(256)]
        public string Password { get; set; }

        [Required]
        [StringLength(64)]
        [Column("FIRST_NAME")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(64)]
        [Column("LAST_NAME")]
        public string LastName { get; set; }

        [Column("APPOINTMENT_DATE")]
        public DateTime AppointmentDate { get; set; }

        public double Salary { get; set; }

        [Column("ROLE_ID")]
        public int RoleId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSchedule> EMPLOYEE_SCHEDULES { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorSchedule> INSTRUCTOR_SCHEDULES { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Salary> SALARIES { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
