using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistration.Data.Entities
{
    public class VisitType
    {
        public int Id { get; set; }

        [Display(Name = "Bezoekstype")]
        public string Type { get; set; }
    }
}
