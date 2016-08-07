using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت")]
    public enum UserCommentStatus
    {
       [PersianTitle("خوانده شده")]
        Read = 1,
       [PersianTitle("خوانده نشده")]
        UnRead = 2
    }
}
