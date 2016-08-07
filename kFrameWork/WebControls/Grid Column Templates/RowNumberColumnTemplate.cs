using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace kFrameWork.WebControls
{
    public class RowNumberColumnTemplate:BaseGridColumnTemplate
    {
        public override bool Initialize(bool sortingEnabled, System.Web.UI.Control control)
        {
            base.InstantiateIn(control);

            this.Grid.RowDataBound += new GridViewRowEventHandler(RowNumberColumnTemplate_RowDataBound);

            return base.Initialize(sortingEnabled, control);
        }

        void RowNumberColumnTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int RowNumber = e.Row.RowIndex + (this.Grid.PageIndex* this.Grid.PageSize) + 1;

                e.Row.Cells[this.Index].Text = RowNumber.ToString();
            }
            else if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[Index].Text = HeaderText;
        }
    }
}
