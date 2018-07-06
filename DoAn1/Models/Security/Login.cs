using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoAn1.Models.Security
{
    public class Login
    {
        [Required]
        [DisplayName ("User name")]
        public string Username1 { get; set; }

         [Required]
           [DataType(DataType.Password)]
         [DisplayName("Password")]
        public string Password_ { get; set; }

         [Required]
         [DisplayName("Remember me?")]
        public bool RememberMe { get; set; }
    }
}