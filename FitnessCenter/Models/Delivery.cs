namespace FitnessCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DELIVERIES")]
    public partial class Delivery
    {
        public int Id { get; set; }

        [Column("PROVIDER_ID")]
        public int ProviderId { get; set; }

        public DateTime Date { get; set; }

        public double Price { get; set; }

        public virtual Provider Provider { get; set; }
    }
}
