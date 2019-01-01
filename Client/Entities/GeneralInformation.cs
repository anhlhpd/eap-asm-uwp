using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Entities
{
    class GeneralInformation
    {
        public GeneralInformation()
        {
            this.Gender = Gender.Other;
        }
        [Key]
        public string AccountId { get; set; }
        [Required(ErrorMessage = "Please enter first name"),
            MaxLength(10, ErrorMessage = "Please enter first name under 11 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter last name"),
            MaxLength(10, ErrorMessage = "Please enter last name under 11 characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please input birthday")]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Please input gender")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Please enter phone"),
            MinLength(10, ErrorMessage = "Please enter phone above 9 characters"),
            MaxLength(12, ErrorMessage = "Please enter phone under 13 characters")]
        public string Phone { get; set; }
        public Account Account { get; set; }
    }

    public enum Gender
    {
        Male = 0,
        Female = 1,
        Other = 2
    }
}
}
