using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت بازیها")]
    public enum GameHowType
    {
       [PersianTitle("آنلاین")]
        Online = 1,
       [PersianTitle("آفلاین")]
        Offline = 2
    }
}
