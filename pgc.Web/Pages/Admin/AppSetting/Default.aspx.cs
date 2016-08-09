using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using kFrameWork.Business;
using kFrameWork.Util;
using pgc.Model;
using pgc.Model.Enums;
using kFrameWork.Model;
using pgc.Business.General;

public partial class Pages_Admin_Option_Default : BasePage
{
    AppBusiness Business = new AppBusiness();
    public AppSetting sett;
    protected void Page_Load(object sender, EventArgs e)
    {
        sett = Business.Retrive();
        if (!this.IsPostBack)
        {
            BindValues();
        }
    }

    private void BindValues()
    {


        //if (sett != null)
        //{
        //    fupNavImage.FilePath = sett.NavHeaderImage;
        //    fupBranchImg.FilePath = sett.BranchAgreement_Image;
        //    txtBranchContent.Text = sett.BranchAgreement_Content;
        //    txtpgciziLatLng.Text = sett.pgcizi_LatLng;
        //    txtQualityCharter.Text = sett.QualityCharter;
        //    txtNewsCnt.SetNumber(sett.NewsCount);
        //}
    }


    protected void OnSave(object sender, EventArgs e)
    {
        //sett.NavHeaderImage = fupNavImage.FilePath;
        //sett.QualityCharter = txtQualityCharter.Text;
        //sett.NewsCount = txtNewsCnt.GetNumber<int>();
        //sett.pgcizi_LatLng = txtpgciziLatLng.Text;
        //sett.BranchAgreement_Content = txtBranchContent.Text;
        //sett.BranchAgreement_Image = fupBranchImg.FilePath;
        //OperationResult Res = Business.Save();
        //UserSession.AddMessage(Res.Messages);

    }



}