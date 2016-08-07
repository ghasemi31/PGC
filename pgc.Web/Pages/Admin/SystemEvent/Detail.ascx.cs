using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model.Patterns;
using System.Collections.Generic;
using kFrameWork.Util;
using pgc.Business.Lookup;

public partial class Pages_Admin_SystemEvent_Detail : BaseDetailControl<SystemEvent>
{
    public override SystemEvent GetEntity(SystemEvent Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new SystemEvent();

        //Data.Title = txtTitle.Text;
        //Data.Description = txtDescirption.Text;

        Data.Template_Admin_Email = htmlEmailAdmin.Text;
        Data.Template_Admin_SMS = txtSMSAdmin.TextBody;
        Data.Template_User_Email = htmlEmailUser.Text;
        Data.Template_User_SMS = txtSMSUser.TextBody;
        
        //Data.Support_Manual_Email =((int) lkpSupport_Manual_Email.GetSelectedValue<BooleanStatus>()==1)?true:false;
        //Data.Support_Manual_SMS =((int) lkpSupport_Manual_SMS.GetSelectedValue<BooleanStatus>()==1)?true:false;

        //Data.Support_Every_Admin_Email =((int) lkpSupport_Every_Admin_Email.GetSelectedValue<BooleanStatus>()==1)?true:false;
        //Data.Support_Every_Admin_SMS =((int) lkpSupport_Every_Admin_SMS.GetSelectedValue<BooleanStatus>()==1)?true:false;
        //Data.Support_Every_User_Email =((int) lkpSupport_Every_User_Email.GetSelectedValue<BooleanStatus>()==1)?true:false;
        //Data.Support_Every_User_SMS =((int) lkpSupport_Every_User_SMS.GetSelectedValue<BooleanStatus>()==1)?true:false;

        //Data.Support_Related_Guest_Email =((int) lkpSupport_Related_Guest_Email.GetSelectedValue<BooleanStatus>()==1)?true:false;
        //Data.Support_Related_User_Email =((int) lkpSupport_Related_User_Email.GetSelectedValue<BooleanStatus>()==1)?true:false;
        //Data.Support_Related_Branch_Email =((int) lkpSupport_Related_Branch_Email.GetSelectedValue<BooleanStatus>()==1)?true:false;
        //Data.Support_Related_Doer_Email =((int) lkpSupport_Related_Doer_Email.GetSelectedValue<BooleanStatus>()==1)?true:false;
        //Data.Support_Related_Guest_SMS =((int) lkpSupport_Related_Guest_SMS.GetSelectedValue<BooleanStatus>()==1)?true:false;
        //Data.Support_Related_User_SMS =((int) lkpSupport_Related_User_SMS.GetSelectedValue<BooleanStatus>()==1)?true:false;
        //Data.Support_Related_Branch_SMS =((int) lkpSupport_Related_Branch_SMS.GetSelectedValue<BooleanStatus>()==1)?true:false;
        //Data.Support_Related_Doer_SMS =((int) lkpSupport_Related_Doer_SMS.GetSelectedValue<BooleanStatus>()==1)?true:false;
        if (Data.SystemEventAction == null)
            Data.SystemEventAction = new SystemEventAction();

        Data.SystemEventAction.PrivateNo_ID = lkcPrivateNo.GetSelectedValue<long>();
        if (Data.SystemEventAction.PrivateNo_ID < 1)
            Data.SystemEventAction.PrivateNo_ID = null;

        Data.SystemEventAction.ManualEmail = txtManual_Email.Text;
        Data.SystemEventAction.ManualSMS = txtManual_SMS.Text;


        int role = lkpCustomRole.GetSelectedValue<int>();
        Data.SystemEventAction.Custom_Role_ID = (role > 0) ? (int?)role : (int?)null;
        int role_sms = lkpCustomRole_SMS.GetSelectedValue<int>();
        Data.SystemEventAction.Custom_Role_ID_SMS = (role_sms > 0) ? (int?)role_sms : (int?)null;

        long access = lkpCustomAccess.GetSelectedValue<long>();
        Data.SystemEventAction.Custom_AccessLevel_ID = (access > 0) ? (long?)access : (long?)null;
        long access_sms = lkpCustomAccess_SMS.GetSelectedValue<long>();
        Data.SystemEventAction.Custom_AccessLevel_ID_SMS = (access_sms > 0) ? (long?)access_sms : (long?)null;


        long user = lkpCustomUser.GetSelectedValue<long>();
        Data.SystemEventAction.Custom_User_ID = (user > 0) ? (long?)user : (long?)null;
        long user_sms = lkpCustomUser_SMS.GetSelectedValue<long>();
        Data.SystemEventAction.Custom_User_ID_SMS = (user_sms > 0) ? (long?)user_sms : (long?)null;

        Data.SystemEventAction.Custom_User_SMS = chbCustom_User_SMS.Checked;
        Data.SystemEventAction.Custom_User_Email = chbCustom_User_Email.Checked;
        Data.SystemEventAction.Custom_AccessLevel_SMS = chbCustom_Access_SMS.Checked;
        Data.SystemEventAction.Custom_AccessLevel_Email = chbCustom_Access_Email.Checked;
        Data.SystemEventAction.Custom_Role_SMS= chbCustom_Role_SMS.Checked;
        Data.SystemEventAction.Custom_Role_Email= chbCustom_Role_Email.Checked;


        Data.SystemEventAction.Related_Guest_Email = chbRelated_Guest_Email.Checked;
        Data.SystemEventAction.Related_User_Email = chbRelated_User_Email.Checked;
        Data.SystemEventAction.Related_Branch_Email = chbRelated_Branch_Email.Checked;
        Data.SystemEventAction.Related_Doer_Email = chbRelated_Doer_Email.Checked;
        
        Data.SystemEventAction.Related_Guest_SMS = chbRelated_Guest_SMS.Checked;
        Data.SystemEventAction.Related_User_SMS = chbRelated_User_SMS.Checked;
        Data.SystemEventAction.Related_Branch_SMS = chbRelated_Branch_SMS.Checked;
        Data.SystemEventAction.Related_Doer_SMS = chbRelated_Doer_SMS.Checked;

        return Data;
    }

