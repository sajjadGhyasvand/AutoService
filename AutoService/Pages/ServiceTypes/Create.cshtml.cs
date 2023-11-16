using AutoService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoService.Pages.ServiceTypes
{
    public class CreateModel : PageModel
    {
        public ServiceType ServiceTypes { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
