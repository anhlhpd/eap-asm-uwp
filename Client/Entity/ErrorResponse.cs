using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Entity
{
    class ErrorResponse
    {
        private int _status;
        private string _message;
        private Dictionary<string, string> _error;

        public int status { get => _status; set => _status = value; }
        public string message { get => _message; set => _message = value; }
        public Dictionary<string, string> error { get => _error; set => _error = value; }
    }
}
