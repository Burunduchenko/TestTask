using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;


namespace BLL.AddModels
{
    public class IncedentAddModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string AccountName { get; set; }
        [Required]
        public string ContactFirstName { get; set; }
        [Required]
        public string ContactLastName { get; set; }
        [Email]
        [Required]
        public string ContactEmail { get; set; }
        [Required]
        public string IncedentDescription { get; set; }
    }
}
