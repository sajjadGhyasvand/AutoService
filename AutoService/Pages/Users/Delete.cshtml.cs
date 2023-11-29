using AutoService.Data;
using AutoService.Models;
using AutoService.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace AutoService.Pages.Users
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class DeleteModel : PageModel
    {
        private ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        public DeleteModel(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> rolemanager)
        {
            _context = context;
            _userManager = userManager;
            _rolemanager = rolemanager;
        }
        [BindProperty]
        public ApplicationUser applicationUser { get; set; }
        public async Task<IActionResult> OnGet(string id)
        {
            if (id.Trim().Length == 0)
                return NotFound();

            applicationUser = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (applicationUser == null)
                return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.ApplicationUsers == null)
            {
                return NotFound();
            }
            var applicationUser = await _context.ServiceTypes.FindAsync(id);

            if (applicationUser != null)
            {
                _context.ServiceTypes.Remove(applicationUser);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
