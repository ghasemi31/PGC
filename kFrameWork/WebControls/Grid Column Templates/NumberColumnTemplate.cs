using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.Util;

namespace kFrameWork.WebControls
{
    public class NumberColumnTemplate : BaseGridColumnTemplate
    {
        public bool CommaSeparated { get; set; }
        public string UnitText { get; set; }
        public string DataField { get; set; }

        public override bool Initialize(bool sortingEnabled, System.Web.UI.Control control)
        {
            base.InstantiateIn(control);
            this.Grid.RowDataBound +=new GridViewRowEventHandler(Grid_RowDataBound);
            return base.Initialize(sortingEnabled, control);
        }

        void Grid_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.DataItem != null)
            {
                if (CommaSeparated)
                    e.Row.Cells[Index].Text = UIUtil.GetCommaSeparatedOf(DataBinder.Eval(e.Row.DataItem, DataField).ToString());
                else
                    e.Row.Cells[Index].Text = DataBinder.Eval(e.Row.DataItem, DataField).ToString();
                if (UnitText != "")
                    e.Row.Cells[Index].Text += " " + UnitText;
            }
        }
    }
}
