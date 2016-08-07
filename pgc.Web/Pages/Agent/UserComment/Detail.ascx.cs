using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using kFrameWork.Util;
using System.Web.UI.WebControls;
using pgc.Model.Enums;

public partial class Pages_Agent_UserComment_Detail : BaseDetailControl<UserComment>
{
    public override UserComment GetEntity(UserComment Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new UserComment();

        Data.Status = lkcStatus.GetSelectedValue<int>();

        return Data;
    }

    public override void SetEntity(UserComment Data, ManagementPageMode Mode)
    {
      
        lblName.Text = Data.Name;
        lblPhone.Text = Data.Phone;
        lblTopic.Text = Data.Topic;
        lblEmail.Text = Data.Email;
        lblBody.Text = Data.Body;
        lkcStatus.SetSelectedValue(Data.Status);
        lblCommentType.Text = EnumUtil.GetEnumElementPersianTitle((UserCommentType)Data.Type);
        lblUCPersianDate.Text= DateUtil.GetPersianDateWithTime(Data.UCDate).ToString();
        lblBranch.Text = Data.Branch.Title;


    }
}