using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kFrameWork.WebControls
{
    public class PersianDateColumnTemplate:BaseGridColumnTemplate
    {
        public string OriginalDataField{ get; set; }
        public string DataField { get; set; }

        public override bool Initialize(bool sortingEnabled, System.Web.UI.Control control)
        {
            this.InstantiateIn(control);
            this.Grid.RowDataBound += new GridViewRowEventHandler(PersianDateColumnTemplate_RowDataBound);
            return base.Initialize(sortingEnabled, control);
        }

        void PersianDateColumnTemplate_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.DataItem != null )
            {
                if (OriginalDataField == null || DataBinder.Eval(e.Row.DataItem, OriginalDataField) == null)
                {
                    if (DataBinder.Eval(e.Row.DataItem, DataField) != null)
                    {
                        e.Row.Cells[this.Index].Text = DataBinder.Eval(e.Row.DataItem, DataField).ToString();
                    }
                    return;
                }

                DateTime OriginalValue = (DateTime)DataBinder.Eval(e.Row.DataItem, OriginalDataField);

                string strHour = OriginalValue.Hour + ":" + OriginalValue.Minute;
                string strDay = "";
                string strDirection = "rtl";
                if (OriginalValue.Date.Subtract(DateTime.Now.Date).Days == 0)
                    strDay = "امروز";
                else if (OriginalValue.Date.Subtract(DateTime.Now.Date).Days == -1)
                    strDay = "دیروز";
                else
                {
                    strDay = DataBinder.Eval(e.Row.DataItem, DataField).ToString(); ;
                    strDirection = "rtl";
                }

                if (strHour == "0:0")
                    strHour = "";

                e.Row.Cells[this.Index].Text = string.Format("<span dir=\"{2}\">{0} &nbsp;&nbsp; {1}</span>", strDay, strHour, strDirection);
                e.Row.Cells[this.Index].ToolTip = kFrameWork.Util.DateUtil.GetPersianDate(OriginalValue) + "    " + strHour;
                
            }
        }
        
    }
}
