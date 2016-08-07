using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("بازشدن صفحه جدید")]
    public enum HrefTarget
    {
        [PersianTitle("صفحه جدید")]
        _blank= 1,
        [PersianTitle("صفحه والد")]
        _parent= 2,
        [PersianTitle("خود صفحه")]
        _self= 3,
        [PersianTitle("جستجو")]
        _search= 4,
       [PersianTitle("اول")]
        _top= 5
    }
}
