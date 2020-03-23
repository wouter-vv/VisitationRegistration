using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using VisitorRegistration.ViewModels;

namespace VisitorRegistration
{
    [Authorize]
    public class GraphModel : PageModel
    {

        private readonly VisitorRegistration.Data.AppDbContext _context;


        [BindProperty]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SelectedDate { get; set; }

        public int[] TimeStayed { get; set; }
        public const int MaxTime = 4;
        public IEnumerable<int> TimeStayedEnumerable { get; private set; }

        public GraphModel(VisitorRegistration.Data.AppDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            if (TempData.Count != 0)
            {
                var selectedDateTempData = TempData["SelectedDateChart"];
                if (selectedDateTempData != null)
                {
                    SelectedDate = DateTime.Parse(selectedDateTempData.ToString());
                } 


            }
            else
            {
                SelectedDate = DateTime.Now.Date;
            }
            await GetVisits();
        }
        public async Task OnPostAsync()
        {

            TempData["SelectedDateChart"] = SelectedDate;
            await GetVisits();
        }

        private async Task GetVisits()
        {
            TimeStayed = new int[MaxTime];

            var visitations = _context.Visitations;
            for (int i = 0; i < MaxTime; i++)
            {
                TimeStayed[i] = visitations.Where(x => x.CheckOutDateTime.Hour - x.CheckInDateTime.Hour > MaxTime - i - 1 && x.CheckOutDateTime.Hour - x.CheckInDateTime.Hour <= MaxTime - i && x.CheckInDateTime.Date == SelectedDate.Date).Count();
            }
            TimeStayedEnumerable = TimeStayed;

        }

    }
    public static class JavaScriptConvert
    {
        public static IHtmlString SerializeObject(object value)
        {
            using (var stringWriter = new StringWriter())
            using (var jsonWriter = new JsonTextWriter(stringWriter))
            {
                var serializer = new JsonSerializer
                {
                    // Let's use camelCasing as is common practice in JavaScript
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                // We don't want quotes around object names
                jsonWriter.QuoteName = false;
                serializer.Serialize(jsonWriter, value);

                return new HtmlString(stringWriter.ToString());
            }
        }
    }
}
