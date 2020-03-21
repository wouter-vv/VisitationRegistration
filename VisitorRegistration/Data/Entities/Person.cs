using System.ComponentModel.DataAnnotations;

namespace VisitorRegistration.Data.Entities
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Achternaam")]
        public string LastName { get; set; }
        public string Company { get; set; }
        public string LicencePlate { get; set; }
    }
}
