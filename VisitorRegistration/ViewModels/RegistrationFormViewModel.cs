using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitorRegistration.ViewModels
{
    public class RegistrationFormViewModel
    {
        public RegistrationFormViewModel ()
        {
            Registrations = new List<RegistrationFormViewModel>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? VisitTypeId { get; set; }

        [NotMapped]
        public SelectList VisitTypes { get; set; }

        public List<RegistrationFormViewModel> Registrations { get; set; }
    }
}
