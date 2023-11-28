using AutoService.Data;
using AutoService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace AutoService.Pages.Users
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        public EditModel(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> rolemanager)
        {
            _context = context;
            _userManager = userManager;
            _rolemanager = rolemanager;
        }
        [BindProperty]
        public ApplicationUser applicationUser { get; set; }
        [BindProperty]
        public string SelecetedRoles { get; set; }
        public SelectList  Roles { get; set; }
        public async Task<IActionResult> OnGet(string id)
        {
            if (id.Trim().Length == 0)
                return NotFound();

            applicationUser = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (applicationUser == null) 
                return NotFound();
            var userRoles = _userManager.GetRolesAsync(new IdentityUser() { Id=applicationUser.Id}).Result;  /*(ClaimIdentity) User.Identity)*/
            Roles = new SelectList(_rolemanager.Roles,"Name","Name", userRoles.First());
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
                return Page();
            var  UserInDb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == applicationUser.Id);
            if(UserInDb == null)
                return NotFound();
            UserInDb.Name = applicationUser.Name;
            UserInDb.Address = applicationUser.Address;
            UserInDb.Email = applicationUser.Email;
            UserInDb.City   = applicationUser.City;
            var userRoles = _userManager.GetRolesAsync(new IdentityUser() { Id = applicationUser.Id }).Result;
            if (SelecetedRoles != userRoles.FirstOrDefault())
            {
                await _userManager.RemoveFromRoleAsync(new IdentityUser() { Id = applicationUser.Id }, userRoles.First());
                await _userManager.AddToRoleAsync(new IdentityUser() { Id = applicationUser.Id }, SelecetedRoles);
            }
            _context.Update(UserInDb);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
