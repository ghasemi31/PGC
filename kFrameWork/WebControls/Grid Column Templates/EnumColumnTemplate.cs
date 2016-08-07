using System;
using System.Web.UI;
using kFrameWork.Util;

namespace kFrameWork.WebControls
{
    public class EnumColumnTemplate : BaseGridColumnTemplate
    {
        protected Type EnumType { get; set; }

        public string DataField { get; set; }

        public string EnumPath { get; set; }

        public string Enum_dllName { get; set; }

        public string ImagesFolderPath { get; set; }

        public ViewType ViewMode { get; set; }

        public ImageType ImageMode { get; set; }

        public enum ViewType
        {
            PersianTitle,
            Image
        }

        public enum ImageType
        {
            png = 1,
            jpg,
            gif
        }

        public override bool Initialize(bool sortingEnabled, System.Web.UI.Control control)
        {
            base.InstantiateIn(control);
            EnumType = Type.GetType(EnumPath+","+Enum_dllName);
            this.Grid.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(EnumColumnTemplate_RowDataBound);
            return base.Initialize(sortingEnabled, control);
        }

        private void EnumColumnTemplate_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow && e.Row.DataItem != null)
            {
                int Value = ConvertorUtil.ToInt32(DataBinder.Eval(e.Row.DataItem, DataField));

                if (ViewMode == ViewType.Image)
                {
                    string strImg = "<img src=\"" + string.Concat(this.Grid.ResolveClientUrl(ImagesFolderPath),Enum.ToObject(EnumType, Value).ToString(),".",ImageMode.ToString()) +  "\" alt=\"\"  />";
                    e.Row.Cells[Index].Text = strImg;
                    e.Row.Cells[Index].ToolTip = EnumUtil.GetEnumElementPersianTitle(Enum.ToObject(EnumType, Value));
                }
                else if (ViewMode == ViewType.PersianTitle)
                {
                    e.Row.Cells[Index].Text = EnumUtil.GetEnumElementPersianTitle(Enum.ToObject(EnumType, Value));
                }
            }
        }
    }
}