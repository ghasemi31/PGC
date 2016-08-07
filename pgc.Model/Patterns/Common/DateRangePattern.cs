using System;
using pgc.Model.Enums;

namespace pgc.Model
{
    [Serializable]
    public class DateRangePattern:BasePattern
    {
        public DateRangePattern()
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            Date = string.Empty;
            SearchMode = SearchType.Nothing;
        }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string Date { get; set; }

        public SearchType SearchMode { get; set; }

        public enum SearchType
        {
            [PersianTitle("هیچ")]
            Nothing = 0,
            [PersianTitle("در تاریخ")]
            Equal = 1,
            [PersianTitle("بعد از")]
            Greater =2,
            [PersianTitle("قبل از")]
            Less = 3,
            [PersianTitle("ما بین")]
            Between =4
        }

        public bool HasFromDate
        {
            get
            {
                return (!string.IsNullOrEmpty(this.FromDate));
            }
        }

        public bool HasToDate
        {
            get
            {
                return (!string.IsNullOrEmpty(this.ToDate));
            }
        }

        public bool HasDate
        {
            get
            {
                return (!string.IsNullOrEmpty(this.Date));
            }
        }
    }
}