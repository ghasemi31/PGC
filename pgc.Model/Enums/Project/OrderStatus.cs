using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت درخواست از شعبه")]
    public enum OrderStatus
    {
       [PersianTitle("جدید")]
        New = 1,
       [PersianTitle("لغو ")]
        Cancel = 2,
       [PersianTitle("تحویل داده شده")]
        Done = 3,
       [PersianTitle("در حال تهیه")]
        Prepare = 4,
       [PersianTitle("در حال ارسال")]
        Sending = 5
    }
}
