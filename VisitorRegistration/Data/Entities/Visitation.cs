using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistration.Data.Entities
{
    public class Visitation
    {
        public int Id { get; set; }


        [Display(Name = "Check in moment")]
        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime CheckInDateTime { get; set; }

        [Display(Name = "Check out moment")]
        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime CheckOutDateTime { get; set; }

        public int? PersonId { get; set; }
        public Person Person { get; set; }
        public int? VisitTypeId { get; set; }
        public VisitType VisitType { get; set; }

    }
}
