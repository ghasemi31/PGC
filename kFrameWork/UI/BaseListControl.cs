using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.WebControls;

namespace kFrameWork.UI
{
    public class BaseListControl:UserControl
    {
        
        #region Events

        public delegate void EditDelegate(object Sender, GridViewCommandEventArgs e);
        public event EditDelegate Edit;

        public delegate void DeleteDelegate(object Sender, GridViewCommandEventArgs e);
        public event DeleteDelegate Delete;

        public delegate void CommandDelegate(object Sender, GridViewCommandEventArgs e);
        public event CommandDelegate Command;

        public delegate void AddDelegate(object Sender, EventArgs e);
        public event AddDelegate Add;

        protected void OnAdd(object Sender, EventArgs e)
        {
            if (this.Add != null)
                Add(Sender, e);
        }

        public delegate void BulkDeleteDelegate(object Sender, EventArgs e);
        public event BulkDeleteDelegate BulkDelete;

        protected void OnBulkDelete(object Sender, EventArgs e)
        {
            if (this.BulkDelete != null)
                BulkDelete(Sender, e);
        }
        
        #endregion

        public GridView Grid { get; set; }
        public ObjectDataSource DataSource { get; set; }

        protected virtual void Grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "editrow" && this.Edit != null)
                Edit(sender, e);

            if (e.CommandName.ToLower() == "deleterow" && this.Delete != null)
                Delete(sender, e);

            if (this.Command != null)
                Command(sender, e);
        }

        public new void DataBind(bool ResetPageIndex)
        {
            if (ResetPageIndex)
                this.Grid.PageIndex = 0;
            if (this.Grid is HKGrid)
            {
                (this.Grid as HKGrid).SelectedIDs.Clear();
                (this.Grid as HKGrid).PagesHeaderChecked.Clear();
            }
            this.Grid.DataBind();
        }

        
    }
}