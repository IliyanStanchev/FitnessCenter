namespace FitnessCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EMPLOYEE_SCHEDULES")]
    public partial class EmployeeSchedule
    {
        public int Id { get; set; }

        [Column("EMPLOYEE_ID")]
        public int EmployeeId { get; set; }

        [Column("START_DATE")]
        public DateTime StartDate { get; set; }

        [Column("END_DATE")]
        public DateTime EndDate { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
