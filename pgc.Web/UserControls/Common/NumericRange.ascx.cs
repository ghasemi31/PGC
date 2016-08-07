using System;
using System.Web.UI;
using kFrameWork.Util;
using kFrameWork.Model;
using kFrameWork.UI;
using pgc.Model;

public partial class UserControl_Common_NumericRange : BaseUserControl
{
    public NumericRangePattern Pattern
    {
        get
        {
            return new NumericRangePattern()
            {
                Type = (RangeType)ConvertorUtil.ToInt16(cboType.SelectedValue),
                HasFirstNumber = ntbFirst.HasValue,
                FirstNumber = ntbFirst.GetNumber<long>(),
                HasSecondNumber = ntbSecond.HasValue,
                SecondNumber = ntbSecond.GetNumber<long>()
            };
        }
        set
        {
            cboType.SelectedValue = ConvertorUtil.ToInt16(value.Type).ToString();
            if (value.HasFirstNumber)
                ntbFirst.SetNumber(value.FirstNumber);
            if (value.HasSecondNumber)
                ntbSecond.SetNumber(value.SecondNumber);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //UIUtil.AddScriptReference("~/Scripts/numeric.js", this, ScriptManager.GetCurrent(this.Page));
        UIUtil.AddStartupScript(string.Format("formatRange( '{0}' , '{1}' , '{2}' , '{3}' )", cboType.ClientID, lblAnd.ClientID ,ntbFirst.TextBoxControl.ClientID,ntbSecond.TextBoxControl.ClientID), this);
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        UIUtil.SetAttribute(cboType, "onchange", string.Format("formatRange( '{0}' , '{1}' , '{2}' , '{3}' )",
            cboType.ClientID,
            lblAnd.ClientID,
            ntbFirst.TextBoxControl.ClientID,
            ntbSecond.TextBoxControl.ClientID
            ));

        switch (cboType.SelectedValue)
        {
            case ("0"):
                ntbFirst.TextBoxDisplay = "none";
                lblAnd.Style["display"] = "none";
                ntbSecond.TextBoxDisplay = "none";
                break;
            case ("1"):
            case ("2"):
            case ("3"):
                ntbFirst.TextBoxDisplay = "block";
                lblAnd.Style["display"] = "none";
                ntbSecond.TextBoxDisplay = "none";
                break;
            case ("4"):
                ntbFirst.TextBoxDisplay = "block";
                lblAnd.Style["display"] = "block";
                ntbSecond.TextBoxDisplay = "block";
                break;
        }
    }
}