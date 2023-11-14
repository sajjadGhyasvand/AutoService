using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AutoService.Models
{
    public class ServiceType
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }

    }
}
