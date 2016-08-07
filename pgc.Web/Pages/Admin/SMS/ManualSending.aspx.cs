using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Model;
using pgc.Model.Patterns;
using pgc.Business;
using pgc.Model.Enums;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Business.Core;

public partial class Pages_Admin_SMS_ManualSending : BasePage
{
    UserBusiness userBusiness = new UserBusiness();

    ViewStateCollection<long> users;
    ViewStateCollection<string> mobiles;
    List<Person> PersonList = new List<Person>();

    public Pages_Admin_SMS_ManualSending()
    {
        users= new ViewStateCollection<long>(this.ViewState, "users");
        mobiles= new ViewStateCollection<string>(this.ViewState, "mobiles");        
    }

    public void Page_Load(object sender, EventArgs e)
    {        
        Grid.PageSize = (this.Page as BasePage).Entity.PageSize;
        RecipientGrid.PageSize = (this.Page as BasePage).Entity.PageSize;
        RecipientGridDataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool isManual = (rdb.SelectedIndex == 1);
        
        if (!isManual)
        {
            foreach (long id in Grid.SelectedIDs.List)
                users.AddItem(id);
        }
        else
        {
            foreach (string dest in txtList.Text.Split('\n'))
            {
                string text = dest.Trim();

                //Bypass Empty Lines
                if (string.IsNullOrEmpty(text))
                    continue;

                mobiles.AddItem(text);
            }
        }

        ResetAll();

        RecipientGridDataBind();
    }

    protected void OnCancel(object sender, EventArgs e)
    {
        users.Clear();
        mobiles.Clear();

        Response.Redirect(GetRouteUrl("admin-default", null));
    }

    private void ResetAll()
    {
        foreach (System.Web.UI.WebControls.TableRow row in Grid.Rows)
            ((System.Web.UI.WebControls.CheckBox)row.Cells[0].Controls[0]).Checked = false;

        //Manual Insert
        txtList.Text = "";

        //Search Panel
        txtUsername.Text = "";
        txtName.Text = "";
        lkpBranch.SetSelectedValue(-1);
        lkpGender.SetSelectedValue(-1);
        lkcCity.SetSelectedValue(-1);
        lkcProvince.SetSelectedValue(-1);
        lkcActivityStatus.SetSelectedValue(-1);
        lkcPrivateNo.SetSelectedValue(-1);
        lkpRole.SetSelectedValue(-1);
    }

    public class Person
    {
        public Person()
        {
            FullName = "";
            Mobile = "";
            UserName = "";
        }

        public string FullName { get; set; }
        public Role Role { get; set; }
        public string Mobile { get; set; }
        public string UserName { get; set; }
        public long ID { get; set; }
    }



    #region List Of User Search

    UserPattern up = new UserPattern();

    protected void OnSearchAll(object sender, EventArgs e)
    {
        ResetAll();
        up = new UserPattern();
        Grid.DataBind();
    }

    protected void OnSearch(object sender, EventArgs e)
    {
        up.Username = txtUsername.Text;
        up.Name = txtName.Text;
        up.Branch_ID = lkpBranch.GetSelectedValue<long>();
        up.Gender = lkpGender.GetSelectedValue<Gender>();
        up.ActivityStatus = lkcActivityStatus.GetSelectedValue<UserActivityStatus>();
        up.Province_ID = lkcProvince.GetSelectedValue<long>();
        up.City_ID = lkcCity.GetSelectedValue<long>();
        up.Role = lkpRole.GetSelectedValue<Role>();
        Grid.DataBind();
    }

