using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistration.Data;
using VisitorRegistration.ViewModels;

namespace VisitorRegistration.Pages.Shared.Components.RegistrationForm
{
    public class RegistrationForm : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        [BindProperty]
        public int FirstName { get; set; }
        public RegistrationForm(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(RegistrationFormViewModel registrationFormViewModel)
        {

            return View(registrationFormViewModel);
        }
    }
}
