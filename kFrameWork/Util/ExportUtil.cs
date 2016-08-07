using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Text;

namespace kFrameWork.Util
{
    public class ExportUtil
    {
        public static void Export(string fileName, GridView gv,bool includeGridLines)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;


            switch (Path.GetExtension(fileName).TrimStart('.'))
            {
                case "xls":
                    HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
                    HttpContext.Current.Response.ContentType = "application/ms-excel";
                break;
                case "html":
                    HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
                    HttpContext.Current.Response.ContentType = "text/html";
                break;
                case "doc":
                    HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}",fileName));
                    HttpContext.Current.Response.ContentType = "application/vnd.ms-word ";
                break;
            }
            
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Unicode;
            HttpContext.Current.Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            gv.PageSize = 10000000;
            gv.PageIndex = 0;
            gv.DataBind();

            using (StringWriter sw = new StringWriter())
            {
                
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a form to contain the grid
                    Table table = new Table();

                    if (includeGridLines)
                    {
                        table.GridLines = GridLines.Both;
                    }

                    //  add the header row to the table
                    if (gv.HeaderRow != null)
                    {
                        for (int ColIndex = gv.Columns.Count - 1; ColIndex >= 0; ColIndex--)
                            if (!gv.Columns[ColIndex].Visible)
                                gv.HeaderRow.Cells.RemoveAt(ColIndex);

                        ExportUtil.PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }

                    //  add each of the data rows to the table
                    foreach (GridViewRow row in gv.Rows)
                    {
                        for (int ColIndex = gv.Columns.Count - 1; ColIndex >= 0 ; ColIndex--)
                            if (!gv.Columns[ColIndex].Visible)
                                row.Cells.RemoveAt(ColIndex);

                        ExportUtil.PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    //  add the footer row to the table
                    if (gv.FooterRow != null)
                    {
                        for (int ColIndex = gv.Columns.Count - 1; ColIndex >= 0; ColIndex--)
                            if (!gv.Columns[ColIndex].Visible)
                                gv.FooterRow.Cells.RemoveAt(ColIndex);

                        ExportUtil.PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }

        public static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is System.Web.UI.WebControls.Image)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as System.Web.UI.WebControls.Image).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "√" : ""));
                }
                else if (current is Button)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl(""));
                }
                else if (current is DataControlFieldCell)
                {
                    if ((current as DataControlFieldCell).Text.StartsWith("<img") &&
                        (current as DataControlFieldCell).Controls.Count == 0 &&
                        (current as DataControlFieldCell).ToolTip != "")
                    {
                        (current as DataControlFieldCell).Text = (current as DataControlFieldCell).ToolTip;
                    }
                    //else if ((current as DataControlFieldCell).ContainingField is System.Web.UI.WebControls.ButtonField)
                    //{
                    //    control.Controls.Remove(current);
                    //    control.Controls.AddAt(i, new LiteralControl(""));
                    //}
                }

                if (current.HasControls())
                {
                    ExportUtil.PrepareControlForExport(current);
                }
            }
        }
    }
}