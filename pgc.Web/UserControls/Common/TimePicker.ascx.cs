using System;
using kFrameWork.UI;

public partial class UserControl_TimePicker : BaseUserControl
{
    public string SelectedTime
    {
        get
        {
            return string.Concat(Hour.SelectedValue, ":", Minute.SelectedValue);
        }
        set
        {
            if (string.IsNullOrEmpty(value) || !value.Contains(":"))
            {
                Hour.SelectedValue = "00";
                Minute.SelectedValue = "00";
                return;
            }
            string[] Data=value.Split(':');
           
            Hour.SelectedValue = Data[0];
            Minute.SelectedValue = Data[1];
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        Hour.SelectedValue = DateTime.Now.TimeOfDay.ToString().Substring(0, 2);
        
        int SelectedMinutes=int.Parse(DateTime.Now.TimeOfDay.ToString().Substring(3, 2));
        SelectedMinutes = 5 * ((int)SelectedMinutes / 5);        
        Minute.SelectedValue = ((SelectedMinutes<10)?"0":"")+SelectedMinutes.ToString();
    }    
}