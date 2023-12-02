using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace AutoService.Models
{
    public class ServiceHeader
    {
        public int Id { get; set; }
        public double KiloMetter { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        public string Description  { get; set; }
        [Required]
        [DisplayFormat(DataFormatString =  "yyyy/MM/dd")]
        public DateTime DateAdded { get; set; }
        public int CarId { get; set; }
        public virtual Car Car { get; set; }
    }
}
