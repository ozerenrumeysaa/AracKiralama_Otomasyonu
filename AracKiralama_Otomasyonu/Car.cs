using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AracKiralama_Otomasyonu
{
    [Table("Table_Car")]
    public class Car
    {
        [Key]
        public int Car_Id { get; set; }

        [Required, MaxLength(50)]
        public string Car_BrandModel { get; set; }

        [Required, MaxLength(20)]
        public string Car_Plate { get; set; }

        [Required]
        public decimal Car_DailyPrice { get; set; }

       
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}