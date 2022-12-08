namespace FitnessCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BALANCES")]
    public partial class Balance
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Column("OPERATION_TYPE")]
        public int OperationType { get; set; }

        public double Value { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }
    }
}
