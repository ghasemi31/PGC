using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("وضعیت تراکنش برگشتی از بانک")]
    public enum OnlineTransactionStatus
    {

        [PersianTitle("تراکنش توسط خريدار کنسل شده است.")]
        Canceled_By_User = 1,

        [PersianTitle("مبلغ سند برگشتي، از مبلغ تراکنش اصلي بيشتر است.")]
        Invalid_Amount = 2,

        [PersianTitle("درخواست برگشت يک تراکنش رسيده است، در حالي که تراکنش اصلي پيدا نمي شود.")]
        Invalid_Transaction = 3,

        [PersianTitle("شماره کارت اشتباه است.")]
        Invalid_Card_Number = 4,

        [PersianTitle("چنين صادر کننده کارتي وجود ندارد.")]
        No_Such_Issuer = 5,

        [PersianTitle("از تاريخ انقضاي کارت گذشته است و کارت ديگر معتبر نيست.")]
        Expired_Card_Pick_Up = 6,

        [PersianTitle("رمز کارت (PIN) 3 مرتبه اشتباه وارد شده است در نتيجه کارت غير فعال خواهد شد.")]
        Allowable_PIN_Tries_Exceeded_Pick_Up = 7,

        [PersianTitle("خريدار رمز کارت (PIN) را اشتباه وارد کرده است.")]
        Incorrect_PIN = 8,

        [PersianTitle("مبلغ بيش از سقف برداشت مي باشد.")]
        Exceeds_Withdrawal_Amount_Limit = 9,

        [PersianTitle("تراکنش Authorize شده است ( شماره PIN و PAN درست هستند) ولي امکان سند خوردن وجود ندارد.")]
        Transaction_Cannot_Be_Completed = 10,

        [PersianTitle("تراکنش در شبکه بانکي Timeout خورده است.")]
        Response_Received_Too_Late = 11,

        [PersianTitle("خريدار يا فيلد CVV2 و يا فيلد ExpDate را اشتباه زده است. ( يا اصلا وارد نکرده است)")]
        Suspected_Fraud_Pick_Up = 12,

        [PersianTitle("موجودي به اندازي کافي در حساب وجود ندارد.")]
        No_Sufficient_Funds = 13,

        [PersianTitle("سيستم کارت بانک صادر کننده در وضعيت عملياتي نيست.")]
        Issuer_Down_Slm = 14,

        [PersianTitle("کليه خطاهاي ديگر بانکي باعث ايجاد چنين خطايي مي گردد.")]
        TME_Error = 15,

        [PersianTitle("عمليات پرداخت آنلاين با موفقيت انجام شد")]
        OK = 16,

        [PersianTitle("فاقد رسید دیجیتالی")]
        OnlineRefNumIsEmpty = 17,
    }
}
