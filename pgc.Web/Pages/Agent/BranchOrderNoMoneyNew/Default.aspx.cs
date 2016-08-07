using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Model;
using pgc.Business.Core;
using pgc.Model.Enums;
using kFrameWork.Util;
using pgc.Business;
using System.Web.UI.HtmlControls;
using kFrameWork.Business;

public partial class Pages_Agent_BranchOrderNoMoneyNew_Default : BasePage
{
    ViewStateCollection<BranchOrderTitleSelect> BranchOrderTitleSelectList;
    public BranchOrder branchOrder = new BranchOrder();

    public Pages_Agent_BranchOrderNoMoneyNew_Default()
    {
        BranchOrderTitleSelectList = new ViewStateCollection<BranchOrderTitleSelect>(this.ViewState, "BranchOrderTitleSelectList");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!UserSession.User.Branch_ID.HasValue)
            Response.Redirect(GetRouteUrl("agent-default", null));

        Branch branch = new BranchBusiness().Retrieve(UserSession.User.Branch_ID.Value);

        //If Branch Dont Allow Order Any OrderTitle
        if (branch.Branch_BranchOrderTitle.Count() == 0)
        {
            UserSession.AddMessage(UserMessageKey.BranchOrderNew_DontAllowAnyOrderTitle);
            btnCancle_Click(sender, e);
        }


