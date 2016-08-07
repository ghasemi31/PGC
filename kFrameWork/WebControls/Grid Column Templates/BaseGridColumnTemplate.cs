using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kFrameWork.WebControls
{
    public class BaseGridColumnTemplate : DataControlField, ITemplate
    {
        public Control Container { get; set; }        
        public HKGrid Grid { get; set; }

        protected int Index
        {
            get { return this.Grid.Columns.IndexOf(this as DataControlField); }
        }

        protected override DataControlField CreateField()
        {
            return this;
        }

        public void InstantiateIn(Control container)
        {
            this.Container = container;
            this.Grid = Container as HKGrid;
        }
    }
}
