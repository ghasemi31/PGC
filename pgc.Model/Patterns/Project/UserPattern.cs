using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class UserPattern:BasePattern
    {
        public UserPattern()
        {
            Username = "";
            NationalCode = "";
            Email = "";
            Address = "";
            PostalCode = "";
            Tel = "";
            Mobile = "";
            Fax = "";
            Name = "";
            BranchTitle = "";
            SignUpPersianDate = new DateRangePattern();
        }


        public long Province_ID { get; set; }
        public long Branch_ID { get; set; }
        public long City_ID { get; set; }
        public long AccessLevel_ID { get; set; }

        public Role Role { get; set; }
        public UserActivityStatus ActivityStatus { get; set; }
        public DateRangePattern SignUpPersianDate { get; set; }
        public Gender Gender { get; set; }

        public string Username { get; set; }
        public string NationalCode { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Name { get; set; }

        public string BranchTitle { get; set; }
    }
}