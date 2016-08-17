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
using System.Text.RegularExpressions;

public partial class Pages_Admin_Email_ManualSending : BasePage
{
    UserBusiness userBusiness = new UserBusiness();

    ViewStateCollection<long> users;
    ViewStateCollection<string> emails;
    List<Person> PersonList = new List<Person>();

    public Pages_Admin_Email_ManualSending()
    {
        users = new ViewStateCollection<long>(this.ViewState, "users");
        emails = new ViewStateCollection<string>(this.ViewState, "mobiles");
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

                emails.AddItem(text);
            }
        }

        ResetAll();

        RecipientGridDataBind();
    }

    protected void OnCancel(object sender, EventArgs e)
    {
        users.Clear();
        emails.Clear();

        Response.Redirect(GetRouteUrl("admin-default", null));
    }

    private void ResetAll()
    {
        foreach (System.Web.UI.WebControls.TableRow row in Grid.Rows)
            ((System.Web.UI.WebControls.CheckBox)row.Cells[0].Controls[0]).Checked = false;

        //Manual Insert
        txtList.Text = "";

        //Search Panel
        //txtUsername.Text = "";
        //txtName.Text = "";
        //lkpImagingCenter.SetSelectedValue(-1, "");
        //lkpContractStatus.SetSelectedValue(-1, "");
        //lkpGender.SetSelectedValue(-1);
        //lkcCity.SetSelectedValue(-1);
        //lkcProvince.SetSelectedValue(-1);
        //lkcActivityStatus.SetSelectedValue(-1);        
    }

    public class Person
    {
        public Person()
        {
            FullName = "";
            Email = "";
            UserName = "";
        }

        public string FullName { get; set; }
        public Role Role { get; set; }
        public string Email { get; set; }
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

        for (int i = emails.Count - 1; -1 < i; i--)
        {
            string text = emails.GetItem(i).Trim();
            bool isValid = true;

            //Bypass Empty Lines
            if (string.IsNullOrEmpty(text))
                continue;

            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
            Match match = Regex.Match(text, pattern, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                if (!UserSession.CurrentMessages.Contains(UserMessageKey.InvalidRecipientEmail))
                    UserSession.AddMessage(UserMessageKey.InvalidRecipientEmail);

                isValid = false;
                emails.RemoveAt(i);
            }

            if (PersonList.Any(f => f.Email == text))
            {
                if (!UserSession.CurrentMessages.Contains(UserMessageKey.DuplicateManualEmailEvent))
                    UserSession.AddMessage(UserMessageKey.DuplicateManualEmailEvent);

                isValid = false;
            }


            if (isValid)
            {
                Person person = new Person();
                person.FullName = "------------";
                person.ID = PersonList.Count;
                person.Email = text;
                person.UserName = "------------";
                PersonList.Add(person);
            }
        }


        for (int i = users.Count - 1; -1 < i; i--)
        {
            bool isValid = true;
            User user = userBusiness.Retrieve(users.GetItem(i));
            if (string.IsNullOrEmpty(user.Email))
            {
                if (!UserSession.CurrentMessages.Contains(UserMessageKey.UserNoEmail))
                    UserSession.AddMessage(UserMessageKey.UserNoEmail);

                isValid = false;
                users.RemoveAt(i);
            }
            else if (PersonList.Any(f => f.UserName == user.Username))
            {
                if (!UserSession.CurrentMessages.Contains(UserMessageKey.DuplicateManualEmailEvent))
                    UserSession.AddMessage(UserMessageKey.DuplicateManualEmailEvent);

                isValid = false;
                users.RemoveAt(i);
            }

            if (isValid)
            {

                Person person = new Person();
                person.FullName = user.Fname + " " + user.Lname;
                person.ID = PersonList.Count;
                person.Email = user.Email;
                person.Role = (Role)user.AccessLevel.Role;
                person.UserName = user.Username;
                PersonList.Add(person);
            }
        }

        for (int i = PersonList.Count - 1; -1 < i; i--)
        {
            if (PersonList.Any(f => f.Email == PersonList[i].Email && f.ID != PersonList[i].ID))
            {
                if (!UserSession.CurrentMessages.Contains(UserMessageKey.DuplicateManualEmailEvent))
                    UserSession.AddMessage(UserMessageKey.DuplicateManualEmailEvent);

                if (i < emails.Count)
                    emails.RemoveAt(emails.Count - i - 1);
                else
                    users.RemoveAt(users.Count - (i - emails.Count) - 1);


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
            if (Row_ID < emails.Count)
                emails.RemoveAt(emails.Count - Row_ID - 1);
            else
                users.RemoveAt(users.Count - (Row_ID - emails.Count) - 1);

            UserSession.AddMessage(UserMessageKey.Succeed);
        }
        RecipientGridDataBind();
    }

    protected void RecipientGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int cellRole = 4;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem, "UserName").ToString().Contains("--"))
                e.Row.Cells[cellRole].Text = DataBinder.Eval(e.Row.DataItem, "UserName").ToString();
        }
    }

    protected void RecipientGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        RecipientGrid.PageIndex = e.NewPageIndex;
        RecipientGridDataBind();
    }

    #endregion



    #region Email Sending

    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> Recipients = new List<string>();
            Recipients = PersonList.Select(f => f.Email).ToList();

            List<string> FilePaths = new List<string>();
            if (!string.IsNullOrEmpty(fup1.FilePath))
                FilePaths.Add(fup1.FilePath);
            if (!string.IsNullOrEmpty(fup2.FilePath))
                FilePaths.Add(fup2.FilePath);
            if (!string.IsNullOrEmpty(fup3.FilePath))
                FilePaths.Add(fup3.FilePath);
            if (!string.IsNullOrEmpty(fup4.FilePath))
                FilePaths.Add(fup4.FilePath);
            if (!string.IsNullOrEmpty(fup5.FilePath))
                FilePaths.Add(fup5.FilePath);

            bool isValid = true;
            if (Recipients.Count == 0)
            {
                UserSession.AddMessage(UserMessageKey.NoRecipientEmail);
                isValid = false;
            }
            if (string.IsNullOrEmpty(txtSubject.Text))
            {
                UserSession.AddMessage(UserMessageKey.NoEmailSubject);
                isValid = false;
            }
            if (string.IsNullOrEmpty(ckBody.GetValue()))
            {
                UserSession.AddMessage(UserMessageKey.NoEmailBody);
                isValid = false;
            }



            if (!isValid)
                return;
            MailBusiness m = new MailBusiness();
            SendEmailResult ser = m.Send(txtSubject.Text, ckBody.GetValue(), Recipients, EventType.Manual, FilePaths, null, txtDisplayAddressName.Text, "", "", chUseTemplate.Checked);

            if (ser.UserMessages.Contains(UserMessageKey.Succeed))
                UserSession.AddMessage(UserMessageKey.AllEmailSent);
            else
                UserSession.AddMessage(ser.UserMessages);

            serResult.Result = ser;
            pnlResult.Visible = true;
            pnlSend.Visible = !pnlResult.Visible;
        }
        catch
        {
            UserSession.AddMessage(UserMessageKey.Failed);
        }
    }

    protected void cmdNewSend_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("admin-manualemail", null));
    }

    protected void cmdSentMessages_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("admin-emailarchive", null);
    }

    #endregion
    
}