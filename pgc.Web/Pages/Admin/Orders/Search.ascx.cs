using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Model;
using pgc.Business;
using System;
using System.Web.UI.WebControls;
using kFrameWork.Enums;

public partial class Pages_Admin_GameOrder_Search : BaseSearchControl<GameOrdersPattern>
{

    public override GameOrdersPattern Pattern
    {
        get
        {
            GameOrdersPattern pattern = new GameOrdersPattern()
            {
                OrderPersianDate = pdrGameOrderPersianDate.DateRange,

                Game_ID = lkcGame.GetSelectedValue<long>(),
                GameOrderPaymentStatus = lkcPaymentStatus.GetSelectedValue<GameOrderPaymentStatus>(),
                Amount = nrAmount.Pattern,
                //RefNum=txtRefNum.Text,                
                UserName = txtUser.Text
            };

            int number = 0;
            int.TryParse(txtNumber.Text, out number);
            pattern.Numbers = number;
            Session["GameOrderPattern"] = pattern;
            return pattern;
        }
        set
        {

            txtNumber.Text = (value.Numbers == 0) ? "" : value.Numbers.ToString();
            lkcGame.SetSelectedValue(value.Game_ID);
            lkcPaymentStatus.SetSelectedValue(value.GameOrderPaymentStatus);
            pdrGameOrderPersianDate.DateRange = value.OrderPersianDate;
            nrAmount.Pattern = value.Amount;
            //txtRefNum.Text = value.RefNum;
            txtUser.Text = value.UserName;
        }
    }
    public override GameOrdersPattern DefaultPattern
    {
        get
        {
            GameOrdersPattern p = new GameOrdersPattern();
            if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.id))
            {
                if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.id))
                {
                    var page = (this.Page as BaseManagementPage<GameOrdersBusiness, GameOrder, GameOrdersPattern, pgcEntities>);
                    page.SelectedID = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.id);
                    page.DetailControl.BeginMode(ManagementPageMode.Edit);
                    page.DetailControl.SetEntity(new GameOrdersBusiness().Retrieve(page.SelectedID), ManagementPageMode.Edit);
                    page.Mode = kFrameWork.Enums.ManagementPageMode.Edit;
                }
            }

            if (Session["GameOrderPattern"] != null)
            {
                try { p = (GameOrdersPattern)Session["GameOrderPattern"]; }
                catch (Exception) { }
            }
            return p;
        }
    }

    public override GameOrdersPattern SearchAllPattern
    {
        get
        {
            Session["GameOrderPattern"] = null;
            return base.SearchAllPattern;
        }
    }

    //protected override void OnPreRender(System.EventArgs e)
    //{
    //    base.OnPreRender(e);
    //    Session["GameOrderPattern"] = Pattern;
    //}
}