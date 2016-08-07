using System;
using kFrameWork.UI;
using System.Web.UI.WebControls;
using System.Web.UI;
using pgc.Model.Enums;
using pgc.Business;
using kFrameWork.Util;
using System.Collections.Generic;
using pgc.Model;
using pgc.Model.Patterns;
using System.Linq;

public partial class Pages_Agent_Order_List : BaseListControl
{
    long NewTotalAmount = 0;
    long OldTotalAmount = 0;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }

    protected override void OnPreRender(EventArgs e)
    {
        int second = kFrameWork.Business.OptionBusiness.GetInt(pgc.Model.Enums.OptionKey.SecondOfRefreshOrderPage);

        if (second > 0)
            Timer.Interval = second * 1000;

        base.OnPreRender(e);
    }

    protected void Timer_Tick(object sender, EventArgs e)
    {
        grdList.DataBind();
    }

    protected void grdList_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        int cellOnlineBtn = 7;
        int cellOnlineStatus = 5;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //OnlineTransaction
            HyperLink hp = new HyperLink()
            {
                NavigateUrl = GetRouteUrl("agent-onlinepayment", null) + "?fid=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString(),
                Text = "تراکنش ها",
                CssClass = "hbtn"
            };
            e.Row.Cells[cellOnlineBtn].Controls.Add(hp);


            //OrderPaymentStatus
            Image iStatus = new Image();
            OnlinePayment online = ((List<OnlinePayment>)DataBinder.Eval(e.Row.DataItem, "OnlinePayment")).Count > 0 ?
                ((List<OnlinePayment>)DataBinder.Eval(e.Row.DataItem, "OnlinePayment"))[0] :
                new OnlinePayment();
            
            PaymentType pType = (PaymentType)DataBinder.Eval(e.Row.DataItem, "PaymentType");
            
            OrderPaymentStatus status = (pType == PaymentType.Presence) ?
                                        OrderPaymentStatus.Presence :
                                        (
                                        (online.TransactionState == "OK" && online.ResultTransaction > 0 && online.RefNum != "") ?
                                        OrderPaymentStatus.OnlineSucced :
                                        OrderPaymentStatus.OnlineFailed
                                        );

            switch (status)
            {
                case OrderPaymentStatus.Presence:
                    iStatus.ImageUrl = "~/styles/images/payment/presence.png";
                    iStatus.AlternateText = "پرداخت حضوری";
                    iStatus.ToolTip = iStatus.AlternateText;

                    break;
                case OrderPaymentStatus.OnlineSucced:
                    iStatus.ImageUrl = "~/styles/images/payment/paid.png";
                    iStatus.AlternateText = "رسید دیجیتالی: " + online.RefNum;
                    iStatus.ToolTip = iStatus.AlternateText;
                    break;
                case OrderPaymentStatus.OnlineFailed:
                    iStatus.ImageUrl = "~/styles/images/payment/notpaid.png";

                    if (online.TransactionState == "OK")
                    {
                        //modification ver 83 on 1392/11/16
                        if (online.ResultTransaction >= 0 || online.ResultTransaction < 20)
                            iStatus.AlternateText = "توضیحات موجود نیست";
                        else
                            iStatus.AlternateText = (UserMessageKeyBusiness.GetUserMessageDescription((UserMessageKey)Enum.Parse(typeof(UserMessageKey), "Err_" + online.ResultTransaction.ToString().Substring(1))));
                    }
                    else if (!string.IsNullOrEmpty(online.TransactionState))
                        iStatus.AlternateText = (EnumUtil.GetEnumElementPersianTitle((OnlineTransactionStatus)Enum.Parse(typeof(OnlineTransactionStatus), (online.TransactionState))));
                    else
                        iStatus.AlternateText = "پرداخت نشده";


                    iStatus.ToolTip = iStatus.AlternateText;

                    break;
                case OrderPaymentStatus.Online:
                default:
                    break;
            }


            e.Row.Cells[cellOnlineStatus].Controls.Add(iStatus);
            NewTotalAmount += (long)DataBinder.Eval(e.Row.DataItem, "PayableAmount");
        }

        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.Footer)
        {
            BaseManagementPage<OrdersAgentBusiness, Order, OrdersAgentPattern, pgcEntities> page = (this.Page as BaseManagementPage<OrdersAgentBusiness, Order, OrdersAgentPattern, pgcEntities>);
            
            e.Row.Cells[3].Text = "جمع کل";
            e.Row.Cells[4].Text = UIUtil.GetCommaSeparatedOf(page.Business.Search_Select(page.SearchControl.Pattern).Sum(f => f.PayableAmount).ToString()) + " ریال";
            e.Row.Cells[4].ColumnSpan = 3;
            e.Row.Cells[4].Style.Add("padding-top", "10px");
            e.Row.Font.Bold = true;
            e.Row.Font.Size = 8;
        }
    }
}