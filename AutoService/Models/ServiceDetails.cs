using System.ComponentModel.DataAnnotations;

namespace AutoService.Models
{
    public class ServiceDetails
    {
        public int Id { get; set; }
        public int ServiceHeaderId { get; set; }
        public virtual  ServiceHeader ServiceHeader { get; set; }
        [Display(Name = "سرویس")]
        public int ServiceTypeId { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public double ServicePrice { get; set; }
        public string ServiceName { get; set; }

    }
}
