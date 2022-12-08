namespace FitnessCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CUSTOMER_SUBSCRIPTIONS")]
    public partial class CustomerSubscriptions
    {
        public int Id { get; set; }

        [Column("CREATION_DATE")]
        public DateTime CreationDate { get; set; }

        [Column("EXPIRATION_DATE")]
        public DateTime ExpirationDate { get; set; }

        public int Type { get; set; }

        [Column("CUSTOMER_ID")]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
