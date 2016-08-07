using System;
using System.Linq;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using kFrameWork.Util;
using pgc.Model.Patterns;
using pgc.Business;
using System.Web.UI;
using pgc.Business.Lookup;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Advertisement_Detail : BaseDetailControl<Advertisement>
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.btnSave);
    }

    public override Advertisement GetEntity(Advertisement Data, ManagementPageMode Mode)
    {
        string SavePath = @"~/UserFiles/adv/";

        if (Data == null)
            Data = new Advertisement();



        if (ddrAdvType.SelectedValue == "1")
        {
            /*****************WithOut IOUtil*************/

            //SavePath += fupImage.FileName;
            //SavePath = Server.MapPath(SavePath);
            //fupImage.SaveAs(SavePath);
            //Data.FileAddress = SavePath;

            /*****************WithOut IOUtil*************/

          //  Data.FileAddress = IOUtil.SaveFile(fupImage.PostedFile, SavePath);
            Data.AltText = txtAlt.Text;
            Data.Height = txtHeight.GetNumber<int>();
            Data.NavigateUrl = txtNavigateUrl.Text;
            Data.Width = txtWidth.GetNumber<int>();
            Data.Html = "";


        }
        else if (ddrAdvType.SelectedValue == "2")
        {

         //   Data.FileAddress = IOUtil.SaveFile(fupImage.PostedFile, SavePath);
            Data.AltText = txtFlashAlt.Text;
            Data.Height = txtFlashHeight.GetNumber<int>();
            Data.Width = txtFlashWidth.GetNumber<int>();
            Data.NavigateUrl = "";
            Data.Html = "";
        }
        else if (ddrAdvType.SelectedValue == "3")
        {
            Data.Html = txtHtml.Text;
            Data.AltText = "";
            Data.FileAddress = "";
            Data.NavigateUrl = "";

        }

        Data.AdvType = ConvertorUtil.ToInt32(ddrAdvType.SelectedValue);
        Data.ExpirePersianDate = dpExpirePersianDate.PersianDate;
        Data.RegPersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
        Data.Title = txtTitle.Text;
        Data.DispOrder = txtDispOrder.GetNumber<int>();
        Data.MarginBottom = txtMarginBottom.GetNumber<int>();
        Data.MarginLeft = txtMarginLeft.GetNumber<int>();
        Data.MarginRight = txtMarginRight.GetNumber<int>();
        Data.MarginTop = txtMarginTop.GetNumber<int>();

        if (fupImage.Visible == true)
        {
            if (ddrAdvType.SelectedValue == "1" || ddrAdvType.SelectedValue == "2")
            {
                Data.FileAddress = IOUtil.SaveFile(fupImage.PostedFile, SavePath);
            }
        }


        AdvBusiness business = (this.Page as BaseManagementPage<AdvBusiness, Advertisement, AdvPattern, pgcEntities>).Business;
        if (Mode == ManagementPageMode.Add)
        {
            
            
            foreach (ListItem item in cblPages.Items)
            {
                if (item.Selected)
                    Data.PanelPages.Add(business.RetrivePage(ConvertorUtil.ToInt64(item.Value)));
            }
        }
        else if (Mode == ManagementPageMode.Edit)
        {
            Data.PanelPages.Clear();
            foreach (ListItem item in cblPages.Items)
            {
                if (item.Selected)
                    Data.PanelPages.Add(business.RetrivePage(ConvertorUtil.ToInt64(item.Value)));
            }
        }

        return Data;
    }

    public override void SetEntity(Advertisement Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        dpExpirePersianDate.PersianDate = Data.ExpirePersianDate;
        txtDispOrder.SetNumber(Data.DispOrder);
        txtMarginTop.SetNumber(Data.MarginTop);
        txtMarginRight.SetNumber(Data.MarginRight);
        txtMarginLeft.SetNumber(Data.MarginLeft);
        txtMarginBottom.SetNumber(Data.MarginBottom);

        cblPages_DataBound();

        foreach (ListItem item in cblPages.Items)
        {
            long PageID = ConvertorUtil.ToInt64(item.Value);
            if (Data.PanelPages.Any(f => f.ID == PageID))
                item.Selected = true;
            else
                item.Selected = false;
        }

        ddrAdvType.SelectedValue = Data.AdvType.ToString();
        ChangemlvControl(ddrAdvType.SelectedValue);

        if (Data.FileAddress != "")
        {
            hplFile.Visible = true;
            btnDeleteFile.Visible = true;
            hplFile.Text = System.IO.Path.GetFileName(Data.FileAddress);
            hplFile.NavigateUrl = Data.FileAddress;
            fupImage.Visible = false;
        }
        else
        {
            fupImage.Visible = true;
            btnDeleteFile.Visible = false;
            hplFile.Visible = false;
        }


        if (Data.AdvType == 1)
        {

            txtAlt.Text = Data.AltText;
            txtHeight.SetNumber(Data.Height);
            txtNavigateUrl.Text = Data.NavigateUrl;
            txtWidth.SetNumber(Data.Width);
        }
        else if (Data.AdvType == 2)
        {

            txtFlashAlt.Text = Data.AltText;
            txtFlashHeight.SetNumber(Data.Height);
            txtFlashWidth.SetNumber(Data.Width);

        }
        else if (Data.AdvType == 3)
        {

            txtHtml.Text = Data.Html;
        }

    }

    public override void BeginMode(ManagementPageMode Mode)
    {
        base.BeginMode(Mode);
        ChangemlvControl(ddrAdvType.SelectedValue);
        lblRequired.Visible = false;


        if (Mode == ManagementPageMode.Add)
        {
            
            cblPages_DataBound();
        }

        

    }

    private void cblPages_DataBound()
    {
        cblPages.Items.Clear();
        cblPages.DataSource = new AdvPageLookupBusiness().GetLookupList();
        cblPages.DataTextField = "Title";
        cblPages.DataValueField = "ID";
        cblPages.DataBind();
    }

    protected void ddrAdvType_IndexChanged(object sender, System.EventArgs e)
    {
        ChangemlvControl(ddrAdvType.SelectedValue);

    }

    protected void ChangemlvControl(string SelectItem)
    {
        //if (SelectItem == "0")
        //    mlvControls.ActiveViewIndex = 3;
        if (SelectItem == "1")
            mlvControls.ActiveViewIndex = 0;
        else if (SelectItem == "2")
            mlvControls.ActiveViewIndex = 1;
        else if (SelectItem == "3")
            mlvControls.ActiveViewIndex = 2;

    }

    protected void btnDeleteFile_Click(object sender, EventArgs e)
    {

        long AdvID = (this.Page as BaseManagementPage<AdvBusiness, Advertisement, AdvPattern, pgcEntities>).SelectedID;
        Advertisement adv = (this.Page as BaseManagementPage<AdvBusiness, Advertisement, AdvPattern, pgcEntities>).Business.Retrieve(AdvID);
        IOUtil.DeleteFile(adv.FileAddress, true);

        fupImage.Visible = true;
        hplFile.Visible = false;
        btnDeleteFile.Visible = false;
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        string str = "";

        if (ddrAdvType.SelectedValue == "3" || ddrAdvType.SelectedValue == "0")
            str = "display:none";

        if (rowFup.Attributes["style"] != null)
            rowFup.Attributes.Add("style", str);
        else
            rowFup.Attributes["style"] = str;



    }

    public override bool Validate(ManagementPageMode Mode)
    {

        if (Mode == ManagementPageMode.Add )
        {
            if (ddrAdvType.SelectedValue == "1" || ddrAdvType.SelectedValue == "2")
            {
                if (fupImage.HasFile == false)
                {
                    lblRequired.Visible = true;
                    return false;
                }
            }
        }

        if (Mode == ManagementPageMode.Edit)
        {
            if (ddrAdvType.SelectedValue == "1" || ddrAdvType.SelectedValue == "2")
            {
                if (fupImage.Visible != false && fupImage.HasFile == false)
                {
                    lblRequired.Visible = true;
                    return false;
                }
            }
        }
        return base.Validate(Mode);
    }

    public override void Reset()
    {
        fupImage.Visible = true;
        btnDeleteFile.Visible = false;
        hplFile.Visible = false;

        base.Reset();
    }
}