using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AutoService.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(200)]
        [Display(Name = "نام مشتری")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [MaxLength(200)]
        [Display(Name = "کد پستی")]
        public string PostalCode { get; set; }
        [Display(Name = " ایمیل")]
        public override string Email
        {
            get { return base.Email; }
            set { base.Email = value; }
        }

        [Display(Name = "تلفن")]
        public override string PhoneNumber
        {
            get { return base.PhoneNumber; }
            set { base.PhoneNumber = value; }
        }
        public virtual List<Car> Cars { get; set; }

    }
}