    public override void SetEntity(SystemEvent Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtDescription.Text = Data.Description;

        htmlEmailAdmin.Text = Data.Template_Admin_Email;
        htmlEmailUser.Text = Data.Template_User_Email;

        txtSMSAdmin.TextBody = Data.Template_Admin_SMS;
        txtSMSUser.TextBody = Data.Template_User_SMS;

        //lkpSupport_Manual_Email.SetSelectedValue((Data.Support_Manual_Email)?1:2);
        //lkpSupport_Manual_SMS.SetSelectedValue((Data.Support_Manual_SMS)?1:2);

        //lkpSupport_Every_Admin_Email.SetSelectedValue((Data.Support_Every_Admin_Email)?1:2);
        //lkpSupport_Every_Admin_SMS.SetSelectedValue((Data.Support_Every_Admin_SMS)?1:2);
        //lkpSupport_Every_User_Email.SetSelectedValue((Data.Support_Every_User_Email)?1:2);
        //lkpSupport_Every_User_SMS.SetSelectedValue((Data.Support_Every_User_SMS)?1:2);

        //lkpSupport_Related_Guest_Email.SetSelectedValue((Data.Support_Related_Guest_Email)?1:2);
        //lkpSupport_Related_User_Email.SetSelectedValue((Data.Support_Related_User_Email)?1:2);
        //lkpSupport_Related_Branch_Email.SetSelectedValue((Data.Support_Related_Branch_Email)?1:2);
        //lkpSupport_Related_Doer_Email.SetSelectedValue((Data.Support_Related_Doer_Email)?1:2);
        //lkpSupport_Related_Guest_SMS.SetSelectedValue((Data.Support_Related_Guest_SMS)?1:2);
        //lkpSupport_Related_User_SMS.SetSelectedValue((Data.Support_Related_User_SMS)?1:2);
        //lkpSupport_Related_Branch_SMS.SetSelectedValue((Data.Support_Related_Branch_SMS)?1:2);
        //lkpSupport_Related_Doer_SMS.SetSelectedValue((Data.Support_Related_Doer_SMS)?1:2);

        page = (this.Page as BaseManagementPage<SystemEventBusiness, SystemEvent, SystemEventPattern, pgcEntities>);

        SystemEvent = page.Business.Retrieve(page.SelectedID);

        if (SystemEvent.SystemEventAction == null)
            SystemEvent.SystemEventAction = new SystemEventAction();

        if (SystemEvent.SystemEventAction.PrivateNo_ID.HasValue)
            lkcPrivateNo.SetSelectedValue(SystemEvent.SystemEventAction.PrivateNo_ID);

        txtManual_Email.Text = SystemEvent.SystemEventAction.ManualEmail;
        txtManual_SMS.Text = SystemEvent.SystemEventAction.ManualSMS;

        lkpCustomAccess.SetSelectedValue(SystemEvent.SystemEventAction.Custom_AccessLevel_ID);
        lkpCustomRole.SetSelectedValue(SystemEvent.SystemEventAction.Custom_Role_ID);
        lkpCustomUser.SetSelectedValue(SystemEvent.SystemEventAction.Custom_User_ID);

        lkpCustomAccess_SMS.SetSelectedValue(SystemEvent.SystemEventAction.Custom_AccessLevel_ID_SMS);
        lkpCustomRole_SMS.SetSelectedValue(SystemEvent.SystemEventAction.Custom_Role_ID_SMS);
        lkpCustomUser_SMS.SetSelectedValue(SystemEvent.SystemEventAction.Custom_User_ID_SMS);

        chbCustom_Access_Email.Checked = SystemEvent.SystemEventAction.Custom_AccessLevel_Email;
        chbCustom_Access_SMS.Checked = SystemEvent.SystemEventAction.Custom_AccessLevel_SMS;
        chbCustom_Role_Email.Checked = SystemEvent.SystemEventAction.Custom_Role_Email;
        chbCustom_Role_SMS.Checked = SystemEvent.SystemEventAction.Custom_Role_SMS;
        chbCustom_User_Email.Checked = SystemEvent.SystemEventAction.Custom_User_Email;
        chbCustom_User_SMS.Checked = SystemEvent.SystemEventAction.Custom_User_SMS;


        chbRelated_Guest_Email.Checked = SystemEvent.SystemEventAction.Related_Guest_Email;
        chbRelated_User_Email.Checked = SystemEvent.SystemEventAction.Related_User_Email;
        chbRelated_Branch_Email.Checked = SystemEvent.SystemEventAction.Related_Branch_Email;
        chbRelated_Doer_Email.Checked = SystemEvent.SystemEventAction.Related_Doer_Email;
        
        chbRelated_Guest_SMS.Checked = SystemEvent.SystemEventAction.Related_Guest_SMS;
        chbRelated_User_SMS.Checked = SystemEvent.SystemEventAction.Related_User_SMS;
        chbRelated_Branch_SMS.Checked = SystemEvent.SystemEventAction.Related_Branch_SMS;
        chbRelated_Doer_SMS.Checked = SystemEvent.SystemEventAction.Related_Doer_SMS;


        GetLookupList_RunTimeParam(null, new List<Type>(), new List<object>());
        var result = new SystemEventVariableLookupBusiness().GetLookupList(Data.ID);
        lsb1.DataSource = result;
        lsb1.DataValueField = "ID";
        lsb1.DataTextField = "Title";
        lsb1.DataBind();

        lsb2.DataSource = result;
        lsb2.DataValueField = "ID";
        lsb2.DataTextField = "Title";
        lsb2.DataBind();

        lsb3.DataSource = result;
        lsb3.DataValueField = "ID";
        lsb3.DataTextField = "Title";
        lsb3.DataBind();

        lsb4.DataSource = result;
        lsb4.DataValueField = "ID";
        lsb4.DataTextField = "Title";
        lsb4.DataBind();
    }

