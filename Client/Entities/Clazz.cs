using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Entities
{
    class Clazz
    {
        private string _id;
        private DateTime _startDate;
        private ClazzSession _session;
        private ClazzStatus _status;
        private string _currentSubjectId;

        public string id { get => _id; set => _id = value; }
        public DateTime startDate { get => _startDate; set => _startDate = value; }
        public ClazzSession session { get => _session; set => _session = value; }
        public ClazzStatus status { get => _status; set => _status = value; }
        public string currentSubjectId { get => _currentSubjectId; set => _currentSubjectId = value; }
    }
    public enum ClazzSession
    {
        Morning = 0,
        Afternoon = 1,
        Evening = 2
    }
    public enum ClazzStatus
    {
        Active = 1,
        Deactive = 0
    }
}
