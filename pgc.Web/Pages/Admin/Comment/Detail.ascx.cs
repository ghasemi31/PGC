using kFrameWork.Enums;
using kFrameWork.UI;
using kFrameWork.Util;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Comment_Detail : BaseDetailControl<Comment>
{
    public override Comment GetEntity(Comment Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Comment();
        Data.IsDisplay = chkIsDisplay.Checked;
        return Data;
    }

    public override void SetEntity(Comment Data, ManagementPageMode Mode)
    {
        chkIsDisplay.Checked = Data.IsDisplay;
        txtName.Text = Data.SenderName;
        txtEmail.Text = Data.SenderEmail;
        txtDate.Text = DateUtil.GetPersianDateWithTime(Data.Date);
        txtBody.Text = Data.Body;
    }
}