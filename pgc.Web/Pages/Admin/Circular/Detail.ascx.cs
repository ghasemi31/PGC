using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using kFrameWork.Util;
using pgc.Model.Patterns;
using pgc.Business;
using pgc.Model.Enums;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using System.Linq;


public partial class Pages_Admin_Circular_Detail : BaseDetailControl<Circular>
{
    private BranchBusiness business = new BranchBusiness();
    public override Circular GetEntity(Circular Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Circular();

        Data.Title = txtTitle.Text;
        Data.Body = txtBody.Text;
        Data.IsVisible = Convert.ToBoolean(chkIsVisible.Checked);
        Data.IsActiveForUser = Convert.ToBoolean(chkIsActiveForUser.Checked);
        Data.Date = DateTime.Now;
        Data.PersiabDate = kFrameWork.Util.DateUtil.GetPersianDateShortString(Data.Date);
        if (Mode == ManagementPageMode.Edit)
        {
            Data.Branches.Clear();
        }
        CircularBusiness pro_business = (this.Page as BaseManagementPage<CircularBusiness,Circular, CircularPattern, pgcEntities>).Business;
        foreach (ListItem item in chlBranch.Items)
        {
            if (item.Selected)
                Data.Branches.Add(pro_business.retriveBranch(ConvertorUtil.ToInt64(item.Value)));
        }
        return Data;
    }

    public override void SetEntity(Circular Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtBody.Text = Data.Body;
        chkIsVisible.Checked = Data.IsVisible;
        chkIsActiveForUser.Checked = Data.IsActiveForUser;
        bindBranch();
        var values = Data.Branches.Select(m => m.ID).ToList();
        foreach (ListItem item in chlBranch.Items)
        {
            item.Selected = values.Contains(ConvertorUtil.ToInt64(item.Value));
        }

    }
    public override void BeginMode(ManagementPageMode Mode)
    {
        base.BeginMode(Mode);
        if (Mode == ManagementPageMode.Add)
        {
            bindBranch();
        }
    }

    private void bindBranch()
    {
        List<Branch> branches = business.GetAllBranch();
        chlBranch.DataSource = branches;
        chlBranch.DataTextField = "Title";
        chlBranch.DataValueField = "ID";
        chlBranch.DataBind();
    }


}