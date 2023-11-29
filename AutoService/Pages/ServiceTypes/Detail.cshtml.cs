using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoService.Data;
using AutoService.Models;
using AutoService.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace AutoService.Pages.ServiceTypes
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class DetailModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;

        public DetailModel(AutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public ServiceType ServiceType { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ServiceTypes == null)
            {
                return NotFound();
            }

            var servicetype = await _context.ServiceTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (servicetype == null)
            {
                return NotFound();
            }
            else 
            {
                ServiceType = servicetype;
            }
            return Page();
        }
    }
}
