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

        public async Task<IActionResult> OnPost()
        {
                carServiceVM.ServiceHeader.DateAdded = DateTime.Now;
                carServiceVM.ServiceShopping = _context.ServicesShopingCarts.Include(c => c.ServiceType)
                    .Where(c => c.CarId == carServiceVM.Car.Id).ToList();
                foreach (var shop in carServiceVM.ServiceShopping)
                {
                    carServiceVM.ServiceHeader.TotalPrice += shop.ServiceType.Price;
                }

                carServiceVM.ServiceHeader.CarId = carServiceVM.Car.Id;
                _context.ServiceHeaders.Add(carServiceVM.ServiceHeader);
                await _context.SaveChangesAsync();

                foreach (var shop in carServiceVM.ServiceShopping)
                {
                    ServiceDetails details = new ServiceDetails()
                    {
                        ServiceHeaderId = carServiceVM.ServiceHeader.Id,
                        ServiceName = shop.ServiceType.Name,
                        ServicePrice = shop.ServiceType.Price,
                        ServiceTypeId = shop.ServiceTypeId
                    };
                    _context.ServiceDetails.Add(details);
                }
                _context.ServicesShopingCarts.RemoveRange(carServiceVM.ServiceShopping);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Cars/Index", new { userId = carServiceVM.Car.UserId });

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCart()
        {
            ServicesShopingCart shopping = new ServicesShopingCart()
            {
                CarId = carServiceVM.Car.Id,
                ServiceTypeId = carServiceVM.ServiceDetails.ServiceTypeId
            };
            _context.ServicesShopingCarts.Add(shopping);
            await _context.SaveChangesAsync();
            return RedirectToPage("Create", new { carId = carServiceVM.Car.Id });
        }

        public async Task<IActionResult> OnPostRemoveFromCart(int serviceTypeId)
        {
            ServicesShopingCart shopping = _context.ServicesShopingCarts
                .First(u => u.CarId == carServiceVM.Car.Id && u.ServiceTypeId == serviceTypeId);
            _context.Remove(shopping);
            await _context.SaveChangesAsync();
            return RedirectToPage("Create", new { carId = carServiceVM.Car.Id });
        }
    }
}
