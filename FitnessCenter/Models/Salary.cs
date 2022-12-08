namespace FitnessCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SALARIES")]
    public partial class Salary
    {
        public int Id { get; set; }

        [Column("EMPLOYEE_ID")]
        public int EmployeeId { get; set; }

        public DateTime Date { get; set; }

        public double Value { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
