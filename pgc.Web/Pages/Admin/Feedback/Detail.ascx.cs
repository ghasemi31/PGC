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

public partial class Pages_Admin_Feedback_Detail : BaseDetailControl<Feedback>
{
    public override Feedback GetEntity(Feedback Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Feedback();
        //Data.IsDisplay = chkIsDisplay.Checked;
        return Data;
    }

    public override void SetEntity(Feedback Data, ManagementPageMode Mode)
    {
        //chkIsDisplay.Checked = Data.IsDisplay;
        txtName.Text = Data.FullName;
        txtEmail.Text = Data.Email;
        lblGameManager.Text = Data.GameManager.ToString();
        txtDate.Text = DateUtil.GetPersianDateWithTime(Data.Date);
        txtBody.Text = Data.Body;
    }
}