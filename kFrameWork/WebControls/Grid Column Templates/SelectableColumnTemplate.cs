using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace kFrameWork.WebControls
{
    public class SelectableColumnTemplate : BaseGridColumnTemplate
    {
        public override bool Initialize(bool sortingEnabled, System.Web.UI.Control control)
        {
            base.InstantiateIn(control);

            (this.Container as HKGrid).RowCreated += new GridViewRowEventHandler(SelectableColumnTemplate_RowCreated);
            (this.Container as HKGrid).RowDataBound += new GridViewRowEventHandler(SelectableColumnTemplate_RowDataBound);
            (this.Container as HKGrid).Load += new EventHandler(SelectableColumnTemplate_Load);

            return base.Initialize(sortingEnabled, control);
        }

        void SelectableColumnTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Grid.SelectedIDs.Count != 0)
                {
                    long ID = (long)Convert.ToDecimal(this.Grid.DataKeys[e.Row.RowIndex].Value);
                    if (Grid.SelectedIDs.Contains(ID))
                    {
                        CheckBox Chk = (CheckBox)e.Row.Cells[this.Index].FindControl("ChkContent");
                        Chk.Checked = true;
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Header &&
                //ViewState["ChkHeader" + this.Grid.PageIndex.ToString()] != null)
                Grid.PagesHeaderChecked.ContainsKey(Grid.PageIndex))
            {
                //bool ChkState = (bool)ViewState["ChkHeader" + this.Grid.PageIndex.ToString()];
                bool ChkState = Grid.PagesHeaderChecked.GetItem(Grid.PageIndex);
                CheckBox Chk = (CheckBox)e.Row.Cells[this.Index].FindControl("ChkHeader");
                Chk.Checked = ChkState;
            }
        }

        void SelectableColumnTemplate_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if ((e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells[this.Index].FindControl("ChkContent") == null) &&
                (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
            {
                CheckBox ChkContent = new CheckBox(){ID = "ChkContent"};
                string HeaderID = this.Grid.Controls[0].Controls[0].Controls[0].FindControl("ChkHeader").ClientID;
                string ChkContentScript = string.Concat(" var ChkHeaderObj = document.getElementById('",
                HeaderID,
                "'); ",
                " ChkHeaderObj.checked = true; ",
                " $('table >tbody >tr >td >input:checkbox').each(function (index, element) { ",
                "    if (element.checked == false) { ",
                "       ChkHeaderObj.checked = element.checked; ",
                "       return false;",
                "    }",
                " });");

                ChkContent.Attributes["onclick"] = ChkContentScript;
                e.Row.Cells[this.Index].Controls.Add(ChkContent);
            }
            else if (e.Row.RowType == DataControlRowType.Header && e.Row.Cells[this.Index].FindControl("ChkHeader") == null)
            {
                CheckBox ChkHeader = new CheckBox(){ID = "ChkHeader"};
                string ChkHeaderScript = "$('table >tbody >tr >td >input:checkbox').attr('checked',this.checked);";
                ChkHeader.Attributes["onclick"] = ChkHeaderScript;
                e.Row.Cells[this.Index].Controls.Add(ChkHeader);
            }
        }

        void SelectableColumnTemplate_Load(object sender, EventArgs e)
        {
            foreach (GridViewRow Row in this.Grid.Rows)
            {
                long ID = (long)Convert.ToDecimal(this.Grid.DataKeys[Row.RowIndex].Value);
                if (this.Grid.SelectedIDs.Count != 0)
                {
                    if (((CheckBox)Row.Cells[this.Index].FindControl("ChkContent")).Checked)
                    {
                        if (!Grid.SelectedIDs.Contains(ID))
                            Grid.SelectedIDs.AddItem(ID);
                    }
                    else
                    {
                        if (Grid.SelectedIDs.Contains(ID))
                            Grid.SelectedIDs.RemoveItem(ID);
                    }
                }
                else
                {
                    if (((CheckBox)Row.Cells[this.Index].FindControl("ChkContent")).Checked)
                    {
                        Grid.SelectedIDs.AddItem(ID);
                    }
                }
            }
            //ViewState["ChkHeader" + this.Grid.PageIndex.ToString()] = ((CheckBox)this.Grid.HeaderRow.FindControl("ChkHeader")).Checked;
            Grid.PagesHeaderChecked.SetItem(Grid.PageIndex,((CheckBox)this.Grid.HeaderRow.FindControl("ChkHeader")).Checked) ;
        }
    }
}
