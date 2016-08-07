using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.Objects.DataClasses;
using kFrameWork.Util;
using kFrameWork.Enums;



namespace kFrameWork.UI
{
    public abstract class BaseDetailControl<EntityType> : UserControl 
        where EntityType : EntityObject
    {
        #region Events

        public delegate void SaveDelegate(object Sender, EventArgs e);
        public event SaveDelegate Save;

        protected void OnSave(object Sender, EventArgs e)
        {
            if (this.Save != null)
                Save(Sender, e);
        }

        public delegate void CancelDelegate(object Sender, EventArgs e);
        public event CancelDelegate Cancel;

        protected void OnCancel(object Sender, EventArgs e)
        {
            if (this.Cancel != null)
                Cancel(Sender, e);
        }

        #endregion

        public virtual bool Validate(ManagementPageMode Mode)
        {
            return true;
        }

        public virtual void BeginMode(ManagementPageMode Mode)
        {
            if (Mode == ManagementPageMode.Add)
            {
                Reset();
            }
        }

        public virtual void EndMode(ManagementPageMode Mode)
        {

        }

        public virtual void Reset()
        {
            //Generic clearnce of controls
            UIUtil.ClearControls(this.Controls);
        }

        /// <summary>
        /// Reads the control values and fill out  the given entity
        /// </summary>
        /// <param name="Data">a default entity to be filled by control values</param>
        /// <returns>Given entity which is filled by control values</returns>
        public abstract EntityType GetEntity(EntityType Data, ManagementPageMode Mode);

        /// <summary>
        /// Shows the given entity values in controls
        /// </summary>
        /// <param name="Data">Entity to be shown in controls</param>
        public abstract void SetEntity(EntityType Data, ManagementPageMode Mode);
    }
}