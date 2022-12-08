namespace FitnessCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PRODUCTS")]
    public partial class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        public int Amount { get; set; }

        [Required]
        [StringLength(128)]
        public string Barcode { get; set; }

        public double Price { get; set; }

        public int Category { get; set; }

        [Column("LAST_DELIVERY_DATE")]
        public DateTime LastDeliveryDate { get; set; }

        [Column("NEXT_DELIVERY_DATE")]
        public DateTime NextDeliveryDate { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
