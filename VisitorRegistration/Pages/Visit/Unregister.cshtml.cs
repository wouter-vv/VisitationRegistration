using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VisitorRegistration.Data;
using VisitorRegistration.Data.Entities;

namespace VisitorRegistration
{
    public class UnregisterModel : PageModel
    {
        private readonly VisitorRegistration.Data.AppDbContext _context;

        public UnregisterModel(VisitorRegistration.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Visitation Visitation { get; set; }


        public IEnumerable<User> FollowUpUsers { get; set; }
        public SelectList FollowUpSelectList { get; set; }

        public IEnumerable<Visitation> PersonsPresent { get; set; }
        public SelectList PersonsPresentSelectList { get; set; }


        public IActionResult OnGet()
        {

            PersonsPresent = _context.Visitations
                .Include(v => v.Person)
                .Include(v => v.VisitType);

            var personsLoggedIn =
                 _context.Visitations
                    .Where(x => x.CheckOutDateTime > DateTime.Now)
                    .Select(s => new
                    {
                        Id = s.Id,
                        Fullname = s.Person.FirstName + " " + s.Person.LastName
                    })
                    .ToList();

            //ViewBag.StandID = new SelectList(stands, "StandID", "Description")

            //var personsLoggedIn = PersonsPresent.Where(x => x.CheckOutDateTime > DateTime.Now);
            ViewData["PersonsLoggedIn"] = new SelectList(personsLoggedIn, "Id", "Fullname");

            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Visitation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitationExists(Visitation.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool VisitationExists(int id)
        {
            return _context.Visitations.Any(e => e.Id == id);
        }
    }
}
