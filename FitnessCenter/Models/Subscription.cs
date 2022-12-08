namespace FitnessCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SUBSCRIPTIONS")]
    public partial class Subscription
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        public double Price { get; set; }

        [Column("DURATION_IN_MONTHS")]
        public int DurationInMonths { get; set; }
    }
}
