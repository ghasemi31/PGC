using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using kFrameWork.Util;
using System.Web.UI.WebControls;
using pgc.Business;

public partial class Pages_Admin_UserComment_Detail : BaseDetailControl<UserComment>
{
    public UserComment userComment = new UserComment();

    public override UserComment GetEntity(UserComment Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new UserComment();

        Data.Status = lkcStatus.GetSelectedValue<int>();
        Data.Type = lkcType.GetSelectedValue<int>();
        if (lkcBranch.GetSelectedValue<long>() == -1)
            Data.Branch_ID = null;
        else
        {
            Data.Branch_ID = lkcBranch.GetSelectedValue<long>();
            Data.BranchTitle = new BranchBusiness().Retrieve(lkcBranch.GetSelectedValue<long>()).Title;
        }
        //Data.UCPersianDate=pdpUCPersianDate.PersianDate;
        //Data.UCDate = DateUtil.GetEnglishDateTime(pdpUCPersianDate.PersianDate);

        return Data;
    }

    public override void SetEntity(UserComment Data, ManagementPageMode Mode)
    {
        userComment = Data;
        lblName.Text = Data.Name;
        lblTopic.Text = Data.Topic;
        lblEmail.Text = Data.Email;
        lblBody.Text = Data.Body;
        lkcStatus.SetSelectedValue(Data.Status);
        lkcType.SetSelectedValue(Data.Type);
        lblUCPersianDate.Text= DateUtil.GetPersianDateWithTime(Data.UCDate).ToString();
        if (Data.Branch_ID == null)
            
            lkcBranch.SetSelectedValue(-1);
        else
            lkcBranch.SetSelectedValue(Data.Branch_ID);

    }
}