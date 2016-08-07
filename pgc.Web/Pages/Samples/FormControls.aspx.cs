using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pgc.Model.Enums;
using pgc.Model;
using kFrameWork.UI;
using pgc.Model.Patterns;

public partial class Pages_Samples_FormControls :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Province_Changed(object sender, EventArgs e)
    {


    }

    #region Sample Block A

    protected void LookupCombo_Selecting(object sender, List<Type> ParamTypes, List<object> ParamValues)
    {
        ParamTypes.Add(typeof(bool));
        ParamValues.Add(chkBool.Checked);
        //e.InputParameters["TestBool"] = chkBool.Checked;
    }
    protected void btnRebind_Click(object sender, EventArgs e)
    {
        lkcDynamicParam.DataBind();
    }

    #endregion

    #region Sample Block B

    protected void btnSampleGet_Click(object sender, EventArgs e)
    {
        EnumSample enumValue = LookupCombo9.GetSelectedValue<EnumSample>();

        lblEnum.Text = enumValue.ToString();

        //if you want to find out whether Enum is assigned or leaved with "فرقی نمی کند"
        if (!BasePattern.IsEnumAssigned(enumValue))
            lblEnum.Text = "Not Assigned!";
    }

    #endregion

    #region Sample Block C

    protected void btnSampleGetNum_Click(object sender, EventArgs e)
    {
        lblEnumNum.Text = LookupCombo10.GetSelectedValue<Int32>().ToString();
    }

    #endregion

    #region Sample Block D

    protected void btnSet_Click(object sender, EventArgs e)
    {
        LookupCombo11.SetSelectedValue(3);
    }

    #endregion

    #region Sample Block E

    protected void btnSetEnum_Click(object sender, EventArgs e)
    {
        LookupCombo12.SetSelectedValue(EnumSample.User);
    }

    #endregion

    #region Sample Block G

    protected void SetFup1(object sender, EventArgs e)
    {
        fup1.FilePath = txtFup1.Text;
    }

    protected void GetFup1(object sender, EventArgs e)
    {
        lblFup1.Text = fup1.FilePath;
    }

    #endregion

    #region Sample Block H

    protected void SetFup2(object sender, EventArgs e)
    {
        fup2.FilePath = txtFup2.Text;
    }

    protected void GetFup2(object sender, EventArgs e)
    {
        lblFup2.Text = fup2.FilePath;
    }

    #endregion


    protected void obdSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["Pattern"] = new SamplePattern();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        UserSession.AddMessage(UserMessageKey.Succeed);
        UserSession.AddMessage(UserMessageKey.ErrorSample);
        //UserSession.AddMessage(UserMessageKey.WarningSample);
        //UserSession.AddMessage(UserMessageKey.InfoSample);
        UserSession.AddData("myname", "kamran");
        UserSession.AddData("تعداد ", "kamran");
    }
}