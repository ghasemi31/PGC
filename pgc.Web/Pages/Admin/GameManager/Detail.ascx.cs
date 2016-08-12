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

public partial class Pages_Admin_GameManager_Detail : BaseDetailControl<User>
{
    public GameManagerBusiness business = new GameManagerBusiness();
    public override User GetEntity(User Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new User();

        Data.AccessLevel_ID = lkcAccessLevel.GetSelectedValue<long>();
        Data.ActivityStatus = lkcActivityStatus.GetSelectedValue<int>();
        Data.Address = txtAddress.Text;
        //Data.City_ID = lkcCity.GetSelectedValue<long>();
        Data.Email = txtEmail.Text;
        Data.FullName = txtFullName.Text;
        Data.FatherName = txtFatherName.Text;
        Data.Mobile = txtMobile.Text;
        Data.NationalCode = txtNationalCode.Text;
        if (Mode == ManagementPageMode.Add)
            Data.pwd = txtPassword.Text;
        Data.Tel = txtTel.Text;
        Data.Username = txtEmail.Text;
        Data.SignUpPersianDate = pdpSignUpPersianDate.PersianDate;
        Data.Gender = lkcGender.GetSelectedValue<int>();

        if (lkcRole.GetSelectedValue<Role>() == Role.Agent)
            Data.IsGameManager = true;
        else
            Data.IsGameManager = false;

        return Data;
    }

    public override void SetEntity(User Data, ManagementPageMode Mode)
    {


        lblUserID.Text = Data.ID.ToString();

        lkcRole.SetSelectedValue(Data.AccessLevel.Role);
        lkcAccessLevel.SetSelectedValue(Data.AccessLevel_ID);
        lkcActivityStatus.SetSelectedValue(Data.ActivityStatus);
        txtAddress.Text = Data.Address;
        //lkcProvince.SetSelectedValue(Data.City.Province_ID);
        //lkcCity.SetSelectedValue(Data.City_ID );
        txtEmail.Text = Data.Email;
        txtFullName.Text = Data.FullName;
        txtFatherName.Text = Data.FatherName;
        txtMobile.Text = Data.Mobile;
        txtNationalCode.Text = Data.NationalCode;
        //txtPassword.Text = Data.pwd;
        txtTel.Text = Data.Tel;
        //txtUsername.Text = Data.Username;
        pdpSignUpPersianDate.PersianDate = Data.SignUpPersianDate;
        lkcGender.SetSelectedValue(Data.Gender);
        //hplResetPwd.NavigateUrl = string.Format("~/Pages/Admin/ResetPwd/Default.aspx?id={0}", Data.ID);
        hplResetPwd.NavigateUrl = GetRouteUrl("Admin-resetpwd", null) + "?" + QueryStringKeys.id.ToString() + "=" + Data.ID;


        //bindGame();
        //var values = Data.Games.Select(m => m.ID).ToList();
        //foreach (ListItem item in chlgame.Items)
        //{
        //    item.Selected = values.Contains(ConvertorUtil.ToInt64(item.Value));
        //}


        //if (Data.Branch_ID == null)
        //    lkcBranch.SetSelectedValue(-1);
        //else
        //    lkcBranch.SetSelectedValue(Data.Branch_ID);
    }

    public override void BeginMode(ManagementPageMode Mode)
    {
        base.BeginMode(Mode);
        PasswordEntranceRow.Visible = (Mode == ManagementPageMode.Add);
        PasswordResetRow.Visible = (Mode == ManagementPageMode.Edit);
        UserID.Visible = (Mode == ManagementPageMode.Edit);

        //if (Mode == ManagementPageMode.Add)
        //    branch.Visible = false;
    }

    public override bool Validate(ManagementPageMode Mode)
    {
        if (Mode == ManagementPageMode.Add && txtPassword.Text.Trim().Length < 4)
        {
            UserSession.AddMessage(pgc.Model.Enums.UserMessageKey.PasswordMinLengthExceed);
            return false;
        }

        return base.Validate(Mode);
    }

    protected void Role_Changed(object sender, EventArgs e)
    {

        //if (lkcRole.GetSelectedValue<Role>() == Role.Agent)
        //{
        //    branch.Visible = true;
        //}
        //else
        //    branch.Visible = false;
    }

    public override void Reset()
    {
        base.Reset();
        //lkcCity.DependOnParameterValue = lkcProvince.GetSelectedValue<long>();
        //lkcCity.DataBind();
        lkcAccessLevel.DependOnParameterValue = lkcRole.GetSelectedValue<long>();
        lkcAccessLevel.DataBind();
    }

    private void bindGame()
    {
        List<Game> games = business.GetAllGames();
        chlgame.DataSource = games;
        chlgame.DataTextField = "Title";
        chlgame.DataValueField = "ID";
        chlgame.DataBind();
    }
}