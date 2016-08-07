using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model.Patterns;
using kFrameWork.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using kFrameWork.Util;

public partial class Pages_Admin_BranchOrderNoMoney_Detail : BaseDetailControl<BranchOrder>
{
    public BranchOrder Order = new BranchOrder();
    BaseManagementPage<BranchOrderBusiness, BranchOrder, BranchOrderPattern, pgcEntities> _Page = new BaseManagementPage<BranchOrderBusiness, BranchOrder, BranchOrderPattern, pgcEntities>();

    public void Page_Load()
    {
        _Page = this.Page as BaseManagementPage<BranchOrderBusiness, BranchOrder, BranchOrderPattern, pgcEntities>;
    }

    public override BranchOrder GetEntity(BranchOrder Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new BranchOrder();


        return Data;
    }

    public override bool Validate(ManagementPageMode Mode)
    {
        return true;
    }

    public override void SetEntity(BranchOrder Data, ManagementPageMode Mode)
    {
        Order = Data;

        txtAdminDesc.Text = Data.AdminDescription;
        txtBranchDesc.Text = Data.BranchDescription;
        lkpShipment.SetSelectedValue(Data.ShipmentStatus_ID);

        CreatePreviewTable();
    }

    public override void EndMode(ManagementPageMode Mode)
    {
        base.EndMode(Mode);
        var _Page = this.Page as BaseManagementPage<BranchOrderBusiness, BranchOrder, BranchOrderPattern, pgcEntities>;
        _Page.ListControl.Grid.DataBind();
    }


    private void CreatePreviewTable()
    {
        var OrderDetails = Order.BranchOrderDetails;


        #region Preview Table


        bool isFirstTbl = true;

        //Header
        HtmlTable table = new HtmlTable();
        HtmlTableRow hrow = new HtmlTableRow();
        hrow.Style.Add("font-weight", "bold");
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "ردیف" });
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "نام کالا" });
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "گروه" });
        //hrow.Cells.Add(new HtmlTableCell() { InnerText = "مبلغ واحد" });
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "تعداد سفارش شده" });
        //hrow.Cells.Add(new HtmlTableCell() { InnerText = "مبلغ کل" });

        hrow.Attributes.Add("class", "theader");

        table.Rows.Add(hrow);
        table.Attributes.Add("class", (isFirstTbl) ? "tabBody selectTab" : "tabBody");

        isFirstTbl = false;

        int rowNumber = 1;
        //Add each BranchOrderTitle
        foreach (var orderT in OrderDetails.OrderBy(f => f.BranchOrderTitle.BranchOrderTitleGroup.DisplayOrder).ThenBy(g => g.BranchOrderTitle.DisplayOrder))
        {

            HtmlTableRow row = new HtmlTableRow();


            //Cell 1 RowNumber
            row.Cells.Add(new HtmlTableCell() { InnerText = (rowNumber++).ToString() + "-" });


            //Cell 2 Title
            row.Cells.Add(new HtmlTableCell() { InnerText = orderT.BranchOrderTitle_Title });


            //Cell 3 Group Title
            row.Cells.Add(new HtmlTableCell() { InnerText = orderT.BranchOrderTitle.BranchOrderTitleGroup.Title });


            //Cell 4 Single Price
            //row.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(orderT.SinglePrice) + " ریال" });


            //Cell 5 OrderedQuantity
            row.Cells.Add(new HtmlTableCell() { InnerText = orderT.Quantity + " عدد" });

            //Cell 6 TotalPrice
            //row.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(orderT.SinglePrice * orderT.Quantity) + " ریال" });


            table.Rows.Add(row);
        }



        //#region Footer

        //HtmlTableRow fRow = new HtmlTableRow();
        //fRow.Attributes.Add("class", "footerrow");
        //fRow.Cells.Add(new HtmlTableCell() { InnerText = "مجموع کل :", ColSpan = 5 });
        //fRow.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(Order.TotalPrice) + "ریال" });
        //table.Rows.Add(fRow);

        //#endregion

        for (int i = detailList.Controls.Count - 1; 0 < i; i--)
            detailList.Controls.RemoveAt(i);

        detailList.Controls.Add(table);

        #endregion

    }

    protected void Confirmation_Click(object sender, EventArgs e)
    {
        OperationResult op = _Page.Business.AdminConfirmation(_Page.SelectedID);

        foreach (var item in op.Messages)
            UserSession.AddMessage(item);

        if (op.Result == ActionResult.Done)
        {
            _Page.ListControl.Grid.DataBind();
            _Page.DetailControl.EndMode(ManagementPageMode.Edit);
            _Page.Mode = ManagementPageMode.Search;
        }
    }

    protected void GoCorrection_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("admin-branchordernomoneyedit", null) + "?id=" + _Page.SelectedID);
    }

    protected void Cancelation_Click(object sender, EventArgs e)
    {
        OperationResult op = _Page.Business.AdminCancelation(_Page.SelectedID);

        foreach (var item in op.Messages)
            UserSession.AddMessage(item);

        if (op.Result == ActionResult.Done)
        {
            _Page.ListControl.Grid.DataBind();
            _Page.DetailControl.EndMode(ManagementPageMode.Edit);
            _Page.Mode = ManagementPageMode.Search;
        }
    }

    protected void Finalize_Click(object sender, EventArgs e)
    {
        OperationResult op = _Page.Business.AdminFinalize(_Page.SelectedID);

        foreach (var item in op.Messages)
            UserSession.AddMessage(item);

        if (op.Result == ActionResult.Done)
        {
            _Page.ListControl.Grid.DataBind();
            _Page.DetailControl.EndMode(ManagementPageMode.Edit);
            _Page.Mode = ManagementPageMode.Search;
        }
    }


    protected void btnShipment_Click(object sender, EventArgs e)
    {
        BranchOrder Data = _Page.Business.Retrieve(_Page.SelectedID);

        Data.AdminDescription = txtAdminDesc.Text;
        Data.BranchDescription = txtBranchDesc.Text;
        Data.ShipmentStatus_ID = lkpShipment.GetSelectedValue<long>();
        Data.ShipmentStatus_ID = (Data.ShipmentStatus_ID > 0) ? Data.ShipmentStatus_ID : null;
        OperationResult op = _Page.Business.AdminUpdateShipment(Data);

        foreach (var msg in op.Messages)
            UserSession.AddMessage(msg);

        if (op.Result == ActionResult.Done)
        {
            _Page.Mode = ManagementPageMode.Search;
            EndMode(ManagementPageMode.Edit);
        }
    }
}