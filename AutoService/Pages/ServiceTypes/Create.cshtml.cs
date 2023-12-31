using AutoService.Data;
using AutoService.Models;
using AutoService.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoService.Pages.ServiceTypes
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class CreateModel : PageModel
    {
        private ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public ServiceType ServiceType { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPost(ServiceType serviceType)
        {
            if (!ModelState.IsValid)
                return Page();
            _context.ServiceTypes.Add(serviceType);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
