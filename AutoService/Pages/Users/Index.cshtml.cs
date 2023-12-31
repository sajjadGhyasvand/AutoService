using AutoService.Data;
using AutoService.Models;
using AutoService.Models.ViewModel;
using AutoService.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;

namespace AutoService.Pages.Users
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public UsersListViewModel UserListViewModel { get; set; }
        public async Task<IActionResult> OnGet( int pageId = 1,string searchName=null,string searchEmail = null)
        {
            UserListViewModel = new()
            {
                ApplicationUsers = await _context.ApplicationUsers.ToListAsync(),
            };
            StringBuilder param = new();
            param.Append("/Users?productPage=:");
            param.Append("$searchName=");
            if (searchName != null)
                param.Append(searchName);
            param.Append("$searchEmail=");
            if (searchName != null)
                param.Append(searchEmail);
            if (searchEmail!=null || searchEmail!=null)
                UserListViewModel.ApplicationUsers = _context.ApplicationUsers.Where(u=>u.Name.Contains(searchName) || u.Email.Contains(searchEmail)).ToList();
            var count = UserListViewModel.ApplicationUsers.Count;
            UserListViewModel.PaginingInfo = new()
            {
                CurrentPage = pageId,
                ItemPerPage = SD.PagingUserCount,
                TotalItems = count,
                UrlParam = param.ToString(),

            };
            UserListViewModel.ApplicationUsers = UserListViewModel.ApplicationUsers.OrderBy(x => x.Name)
                .Skip((pageId - 1) * SD.PagingUserCount)
                .Take(SD.PagingUserCount).ToList();
            return Page();
        }
    }
}
