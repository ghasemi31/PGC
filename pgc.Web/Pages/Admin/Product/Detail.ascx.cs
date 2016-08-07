using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model.Patterns;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using kFrameWork.Util;
using System.Linq;

public partial class Pages_Admin_Product_Detail : BaseDetailControl<Product>
{
    private MaterialBusiness business = new MaterialBusiness();
    public override Product GetEntity(Product Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Product();

        Data.Title = txtTitle.Text;
        Data.Body = txtBody.Text;
        Data.ProductPicPath = fupProductPic.FilePath;
        Data.DispOrder = Convert.ToInt32(txtDispOrder.Text);
        Data.SliderProductPicPath = fupSliderProductPic.FilePath;
        Data.SliderHoverProductPicPath = fupSliderHoverProductPic.FilePath;
        Data.Price = txtPrice.GetNumber<long>();
        Data.DisplayInSlider = chkDisplayInSlider.Checked;
        Data.PageTitle = txtPageTitle.Text;
        Data.PageDescription = txtPageDescription.Text;
        Data.PageKeywords = txtPageKeywords.Text.Replace("\n", ", ");
        Data.Accessories = Convert.ToBoolean(chkAccessories.Checked);
        Data.IsActive = Convert.ToBoolean(chkIsActive.Checked);
        if (Mode == ManagementPageMode.Edit)
        {
            Data.Materials.Clear();
        }
        if (Mode == ManagementPageMode.Add)
        {
            Data.AllowOnlineOrder = false;

        }
        ProductBusiness pro_business = (this.Page as BaseManagementPage<ProductBusiness, Product, ProductPattern, pgcEntities>).Business;
        foreach (ListItem item in chlMaterials.Items)
        {
            if (item.Selected)
                Data.Materials.Add(pro_business.retrieveMaterial(ConvertorUtil.ToInt64(item.Value)));
        }
        return Data;
    }

    public override void SetEntity(Product Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtBody.Text = Data.Body;
        fupProductPic.FilePath = Data.ProductPicPath;
        fupSliderProductPic.FilePath = Data.SliderProductPicPath;
        fupSliderHoverProductPic.FilePath = Data.SliderHoverProductPicPath;
        txtPrice.SetNumber(Data.Price);
        chkDisplayInSlider.Checked = Data.DisplayInSlider;
        chkAccessories.Checked = Data.Accessories;
        chkIsActive.Checked = Data.IsActive;
        txtDispOrder.Text = Data.DispOrder.ToString();
        txtPageTitle.Text = Data.PageTitle;
        txtPageDescription.Text = Data.PageDescription;
        txtPageKeywords.Text = Data.PageKeywords.Replace(", ", "\n");
        bindMaterial();
        var values = Data.Materials.Select(m => m.ID).ToList();
        foreach (ListItem item in chlMaterials.Items)
        {
            item.Selected = values.Contains(ConvertorUtil.ToInt64(item.Value));
        }

    }
    public override void BeginMode(ManagementPageMode Mode)
    {
        base.BeginMode(Mode);
        if (Mode == ManagementPageMode.Add)
        {
            bindMaterial();
        }
    }

    private void bindMaterial()
    {
        List<Material> materials = business.GetAllMaterial();
        chlMaterials.DataSource = materials;
        chlMaterials.DataTextField = "Title";
        chlMaterials.DataValueField = "ID";
        chlMaterials.DataBind();
    }

    
}