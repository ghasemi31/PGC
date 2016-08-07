using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kFrameWork.Business;
using System.Data.Objects.DataClasses;
using System.Web.UI.WebControls;
using kFrameWork.Model;
using kFrameWork.Util;
using kFrameWork.Enums;
using kFrameWork.WebControls;
using System.Web.UI;
using System.Data.Objects;
using pgc.Model;
using pgc.Model.Enums;

namespace kFrameWork.UI
{
    public class BaseManagementPage<BusinessType,EntityType,PatternType,ContextType> : BasePage
        where BusinessType : BaseEntityManagementBusiness<EntityType,ContextType> 
        where EntityType : EntityObject ,  new() 
        where PatternType : BasePattern
        where ContextType: ObjectContext
    {
        public BaseDetailControl<EntityType> DetailControl { get; set; }
        public BaseSearchControl<PatternType> SearchControl{ get; set; } 
        public BaseListControl ListControl{ get; set; }
        public BusinessType Business { get; set; }
        public PatternType CurrentPattern
        {
            get
            {
                if (this.ViewState["CurrentPattern"] == null)
                    return null;
                return this.ViewState["CurrentPattern"] as PatternType;
            }
            set
            {
                this.ViewState["CurrentPattern"] = value;
            }
        }
        public long SelectedID
        {
            get
            {
                if (this.ViewState["SelectedID"] == null)
                    return -1;
                return (long)this.ViewState["SelectedID"];
            }
            set
            {
                this.ViewState["SelectedID"] = value;
            }
        }
        public ManagementPageMode Mode
        {
            get
            {
                if (this.ViewState["Mode"] == null)
                    return ManagementPageMode.Search;
                return (ManagementPageMode)this.ViewState["Mode"];
            }
            set
            {
                this.ViewState["Mode"] = value;
            }
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);

            this.ListControl.DataSource.Selecting += new ObjectDataSourceSelectingEventHandler(SelectDataSource_Selecting);
            this.ListControl.DataSource.TypeName = typeof(BusinessType).FullName;
            this.ListControl.DataSource.SelectMethod = "Search_Select";
            this.ListControl.DataSource.SelectCountMethod = "Search_Count";

            this.ListControl.Edit += new BaseListControl.EditDelegate(Edit);
            this.ListControl.Delete +=new BaseListControl.DeleteDelegate(Delete);
            this.ListControl.Command +=new BaseListControl.CommandDelegate(Command);
            this.ListControl.Add +=new BaseListControl.AddDelegate(Add);
            this.ListControl.BulkDelete += new BaseListControl.BulkDeleteDelegate(BulkDelete);
        }
        protected override void  OnLoad(EventArgs e)
        {
 	        base.OnLoad(e);

            if (!this.IsPostBack)
                this.Mode = ManagementPageMode.Search;

            this.ListControl.Grid.PageSize = (this as BasePage).Entity.PageSize;
        }
        protected void SelectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["Pattern"] = CurrentPattern;
        }
        public virtual void Select(PatternType Pattern)
        {
            this.CurrentPattern = Pattern;
            this.ListControl.DataSource.SelectParameters.Clear();
            this.ListControl.DataSource.Select();
            this.ListControl.DataBind(true);
        }
        public virtual void Search(object Sender, EventArgs e)
        {
            Select(SearchControl.Pattern);
        }
        public virtual void SearchAll(object Sender, EventArgs e)
        {
            if (this.SearchControl.SearchAllPattern != null)
            {
                this.SearchControl.Pattern = this.SearchControl.SearchAllPattern;
                Select(this.SearchControl.SearchAllPattern);
            }
            else
            {
                this.SearchControl.Reset();
                Select(null);
            }
        }
        public virtual void Add(object Sender, EventArgs e)
        {
            SelectedID = 0;
            this.Mode = ManagementPageMode.Add;
            this.DetailControl.BeginMode(ManagementPageMode.Add);
        }
        public virtual void Edit(object Sender, GridViewCommandEventArgs e)
        {
            this.Mode = ManagementPageMode.Edit;
            SelectedID = ConvertorUtil.ToInt64(ListControl.Grid.DataKeys[ConvertorUtil.ToInt32(e.CommandArgument)].Value);
            EntityType Entity = this.Business.Retrieve(SelectedID);
            this.DetailControl.SetEntity(Entity, ManagementPageMode.Edit);
            this.DetailControl.BeginMode(ManagementPageMode.Edit);
        }
        public virtual void Delete(object Sender, GridViewCommandEventArgs e)
        {
            long DeletingID = ConvertorUtil.ToInt64(ListControl.Grid.DataKeys[ConvertorUtil.ToInt32(e.CommandArgument)].Value);
            OperationResult ValidationResult = Business.Validate(new List<long>() { DeletingID }, DeleteValidationMode.Delete);
            if (ValidationResult.Result != ActionResult.Done)
            {
                UserSession.AddMessage(ValidationResult.Messages);
                return;
            }
            OperationResult Res = Business.Delete(DeletingID);
            UserSession.AddMessage(Res.Messages);
            if (Res.Result != ActionResult.Done)
                return;
            this.ListControl.DataSource.SelectParameters.Clear();
            this.ListControl.DataSource.Select();
            this.ListControl.DataBind(false);
            //Handle Last Page Missing
            if (this.ListControl.Grid.Rows.Count == 0)
            {
                this.ListControl.DataSource.SelectParameters.Clear();
                this.ListControl.DataSource.Select();
                this.ListControl.DataBind(true);
            }
        }
        public virtual void Command(object Sender, GridViewCommandEventArgs e)
        {

        }
        public virtual void Save(object Sender, EventArgs e)
        {
            if (this.Mode == ManagementPageMode.Add)
            {
                if (!this.DetailControl.Validate(this.Mode))
                    return;
                EntityType Entity = new EntityType();
                this.DetailControl.GetEntity(Entity, this.Mode);
                OperationResult ValidationResult = Business.Validate(Entity, SaveValidationMode.Add);
                if (ValidationResult.Result != ActionResult.Done)
                {
                    UserSession.AddMessage(ValidationResult.Messages);
                    return;
                }
                OperationResult Res = Business.Insert(Entity);
                UserSession.AddMessage(Res.Messages);
                if (Res.Result != ActionResult.Done)
                    return;
                this.DetailControl.EndMode(this.Mode);
                SearchAll(null,null);
                this.Mode = ManagementPageMode.Search;
            }
            else if (this.Mode == ManagementPageMode.Edit)
            {
                EntityType Entity = this.Business.Retrieve(SelectedID);
                if (!this.DetailControl.Validate(this.Mode))
                    return;
                this.DetailControl.GetEntity(Entity, this.Mode);
                OperationResult ValidationResult = Business.Validate(Entity, SaveValidationMode.Edit);
                if (ValidationResult.Result != ActionResult.Done)
                {
                    UserSession.AddMessage(ValidationResult.Messages);
                    return;
                }
                OperationResult Res = Business.Update(Entity);
                UserSession.AddMessage(Res.Messages);
                if (Res.Result != ActionResult.Done)
                    return;
                this.DetailControl.EndMode(this.Mode);
                this.ListControl.DataSource.SelectParameters.Clear();
                this.ListControl.DataSource.Select();
                this.ListControl.DataBind(false);
                this.Mode = ManagementPageMode.Search;
            }
        }
        public virtual void Cancel(object Sender, EventArgs e)
        {
            this.DetailControl.EndMode(this.Mode);
            this.Mode = ManagementPageMode.Search;
        }
        public virtual void BulkDelete(object Sender, EventArgs e)
        {
            if ((this.ListControl.Grid as HKGrid).SelectedIDs.Count == 0)
            {
                UserSession.AddMessage(UserMessageKey.NoItemSelectedForBulkOperation);
                return;
            }
            OperationResult ValidationResult = Business.Validate((this.ListControl.Grid as HKGrid).SelectedIDs.List, DeleteValidationMode.BulkDelete);
            if (ValidationResult.Result != ActionResult.Done)
            {
                UserSession.AddMessage(ValidationResult.Messages);
                return;
            }
            OperationResult Res = Business.BulkDelete((this.ListControl.Grid as HKGrid).SelectedIDs.List);
            UserSession.AddMessage(Res.Messages);

            if (Res.Result == ActionResult.Done || Res.Result == ActionResult.DonWithFailure)
            {
                this.ListControl.DataSource.SelectParameters.Clear();
                this.ListControl.DataSource.Select();
                this.ListControl.DataBind(false);
                //Handle Last Page Missing
                if (this.ListControl.Grid.Rows.Count == 0)
                {
                    this.ListControl.DataSource.SelectParameters.Clear();
                    this.ListControl.DataSource.Select();
                    this.ListControl.DataBind(true);
                }
            }
            // Can also Add Data of Result (RowsAffected) to UserSession to be shown to user
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!this.IsPostBack)
            {
                if (this.SearchControl.DefaultPattern != null)
                {
                    this.SearchControl.Pattern = this.SearchControl.DefaultPattern;
                    Select(this.SearchControl.DefaultPattern);
                }
                else
                {
                    this.SearchControl.Reset();
                    Select(null);
                }
            }

            switch (this.Mode)
            {
                case ManagementPageMode.Search:
                    if (this.SearchControl != null) { this.SearchControl.Visible = true; }
                    if (this.ListControl != null) { this.ListControl.Visible = true; }
                    if (this.DetailControl != null) { this.DetailControl.Visible = false; }
                    break;
                case ManagementPageMode.Add:
                case ManagementPageMode.Edit:
                    if (this.SearchControl != null) { this.SearchControl.Visible = false; }
                    if (this.ListControl != null) { this.ListControl.Visible = false; }
                    if (this.DetailControl != null) { this.DetailControl.Visible = true; }
                    break;
            }
        }
    }
}
