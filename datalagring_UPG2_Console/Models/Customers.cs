using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace datalagring_UPG2_Console.Models
{
    public partial class Customers
    {
        public Customers()
        {
            SupportCases = new HashSet<SupportCases>();
        }

        [Key]
        [Column("SSNumber")]
        public int Ssnumber { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [InverseProperty("CustomerNumberNavigation")]
        public virtual ICollection<SupportCases> SupportCases { get; set; }
    }
}
