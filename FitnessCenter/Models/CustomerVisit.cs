using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessCenter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [Table("CUSTOMER_VISITS")]
    public partial class CustomerVisit
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Column("VISIT_TYPE")]
        public int VisitType { get; set; }

        [Required]
        [StringLength(64)]
        [Column("CUSTOMER_NAME")]
        public string CustomerName { get; set; }
    }
}