    protected void GetLookupList_RunTimeParam(object sender, List<Type> ParamTypes, List<object> ParamValues)
    {
        ParamTypes.Add(typeof(long));
        ParamValues.Add((this.Page as BaseManagementPage<SystemEventBusiness, SystemEvent, SystemEventPattern, pgcEntities>).SelectedID);
    }

    public override bool Validate(ManagementPageMode Mode)
    {
        page = (this.Page as BaseManagementPage<SystemEventBusiness, SystemEvent, SystemEventPattern, pgcEntities>);

        SystemEvent = page.Business.Retrieve(page.SelectedID);


        if ((   (lkpCustomRole.GetSelectedValue<int>()==(int)Role.User && chbCustom_Role_Email.Checked)||
                chbRelated_Guest_Email.Checked ||
                chbRelated_User_Email.Checked) &&
                string.IsNullOrEmpty(htmlEmailUser.Text))
        {
            UserSession.AddMessage(UserMessageKey.PreventSaveCauseHaveEmailUserAction);
            return false;
        }


        if ((   (lkpCustomRole_SMS.GetSelectedValue<int>() == (int)Role.User && chbCustom_Role_SMS.Checked) ||
                chbRelated_Guest_SMS.Checked ||
                chbRelated_User_SMS.Checked) &&
                string.IsNullOrEmpty(txtSMSUser.TextBody))
        {
            UserSession.AddMessage(UserMessageKey.PreventSaveCauseHaveSMSUserAction);
            return false;
        }


        if ((   (lkpCustomRole.GetSelectedValue<int>()!=(int)Role.User && chbCustom_Role_Email.Checked)||
                (lkpCustomAccess.GetSelectedValue<long>()>0 && chbCustom_Access_Email.Checked)||
                (lkpCustomUser.GetSelectedValue<long>()>0&& chbCustom_User_Email.Checked)||
                !string.IsNullOrEmpty(txtManual_Email.Text) ||
                chbRelated_Branch_Email.Checked||
                chbRelated_Doer_Email.Checked) &&
                string.IsNullOrEmpty(htmlEmailAdmin.Text))
        {
            UserSession.AddMessage(UserMessageKey.PreventSaveCauseHaveEmailAdminAction);
            return false;
        }

        if ((   (lkpCustomRole_SMS.GetSelectedValue<int>()!=(int)Role.User && chbCustom_Role_SMS.Checked)||
                (lkpCustomAccess_SMS.GetSelectedValue<long>() > 0 && chbCustom_Access_SMS.Checked) ||
                (lkpCustomUser_SMS.GetSelectedValue<long>() > 0 && chbCustom_User_SMS.Checked) || 
                !string.IsNullOrEmpty(txtManual_SMS.Text) ||
                chbRelated_Doer_SMS.Checked) &&
                string.IsNullOrEmpty(txtSMSAdmin.TextBody))
        {
            UserSession.AddMessage(UserMessageKey.PreventSaveCauseHaveSMSAdminAction);
            return false;
        }


        if ((chbCustom_Access_Email.Checked && lkpCustomAccess.GetSelectedValue<long>() < 1) || (chbCustom_Access_SMS.Checked && lkpCustomAccess_SMS.GetSelectedValue<long>() < 1))
        {
            UserSession.AddMessage(UserMessageKey.PreventSaveCauseAccessNotSelect);
            return false;
        }
        

        if ((chbCustom_Role_Email.Checked && !BasePattern.IsEnumAssigned(lkpCustomRole.GetSelectedValue<Role>()))||
            (chbCustom_Role_SMS.Checked && !BasePattern.IsEnumAssigned(lkpCustomRole_SMS.GetSelectedValue<Role>())))
        {
            UserSession.AddMessage(UserMessageKey.PreventSaveCauseRoleNotSelect);
            return false;
        }

        if ((chbCustom_Role_Email.Checked && lkpCustomRole.GetSelectedValue<long>()<1) ||
            (chbCustom_Role_SMS.Checked && lkpCustomRole_SMS.GetSelectedValue<long>() < 1))
        {
            UserSession.AddMessage(UserMessageKey.PreventSaveCauseUserNotSelect);
            return false;
        }       
        return base.Validate(Mode);
    }

    public SystemEvent SystemEvent = new SystemEvent();
    BaseManagementPage<SystemEventBusiness, SystemEvent, SystemEventPattern, pgcEntities> page = new BaseManagementPage<SystemEventBusiness, SystemEvent, SystemEventPattern, pgcEntities>();
}