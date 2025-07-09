namespace Clinic.Core.Entities
{

    public class Person
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateOnly BDate { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }


    }
}
