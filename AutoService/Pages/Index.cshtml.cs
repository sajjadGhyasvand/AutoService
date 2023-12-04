using AutoService.Data;
using AutoService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoService.Pages
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<ServiceType> ServiceTypes { get; set; }
        public List<Car> Cars { get; set; }
        public void OnGet()
        {
            ServiceTypes = _context.ServiceTypes.ToList();
            Cars = _context.Cars.ToList();
        }
    }
}