    protected void GridObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        up.Username = txtUsername.Text;
        up.Name = txtName.Text;
        up.Branch_ID = lkpBranch.GetSelectedValue<long>();
        up.Gender = lkpGender.GetSelectedValue<Gender>();
        up.ActivityStatus = lkcActivityStatus.GetSelectedValue<UserActivityStatus>();
        up.Province_ID = lkcProvince.GetSelectedValue<long>();
        up.City_ID = lkcCity.GetSelectedValue<long>();
        up.Role = lkpRole.GetSelectedValue<Role>();
        e.InputParameters["Pattern"] = up;
    }

    #endregion        



    #region RecipientGrid Functions

    private void RecipientGridDataBind()
    {
        PersonList = new List<Person>();

        for (int i = mobiles.Count - 1; -1 < i; i--)
        {
            string text = mobiles.GetItem(i).Trim();

            //Bypass Empty Lines
            if (string.IsNullOrEmpty(text))
                continue;

            double testDouble = 0;

            bool isValid = true;
            if (PersonList.Any(f => f.Mobile == text))
            {
                if (!UserSession.CurrentMessages.Contains(UserMessageKey.DuplicateManualMobileEvent))
                    UserSession.AddMessage(UserMessageKey.DuplicateManualMobileEvent);

                isValid = false;
                mobiles.RemoveAt(i);
            }
            else if (!double.TryParse(text, out testDouble))
            {
                if (!UserSession.CurrentMessages.Contains(UserMessageKey.InvalidRecipientNumber))
                    UserSession.AddMessage(UserMessageKey.InvalidRecipientNumber);

                isValid = false;
                mobiles.RemoveAt(i);
            }

            if (isValid)
            {
                Person person = new Person();
                person.FullName = "------------";
                person.ID = PersonList.Count;
                person.Mobile = text;
                person.UserName = "------------";
                PersonList.Add(person);
            }
        }


        for (int i = users.Count - 1; -1 < i; i--)
        {
            bool isValid = true;
            User user = userBusiness.Retrieve(users.GetItem(i));
            if (string.IsNullOrEmpty(user.Mobile))
            {
                if (!UserSession.CurrentMessages.Contains(UserMessageKey.UserNoMobile))
                    UserSession.AddMessage(UserMessageKey.UserNoMobile);
                isValid = false;
                users.RemoveAt(i);
            }
            else if (PersonList.Any(f => f.UserName == user.Username))
            {
                if(!UserSession.CurrentMessages.Contains(UserMessageKey.DuplicateUserForEvent))
                    UserSession.AddMessage(UserMessageKey.DuplicateUserForEvent);

                isValid = false;
                users.RemoveAt(i);
            }

            if (isValid)
            {

                Person person = new Person();
                person.FullName = user.Fname + " " + user.Lname;
                person.ID = PersonList.Count;
                person.Mobile = user.Mobile;
                person.Role = (Role)user.AccessLevel.Role;
                person.UserName = user.Username;
                PersonList.Add(person);
            }
        }


        for (int i = PersonList.Count - 1 ; -1 < i; i--)
        {
            if (PersonList.Any(f => f.Mobile == PersonList[i].Mobile && f.ID != PersonList[i].ID))
            {
                if (!UserSession.CurrentMessages.Contains(UserMessageKey.DuplicateManualMobileEvent))
                    UserSession.AddMessage(UserMessageKey.DuplicateManualMobileEvent);

                if (i < mobiles.Count)
                    mobiles.RemoveAt(mobiles.Count - i - 1);
                else
                    users.RemoveAt(users.Count - (i - mobiles.Count) - 1);

                PersonList.RemoveAt(i);
            }
        }


        RecipientGrid.DataSource = PersonList;
        RecipientGrid.DataBind();
    }

    protected void RecipientGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRow")
        {
            int Row_ID = ConvertorUtil.ToInt32(RecipientGrid.DataKeys[ConvertorUtil.ToInt32(e.CommandArgument)].Value);
            if (Row_ID < mobiles.Count )
                mobiles.RemoveAt(mobiles.Count - Row_ID - 1);
            else
                users.RemoveAt(users.Count - (Row_ID - mobiles.Count) - 1);

            UserSession.AddMessage(UserMessageKey.Succeed);
        }
        RecipientGridDataBind();
    }

    protected void RecipientGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int cellRole = 4;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem,"UserName").ToString().Contains("--"))
                e.Row.Cells[cellRole].Text=DataBinder.Eval(e.Row.DataItem,"UserName").ToString();
        }
    }

    protected void RecipientGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        RecipientGrid.PageIndex = e.NewPageIndex;
        RecipientGridDataBind();
    }
    
    #endregion



    #region SMS Sending

    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            #region Basic UI Validation

            if (txtSMS.Message.Body.Trim() == string.Empty)
            {
                UserSession.AddMessage(UserMessageKey.NoMessageBody);
                return;
            }
            else if (lkcPrivateNo.GetSelectedValue<long>() == -1)
            {
                UserSession.AddMessage(UserMessageKey.NoPrivateNo);
                return;
            }

            List<string> Recipients = new List<string>();
            Recipients.AddRange(PersonList.Select(f => f.Mobile));

            #endregion Basic UI Validation

            SendSMSBusiness Business = new SendSMSBusiness(
                txtSMS.Message,
                Recipients,
                this.lkcPrivateNo.GetSelectedValue<long>()
                );

            OperationResult ValidationResult = Business.ValidateForSend();
            if (ValidationResult.Result != ActionResult.Done)
            {
                UserSession.AddMessage(ValidationResult.Messages);
                return;
            }

            SendSMSResult Res = Business.Send(null, EventType.Manual);
            UserSession.AddMessage(Res.UserMessages);
            this.snrResult.Result = Res;
            pnlResult.Visible = true;
            pnlSend.Visible = !pnlResult.Visible;
        }
        catch
        {
            UserSession.AddMessage(UserMessageKey.Failed);
        } 
        // OperationResult result= business.ValidateForSend(
        //        mscBody.Message,
        //        lkpPrivateNo.GetSelectedValue<long>(),
        //        Recipients);

        // UserSession.AddMessage(result.Messages);

        //SendResult Res= business.Send(
        //         mscBody.Message,
        //         lkpPrivateNo.GetSelectedValue<long>(),
        //         Recipients);

        //UserSession.AddMessage(Res.UserMessages);
    }

    protected void cmdNewSend_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("admin-manualsms", null));
    }

    protected void cmdSentMessages_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("admin-smsarchive", null);
    }

    #endregion


}