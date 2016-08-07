using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Business;

public partial class Pages_Admin_SystemEvent_Search : BaseSearchControl<SystemEventPattern>
{
    public override SystemEventPattern Pattern
    {
        get
        {
            return new SystemEventPattern()
            {
                Title = txtTitle.Text,
                //Support_Manual_Email = lkpSupport_Manual_Email.GetSelectedValue<BooleanStatus>(),
                //Support_Manual_SMS = lkpSupport_Manual_SMS.GetSelectedValue<BooleanStatus>(),
                
                //Support_Every_Admin_Email = lkpSupport_Every_Admin_Email.GetSelectedValue<BooleanStatus>(),
                //Support_Every_Admin_SMS = lkpSupport_Every_Admin_SMS.GetSelectedValue<BooleanStatus>(),
                //Support_Every_User_Email = lkpSupport_Every_User_Email.GetSelectedValue<BooleanStatus>(),
                //Support_Every_User_SMS = lkpSupport_Every_User_SMS.GetSelectedValue<BooleanStatus>(),
                
                //Support_Related_Guest_Email = lkpSupport_Related_Guest_Email.GetSelectedValue<BooleanStatus>(),
                //Support_Related_User_Email = lkpSupport_Related_User_Email.GetSelectedValue<BooleanStatus>(),
                //Support_Related_ImagingCenter_Email = lkpSupport_Related_ImagingCenter_Email.GetSelectedValue<BooleanStatus>(),
                //Support_Related_Department_Email = lkpSupport_Related_Department_Email.GetSelectedValue<BooleanStatus>(),
                //Support_Related_Guest_SMS = lkpSupport_Related_Guest_SMS.GetSelectedValue<BooleanStatus>(),
                //Support_Related_User_SMS = lkpSupport_Related_User_SMS.GetSelectedValue<BooleanStatus>(),
                //Support_Related_ImagingCenter_SMS = lkpSupport_Related_ImagingCenter_SMS.GetSelectedValue<BooleanStatus>(),
                //Support_Related_Department_SMS = lkpSupport_Related_Department_SMS.GetSelectedValue<BooleanStatus>(),

            };
        }
        set
        {
            txtTitle.Text = value.Title;
            //lkpSupport_Manual_Email.SetSelectedValue(value.Support_Manual_Email);
            //lkpSupport_Manual_SMS.SetSelectedValue(value.Support_Manual_SMS);
            
            //lkpSupport_Every_Admin_Email.SetSelectedValue(value.Support_Every_Admin_Email);
            //lkpSupport_Every_Admin_SMS.SetSelectedValue(value.Support_Every_Admin_SMS);
            //lkpSupport_Every_User_Email.SetSelectedValue(value.Support_Every_User_Email);
            //lkpSupport_Every_User_SMS.SetSelectedValue(value.Support_Every_User_SMS);
            
            //lkpSupport_Related_Guest_Email.SetSelectedValue(value.Support_Related_Guest_Email);
            //lkpSupport_Related_User_Email.SetSelectedValue(value.Support_Related_User_Email);
            //lkpSupport_Related_ImagingCenter_Email.SetSelectedValue(value.Support_Related_ImagingCenter_Email);
            //lkpSupport_Related_Department_Email.SetSelectedValue(value.Support_Related_Department_Email);
            //lkpSupport_Related_Guest_SMS.SetSelectedValue(value.Support_Related_Guest_SMS);
            //lkpSupport_Related_User_SMS.SetSelectedValue(value.Support_Related_User_SMS);
            //lkpSupport_Related_ImagingCenter_SMS.SetSelectedValue(value.Support_Related_ImagingCenter_SMS);
            //lkpSupport_Related_Department_SMS.SetSelectedValue(value.Support_Related_Department_SMS);

        }
    }
}