using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت بازیها")]
    public enum GameType
    {
       [PersianTitle("بازیهای ایرانی")]
        Iranian = 1,
       [PersianTitle("بازیهای خارجی")]
        Foreign = 2
    }
}
