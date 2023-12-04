using AutoService.Data;
using AutoService.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoService.ViewComponents
{
    public class LoggedInUserViewComponent : ViewComponent
    {
        private ApplicationDbContext _context;

        public LoggedInUserViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            LoggedInUserViewModel logged = new LoggedInUserViewModel()
            {
                Name = _context.ApplicationUsers.First(u => u.Email == User.Identity.Name).Name,
                ShoppingCart = _context.ServicesShopingCarts
                    .Include(c => c.Car).ThenInclude(c => c.User)
                    .Include(c => c.ServiceType)
                    .Where(u => u.Car.User.Email == User.Identity.Name)
                    .ToList()
            };
            return View("~/Pages/Shared/Components/LoggedInUser.cshtml", logged);
        }
    }
}
