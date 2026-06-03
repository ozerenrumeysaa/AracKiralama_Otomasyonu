using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AracKiralama_Otomasyonu
{
    [Table("Table_Customer")]
    public class Customer
    {
        [Key]
        public int Customer_Id { get; set; }

        [Required, MaxLength(50)]
        public string Customer_Name { get; set; }

        [Required, MaxLength(50)]
        public string Customer_Surname { get; set; }

        [Required, MaxLength(50)]
        public string Customer_Telephone { get; set; }

        
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}