using AutoService.Data;
using AutoService.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AutoService.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public CarAndCustomerViewModel CarAndCustomerViewModel { get; set; }
        public async Task<IActionResult> OnGet(string userId = null)
        {
            if (userId == null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                userId = claim.Value;
            }
            CarAndCustomerViewModel = new CarAndCustomerViewModel()
            {
                Cars = await _context.Cars.Where(c=>c.UserId == userId).ToListAsync(),
                User = await _context.ApplicationUsers.FirstOrDefaultAsync(u=>u.Id == userId ),
            };
            return Page();
        }
    }
}
