using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VisitorRegistration.Data;
using VisitorRegistration.Data.Entities;

namespace VisitorRegistration
{
    [Authorize]
    public class OverviewModel : PageModel
    {
        private readonly VisitorRegistration.Data.AppDbContext _context;

        public User LoggedInUser { get; set; }

        public IEnumerable<Visitation> visitationQuery { get; set; }

        [BindProperty]
        public IList<Visitation> Visitations { get; set; }

        [BindProperty]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SelectedDate { get; set; }

        [BindProperty]
        public int? ShowDatePicker { get; set; }

        public SelectList DateVisitorsSelectList { get; set; }

        public OverviewModel(VisitorRegistration.Data.AppDbContext context)
        {
            _context = context;
        }

        public Visitation Visitation { get; set; }

        public async Task OnGetAsync()
        {
            if (TempData.Count != 0)
            {
                var showDatePickerTempData = TempData["ShowDatePicker"];
                if (ShowDatePicker != null)
                {
                    ShowDatePicker = Int32.Parse(showDatePickerTempData.ToString());

                    var selectedDateTempData = TempData["SelectedDate"];
                    if (selectedDateTempData != null)
                    {
                        SelectedDate = DateTime.Parse(selectedDateTempData.ToString());
                    }
                }

            }
            

            await GetVisitors();
            
        }
        public async Task OnPostAsync()
        {

            TempData["SelectedDate"] = SelectedDate;
            TempData["ShowDatePicker"] = ShowDatePicker;

            await GetVisitors();
        }

        private async Task GetVisitors()
        {

            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                string[] temp = { "Huidige bezoekers", "Kies een datum" };
                var test = temp.Select((r, index) => new SelectListItem { Text = index.ToString(), Value = r });

                //var test = new SelectList(temp, "Id", "Value");
                if (ShowDatePicker != null)
                {
                    DateVisitorsSelectList = new SelectList(test, "Text", "Value", ShowDatePicker);
                } else
                {
                    DateVisitorsSelectList = new SelectList(test, "Text", "Value");
                }
                if (ShowDatePicker == 1)
                {
                    visitationQuery = _context.Visitations
                    .Include(v => v.Person)
                    .Include(v => v.VisitType);
                    Visitations = visitationQuery.Where(x => x.CheckOutDateTime.Date == SelectedDate).ToList();


                }
                else
                {
                    SelectedDate = DateTime.Now;
                    visitationQuery = _context.Visitations
                    .Include(v => v.Person)
                    .Include(v => v.VisitType);
                    Visitations = visitationQuery.Where(x => x.CheckOutDateTime > DateTime.Now).ToList();

                }

            }
            else
            {
                RedirectToPage("/Account/Login");
            }
        }
    }
}
