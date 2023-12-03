using AutoService.Data;
using AutoService.Models;
using AutoService.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Pages.Services
{
    public class CreateModel : PageModel
    {
        private ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public CarServiceViewModel carServiceVM { get;set; }
        public async Task<IActionResult> OnGet(int carId)
        {
            carServiceVM = new CarServiceViewModel()
            {
                Car = await _context.Cars.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == carId),
                ServiceHeader = new(),
            };
            List<string> lstServiceTypesInShoppingCart = _context.ServicesShopingCarts.Include(c=>c.ServiceType).Where(c=>c.CarId == carId).Select(c=>c.ServiceType.Name).ToList();
            IQueryable<ServiceType> lstServices = from s in _context.ServiceTypes
                                            where !(lstServiceTypesInShoppingCart.Contains(s.Name))
                                            select s;
            carServiceVM.ServiceTypes = lstServices.ToList();
            carServiceVM.ServiceShopping = _context.ServicesShopingCarts.Include(c=>c.ServiceType).Where(c=>c.CarId == carId).ToList();
            foreach (var item  in carServiceVM.ServiceShopping)
            {
                carServiceVM.ServiceHeader.TotalPrice += item.ServiceType.Price;
            }
            return Page();
        }
    }
}
