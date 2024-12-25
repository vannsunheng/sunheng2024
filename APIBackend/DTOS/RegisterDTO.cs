using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIBackend.DTOS
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [RegularExpression("(?=^.{8,30}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;&quot;:;'?/&gt;.&lt;,]).*$",ErrorMessage ="Your password is too weak. Please use a combination of uppercase and lowercase letters, numbers, and special characters, with a minimum length of 8 characters")]
        public string Password { get; set; }
    }
}