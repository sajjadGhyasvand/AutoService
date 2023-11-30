using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutoService.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "نام ماشین")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        public string Name { get; set; }
        [Display(Name = "مدل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        public string Model { get; set; }
        [Display(Name = "رنگ")]
        [MaxLength(50)]
        public string Color { get; set; }
        [Display(Name = "سال")]
        [MaxLength(10)]
        public string Year { get; set; }
        [Display(Name = "تصویر")]
        [MaxLength(50)]
        public string ImageName { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
