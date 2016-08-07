using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;
using pgc.Model.Patterns;
using pgc.Business;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
using kFrameWork.Util;

public partial class Pages_Admin_AccessLevel_Detail : BaseDetailControl<AccessLevel>
{
    public override AccessLevel GetEntity(AccessLevel Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new AccessLevel();

        Data.Title = txtTitle.Text;
        Data.Role = lkcRole.GetSelectedValue<int>();


        AccessLevelBusiness business = (this.Page as BaseManagementPage<AccessLevelBusiness, AccessLevel, AccessLevelPattern, pgcEntities>).Business;
        if (Mode == ManagementPageMode.Add)
        {
            foreach (ListItem item in chlPermissions.Items)
            {
                if (item.Selected)
                    Data.Features.Add(business.RetriveFeature(ConvertorUtil.ToInt64(item.Value)));
            }
        }
        else if (Mode == ManagementPageMode.Edit)
        {
            Data.Features.Clear();
            foreach (ListItem item in chlPermissions.Items)
            {
                if (item.Selected)
                    Data.Features.Add(business.RetriveFeature(ConvertorUtil.ToInt64(item.Value)));
            }
        }

        return Data;
    }

    public override void SetEntity(AccessLevel Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        lkcRole.SetSelectedValue(Data.Role);
    }

    protected void Role_Changed(object sender, EventArgs e)
    {
        AccessLevelBusiness business = (this.Page as BaseManagementPage<AccessLevelBusiness, AccessLevel, AccessLevelPattern, pgcEntities>).Business;
        Role role = lkcRole.GetSelectedValue<Role>();
        List<Feature> features = business.GetFeaturesForRole(role);
        var datasource = from x in features
                         select new
                         {
                             x.ID,
                             DisplayField = (x.IsNew == true) ? String.Format("{0} {1}", x.Title, "<span style='color:red;font-size: 8px;'> (جدید)</span>") : x.Title
                         };

        chlPermissions.DataSource = datasource;
        chlPermissions.DataTextField = "DisplayField";
        chlPermissions.DataValueField = "ID";
        chlPermissions.DataBind();

        //set data

        long selectedID = (this.Page as BaseManagementPage<AccessLevelBusiness, AccessLevel, AccessLevelPattern, pgcEntities>).SelectedID;
        if (selectedID > 0)
        {
            List<Feature> currentFieatures = business.GetCurrentFeatures(selectedID);
            foreach (ListItem item in chlPermissions.Items)
            {
                long value = ConvertorUtil.ToInt64(item.Value);
                item.Selected = (currentFieatures.Count(f => f.ID == value) > 0);
            }
        }
    }

    public override void BeginMode(ManagementPageMode Mode)
    {
        base.BeginMode(Mode);
        Role_Changed(null, null);
    }
}