using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AracKiralama_Otomasyonu
{
    [Table("Table_Rental")]
    public class Rental
    {
        [Key]
        public int Rental_Id { get; set; }

        [ForeignKey("Customer")]
        public int Customer_Id { get; set; }

        [ForeignKey("Car")]
        public int Car_Id { get; set; }

        [Required]
        public int RentDays { get; set; }

       
        public virtual Customer Customer { get; set; }
        public virtual Car Car { get; set; }
        public DateTime RentDate { get; set; }
    }
}