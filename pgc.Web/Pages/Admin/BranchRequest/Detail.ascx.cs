using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using kFrameWork.Util;

public partial class Pages_Admin_BranchRequest_Detail : BaseDetailControl<BranchRequest>
{
    public override BranchRequest GetEntity(BranchRequest Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new BranchRequest();

        
        Data.Status = lkcStatus.GetSelectedValue<int>();

        return Data;
    }

    public override void SetEntity(BranchRequest Data, ManagementPageMode Mode)
    {
        lkcStatus.SetSelectedValue(Data.Status);
        //lblFname.Text = Data.Fname;
        //lblLname.Text = Data.Lname;
        lblFullname.Text = Data.FullName;
        lblAplicatorName.Text = Data.ApplicatorName;
        lblLocation.Text = Data.BranchLocation;
        if (Data.LocationType==1)
            lblLocationType.Text = "شخصی";
        else if (Data.LocationType==2)
            lblLocationType.Text = "استیجاری";
        lblPhone.Text=Data.Tel;
        lblMobile.Text=Data.Mobile;
        lblEmail.Text=Data.Email;
        lblAddress.Text=Data.Address;
        lblDesc.Text=Data.Description;
        if (Data.HaveBackgroung == true)
            lblBackground.Text = "دارم";
        else if (Data.HaveBackgroung == false)
            lblBackground.Text = "ندارم";
        lblBRPersianDate.Text= DateUtil.GetPersianDateWithTime(Data.BRDate).ToString();
      
    }

    //public override void BeginMode(ManagementPageMode Mode)
    //{
    //    base.BeginMode(Mode);
    //    PasswordEntranceRow.Visible = (Mode == ManagementPageMode.Add);
    //    PasswordResetRow.Visible = (Mode == ManagementPageMode.Edit);
    //}

    //public override bool Validate(ManagementPageMode Mode)
    //{
    //    if (Mode ==  ManagementPageMode.Add && txtPassword.Text.Trim().Length < 4)
    //    {
    //        UserSession.AddMessage(pgc.Model.Enums.UserMessageKey.PasswordMinLengthExceed);
    //        return false;
    //    }

    //   return base.Validate(Mode);
    //}
}