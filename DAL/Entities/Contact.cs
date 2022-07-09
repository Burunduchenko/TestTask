using System.ComponentModel.DataAnnotations;


namespace DAL.Entities
{
    public class Contact
    {
        [Key]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Account Account { get; set; }
    }
}
