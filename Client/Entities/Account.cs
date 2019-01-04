using System;
using System.ComponentModel.DataAnnotations;

namespace Client.Entities
{
    class Account
    {
        private string _id;
        private string _email;
        private string _username;
        private string _password;
        private string _salt;
        private DateTime _createdAt;
        private DateTime _updatedAt;
        private DateTime? _deletedAt;
        private AccountStatus _status;
        private GeneralInformation _generalInformation;

        public string id { get => _id; set => _id = value; }
        public string email { get => _email; set => _email = value; }
        public string username { get => _username; set => _username = value; }
        public string password { get => _password; set => _password = value; }
        public string salt { get => _salt; set => _salt = value; }
        public DateTime createdAt { get => _createdAt; set => _createdAt = value; }
        public DateTime updatedAt { get => _updatedAt; set => _updatedAt = value; }
        public DateTime? deletedAt { get => _deletedAt; set => _deletedAt = value; }
        public AccountStatus status { get => _status; set => _status = value; }
    }

    public enum AccountStatus
    {
        Active = 1,
        Deactive = 0
    }
}


