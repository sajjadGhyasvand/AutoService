using AutoService.Data;
using AutoService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public List<ApplicationUser> UserList { get; set; }
        public async Task<IActionResult> OnGet()
        {
            UserList = await _context.ApplicationUsers.ToListAsync();
            return Page(); 
        }
    }
}
