using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoService.Data;
using AutoService.Models;
using System.Security.Claims;

namespace AutoService.Pages.Cars
{
    public class CreateModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;

        public CreateModel(AutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Car Car { get; set; } = default!;
        public IActionResult OnGet(string userId = null)
        {
            Car = new Car();
            if (userId == null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                userId = claim.Value;
            }
            Car.UserId = userId;
            return Page();
        }


        [BindProperty]
        public IFormFile imgInp { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if ( _context.Cars == null || Car == null)
            {
                return Page();
            }
            if (imgInp != null)
            {
                Car.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imgInp.FileName);
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CarImages", Car.ImageName);
                using (var fileStream = new FileStream(savePath,FileMode.Create))
                {
                    imgInp.CopyTo(fileStream);
                }
            }
            _context.Cars.Add(Car);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
