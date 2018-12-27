using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    class PersonalInformation
    {
        public PersonalInformation()
        {
            this.Gender = Gender.Other;
        }
        [Key]
        [Required]
        public string AccountId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
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

