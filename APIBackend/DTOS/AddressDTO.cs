using System.ComponentModel.DataAnnotations;

namespace APIBackend.DTOS
{
    public class AddressDTO
    {
        [Required]
        public string FristName { get; set; }
        [Required]
        public string LastName { get; set; }
         [Required]
        public string Street { get; set; }
         [Required]
        public string City { get; set; }
         [Required]
        public string State { get; set; }
         [Required]
        public string ZipCode { get; set; }
    }
}