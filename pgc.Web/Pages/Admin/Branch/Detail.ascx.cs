using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_Branch_Detail : BaseDetailControl<Branch>
{
    public override Branch GetEntity(Branch Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Branch();

        Data.Title = txtTitle.Text;
        Data.Address = txtAddress.Text;
        Data.HoursServingFood = txtServing.Text;
        Data.HoursOrdering = txtOrdering.Text;
        Data.NumberOfChair = Convert.ToInt32(txtChair.Text);
        Data.TransportCost = txtTransportCost.Text;
        Data.Longitude = Convert.ToDouble(txtLongitude.Text);
        Data.latitude = Convert.ToDouble(txtlatitude.Text);
        Data.UrlKey = txtUrlKey.Text;
        Data.PageTitle = txtPageTitle.Text;
        Data.PageDescription = txtPageDescription.Text;
        Data.PageKeywords = txtPageKeywords.Text.Replace("\n", ", ");
        Data.DispOrder = txtDispOrder.GetNumber<int>();
        Data.ThumbListPath = fupThumbListPic.FilePath;
        Data.CityCode = txtCode.Text;
        Data.PhoneNumbers = txtPhoneNumbers.Text;
        Data.Summary = " ";
        Data.BranchInfo = " ";
        Data.LargeThumbImagePath = " ";
        Data.Description = " ";
        Data.LargeImagePath = " ";
        Data.Body = " ";
        Data.BranchType = Convert.ToInt32(branchType.SelectedValue);
        if (Mode == ManagementPageMode.Add)
            Data.AllowOnlineOrder = false;

        return Data;
    }

    public override void SetEntity(Branch Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtAddress.Text = Data.Address;
        txtServing.Text = Data.HoursServingFood;
        txtOrdering.Text = Data.HoursOrdering;
        txtChair.Text = Data.NumberOfChair.ToString();
        txtTransportCost.Text = Data.TransportCost;
        txtLongitude.Text = Data.Longitude.ToString();
        txtlatitude.Text = Data.latitude.ToString();
        txtUrlKey.Text = Data.UrlKey;
        txtPageTitle.Text = Data.PageTitle;
        txtPageDescription.Text = Data.PageDescription;
        txtPageKeywords.Text = Data.PageKeywords.Replace(", ", "\n");
        txtDispOrder.SetNumber(Data.DispOrder);
        fupThumbListPic.FilePath = Data.ThumbListPath;
        branchType.SelectedValue = Data.BranchType.ToString();
        txtPhoneNumbers.Text = Data.PhoneNumbers;
        txtCode.Text = Data.CityCode;
    }
}