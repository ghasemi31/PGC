using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model.Patterns;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Linq;
using kFrameWork.Util;
using System.Web.UI.WebControls;

public partial class Pages_Admin_BranchTransaction_Detail : BaseDetailControl<BranchTransaction>
{
    public BranchOrder branchOrder = new BranchOrder();
    public BranchReturnOrder branchReturn = new BranchReturnOrder();
    public BranchLackOrder branchLackOrder = new BranchLackOrder();
    public BranchPayment branchPay = new BranchPayment();
    public OnlinePayment customerPay = new OnlinePayment();
    public BranchTransaction branchTransaction = new BranchTransaction();    
    
    public override BranchTransaction GetEntity(BranchTransaction Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new BranchTransaction();
       
        return Data;
    }

    public override void SetEntity(BranchTransaction Data, ManagementPageMode Mode)
    {
        if (Mode == ManagementPageMode.Add)
            return;

        if (Data == null || Data.ID < 1)
            return;


        branchTransaction = Data;
        switch ((BranchTransactionType)Data.TransactionType)
        {
            case BranchTransactionType.CustomerOnline:
                customerPay = new OnlinePaymentBusiness().Retrieve(Data.TransactionType_ID);
                break;
            
            
            case BranchTransactionType.BranchOrder:
                branchOrder = new BranchOrderBusiness().Retrieve(Data.TransactionType_ID);
                detailListOrder.Controls.RemoveAt(0);
                detailListOrder.Controls.Add(CreateTableOfBranchOrderTitleReadOnly());
                break;


            case BranchTransactionType.BranchLackOrder:
                branchLackOrder = new BranchLackOrderBusiness().Retrieve(Data.TransactionType_ID);
                detailListLackOrder.Controls.RemoveAt(0);
                detailListLackOrder.Controls.Add(CreateTableOfBranchLackOrderTitleReadOnly());
                break;

            case BranchTransactionType.BranchPayment:
                branchPay = new BranchPaymentBusiness().Retrieve(Data.TransactionType_ID);
                break;
            
            
            case BranchTransactionType.BranchReturnOrder:
                branchReturn = new BranchReturnOrderBusiness().Retrieve(Data.TransactionType_ID);
                detailListReturnOrder.Controls.RemoveAt(0);
                detailListReturnOrder.Controls.Add(CreateTableOfBranchReturnOrderTitleReadOnly());
                break;
            
            
            default:
                break;
        }
    }

