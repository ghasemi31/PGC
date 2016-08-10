using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("انواع درگاه")]
    public enum OnlineGetway
    {

       [PersianTitle("درگاه بانک ملت")]
       MellatBankGateWay = 1,
        [PersianTitle("درگاه آسان پرداخت")]
        UpGateWay = 2
    }
}
