using System;
using kFrameWork.UI;
using System.Web.UI;
using kFrameWork.Util;
using pgc.Model.Enums;
using pgc.Business;
using System.Web.UI.WebControls;
using pgc.Business.Core;
using pgc.Model;
using kFrameWork.Model;
using kFrameWork.Business;
using System.Web.UI.HtmlControls;
using pgc.Model.Patterns;

public partial class Pages_Admin_OnlinePaymentList_List : BaseListControl
{
    private pgc.Business.General.OnlinePaymentListBusiness business = new pgc.Business.General.OnlinePaymentListBusiness();
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }



    protected void grdList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int cellAMount = 3;
        int cellOrderID = 4;
        int cellRefNum = 5;
        int cellDate = 6;
        int cellStatus = 7;
        int cellPayment = 8;
        //int cellResult = 8;
        //int cellverify = 8;
        //int cellReverse = 9;
        int maxChar = 45;

        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            e.Row.Cells[cellDate].Text = DateUtil.GetPersianDateWithTime((DateTime)DataBinder.Eval(e.Row.DataItem, "Date"));
            e.Row.Cells[cellAMount].Text = UIUtil.GetCommaSeparatedOf(DataBinder.Eval(e.Row.DataItem, "Amount").ToString()) + " ریال";

            //ORDER_ID
            var path = "";
            if (!DataBinder.Eval(e.Row.DataItem, "ResNum").ToString().Contains("b"))
            {
                path = GetRouteUrl("admin-orders", null) + "?id=" + DataBinder.Eval(e.Row.DataItem, "Order_ID").ToString();
                HyperLink hp = new HyperLink()
                {
                    //NavigateUrl = GetRouteUrl("admin-orders", null) + "?id=" + DataBinder.Eval(e.Row.DataItem, "Order_ID").ToString(),
                    NavigateUrl = path,
                    Text = DataBinder.Eval(e.Row.DataItem, "ResNum").ToString(),
                    CssClass = "hbtn"
                };
                e.Row.Cells[cellOrderID].Controls.Add(hp);
            }
            else
            {
                e.Row.Cells[cellOrderID].Text = "--";
            }
            e.Row.Cells[cellOrderID].Style.Add("text-align", "center");

            //STATE
            long result = 0;
            OnlineTransactionStatus status = (OnlineTransactionStatus)Enum.Parse(typeof(OnlineTransactionStatus), DataBinder.Eval(e.Row.DataItem, "TransactionState").ToString());
            if (status != OnlineTransactionStatus.OK)
                e.Row.Cells[cellStatus].Text = EnumUtil.GetEnumElementPersianTitle(status);
            else
            {
                //modification ver 83 on 1392/11/16
                result = long.Parse(DataBinder.Eval(e.Row.DataItem, "ResultTransaction").ToString());
                if (result > 0)
                    e.Row.Cells[cellStatus].Text = "پرداخت شده";
                else if (result == 0 || result < 20)
                    e.Row.Cells[cellStatus].Text = "توضیحات موجود نیست";//"<span title='توضیحات علت عدم موفقیت پرداخت یا بازگشت از بانک موجود نمی باشد'>[بدون توضیح]<span>";
                else
                    e.Row.Cells[cellStatus].Text = UserMessageKeyBusiness.GetUserMessageDescription((UserMessageKey)Enum.Parse(typeof(UserMessageKey), "Err_" + result.ToString().Substring(1)));
            }

            if (e.Row.Cells[cellStatus].Text.Length > maxChar)
            {
                e.Row.Cells[cellStatus].ToolTip = e.Row.Cells[cellStatus].Text;
                e.Row.Cells[cellStatus].Text = e.Row.Cells[cellStatus].Text.Substring(0, maxChar);
            }

            //REFNUM            
            string refnum = DataBinder.Eval(e.Row.DataItem, "RefNum").ToString();
            e.Row.Cells[cellRefNum].Text = string.IsNullOrEmpty(refnum) ? "-------" : refnum;
            //payment
            if (!DataBinder.Eval(e.Row.DataItem, "ResNum").ToString().Contains("b"))
            {
                e.Row.Cells[cellPayment].Text = "کاربر";
            }
            else
            {
                var onlineResNum = DataBinder.Eval(e.Row.DataItem, "ResNum");

                e.Row.Cells[cellPayment].Text = business.RetriveBranchName(onlineResNum.ToString());
            }
                   }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            var _Page = this.Page as BaseManagementPage<OnlinePaymentListBusiness, OnlinePaymentList, OnlinePaymentListPattern, pgcEntities>;

            e.Row.Cells[3].Text = "جمع کل";
            long total = _Page.Business.TotalAmount(_Page.SearchControl.Pattern);

            e.Row.Cells[5].Text = UIUtil.GetCommaSeparatedOf(total) + " ریال";
            e.Row.Cells[5].ColumnSpan = 3;
            e.Row.Cells[5].Style.Add("padding-top", "10px");
            e.Row.Font.Bold = true;
            e.Row.Font.Size = 8;
        }
    }


    protected void btnEvent_Click(object sender, EventArgs e)
    {
        if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.fid))
        {
            Response.Redirect(GetRouteUrl("admin-orders", null));
        }
    }


}