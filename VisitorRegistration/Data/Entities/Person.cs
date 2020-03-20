namespace VisitorRegistration.Data.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }
    }
}
