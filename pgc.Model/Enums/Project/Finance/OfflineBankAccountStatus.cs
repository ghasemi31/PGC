using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت حساب بانکی")]
    public enum OfflineBankAccountStatus
    {
        [PersianTitle("فعال")]
        Enabled = 1,
        [PersianTitle("غیر فعال")]
        Disabled = 2,
    }
}
