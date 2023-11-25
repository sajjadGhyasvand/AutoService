using AutoService.Data;
using AutoService.Models;
using AutoService.Models.ViewModel;
using AutoService.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;

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
        public UsersListViewModel UserListViewModel { get; set; }
        public async Task<IActionResult> OnGet( int pageId = 1)
        {
            UserListViewModel = new()
            {
                ApplicationUsers = await _context.ApplicationUsers.ToListAsync(),
            };
            StringBuilder param = new();
            param.Append("/Users?productPage=:");
            var count = UserListViewModel.ApplicationUsers.Count;
            UserListViewModel.PaginingInfo = new()
            {
                CurrentPage = pageId,
                ItemPerPage = 2,
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
