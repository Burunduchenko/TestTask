using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DAL.Entities
{
    public class Account
    {
        [Key]
        public string Name { get; set; }


        public virtual Incident Incident { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
