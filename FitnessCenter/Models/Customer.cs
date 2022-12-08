namespace FitnessCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CUSTOMERS")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            CUSTOMER_SUBSCRIPTIONS = new HashSet<CustomerSubscriptions>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        [Column("FIRST_NAME")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(64)]
        [Column("LAST_NAME")]
        public string LastName { get; set; }

        [Column("REGISTRATION_DATE")]
        public DateTime RegistrationDate { get; set; }

        [Required]
        [StringLength(64)]
        public string Email { get; set; }

        [Required]
        [StringLength(64)]
        [Column("PHONE_NUMBER")]
        public string PhoneNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerSubscriptions> CUSTOMER_SUBSCRIPTIONS { get; set; }
    }
}
