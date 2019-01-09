using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Entities
{
    class Subject
    {
        private string _id;
        private string _name;
        private string _description;
        public string id { get => _id; set => _id = value; }
        public string name { get => _name; set => _name = value; }
        public string description { get => _description; set => _description = value; }
    }
}
