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
        private string _accountId;
        private string _firstName;
        private string _lastName;
        private DateTime _birthday;
        private Gender _gender;
        private string _phone;

        public string accountId { get => _accountId; set => _accountId = value; }
        public string firstName { get => _firstName; set => _firstName = value; }
        public string lastName { get => _lastName; set => _lastName = value; }
        public string phone { get => _phone; set => _phone = value; }
        public Gender gender { get => _gender; set => _gender = value; }
        public DateTime birthday { get => _birthday; set => _birthday = value; }
    }

    public enum Gender
    {
        Male = 0,
        Female = 1,
        Other = 2
    }
}
