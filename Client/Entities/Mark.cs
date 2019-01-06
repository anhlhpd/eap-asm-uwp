using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Entities
{
    public class Mark
    {
        private static readonly int MaxTheory = 10; // 100%.
        private static readonly int MaxPratice = 15;
        private static readonly int MaxAssignment = 10;
        private static readonly int PercentToPass = 40;

        public Mark() { }

        public Mark(MarkType type, int value)
        {
            this.MarkType = type;
            this.Value = value;
            this.GenerateStatus();
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }

        public Mark(MarkType type, float value, string subjectId, string accountId)
        {

            this.SubjectId = subjectId;
            this.AccountId = accountId;
            this.MarkType = type;
            this.Value = value;
            this.GenerateStatus();
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }

        private void GenerateStatus()
        {
            int max = 0;
            switch (this.MarkType)
            {
                case MarkType.Theory:
                    max = MaxTheory;
                    break;
                case MarkType.Practice:
                    max = MaxPratice;
                    break;
                case MarkType.Assignment:
                    max = MaxAssignment;
                    break;
            }
            double percent = (this.Value / max) * 100;
            this.Status = percent >= PercentToPass ? MarkStatus.Pass : MarkStatus.Fail;
        }


        public long Id { get; set; }

        public string AccountId { get; set; }

        public string SubjectId { get; set; }

        public float Value { get; set; }

        public MarkType MarkType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public MarkStatus Status { get; set; }
    }

    public enum MarkStatus
    {
        Fail = 1,
        Pass = 0,
        Chosen = 2
    }

    public enum MarkType
    {
        Theory = 0,
        Practice = 1,
        Assignment = 2
    }
    //class Mark
    //{
    //    private string _id;
    //    private string _accoountId;
    //    private string _subjectId;
    //    private float _value;
    //    private MarkType _markType;
    //    private DateTime _createdAt;
    //    private DateTime _updatedAt;
    //    private DateTime? _deletedAt;
    //    private MarkStatus _status;
    //    private Account _account;
    //    private Subject _subject;

    //    public string id { get => _id; set => _id = value; }
    //    public string accoountId { get => _accoountId; set => _accoountId = value; }
    //    public string subjectId { get => _subjectId; set => _subjectId = value; }
    //    public float value { get => _value; set => _value = value; }
    //    public MarkType markType { get => _markType; set => _markType = value; }
    //    public DateTime createdAt { get => _createdAt; set => _createdAt = value; }
    //    public DateTime updatedAt { get => _updatedAt; set => _updatedAt = value; }
    //    public MarkStatus status { get => _status; set => _status = value; }
    //    public Account account { get => _account; set => _account = value; }
    //    public Subject subject { get => _subject; set => _subject = value; }
    //}
    //public enum MarkStatus
    //{
    //    Fail = 1,
    //    Pass = 0,
    //    Chosen = 2
    //}

    //public enum MarkType
    //{
    //    Theory = 0,
    //    Practice = 1,
    //    Assignment = 2
    //}
}
