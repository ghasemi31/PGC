using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using kFrameWork.Model;
using kFrameWork.Util;
using pgc.Model;

namespace kFrameWork.UI
{
    public abstract class BaseSearchControl<PatternType> :UserControl 
        where PatternType : BasePattern 
    {
        #region Events

        public delegate void SearchDelegate(object Sender, EventArgs e);
        public event SearchDelegate Search;

        protected void OnSearch(object Sender, EventArgs e)
        {
            if (Validate())
                if (this.Search != null)
                    Search(Sender, e);
        }

        public delegate void SearchAllDelegate(object Sender, EventArgs e);
        public event SearchAllDelegate SearchAll;

        protected void OnSearchAll(object Sender, EventArgs e)
        {
            //Reset();
            if (this.SearchAll != null)
                SearchAll(Sender, e);
        }

        #endregion

        public virtual bool Validate()
        {
            return true;
        }

        public virtual void Reset()
        {
            //Generic Reset of controls
            UIUtil.ClearControls(this.Controls);
        }

        public abstract PatternType Pattern { get; set; }

        public virtual PatternType DefaultPattern
        {
            get
            {
                return null;
            }
        }

        public virtual PatternType SearchAllPattern
        {
            get
            {
                return null;
            }
        }
    }
}