    private HtmlTable CreateTableOfBranchLackOrderTitleReadOnly()
    {
        //Header
        HtmlTable table = new HtmlTable();
        HtmlTableRow hrow = new HtmlTableRow();
        hrow.Style.Add("font-weight", "bold");
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "نام کالا" });
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "نام گروه" });
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "مبلغ واحد" });
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "تعداد سفارش شده" });
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "تعداد کسری " });
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "مبلغ کل کسری" });

        table.Rows.Add(hrow);


        foreach (var item in branchLackOrder.BranchLackOrderDetails.OrderBy(f => f.BranchOrderTitle.BranchOrderTitleGroup.DisplayOrder).ThenBy(g => g.BranchOrderTitle.DisplayOrder))
        {
            HtmlTableRow row = new HtmlTableRow();

            //cell 1 Product Title
            row.Cells.Add(new HtmlTableCell() { InnerText = item.BranchOrderTitle_Title });

            //cell 2 Group Title
            row.Cells.Add(new HtmlTableCell() { InnerText = item.BranchOrderTitle.BranchOrderTitleGroup.Title });

            //cell 3 Single Price
            row.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(item.SinglePrice) + " ریال" });

            //cell 4 ordered Quantity
            long orderedQuantity = branchLackOrder.BranchOrder.BranchOrderDetails.Single(f => f.BranchOrderTitle_ID == item.BranchOrderTitle_ID).Quantity;
            row.Cells.Add(new HtmlTableCell() { InnerText = orderedQuantity + " عدد" });

            //cell 5 lack Quantity
            row.Cells.Add(new HtmlTableCell() { InnerText = item.Quantity + " عدد" });

            //cell 6 total price
            row.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(item.TotalPrice) + " ریال" });

            table.Rows.Add(row);
        }

        #region Footer

        HtmlTableRow fRow = new HtmlTableRow();
        fRow.Attributes.Add("class", "footerRow");
        fRow.Cells.Add(new HtmlTableCell() { InnerText = "مجموع کل :", ColSpan = 5 });
        fRow.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(branchLackOrder.TotalPrice) + " ریال" });
        table.Rows.Add(fRow);

        #endregion


        return table;
    }

    private HtmlTable CreateTableOfBranchReturnOrderTitleReadOnly()
    {
        //Header
        HtmlTable table = new HtmlTable();
        HtmlTableRow hrow = new HtmlTableRow();
        hrow.Style.Add("font-weight", "bold");
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "نام کالا" });
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "نام گروه" });
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "مبلغ واحد" });        
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "تعداد مرجوعی " });
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "مبلغ کل مرجوعی" });

        table.Rows.Add(hrow);


        //Add each BranchReturnOrderTitle
        foreach (var item in branchReturn.BranchReturnOrderDetails.OrderBy(f => f.BranchOrderTitle.BranchOrderTitleGroup.DisplayOrder).ThenBy(g => g.BranchOrderTitle.DisplayOrder))
        {
            HtmlTableRow row = new HtmlTableRow();

            //cell 1 Product Title
            row.Cells.Add(new HtmlTableCell() { InnerText = item.BranchOrderTitle_Title });

            //cell 2 Group Title
            row.Cells.Add(new HtmlTableCell() { InnerText = item.BranchOrderTitle.BranchOrderTitleGroup.Title });

            //cell 3 Single Price
            row.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(item.SinglePrice) + " ریال" });

            //cell 4 quantity
            row.Cells.Add(new HtmlTableCell() { InnerText = item.Quantity + " عدد" });

            //cell 5 total price
            row.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(item.TotalPrice) + " ریال" });

            table.Rows.Add(row);

        }

        #region Footer

        HtmlTableRow fRow = new HtmlTableRow();
        fRow.Attributes.Add("class", "footerRow");
        fRow.Cells.Add(new HtmlTableCell() { InnerText = "مجموع کل :", ColSpan = 4 });
        fRow.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(branchReturn.TotalPrice) + " ریال" });
        table.Rows.Add(fRow);

        #endregion


        return table;
    }

    private HtmlTable CreateTableOfBranchOrderTitleReadOnly()
    {
        //Header
        HtmlTable table = new HtmlTable();
        HtmlTableRow hrow = new HtmlTableRow();
        hrow.Style.Add("font-weight", "bold");
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "نام کالا" });
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "نام گروه" });
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "مبلغ واحد" });
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "تعداد" });
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "مبلغ کل" });

        table.Rows.Add(hrow);


        //Add each BranchOrderTitle
        foreach (var item in branchOrder.BranchOrderDetails.OrderBy(f => f.BranchOrderTitle.BranchOrderTitleGroup.DisplayOrder).ThenBy(g => g.BranchOrderTitle.DisplayOrder))
        {
            HtmlTableRow row = new HtmlTableRow();


            //cell 1 Product Title
            row.Cells.Add(new HtmlTableCell() { InnerText = item.BranchOrderTitle_Title });

            //cell 2 Group Title
            row.Cells.Add(new HtmlTableCell() { InnerText = item.BranchOrderTitle.BranchOrderTitleGroup.Title });

            //cell 3 Single Price
            row.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(item.SinglePrice) + " ریال" });

            //cell 4 quantity
            row.Cells.Add(new HtmlTableCell() { InnerText = item.Quantity + " عدد" });

            //cell 5 total price
            row.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(item.TotalPrice) + " ریال" });


            table.Rows.Add(row);
        }

        #region Footer

        HtmlTableRow fRow = new HtmlTableRow();
        fRow.Attributes.Add("class", "footerRow");
        fRow.Cells.Add(new HtmlTableCell() { InnerText = "مجموع کل :", ColSpan = 4 });
        fRow.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(branchOrder.TotalPrice) + "ریال" });
        table.Rows.Add(fRow);

        #endregion


        return table;
    }
}