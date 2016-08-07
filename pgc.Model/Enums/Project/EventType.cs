using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("نوع ارسال")]
    public enum EventType
    {
       [PersianTitle("زمانبندی شده")]
        Schedule = 1,
       [PersianTitle("سیستمی")]
       System = 2,
       [PersianTitle("توسط مدیر/ دستی")]
       Manual = 3
    }
}
