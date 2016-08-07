using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت تایید")]
    public enum ConfirmStatus
    {
       [PersianTitle("تایید نشده")]
        Rejected = 1,
       [PersianTitle("تایید شده")]
        Accepted = 2,
       [PersianTitle("در انتظار تایید")]
        Waiting = 3
    }
}
