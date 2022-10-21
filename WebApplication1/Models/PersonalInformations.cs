using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class PersonalInformations
    {
        public Guid Id { get; set; } //Id первый первичный ключ 


        [Required(ErrorMessage = "Fill login")]
        [Display(Name = "User login")]
        public string Login { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$")]
        [Required]
        [StringLength(30)]
        public string Password { get; set;}

        [StringLength(60)]
        [Required]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Fill the form")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Fill Gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Year Of Birth")]
        [DataType(DataType.Date)]
        public DateTime YearOfBirth { get; set; } 

    }
}
