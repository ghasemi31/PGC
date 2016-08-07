using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using kFrameWork.Business;
using pgc.Business.General;
using pgc.Model;

public partial class Pages_Admin_OnlineOrder_Default : BasePage
{
    OnlineOrderBusiness Business = new OnlineOrderBusiness();
    public List<Branch> BranchList = new List<Branch>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            UpdateElements();
    }

    private void UpdateElements()
    {
        bool isAllow = OptionBusiness.GetBoolean(pgc.Model.Enums.OptionKey.AllowOnlineOrdering);
        rdbList.SelectedValue = isAllow.ToString().ToLower();
        txtMsg.Text = OptionBusiness.GetLargeText(pgc.Model.Enums.OptionKey.MessageForOnlineOrderIsSuspended);

        BranchList = Business.GetAllBranches().Where(f => f.IsActive).ToList();
        lstBranch.DataSource = BranchList;
        lstBranch.DataBind();

        int branchNumber = 0;
        foreach (var item in BranchList)
        {
            (lstBranch.Items[branchNumber].FindControl("timeFrom") as UserControl_TimePicker).SelectedTime = item.AllowOnlineOrderTimeFrom;
            (lstBranch.Items[branchNumber].FindControl("timeTo") as UserControl_TimePicker).SelectedTime = item.AllowOnlineOrderTimeTo;
            (lstBranch.Items[branchNumber].FindControl("chbAllow") as CheckBox).Checked = item.AllowOnlineOrder;
            (lstBranch.Items[branchNumber].FindControl("chbAllow") as CheckBox).Text = item.Title;
            branchNumber++;
        }

        var ProductList = new pgc.Business.ProductBusiness().GetAllProduct().ToList();
        chbProductList.DataSource = ProductList;
        chbProductList.DataTextField = "Title";
        chbProductList.DataValueField = "ID";
        chbProductList.DataBind();

        for (int i = 0; i < chbProductList.Items.Count; i++)
            chbProductList.Items[i].Selected = ProductList[i].AllowOnlineOrder;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Dictionary<long, bool> ProductList = new Dictionary<long, bool>();

        foreach (ListItem item in chbProductList.Items)
            ProductList.Add(long.Parse(item.Value), item.Selected);

        BranchList = Business.GetAllBranches().Where(f => f.IsActive).ToList();

        if (bool.Parse(rdbList.SelectedValue))
            for (int i = 0; i < BranchList.Count; i++)
            {
                BranchList.ToList()[i].AllowOnlineOrderTimeFrom = (lstBranch.Items[i].FindControl("timeFrom") as UserControl_TimePicker).SelectedTime;
                BranchList.ToList()[i].AllowOnlineOrderTimeTo = (lstBranch.Items[i].FindControl("timeTo") as UserControl_TimePicker).SelectedTime;
                BranchList.ToList()[i].AllowOnlineOrder = (lstBranch.Items[i].FindControl("chbAllow") as CheckBox).Checked;
            }

        UserSession.AddMessage(Business.SaveChanges(ProductList, BranchList, bool.Parse(rdbList.SelectedValue), txtMsg.Text).Messages);

        UpdateElements();
    }
}