        string TomorrowDate = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(1));

        branchOrder = branch.BranchOrders.SingleOrDefault(f => f.OrderedPersianDate == TomorrowDate);



        if (IsPostBack)
            UIUtil.AddStartupScript("SetAll()", this.Page);
        else
        {
            if (branchOrder != null)
            {
                foreach (var item in branchOrder.BranchOrderDetails)
                    BranchOrderTitleSelectList.AddItem(new BranchOrderTitleSelect() { Quantity = item.Quantity, TitleID = item.BranchOrderTitle_ID.Value });
                
                txtDescription.Text = branchOrder.BranchDescription;
            }


            //Out Of Proper Time For Ordering, Only SHow Preview Of Tommorow Order
            string TimeForAcceptStart = OptionBusiness.GetText(OptionKey.BranchOrderNew_AcceptFrom);
            string TimeForAcceptEnd = OptionBusiness.GetText(OptionKey.BranchOrderNew_AcceptTo);
            string CurrentTime = DateTime.Now.TimeOfDay.ToString().Substring(0, 5);

            
            if (CurrentTime.CompareTo(TimeForAcceptEnd) > 0 || CurrentTime.CompareTo(TimeForAcceptStart) < 0)
            {
                closeDesc.Visible = true;
                CreatePreviewTable();
                btnPreview.Visible = false;
            }
            else
            {
                //if (this.HasValidQueryString<long>(QueryStringKeys.id))
                //{
                //    CreateTable();
                //    //btnRevise.Visible = (branchOrder.Status == (int)BranchOrderStatus.Pending);
                //}
                //else 
                    if (branchOrder != null)
                {
                    CreatePreviewTable();
                    btnPreview.Visible = false;
                    btnRevise.Visible = (branchOrder.Status == (int)BranchOrderStatus.Pending);                    
                }
                else
                    CreateTable();
            }

            notPendingDesc.Visible = (branchOrder!=null && branchOrder.Status != (int)BranchOrderStatus.Pending);
        }
    }

    private void CreateTable()
    {

        Branch branch = new BranchBusiness().Retrieve(UserSession.User.Branch_ID.Value);
        var AvailebleOrderTitles = branch.Branch_BranchOrderTitle;



        for (int i = detailList.Controls.Count - 1; 0 < i; i--)
            detailList.Controls.RemoveAt(i);

        #region UL Tabs

        HtmlGenericControl ul = new HtmlGenericControl("ul");
        ul.Attributes.Add("class", "dltabs");

        bool isFirstLi = true;
        foreach (var item in AvailebleOrderTitles.Select(f => f.BranchOrderTitle.BranchOrderTitleGroup).Distinct().OrderBy(g => g.DisplayOrder))
        {
            HtmlGenericControl li = new HtmlGenericControl("li");
            if (isFirstLi)
                li.Attributes.Add("class", "selectli");

            li.InnerText = item.Title;

            ul.Controls.Add(li);
            isFirstLi = false;
        }


        detailList.Controls.Add(ul);
        #endregion


        HtmlGenericControl div = new HtmlGenericControl();
        div.Attributes.Add("class", "tabs");

        bool isFirstTbl = true;
        foreach (var orderTitleGroup in AvailebleOrderTitles.Select(f => f.BranchOrderTitle.BranchOrderTitleGroup).Distinct().OrderBy(g => g.DisplayOrder))
        {

            //Header
            HtmlTable table = new HtmlTable();
            HtmlTableRow hrow = new HtmlTableRow();
            hrow.Style.Add("font-weight", "bold");
            hrow.Cells.Add(new HtmlTableCell() { InnerText = "نام کالا" });
            //hrow.Cells.Add(new HtmlTableCell() { InnerText = "مبلغ واحد" });
            hrow.Cells.Add(new HtmlTableCell() { InnerText = "حداقل مقدار سفارشی" });
            hrow.Cells.Add(new HtmlTableCell() { InnerText = "تعداد" });
            //hrow.Cells.Add(new HtmlTableCell() { InnerText = "مبلغ کل" });

            hrow.Attributes.Add("class", "theader");

            table.Rows.Add(hrow);
            table.Attributes.Add("class", (isFirstTbl) ? "tabBody selectTab" : "tabBody");

            isFirstTbl = false;


            //Add each BranchOrderTitle
            foreach (var orderT in AvailebleOrderTitles.Where(f => f.BranchOrderTitle.BranchOrderTitleGroup_ID == orderTitleGroup.ID).OrderBy(f => f.BranchOrderTitle.DisplayOrder))
            {
                HtmlTableRow row = new HtmlTableRow();

                //Cell 1
                HtmlTableCell checkCell = new HtmlTableCell();
                CheckBox box = new CheckBox();
                box.ID = "chk-" + orderT.BranchOrderTitle_ID;
                box.Text = orderT.BranchOrderTitle.Title;

                if (BranchOrderTitleSelectList.List.Any(f => f.TitleID == orderT.BranchOrderTitle_ID))
                    box.Checked = true;

                checkCell.Controls.Add(box);
                row.Cells.Add(checkCell);


                //Cell 2
                //row.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(orderT.BranchOrderTitle.Price) + " ریال" });


                //Cell 3
                row.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(orderT.MinimumQuantity) + " عدد" });


                //Cell 4
                HtmlTableCell quantityCell = new HtmlTableCell();
                TextBox textBox = new TextBox()
                {
                    ID = "txtNum-" + orderT.BranchOrderTitle_ID.ToString()
                };

                if (BranchOrderTitleSelectList.List.Any(f => f.TitleID == orderT.BranchOrderTitle_ID))
                    textBox.Text = BranchOrderTitleSelectList.List.SingleOrDefault(f => f.TitleID == orderT.BranchOrderTitle_ID).Quantity.ToString();

                quantityCell.Controls.Add(textBox);
                row.Cells.Add(quantityCell);


                //Cell 5
                //HtmlTableCell totalCell = new HtmlTableCell();
                //totalCell.Attributes.Add("class", "totalspan");
                //totalCell.Controls.Add(new Label() { ID = "lbl-" + orderT.BranchOrderTitle_ID.ToString() });
                //totalCell.Controls.Add(new Label() { Text = " ریال" });
                //row.Cells.Add(totalCell);
                //table.Rows.Add(row);



                table.Rows.Add(row);
            }

            div.Controls.Add(table);
        }

        detailList.Controls.Add(div);
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        BranchOrderBusiness Business = new BranchOrderBusiness();

        Branch branch = new BranchBusiness().Retrieve(UserSession.User.Branch_ID.Value);
        string TomorrowDate = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(1));
        var TommorowOrder=branch.BranchOrders.SingleOrDefault(f => f.OrderedPersianDate == TomorrowDate && (f.Status == (int)BranchOrderStatus.Pending || f.Status == (int)BranchOrderStatus.Confirmed));

        BranchOrder branchOrder;

        if (TommorowOrder != null)
            branchOrder = Business.Retrieve(TommorowOrder.ID);
        else
        {
            branchOrder = new BranchOrder();
            branchOrder.AdminDescription = "";
            branchOrder.Branch_ID = UserSession.User.Branch_ID.Value;            
            branchOrder.OrderedPersianDate = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(1));
            branchOrder.RegDate = DateTime.Now;
            branchOrder.RegPersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
            branchOrder.Status = (int)BranchOrderStatus.Pending;            
        }
        
        branchOrder.BranchDescription = txtDescription.Text;

        OperationResult opSuc = Business.AgentUpdateBeforeInsert(branchOrder);



        foreach (var item in BranchOrderTitleSelectList.List)
        {
            BranchOrderTitle orderTitle = Business.RetrieveOrderTitle(item.TitleID);
            branchOrder.BranchOrderDetails.Add(new BranchOrderDetail()
            {
                BranchOrderTitle_ID = orderTitle.ID,
                BranchOrderTitle_Title = orderTitle.Title,
                Quantity = item.Quantity,
                SinglePrice = orderTitle.Price,
                TotalPrice = item.Quantity * orderTitle.Price
            });
        }

        branchOrder.TotalPrice = branchOrder.BranchOrderDetails.Sum(f => f.TotalPrice);

        opSuc = Business.AgentInsert(branchOrder);

        foreach (var item in opSuc.Messages)
            UserSession.AddMessage(item);

        if (opSuc.Result == ActionResult.Done)
            Response.Redirect(GetRouteUrl("agent-branchordernomoney", null));
    }

    protected void btnCancle_Click(object sender, EventArgs e)
    {
        BranchOrderTitleSelectList.Clear();
        Response.Redirect(GetRouteUrl("agent-branchordernomoney", null));
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        var checkBoxList = Request.Form.AllKeys.Where(f => f.Contains("chk-"));

        bool isValid = true;

        BranchOrderTitleSelectList.Clear();

        Branch branch = new BranchBusiness().Retrieve(UserSession.User.Branch_ID.Value);
        var AvailebleOrderTitles = branch.Branch_BranchOrderTitle;


        //Validation Of Credit, Minimum QUantity, Select & Have Quantity
        #region Validating

        if (checkBoxList.Count() == 0)
        {
            if (!UserSession.CurrentMessages.Contains(UserMessageKey.BranchOrderNew_NoTitleSelected))
                UserSession.AddMessage(pgc.Model.Enums.UserMessageKey.BranchOrderNew_NoTitleSelected);
            isValid = false;
        }

        foreach (var checkBox in checkBoxList)
        {
            var temp = Request.Form.AllKeys.SingleOrDefault(f => f.EndsWith("txtNum-" + checkBox.Split('-')[1]));
            long quantity = 0;
            long OrderTitle_ID = long.Parse(checkBox.Split('-')[1]);

            if (!long.TryParse(Request.Form[temp], out quantity))
            {
                if (!UserSession.CurrentMessages.Contains(UserMessageKey.BranchOrderNew_NoQuantitySelected))
                    UserSession.AddMessage(pgc.Model.Enums.UserMessageKey.BranchOrderNew_NoQuantitySelected);
                isValid = false;
            }

            if (quantity < 1)
            {
                if (!UserSession.CurrentMessages.Contains(UserMessageKey.BranchOrderNew_NoQuantitySelected))
                    UserSession.AddMessage(pgc.Model.Enums.UserMessageKey.BranchOrderNew_NoQuantitySelected);
                isValid = false;
            }

            if (AvailebleOrderTitles.Single(f => f.BranchOrderTitle_ID == OrderTitle_ID).MinimumQuantity > quantity)
            {
                if (!UserSession.CurrentMessages.Contains(UserMessageKey.BranchOrderNew_InsufficientQuantitySelected))
                    UserSession.AddMessage(pgc.Model.Enums.UserMessageKey.BranchOrderNew_InsufficientQuantitySelected);
                isValid = false;
            }

            BranchOrderTitleSelectList.AddItem(new BranchOrderTitleSelect()
            {
                Quantity = quantity,
                TitleID = OrderTitle_ID
            });
        }

        long TotalPrice = 0;
        foreach (var item in BranchOrderTitleSelectList.List)
            TotalPrice += (item.Quantity * new BranchOrderTitleBusiness().Retrieve(item.TitleID).Price);

        OperationResult op = new BranchCreditBusiness().CheckCreditOfBranch(UserSession.User.Branch_ID.Value, TotalPrice);

        if (op.Result != ActionResult.Done)
        {
            foreach (var msg in op.Messages)
                UserSession.AddMessage(msg);

            UserSession.AddData("مقدار اعتبار شعبه:", UIUtil.GetCommaSeparatedOf(BranchCreditBusiness.GetBranchCredit(UserSession.User.Branch_ID.Value)) + " ریال");
            UserSession.AddData("مبلغ درخواست:", UIUtil.GetCommaSeparatedOf(TotalPrice) + " ریال");
            UserSession.AddData("حداقل سقف اعتبار:", UIUtil.GetCommaSeparatedOf(branch.MinimumCredit) + " ریال");
           
            isValid = false;
        }

        if (!isValid)
        {
            CreateTable();
            return;
        }
        #endregion



        CreatePreviewTable();


        //State Changing
        btnAdd.Visible = true;
        btnRevise.Visible = true;
        btnPreview.Visible = false;
    }

    private void CreatePreviewTable()
    {
        Branch branch = new BranchBusiness().Retrieve(UserSession.User.Branch_ID.Value);
        var AvailebleOrderTitles = branch.Branch_BranchOrderTitle;


        #region Preview Table

        if (BranchOrderTitleSelectList.Count > 0)
        {
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
            foreach (var orderT in AvailebleOrderTitles.OrderBy(f => f.BranchOrderTitle.BranchOrderTitleGroup.DisplayOrder).ThenBy(g => g.BranchOrderTitle.DisplayOrder))
            {

                if (BranchOrderTitleSelectList.List.All(f => f.TitleID != orderT.BranchOrderTitle_ID))
                    continue;


                HtmlTableRow row = new HtmlTableRow();


                //Cell 1 RowNumber
                row.Cells.Add(new HtmlTableCell() { InnerText = (rowNumber++).ToString() + "-" });


                //Cell 2 Title
                row.Cells.Add(new HtmlTableCell() { InnerText = orderT.BranchOrderTitle.Title });


                //Cell 3 Group Title
                row.Cells.Add(new HtmlTableCell() { InnerText = orderT.BranchOrderTitle.BranchOrderTitleGroup.Title });


                //Cell 4 Single Price
                //row.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(orderT.BranchOrderTitle.Price) + " ریال" });


                //Cell 5 OrderedQuantity
                long OrderedQuantity = BranchOrderTitleSelectList.List.SingleOrDefault(f => f.TitleID == orderT.BranchOrderTitle_ID).Quantity;
                row.Cells.Add(new HtmlTableCell() { InnerText = OrderedQuantity + " عدد" });

                //Cell 6 TotalPrice
                //row.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(orderT.BranchOrderTitle.Price * OrderedQuantity) + " ریال" });


                table.Rows.Add(row);
            }



            //#region Footer

            //long TotalPrice = 0;
            //foreach (var item in BranchOrderTitleSelectList.List)
            //    TotalPrice += (item.Quantity * new BranchOrderTitleBusiness().Retrieve(item.TitleID).Price);


            //HtmlTableRow fRow = new HtmlTableRow();
            //fRow.Attributes.Add("class", "footerrow");
            //fRow.Cells.Add(new HtmlTableCell() { InnerText = "مجموع کل :", ColSpan = 5 });
            //fRow.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(TotalPrice) + "ریال" });
            //table.Rows.Add(fRow);

            //#endregion


            detailList.Controls.Add(table);
        }
        else
        {
            HtmlGenericControl table = new HtmlGenericControl();
            table.InnerText = "هیچ کالایی برای سفارش شعبه انتخاب نشده است";

            detailList.Controls.Add(table);
        }
        #endregion

    }
  
    protected void btnRevise_Click(object sender, EventArgs e)
    {
        btnAdd.Visible = false;
        btnRevise.Visible = false;
        btnPreview.Visible = true;

        CreateTable();
    }



    [Serializable]
    public class BranchOrderTitleSelect
    {
        public long TitleID { get; set; }
        public long Quantity { get; set; }
    }
}