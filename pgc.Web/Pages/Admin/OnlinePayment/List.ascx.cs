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

public partial class Pages_Admin_Payment_List : BaseListControl
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
        int cellverify = 8;
        int cellReverse = 9;
        int maxChar = 45; 

        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            e.Row.Cells[cellDate].Text = DateUtil.GetPersianDateWithTime((DateTime)DataBinder.Eval(e.Row.DataItem, "Date"));
            e.Row.Cells[cellAMount].Text = UIUtil.GetCommaSeparatedOf(DataBinder.Eval(e.Row.DataItem, "Amount").ToString()) + " ریال";

            //ORDER_ID
            HyperLink hp = new HyperLink()
            {
                NavigateUrl = GetRouteUrl("admin-orders", null) + "?id=" + DataBinder.Eval(e.Row.DataItem, "Order_ID").ToString(),
                Text = DataBinder.Eval(e.Row.DataItem, "Order_ID").ToString(),
                CssClass="hbtn"
            };
            e.Row.Cells[cellOrderID].Controls.Add(hp);

            //STATE
    
            MellatOnlinePaymentState status = (MellatOnlinePaymentState)Enum.Parse(typeof(OnlineTransactionStatus), DataBinder.Eval(e.Row.DataItem, "State").ToString());
            if (status != MellatOnlinePaymentState.OK)
                e.Row.Cells[cellStatus].Text = EnumUtil.GetEnumElementPersianTitle(status);
            else
            {
                
                    e.Row.Cells[cellStatus].Text = "پرداخت شده";
               
            }

            if (e.Row.Cells[cellStatus].Text.Length > maxChar)
            {
                e.Row.Cells[cellStatus].ToolTip = e.Row.Cells[cellStatus].Text;
                e.Row.Cells[cellStatus].Text = e.Row.Cells[cellStatus].Text.Substring(0, maxChar);
            }

            //REFNUM            
            string refnum = DataBinder.Eval(e.Row.DataItem, "RefNum").ToString();
            e.Row.Cells[cellRefNum].Text = string.IsNullOrEmpty(refnum) ? "-------" : refnum;

            if (string.IsNullOrEmpty(refnum) || status != MellatOnlinePaymentState.OK )
            {
                e.Row.Cells[cellReverse].Controls.RemoveAt(0);
                e.Row.Cells[cellverify].Controls.RemoveAt(0);
            }
            
            ////RESULT
            //long result = long.Parse(DataBinder.Eval(e.Row.DataItem, "ResultTransaction").ToString());
            //if (result > 0)
            //    e.Row.Cells[cellResult].Text = "پرداخت شده";
            //else
            //    e.Row.Cells[cellResult].Text = UserMessageKeyBusiness.GetUserMessageKey((UserMessageKey)Enum.Parse(typeof(UserMessageKey), "Err_" + result.ToString()));
           
            //if (e.Row.Cells[cellResult].Text.Length > maxChar)
            //{
            //    e.Row.Cells[cellResult].ToolTip = e.Row.Cells[cellResult].Text;
            //    e.Row.Cells[cellResult].Text = e.Row.Cells[cellResult].Text.Substring(0, maxChar);
            //}
        }
    }

    protected override void Grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        long id = (long)Grid.DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
        Payment online=new PaymentBusiness().Retrieve(id);
        OperationResult op=new OperationResult();

        if (e.CommandName == "VerifyRow")
        {
            //op = new SamanPayment().VerifyTransaction(online.RefNum);
        }
        else if (e.CommandName == "ReverseRow")
        {
            //op =new SamanPayment().ReversTransaction(online.Amount, online.RefNum);
        }

        foreach (var item in op.Messages)
        {
            UserSession.AddMessage(item);
        }
        

        base.Grid_RowCommand(sender, e);
        base.Grid.DataBind();
    }
    
    protected void btnEvent_Click(object sender, EventArgs e)
    {
        if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.fid))
        {
            Response.Redirect(GetRouteUrl("admin-orders", null));
        }
    }
    
    protected override void OnPreRender(EventArgs e)
    {
        int second = OptionBusiness.GetInt(OptionKey.SecondOfRefreshOrderPage);

        if (second > 0)
            Timer.Interval = second * 1000;

        base.OnPreRender(e);
        
    }

    protected void Timer_Tick(object sender, EventArgs e)
    {
        grdList.DataBind();
    }

}