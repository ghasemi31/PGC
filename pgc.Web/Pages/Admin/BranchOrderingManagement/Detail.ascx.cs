using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Patterns;
using pgc.Business;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;

public partial class Pages_Admin_BranchOrderingManagement_Detail : BaseDetailControl<Branch>
{
    public Branch Branch = new Branch();
    public BranchOrderingManagementBusiness business = new BranchOrderingManagementBusiness();
    public BaseManagementPage<BranchOrderingManagementBusiness, Branch, BranchOrderingManagementPattern, pgcEntities> _Page = new BaseManagementPage<BranchOrderingManagementBusiness, Branch, BranchOrderingManagementPattern, pgcEntities>();
    ViewStateCollection<BranchOrderTitleSelect> BranchOrderTitleSelectList;

    public Pages_Admin_BranchOrderingManagement_Detail()
    {
        BranchOrderTitleSelectList = new ViewStateCollection<BranchOrderTitleSelect>(this.ViewState, "BranchOrderTitleSelectList");
    }

    public void Page_Load()
    {
        _Page= this.Page as BaseManagementPage<BranchOrderingManagementBusiness, Branch, BranchOrderingManagementPattern, pgcEntities>;

        if (_Page.SelectedID > 0)
            Branch = _Page.Business.Retrieve(_Page.SelectedID);
    }

    public override Branch GetEntity(Branch Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Branch();

        Data.Branch_BranchOrderTitle.Clear();

        foreach (var item in BranchOrderTitleSelectList.List)
        {
            Branch_BranchOrderTitle temp=new Branch_BranchOrderTitle();
            temp.MinimumQuantity = item.Quantity;
            temp.BranchOrderTitle_ID = item.TitleID;
            temp.Branch_ID = _Page.SelectedID;

            Data.Branch_BranchOrderTitle.Add(temp);
        }

        return Data;
    }

    public override bool Validate(ManagementPageMode Mode)
    {
        return true;
    }

    public override void EndMode(ManagementPageMode Mode)
    {
        base.EndMode(Mode);
        BranchOrderTitleSelectList.Clear();
        btnRevise.Visible = false;
        btnSave.Visible = false;
        btnPreview.Visible = true;
    }

    public override void SetEntity(Branch Data, ManagementPageMode Mode)
    {
        Branch = Data;

        foreach (var item in Branch.Branch_BranchOrderTitle)
            BranchOrderTitleSelectList.AddItem(new BranchOrderTitleSelect() { Quantity = item.MinimumQuantity, TitleID = item.BranchOrderTitle_ID });

        CreateTable();
    }

    private void CreateTable()
    {
        for (int i = containerTBL.Controls.Count-1; 0 <= i; i--)
            containerTBL.Controls.RemoveAt(i);


        HtmlGenericControl ul = new HtmlGenericControl("ul");
        ul.Attributes.Add("class", "dltabs");

        bool isFirstLi=true;
        foreach (var item in business.Context.BranchOrderTitleGroups.Where(f => f.BranchOrderTitles.Count() > 0).OrderBy(f => f.DisplayOrder))
        {
            HtmlGenericControl li = new HtmlGenericControl("li");
            if (isFirstLi)
                li.Attributes.Add("class", "selectli");

            li.InnerText = item.Title;

            ul.Controls.Add(li);
            isFirstLi = false;
        }


        containerTBL.Controls.Add(ul);


        HtmlGenericControl div = new HtmlGenericControl();
        div.Attributes.Add("class", "tabs");

        bool isFirstTbl = true;
        foreach (var item in business.Context.BranchOrderTitleGroups.Where(f => f.BranchOrderTitles.Count() > 0).OrderBy(f => f.DisplayOrder))
        {

            //Header
            HtmlTable table = new HtmlTable();
            HtmlTableRow hrow = new HtmlTableRow();
            hrow.Style.Add("font-weight", "bold");
            hrow.Cells.Add(new HtmlTableCell() { InnerText = "نام کالا" });
            hrow.Cells.Add(new HtmlTableCell() { InnerText = "مبلغ واحد" });
            hrow.Cells.Add(new HtmlTableCell() { InnerText = "حداقل مقدار سفارشی" });

            hrow.Attributes.Add("class", "theader");

            table.Rows.Add(hrow);
            table.Attributes.Add("class", (isFirstTbl) ? "tabBody selectTab" : "tabBody");

            isFirstTbl = false;



            #region Select JQ for All

            //Select All Row
            HtmlTableRow selectAllRow = new HtmlTableRow();

            //Cell 1
            HtmlTableCell checkAllCell = new HtmlTableCell();
            checkAllCell.ColSpan = 2;

            CheckBox selectAllBox = new CheckBox();
            selectAllBox.Attributes.Add("class", "checkAllBox");
            selectAllBox.Text = "انتخاب تمامی کالاها";
            selectAllBox.Style.Add("font-weight", "bold");
            selectAllBox.Style.Add("color", "#9e9e9e");

            checkAllCell.Controls.Add(selectAllBox);

            selectAllRow.Cells.Add(checkAllCell);

            //Cell 2 
            HtmlTableCell InputAllQuantity = new HtmlTableCell();

            TextBox InputQuantityOfAll = new TextBox();
            InputQuantityOfAll.Attributes.Add("class", "inputAllBox");

            InputAllQuantity.Controls.Add(InputQuantityOfAll);

            selectAllRow.Controls.Add(InputAllQuantity);

            table.Rows.Add(selectAllRow);

            #endregion



            //Add each BranchOrderTitle
            foreach (var orderT in item.BranchOrderTitles.OrderBy(f=>f.DisplayOrder))
            {
                HtmlTableRow row = new HtmlTableRow();

                //Cell 1
                HtmlTableCell checkCell = new HtmlTableCell();
                CheckBox box = new CheckBox();
                box.ID = "chk-" + orderT.ID;
                box.Text = orderT.Title;
                
                if (BranchOrderTitleSelectList.List.Any(f => f.TitleID == orderT.ID))
                    box.Checked = true;

                checkCell.Controls.Add(box);
                row.Cells.Add(checkCell);


                //Cell 2
                row.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(orderT.Price) + " ریال" });


                //Cell 3
                HtmlTableCell quantityCell = new HtmlTableCell();
                TextBox textBox = new TextBox()
                {
                    ID = "txtNum-" + orderT.ID.ToString()
                };

                if (BranchOrderTitleSelectList.List.Any(f => f.TitleID == orderT.ID))
                    textBox.Text = BranchOrderTitleSelectList.List.SingleOrDefault(f => f.TitleID == orderT.ID).Quantity.ToString();
                
                quantityCell.Controls.Add(textBox);
                row.Cells.Add(quantityCell);
              
                table.Rows.Add(row);
            }

