using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pgc.Model;
using pgc.Model.Enums;

namespace pgc.Business
{
    [Serializable]
    public class SystemEventArgs : EventArgs
    {

        public Device Device_Type { get; set; }

        public User Related_User { get; set; }
       
        public User Related_Doer { get; set; }
        
        public string Related_Guest_Phone { get; set; }
        public string Related_Guest_Email { get; set; }
        
        public Dictionary<string, string> EventVariables { get; set; }


        public SystemEventArgs()
        {
            Related_Guest_Email = "";
            Related_Guest_Phone = "";
            Device_Type = Device.WebApp;

            EventVariables = new Dictionary<string, string>();
        }
    }
}
