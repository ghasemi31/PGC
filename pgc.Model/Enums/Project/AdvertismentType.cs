using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("نوع فایل تبلیغات")]
    public enum AdvertismentType
    {
        [PersianTitle("عکس")]
        img = 1,
        [PersianTitle("Flash")]
        flash = 2,
        [PersianTitle("Gif")]
        gif = 3,
    }
}
