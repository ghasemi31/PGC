using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Other.Project
{
    [Serializable]

    public class UserObject
    {
        public long ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        
    }
}
