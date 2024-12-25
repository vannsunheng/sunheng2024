
using System.ComponentModel.DataAnnotations;


namespace APIBackend.DTOS
{
    public class BasketItemDTO
    {
        [Required]
         public int Id { get; set; } 
         [Required]
        public string ProductName{set;get;}
        [Required]
        [Range(0.10,double.MaxValue,ErrorMessage = "Price must be grater then 0")]
        public decimal Price{set;get;}  
        [Required]
        [Range(1,double.MaxValue,ErrorMessage = "Quantity must be at least 1")]
        public int Quantity{set;get;}
        public string ProductUrl{set;get;}
        public string Brand{set;get;}
        public string Type{set;get;}
    }
}