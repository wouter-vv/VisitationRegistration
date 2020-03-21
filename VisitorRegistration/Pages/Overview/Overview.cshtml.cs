using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VisitorRegistration.Data;
using VisitorRegistration.Data.Entities;

namespace VisitorRegistration
{
    public class OverviewModel : PageModel
    {
        private readonly VisitorRegistration.Data.AppDbContext _context;

        public User LoggedInUser { get; set; }

        public IEnumerable<Visitation> visitationQuery { get; set; }

        [BindProperty]
        public IList<Visitation> visitations { get; set; }

        public OverviewModel(VisitorRegistration.Data.AppDbContext context)
        {
            _context = context;
        }

        public Visitation Visitation { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            visitationQuery = _context.Visitations
                .Include(v => v.Person)
                .Include(v => v.VisitType);
            visitations = visitationQuery.Where(x => x.CheckOutDateTime > DateTime.Now).ToList();

            return Page();
        }
    }
}