            div.Controls.Add(table);
        }

        containerTBL.Controls.Add(div);
    }


    public void Preview_Click(object sender, EventArgs e)
    {
        var checkBoxList = Request.Form.AllKeys.Where(f => f.Contains("chk-"));

        bool isValid = true;

        BranchOrderTitleSelectList.Clear();

        foreach (var checkBox in checkBoxList)
        {
            var temp = Request.Form.AllKeys.SingleOrDefault(f => f.EndsWith("txtNum-" + checkBox.Split('-')[1]));
            long quantity = 0;
            if (!long.TryParse(Request.Form[temp], out quantity))
            {
                if (!UserSession.CurrentMessages.Contains(UserMessageKey.BranchOrderManagement_InvalidQuantity))
                    UserSession.AddMessage(UserMessageKey.BranchOrderManagement_InvalidQuantity);
                isValid = false;
            }

            if (quantity < 1)
            {
                if (!UserSession.CurrentMessages.Contains(UserMessageKey.BranchOrderManagement_SelectedHasNoNumber))
                    UserSession.AddMessage(UserMessageKey.BranchOrderManagement_SelectedHasNoNumber);
                isValid = false;
            }

            BranchOrderTitleSelectList.AddItem(new BranchOrderTitleSelect()
            {
                Quantity = quantity,
                TitleID = long.Parse(checkBox.Split('-')[1])
            });
        }


        if (!isValid)
        {
            CreateTable();
            return;
        }


        #region Preview Table

        //Branch = business.Retrieve(_Page.SelectedID);

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
            hrow.Cells.Add(new HtmlTableCell() { InnerText = "مبلغ واحد" });
            hrow.Cells.Add(new HtmlTableCell() { InnerText = "حداقل مقدار سفارشی" });

            hrow.Attributes.Add("class", "theader");

            table.Rows.Add(hrow);
            table.Attributes.Add("class", (isFirstTbl) ? "tabBody selectTab" : "tabBody");

            isFirstTbl = false;

            int rowNumber = 1;
            //Add each BranchOrderTitle
            foreach (var orderT in BranchOrderTitleSelectList.List)
            {
                HtmlTableRow row = new HtmlTableRow();

                BranchOrderTitle orderTitle = _Page.Business.RetrieveBranchOrderTitle(orderT.TitleID);

                //Cell 0
                row.Cells.Add(new HtmlTableCell() { InnerText = (rowNumber++).ToString()+"-" });

                //Cell 1
                HtmlTableCell checkCell = new HtmlTableCell();
                Label lbl = new Label();

                lbl.Text = orderTitle.Title;

                checkCell.Controls.Add(lbl);
                row.Cells.Add(checkCell);


                //Cell 1
                HtmlTableCell groupCell = new HtmlTableCell();
                Label lblGroup = new Label();

                lblGroup.Text = orderTitle.BranchOrderTitleGroup.Title;

                groupCell.Controls.Add(lblGroup);
                row.Cells.Add(groupCell);


                //Cell 2
                row.Cells.Add(new HtmlTableCell() { InnerText = UIUtil.GetCommaSeparatedOf(orderTitle.Price) + " ریال" });


                //Cell 3
                HtmlTableCell quantityCell = new HtmlTableCell();
                Label lblQuantity = new Label()
                {
                    Text = orderT.Quantity + " عدد"
                };

                quantityCell.Controls.Add(lblQuantity);
                row.Cells.Add(quantityCell);

                table.Rows.Add(row);
            }

            containerTBL.Controls.Add(table);
        }
        else
        {
            HtmlGenericControl table = new HtmlGenericControl();
            table.InnerText = "هیچ کالایی برای سفارش شعبه انتخاب نشده است";

            containerTBL.Controls.Add(table);
        }
        #endregion



        //State Changing
        btnRevise.Visible = true;
        btnSave.Visible = true;
        btnPreview.Visible = false;
    }

    public void CanclePreview_Click(object sender, EventArgs e)
    {
        CreateTable();

        //State Changing
        btnRevise.Visible = false;
        btnSave.Visible = false;
        btnPreview.Visible = true;
    }


    [Serializable]
    public class BranchOrderTitleSelect
    {
        public long TitleID { get; set; }
        public long Quantity { get; set; }
    }
}