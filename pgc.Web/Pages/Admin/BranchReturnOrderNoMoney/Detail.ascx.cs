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

public partial class Pages_Admin_BranchReturnOrderNoMoney_Detail : BaseDetailControl<BranchReturnOrder>
{
    public BranchReturnOrder ReturnOrder = new BranchReturnOrder();
    BaseManagementPage<BranchReturnOrderBusiness, BranchReturnOrder, BranchReturnOrderPattern, pgcEntities> _Page = new BaseManagementPage<BranchReturnOrderBusiness, BranchReturnOrder, BranchReturnOrderPattern, pgcEntities>();

    public void Page_Load()
    {
        _Page = this.Page as BaseManagementPage<BranchReturnOrderBusiness, BranchReturnOrder, BranchReturnOrderPattern, pgcEntities>;
    }

    public override BranchReturnOrder GetEntity(BranchReturnOrder Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new BranchReturnOrder();

        Data.AdminDescription = txtAdminDesc.Text;
        Data.BranchDescription = txtBranchDesc.Text;

        return Data;
    }

    public override bool Validate(ManagementPageMode Mode)
    {
        return true;
        //BranchReturnOrder order = _Page.Business.Retrieve(_Page.SelectedID);

        //if (string.IsNullOrEmpty(orderTitleList.Value))
        //{
        //    UserSession.AddMessage(UserMessageKey.BranchReturnOrderNew_NoTitleSelected);
        //    InitTable();
        //    return false;
        //}

        ////OperationResult op = _Page.Business.UpdateBranchReturnOrderDetailsValidates(order, Request.Form.AllKeys, orderTitleList.Value);

        ////if (op.Result != ActionResult.Done)
        ////{
        ////    foreach (var item in op.Messages)
        ////        UserSession.AddMessage(item);
        ////    InitTable();
        ////    return false;
        ////}

        //return base.Validate(Mode);
    }
  
    public override void SetEntity(BranchReturnOrder Data, ManagementPageMode Mode)
    {
        ReturnOrder = Data;

        txtAdminDesc.Text = Data.AdminDescription;
        txtBranchDesc.Text = Data.BranchDescription;

        CreatePreviewTable();
    }

    public override void EndMode(ManagementPageMode Mode)
    {
        base.EndMode(Mode);
        var _Page = this.Page as BaseManagementPage<BranchReturnOrderBusiness, BranchReturnOrder, BranchReturnOrderPattern, pgcEntities>;
        _Page.ListControl.Grid.DataBind();
    }


    private void CreatePreviewTable()
    {
        var ReturnOrderDetails = ReturnOrder.BranchReturnOrderDetails;


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
        hrow.Cells.Add(new HtmlTableCell() { InnerText = "تعداد مرجوعی" });
        //hrow.Cells.Add(new HtmlTableCell() { InnerText = "مبلغ کل" });

        hrow.Attributes.Add("class", "theader");

        table.Rows.Add(hrow);
        table.Attributes.Add("class", (isFirstTbl) ? "tabBody selectTab" : "tabBody");

        isFirstTbl = false;

        int rowNumber = 1;
        //Add each BranchOrderTitle
        foreach (var orderT in ReturnOrderDetails.OrderBy(f => f.BranchOrderTitle.BranchOrderTitleGroup.DisplayOrder).ThenBy(g => g.BranchOrderTitle.DisplayOrder))
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
        //fRow.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(ReturnOrder.TotalPrice) + "ریال" });
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

        if (op.Result==ActionResult.Done)
        {
            _Page.ListControl.Grid.DataBind();
            _Page.DetailControl.EndMode(ManagementPageMode.Edit);
            _Page.Mode=ManagementPageMode.Search;
        }
    }
    
    protected void GoCorrection_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("admin-branchreturnordernomoneyedit", null) + "?id=" + _Page.SelectedID);
    }  
}