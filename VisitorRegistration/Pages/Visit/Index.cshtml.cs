using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VisitorRegistration.Data;
using VisitorRegistration.Data.Entities;

namespace VisitorRegistration
{
    public class IndexModel : PageModel
    {
        private readonly VisitorRegistration.Data.AppDbContext _context;

        public IndexModel(VisitorRegistration.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {

            ViewData["VisitTypes"] = new SelectList(_context.VisitTypes, "Id", "Type");
            return Page();
        }

        [BindProperty]
        public Visitation Visitation { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            ViewData["VisitTypes"] = new SelectList(_context.VisitTypes, "Id", "Type");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Person person = _context.Persons.FirstOrDefault(x => x.FirstName == Visitation.Person.FirstName && x.LastName == Visitation.Person.LastName);
            if(person == null)
            {
                _context.Persons.Add(new Person { FirstName = Visitation.Person.FirstName, LastName = Visitation.Person.LastName, Company = Visitation.Person.Company, LicencePlate = Visitation.Person.LicencePlate });
                await _context.SaveChangesAsync();

                person = _context.Persons.FirstOrDefault(x => x.FirstName == Visitation.Person.FirstName && x.LastName == Visitation.Person.LastName);
            }


            var newVisitation = new Visitation
            {
                CheckInDateTime = DateTime.Now,
                CheckOutDateTime = DateTime.Now.AddHours(2),
                Person = person,
                PersonId = person.Id,
                VisitTypeId = Visitation.VisitType.Id

            };

            

            // persist to get newOrder.Id

            _context.Visitations.Add(newVisitation);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Confirmation");
        }
    }
}
