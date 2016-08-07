using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class SystemEventPattern : BasePattern
    {
        public SystemEventPattern()
        {
            Title = "";
        }
        public long ID { get; set; }
        public long AccessLevel_ID { get; set; }
        public long CustomUser_ID { get; set; }
        public long PrivateNo_ID { get; set; }

        public string Title { get; set; }
        public string Template_Admin_Email { get; set; }
        public string Template_Admin_SMS { get; set; }
        public string Template_User_Email { get; set; }
        public string Template_User_SMS { get; set; }

        public BooleanStatus Support_Manual_Email { get; set; }
        public BooleanStatus Support_Manual_SMS { get; set; }    
        public BooleanStatus Support_Related_Guest_Email { get; set; }
        public BooleanStatus Support_Related_User_Email { get; set; }
        public BooleanStatus Support_Related_Branch_Email { get; set; }
        public BooleanStatus Support_Related_Doer_Email { get; set; }
        public BooleanStatus Support_Related_Guest_SMS { get; set; }
        public BooleanStatus Support_Related_User_SMS { get; set; }
        public BooleanStatus Support_Related_Branch_SMS { get; set; }
        public BooleanStatus Support_Related_Doer_SMS { get; set; }
    }
}