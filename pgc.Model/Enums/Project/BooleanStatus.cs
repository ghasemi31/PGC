using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت")]
    public enum BooleanStatus
    {
        [PersianTitle("باشد")]
        True = 1,
        [PersianTitle("نباشد")]
        False = 2
    }
}
