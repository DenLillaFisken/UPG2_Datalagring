using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace datalagring_UPG2_Console.Models
{
    public partial class SupportCases
    {
        [Key]
        public int CaseNumber { get; set; }
        public int CustomerNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [ForeignKey(nameof(CustomerNumber))]
        [InverseProperty(nameof(Customers.SupportCases))]
        public virtual Customers CustomerNumberNavigation { get; set; }
    }
}
