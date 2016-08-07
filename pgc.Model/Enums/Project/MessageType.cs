using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("نوع پیامک")]
    public enum MessageType
    {
        [PersianTitle("لاتین")]
        Latin = 1,
        [PersianTitle("فارسی")]
        Persian = 2
    }
}
