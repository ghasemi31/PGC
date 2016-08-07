using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("")]
    public enum SendSMSStatus
    {
        [PersianTitle("وضعیت نامشخص")]
        Unknown = 0,
        [PersianTitle("رسیده به گوشی")]
        DeliveredToPhone = 1,
        [PersianTitle("نرسیده به گوشی")]
        NotDeliveredToPhone = 2,
        [PersianTitle("رسیده به مخابرات")]
        DeliveredToTelecommunication = 8,
        [PersianTitle("نرسیده به مخابرات")]
        NotDeliveredToTelecommunication = 16  
    }
}
