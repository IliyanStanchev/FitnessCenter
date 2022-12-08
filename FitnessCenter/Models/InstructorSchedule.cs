namespace FitnessCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("INSTRUCTOR_SCHEDULES")]
    public partial class InstructorSchedule
    {
        public int Id { get; set; }

        [Column("EMPLOYEE_ID")]
        public int EmployeeId { get; set; }

        [Column("CUSTOMER_ID")]
        public int CustomerId { get; set; }

        [Column("TRAINING_TYPE")]
        public int TrainingType { get; set; }

        [Column("START_DATE")]
        public DateTime StartDate { get; set; }
        
        [Column("END_DATE")]
        public DateTime EndDate { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
