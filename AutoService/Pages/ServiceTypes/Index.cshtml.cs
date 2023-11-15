using AutoService.Data;
using AutoService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Pages.ServiceTypes
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<ServiceType> ServiceTypes { get; set; }     
        public async Task<IActionResult> OnGet()
        {
            ServiceTypes = await _context.ServiceTypes.ToListAsync();
            return Page();
        }
    }
}
