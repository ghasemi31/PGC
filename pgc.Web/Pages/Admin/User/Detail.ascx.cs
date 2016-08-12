using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model.Patterns;

public partial class Pages_Admin_User_Detail : BaseDetailControl<User>
{
    public UserBusiness business = new UserBusiness();
    public override User GetEntity(User Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new User();

        Data.AccessLevel_ID = lkcAccessLevel.GetSelectedValue<long>();
        Data.ActivityStatus = lkcActivityStatus.GetSelectedValue<int>();
        Data.Address = txtAddress.Text;
        Data.City_ID = lkcCity.GetSelectedValue<long>();
        Data.FatherName = txtFatherName.Text;
       
        Data.FullName = txtFullName.Text;
        Data.Mobile = txtMobile.Text;
        Data.NationalCode = txtNationalCode.Text;
        Data.PostalCode = txtPostalCode.Text;
        if (Mode == ManagementPageMode.Add)
            Data.pwd = txtPassword.Text;
        Data.Tel = txtTel.Text;
        
        Data.SignUpPersianDate = pdpSignUpPersianDate.PersianDate;
        Data.Gender = lkcGender.GetSelectedValue<int>();

      
            Data.Branch_ID = null;

        if (Mode == ManagementPageMode.Add)
        {
            Data.Username = txtEmail.Text;
            Data.Email = txtEmail.Text;
        }

        return Data;
    }

    public override void SetEntity(User Data, ManagementPageMode Mode)
    {


        lblUserID.Text = Data.ID.ToString();

        lkcRole.SetSelectedValue(Data.AccessLevel.Role);
        lkcAccessLevel.SetSelectedValue(Data.AccessLevel_ID);
        lkcActivityStatus.SetSelectedValue(Data.ActivityStatus);
        txtAddress.Text = Data.Address;
        lkcProvince.SetSelectedValue(Data.City.Province_ID);
        lkcCity.SetSelectedValue(Data.City_ID);
        txtEmail.Text = Data.Email;
        lblEmail.Text = Data.Email;
   
        txtFullName.Text = Data.FullName;
        txtMobile.Text = Data.Mobile;
        txtNationalCode.Text = Data.NationalCode;
        txtPostalCode.Text = Data.PostalCode;
        txtFatherName.Text = Data.FatherName;
        //txtPassword.Text = Data.pwd;
        txtTel.Text = Data.Tel;
        //txtUsername.Text = Data.Username;
        pdpSignUpPersianDate.PersianDate = Data.SignUpPersianDate;
        lkcGender.SetSelectedValue(Data.Gender);
        //hplResetPwd.NavigateUrl = string.Format("~/Pages/Admin/ResetPwd/Default.aspx?id={0}", Data.ID);
        hplResetPwd.NavigateUrl = GetRouteUrl("Admin-resetpwd", null) + "?" + QueryStringKeys.id.ToString() + "=" + Data.ID;

    }

    public override void BeginMode(ManagementPageMode Mode)
    {
        base.BeginMode(Mode);
        PasswordEntranceRow.Visible = (Mode == ManagementPageMode.Add);
        PasswordResetRow.Visible = (Mode == ManagementPageMode.Edit);
        UserID.Visible = (Mode == ManagementPageMode.Edit);

        if (Mode == ManagementPageMode.Add)
        {
            lblEmail.Visible = false;
            txtEmail.Visible = true;
           
        }

        if (Mode == ManagementPageMode.Edit)
        {
            lblEmail.Visible = true;
            txtEmail.Visible = false;
        }

    }

    public override bool Validate(ManagementPageMode Mode)
    {
        if (Mode ==  ManagementPageMode.Add && txtPassword.Text.Trim().Length < 4)
        {
            UserSession.AddMessage(pgc.Model.Enums.UserMessageKey.PasswordMinLengthExceed);
            return false;
        }

        return base.Validate(Mode);
    }

    protected void Role_Changed(object sender, EventArgs e)
    {

        
    }

    public override void Reset()
    {
        base.Reset();
        lkcCity.DependOnParameterValue = lkcProvince.GetSelectedValue<long>();
        lkcCity.DataBind();
        lkcAccessLevel.DependOnParameterValue = lkcRole.GetSelectedValue<long>();
        lkcAccessLevel.DataBind();
    }
}