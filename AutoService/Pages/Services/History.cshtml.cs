using AutoService.Data;
using AutoService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Pages.Services
{
    public class HistoryModel : PageModel
    {
        private ApplicationDbContext _context;

        public HistoryModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public List<ServiceHeader> ServiceHeaders { get; set; }
        public string UserId { get; set; }
        public void OnGet(int carId)
        {
            ServiceHeaders = _context.ServiceHeaders.Include(s => s.Car).Include(c => c.Car.User).Where(c => c.CarId == carId).ToList();
            UserId = _context.Cars.Where(u => u.Id == carId).FirstOrDefault().UserId;
        }
    }
}
