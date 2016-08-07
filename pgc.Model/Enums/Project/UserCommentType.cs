using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("نوع نظر")]
    public enum UserCommentType
    {
       [PersianTitle("انتقاد")]
        Censure=1,
       [PersianTitle("پیشنهاد")]
        Offer = 2,
       [PersianTitle("پرسش")]
        Question = 3,
       [PersianTitle("درخواست")]
        Request = 4
    }
}
