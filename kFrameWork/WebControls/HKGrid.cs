using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.Model;

namespace kFrameWork.WebControls
{
    public class HKGrid : GridView
    {
        public string HK_CssClass { get; set; }

        public string emptyDataText { get; set; }

        public ViewStateDictionary<int, bool> PagesHeaderChecked { get; private set; }

        public ViewStateCollection<long> SelectedIDs { get; private set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PagesHeaderChecked = new ViewStateDictionary<int, bool>(ViewState, "PagesHeaderChecked");
            SelectedIDs = new ViewStateCollection<long>(ViewState, "SelectedIDs");

            this.PagerSettings.Mode = PagerButtons.NumericFirstLast;
            this.ShowHeaderWhenEmpty = true;
            this.GridLines = System.Web.UI.WebControls.GridLines.None;
            this.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            this.AutoGenerateColumns = false;
            if (this.DataKeyNames == null || this.DataKeyNames.Length == 0)
                this.DataKeyNames = new string[] { "ID" };
            this.EmptyDataText = string.IsNullOrEmpty(this.emptyDataText) ? "هیچ سطری برای نمایش یافت نشد" : this.emptyDataText;
            this.EmptyDataRowStyle.CssClass = HK_CssClass + "Empty";
            this.CssClass = HK_CssClass + "Table";
            this.HeaderStyle.CssClass = HK_CssClass + "Header";
            this.RowStyle.CssClass = HK_CssClass + "Row";
            this.AlternatingRowStyle.CssClass = HK_CssClass + "AltRow";
            this.SelectedRowStyle.CssClass = HK_CssClass + "Selected";
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            #region Columns

            int ColumnIndex = 0;
            foreach (DataControlField Col in this.Columns)
            {
                Col.ItemStyle.CssClass = HK_CssClass + "Cell";
                if (Col is ButtonField)
                {
                    Col.ControlStyle.CssClass = HK_CssClass + "Button";
                    if ((Col as ButtonField).CommandName.Equals("DeleteRow"))
                    {
                        foreach (GridViewRow Row in this.Rows)
                        {
                            (Row.Cells[ColumnIndex].Controls[0] as Button).OnClientClick = "if (!confirm('آیا از حذف سطر فعلی اطمینان دارید؟')){return false;}";
                        }
                    }
                }
                ColumnIndex++;
            }

            #endregion Columns

            #region Pager

            if (this.BottomPagerRow != null)
            {
                if (this.BottomPagerRow.Cells[0].Controls[0].Controls[0] != null)
                {
                    (this.BottomPagerRow.Cells[0].Controls[0].Controls[0] as TableRow).CssClass = HK_CssClass + "Pager";
                }

                foreach (Control PageCell in this.BottomPagerRow.Cells[0].Controls[0].Controls[0].Controls)
                {
                    if (PageCell.Controls[0] is LinkButton)
                    {
                        (PageCell.Controls[0] as LinkButton).CssClass = HK_CssClass + "PagerLink";
                    }
                    else
                    {
                        (PageCell.Controls[0] as Label).CssClass = HK_CssClass + "PagerLabel";
                    }
                }

                string strPagerInfo = string.Format("سطر {0} - {1}", (PageIndex * PageSize + 1).ToString(), (PageIndex * PageSize + this.Rows.Count).ToString());
                strPagerInfo += string.Format("&nbsp;/&nbsp; صفحه {0} از {1} ", (PageIndex + 1).ToString(), (PageCount).ToString());

                this.BottomPagerRow.Visible = true;
                this.BottomPagerRow.CssClass = HK_CssClass + "BottomPagerRow";
                this.BottomPagerRow.Cells[0].Controls.Add(new LiteralControl("<span class=\"" + HK_CssClass + "InfoPager\">" + strPagerInfo + "</span>"));
            }

            #endregion Pager
        }
    }
}