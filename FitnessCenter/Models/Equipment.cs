namespace FitnessCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPMENTS")]
    public partial class Equipment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        public int Count { get; set; }

        [Column("REPAIRS_COUNT")]
        public int RepairsCount { get; set; }

        [Column("WARRANTY_PERIOD")]
        public DateTime WarrantyPeriod { get; set; }

        [Column("REPLACE_DATE")]
        public DateTime ReplaceDate { get; set; }

        public double Price { get; set; }
    }
}
