using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("نوع دستگاه")]
    public enum Device
    {
        [PersianTitle("وبسایت")]
        WebApp = 1,

        [PersianTitle("اندروید")]
        AndroidApp = 2,

        [PersianTitle("ios")]
        IOSApp = 3
    }
}
