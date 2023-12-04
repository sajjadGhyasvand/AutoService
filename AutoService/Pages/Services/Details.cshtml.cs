using AutoService.Data;
using AutoService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Pages.Services
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public ServiceHeader ServiceHeader { get; set; }
        public List<ServiceDetails> ServiceDetailses { get; set; }
        public IActionResult OnGet(int serviceId)
        {
            ServiceHeader = _context.ServiceHeaders
                .Include(s => s.Car)
                .Include(s => s.Car.User)
                .FirstOrDefault(s => s.Id == serviceId);
            if (ServiceHeader == null)
                return NotFound();
            ServiceDetailses = _context.ServiceDetails.Where(d => d.ServiceHeaderId == serviceId).ToList();
            return Page();
        }
    }
}
