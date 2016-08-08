using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت درخواست از شعبه")]
    public enum GameType
    {
       [PersianTitle("ایرانی")]
        Iranian = 1,
       [PersianTitle("خارجی")]
        Foreign = 2,
       [PersianTitle("موبایلی")]
        Mobile = 3
    }
}
