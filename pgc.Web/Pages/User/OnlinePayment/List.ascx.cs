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

public partial class Pages_Admin_OnlinePayment_List : BaseListControl
{
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
        //int cellResult = 8;
        int maxChar = 30;

        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            e.Row.Cells[cellDate].Text = DateUtil.GetPersianDateWithTime((DateTime)DataBinder.Eval(e.Row.DataItem, "Date"));
            e.Row.Cells[cellAMount].Text = UIUtil.GetCommaSeparatedOf(DataBinder.Eval(e.Row.DataItem, "Amount").ToString()) + " ریال";

            //ORDER_ID
            HyperLink hp = new HyperLink()
            {
                NavigateUrl = GetRouteUrl("guest-orderdetail", new { id = DataBinder.Eval(e.Row.DataItem, "Order_ID").ToString() }),
                Text = DataBinder.Eval(e.Row.DataItem, "Order_ID").ToString(),
                CssClass = "hbtn"
            };
            e.Row.Cells[cellOrderID].Controls.Add(hp);


            //RefNum
            string refnum = DataBinder.Eval(e.Row.DataItem, "RefNum").ToString();
            e.Row.Cells[cellRefNum].Text = string.IsNullOrEmpty(refnum) ? "-------" : refnum;


            //STATE
            long result = 0;
            OnlineTransactionStatus status = (OnlineTransactionStatus)Enum.Parse(typeof(OnlineTransactionStatus), DataBinder.Eval(e.Row.DataItem, "TransactionState").ToString());
            if (status != OnlineTransactionStatus.OK)
                e.Row.Cells[cellStatus].Text = EnumUtil.GetEnumElementPersianTitle(status);
            else
            {
                result = long.Parse(DataBinder.Eval(e.Row.DataItem, "ResultTransaction").ToString());
                if (result > 0)
                    e.Row.Cells[cellStatus].Text = "پرداخت شده";
                else
                    e.Row.Cells[cellStatus].Text = UserMessageKeyBusiness.GetUserMessageDescription((UserMessageKey)Enum.Parse(typeof(UserMessageKey), "Err_" + result.ToString().Substring(1)));
            }

            if (e.Row.Cells[cellStatus].Text.Length > maxChar)
            {
                e.Row.Cells[cellStatus].ToolTip = e.Row.Cells[cellStatus].Text;
                e.Row.Cells[cellStatus].Text = e.Row.Cells[cellStatus].Text.Substring(0, maxChar);
            }
        }
    }
}