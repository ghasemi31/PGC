using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت نمایش")]
    public enum CommentStatus
    {
        [PersianTitle("قابل نمایش")]
        Visible = 1,
        [PersianTitle("غیر قابل نمایش")]
        UnVisible = 2,
    }
}
