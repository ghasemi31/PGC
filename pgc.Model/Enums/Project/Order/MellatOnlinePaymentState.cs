using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت تراکنش آنلاین")]
    public enum MellatOnlinePaymentState
    {

        [PersianTitle("عدم بازگشت از درگاه بانک.")]
        NoReturnFromBank = -1,

        [PersianTitle("عمليات پرداخت آنلاين با موفقيت انجام شد")]
        OK = 0,

        [PersianTitle("ﺷﻤﺎﺭﻩ ﻛﺎﺭﺕ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ.")]
        Invalid_Card_Number = 11,

        [PersianTitle("ﻣﻮﺟﻮﺩﻱ ﻛﺎﻓﻲ ﻧﻴﺴﺖ.")]
        No_Sufficient_Funds = 12,

        [PersianTitle("ﺭﻣﺰ ﻧﺎﺩﺭﺳﺖ ﺍﺳﺖ.")]
        Incorrect_PIN = 13,

        [PersianTitle("ﺗﻌﺪﺍﺩ ﺩﻓﻌﺎﺕ ﻭﺍﺭﺩ ﻛﺮﺩﻥ ﺭﻣﺰ ﺑﻴﺶ ﺍﺯ ﺣﺪ ﻣﺠﺎﺯ ﺍﺳﺖ.")]
        Allowable_PIN_Tries_Exceeded_Pick_Up = 14,

        [PersianTitle("ﻛﺎﺭﺕ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ")]
        InalidCard = 15,

        [PersianTitle("ﺩﻓﻌﺎﺕ ﺑﺮﺩﺍﺷﺖ ﻭﺟﻪ ﺑﻴﺶ ﺍﺯ ﺣﺪ ﻣﺠﺎﺯ ﺍﺳﺖ")]
        Exceeds_Withdrawal_Tries_Limit = 16,

        [PersianTitle("ﻛﺎﺭﺑﺮ ﺍﺯ ﺍﻧﺠﺎﻡ ﺗﺮﺍﻛﻨﺶ ﻣﻨﺼﺮﻑ ﺷﺪﻩ ﺍﺳﺖ.")]
        Canceled_By_User = 17,

        [PersianTitle("ﺗﺎﺭﻳﺦ ﺍﻧﻘﻀﺎﻱ ﻛﺎﺭﺕ ﮔﺬﺷﺘﻪ ﺍﺳﺖ.")]
        Expired_Card_Pick_Up = 18,

        [PersianTitle("ﻣﺒﻠﻎ ﺑﺮﺩﺍﺷﺖ ﻭﺟﻪ ﺑﻴﺶ ﺍﺯ ﺣﺪ ﻣﺠﺎﺯ ﺍﺳﺖ.")]
        Exceeds_Withdrawal_Amount_Limit = 19,

        [PersianTitle("ﭘﺬﻳﺮﻧﺪﻩ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ.")]
        No_Such_Acceptor = 21,

        [PersianTitle("ﺧﻄﺎﻱ ﺍﻣﻨﻴﺘﻲ ﺭﺥ ﺩﺍﺩﻩ ﺍﺳﺖ.")]
        Security_Error=23,

        [PersianTitle("ﺍﻃﻼﻋﺎﺕ ﻛﺎﺭﺑﺮﻱ ﭘﺬﻳﺮﻧﺪﻩ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ.")]
        Invalid_Acceptor_Info = 24,

        [PersianTitle("ﻣﺒﻠﻎ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ.")]
        Invalid_Amount = 25,

        [PersianTitle("ﭘﺎﺳﺦ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ.")]
        Invalid_Response = 31,

        [PersianTitle("ﻓﺮﻣﺖ ﺍﻃﻼﻋﺎﺕ ﻭﺍﺭﺩ ﺷﺪﻩ ﺻﺤﻴﺢ ﻧﻤﻲ ﺑﺎﺷﺪ.")]
        Invalid_Entered_Data = 32,

        [PersianTitle("ﺣﺴﺎﺏ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ.")]
        Invalid_Account = 33,

        [PersianTitle("ﺧﻄﺎﻱ ﺳﻴﺴﺘﻤﻲ.")]
        System_Error = 34,

        [PersianTitle("ﺗﺎﺭﻳﺦ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ.")]
        Invalid_Date = 35,

        [PersianTitle("ﺷﻤﺎﺭﻩ ﺩﺭﺧﻮﺍﺳﺖ ﺗﻜﺮﺍﺭﻱ ﺍﺳﺖ.")]
        Duplicate_RequestIP = 41,

        [PersianTitle("ﺗﺮﺍﻛﻨﺶ Sale ﻳﺎﻓﺖ ﻧﺸﺪ.")]
        Sale_NotFound = 42,

        [PersianTitle("ﻗﺒﻼ ﺩﺭﺧﻮﺍﺳﺖ Verify ﺩﺍﺩﻩ ﺷﺪﻩ ﺍﺳﺖ.")]
        Duplicate_Verify = 43,

        [PersianTitle("ﺩﺭﺧﻮﺍﺳﺖ Verfiy ﻳﺎﻓﺖ ﻧﺸﺪ.")]
        Verify_NotFound = 44,

        [PersianTitle("مبلغ واریز ﺷﺪﻩ ﺍﺳﺖ.")]
        Settle_Done = 45,

        [PersianTitle("مبلغ واریز ﻧﺸﺪﻩ ﺍﺳﺖ.")]
        Settle_Faild = 46,

        [PersianTitle("ﺗﺮﺍﻛﻨﺶ واریز ﻳﺎﻓﺖ ﻧﺸﺪ.")]
        Settle_NotFound = 47,

        [PersianTitle("برگشت وجه با موفقیت انجا ﺷﺪﻩ ﺍﺳﺖ.")]
        ReverseDone = 48,

        [PersianTitle("ﺗﺮﺍﻛﻨﺶ بازپرداخت ﻳﺎﻓﺖ ﻧﺸﺪ .")]
        Refun_Trans_NotFound = 49,

        [PersianTitle("ﺗﺮﺍﻛﻨﺶ ﺗﻜﺮﺍﺭﻱ ﺍﺳﺖ.")]
        Dupplicate_Trans = 51,

        [PersianTitle("ﺗﺮﺍﻛﻨﺶ ﻣﺮﺟﻊ ﻣﻮﺟﻮﺩ ﻧﻴﺴﺖ.")]
        Refrence_Trans_NotExist = 54,

        [PersianTitle("ﺗﺮﺍﻛﻨﺶ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ.")]
        Invalid_Trans = 55,

        [PersianTitle("ﺧﻄﺎ ﺩﺭ ﻭﺍﺭﻳﺰ.")]
        Settle_Error = 61,

        [PersianTitle("ﺻﺎﺩﺭ ﻛﻨﻨﺪﻩ ﻛﺎﺭﺕ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ.")]
        Invalid_Card_Issuer = 111,

        [PersianTitle("ﺧﻄﺎﻱ ﺳﻮﻳﻴﭻ ﺻﺎﺩﺭ ﻛﻨﻨﺪﻩ ﻛﺎﺭﺕ.")]
        Issuer_Switcher_Error = 112,

        [PersianTitle("ﭘﺎﺳﺨﻲ ﺍﺯ ﺻﺎﺩﺭ ﻛﻨﻨﺪﻩ ﻛﺎﺭﺕ ﺩﺭﻳﺎﻓﺖ ﻧﺸﺪ.")]
        Issuer_NotResponse = 113,

        [PersianTitle("ﺩﺍﺭﻧﺪﻩ ﻛﺎﺭﺕ ﻣﺠﺎﺯ ﺑﻪ ﺍﻧﺠﺎﻡ ﺍﻳﻦ ﺗﺮﺍﻛﻨﺶ ﻧﻴﺴﺖ.")]
        Unauthorized= 114,

        [PersianTitle("ﺷﻨﺎﺳﻪ ﻗﺒﺾ ﻧﺎﺩﺭﺳﺖ ﺍﺳﺖ.")]
        Invalid_BillID = 412,

        [PersianTitle("ﺷﻨﺎﺳﻪ ﭘﺮﺩﺍﺧﺖ ﻧﺎﺩﺭﺳﺖ ﺍﺳﺖ.")]
        Invalid_OrderID = 413,

        [PersianTitle("ﺳﺎﺯﻣﺎﻥ ﺻﺎﺩﺭ ﻛﻨﻨﺪﻩ ﻗﺒﺾ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ.")]
        Invalid_Org_Issuer = 414,

        [PersianTitle("ﺯﻣﺎﻥ ﺟﻠﺴﻪ ﻛﺎﺭﻱ ﺑﻪ ﭘﺎﻳﺎﻥ ﺭﺳﻴﺪﻩ ﺍﺳﺖ.")]
        Session_Timeout = 415,

        [PersianTitle("ﺧﻄﺎ ﺩﺭ ﺛﺒﺖ ﺍﻃﻼﻋﺎﺕ.")]
        Register_Error = 416,

        [PersianTitle("ﺷﻨﺎﺳﻪ ﭘﺮﺩﺍﺧﺖ ﻛﻨﻨﺪﻩ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ.")]
        Invalid_PaymentID= 417,

        [PersianTitle("ﺍﺷﻜﺎﻝ ﺩﺭ ﺗﻌﺮﻳﻒ ﺍﻃﻼﻋﺎﺕ ﻣﺸﺘﺮﻱ.")]
        Defining_Customer_Error = 418,

        [PersianTitle("ﺗﻌﺪﺍﺩ ﺩﻓﻌﺎﺕ ﻭﺭﻭﺩ ﺍﻃﻼﻋﺎﺕ ﺍﺯ ﺣﺪ ﻣﺠﺎﺯ ﮔﺬﺷﺘﻪ ﺍﺳﺖ")]
        Data_Entri_Tried_Limmited = 419,

        [PersianTitle("IP نا معتبر است.")]
        Invalid_IP = 421,

    }
}
