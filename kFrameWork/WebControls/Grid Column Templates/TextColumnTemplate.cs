using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kFrameWork.WebControls
{
    public class TextColumnTemplate : BaseGridColumnTemplate
    {
        public int MaxLength { get; set; }
        public string DataField { get; set; }

        public override bool Initialize(bool sortingEnabled, System.Web.UI.Control control)
        {
            base.InstantiateIn(control);
            this.Grid.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(TextColumnTemplate_RowDataBound);
            return base.Initialize(sortingEnabled, control);
        }

        void TextColumnTemplate_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.DataItem != null)
            {
                string strText =DataBinder.Eval(e.Row.DataItem, DataField).ToString();
                if (strText.Length > MaxLength && MaxLength != 0)
                {
                    e.Row.Cells[Index].ToolTip = strText;
                    strText  = strText.Substring(0, MaxLength) + " ...";
                }
                e.Row.Cells[Index].Text = strText;
            }
        }
    }
}
