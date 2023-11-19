using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AutoService.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(200)]
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [MaxLength(200)]
        public string PostalCode { get; set; }
    }
}
