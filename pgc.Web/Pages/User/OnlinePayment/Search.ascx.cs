using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Model;
using pgc.Business;

public partial class Pages_Admin_OnlinePayment_Search : BaseSearchControl<OnlinePaymentPattern>
{
    public override OnlinePaymentPattern Pattern
    {
        get
        {
            //long ID=0;
            //long.TryParse(txtOrderID.Text, out ID);

            return new OnlinePaymentPattern()
            {
                //Amount=nmrAMount.Pattern,
                //Order_ID=ID,
                //PersianDate=pdrDate.DateRange,
                //Status=lkpStatus.GetSelectedValue<OnlineTransactionStatus>()
                User_ID=UserSession.UserID
            };
        }
        set
        {
            //nmrAMount.Pattern = value.Amount;
            //if (value.Order_ID > 0)
            //    txtOrderID.Text = value.Order_ID.ToString();
            //pdrDate.DateRange = value.PersianDate;
            //lkpStatus.SetSelectedValue(value.Status);
        }
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        var page = this.Page as BaseManagementPage<OnlinePaymentBusiness, OnlinePayment, OnlinePaymentPattern, pgcEntities>;
        page.SearchControl.Visible = false;
        base.OnPreRender(e);
    }

    public override OnlinePaymentPattern DefaultPattern
    {
        get
        {
            return new OnlinePaymentPattern() { User_ID = UserSession.UserID };
        }
    }
}