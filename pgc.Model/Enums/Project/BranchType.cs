using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت شعبه")]
    public enum BranchType
    {
        [PersianTitle("شعب تهران")]
        TehranBranch=1,
        [PersianTitle("شعب شهرستان")]
        IranBranch=2,
        [PersianTitle("شعب خارج از کشور")]
        WorldBranch=3
    }
}
