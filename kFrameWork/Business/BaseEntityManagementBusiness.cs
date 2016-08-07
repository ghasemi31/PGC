using System.Collections.Generic;
using System.Data;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Data.Objects;
using kFrameWork.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;
using System.Reflection;
using kFrameWork.Util;
using System.IO;


namespace kFrameWork.Business
{
    public class BaseEntityManagementBusiness<EntityType,ContextType>
        where EntityType:EntityObject 
        where ContextType:ObjectContext
    {
        public ContextType Context { get; set; }


        public virtual OperationResult Insert(EntityType Data)
        {
            Context.AddObject(GetEntitySetFullName(), Data);
            Context.SaveChanges();
            return GetSucceedResult();
        }

        public virtual OperationResult Update(EntityType Data)
        {
            Context.ApplyCurrentValues(GetEntitySetFullName(), Data);
            Context.SaveChanges();
            return GetSucceedResult();
        }

        public virtual OperationResult Delete(long ID)
        {
            EntityType Entity = Retrieve(ID);
            OperationResult DeleteDependenciesResult = DeleteDependencies(Entity);
            if (DeleteDependenciesResult.Result != ActionResult.Done)
                return DeleteDependenciesResult;
            Context.ObjectStateManager.ChangeObjectState(Entity, EntityState.Deleted);
            Context.SaveChanges();
            OperationResult FinalRes = GetSucceedResult();
            FinalRes.Messages.AddRange(DeleteDependenciesResult.Messages);
            return FinalRes;
        }

        public virtual OperationResult BulkDelete(List<long> IDs)
        {
            OperationResult Res = new OperationResult();
            int SucceedCount = 0;

            foreach (long ID in IDs)
            {
                OperationResult SingleDeleteResult = Delete(ID);
                if (SingleDeleteResult.Result == ActionResult.Done)
                    SucceedCount++;
            }

            if (SucceedCount == 0)
            {
                Res.Result = ActionResult.Failed;
                Res.AddMessage(UserMessageKey.Failed);
            }
            else if (SucceedCount < IDs.Count)
            {
                Res.Result = ActionResult.DonWithFailure;
                Res.AddMessage(UserMessageKey.Failed);
            }
            else
            {
                Res.Result = ActionResult.Done;
                Res.AddMessage(UserMessageKey.Succeed);
            }
            Res.Data.Add("RowsAffected", SucceedCount);
            return Res;
        }

        public virtual EntityType Retrieve(long ID)
        {
            EntityKey Key = new EntityKey(GetEntitySetFullName(), "ID", ID);
            return (EntityType)Context.GetObjectByKey(Key);
        }

        public virtual OperationResult DeleteDependencies(EntityType Entity)
        {
            OperationResult res = new OperationResult();
            foreach (PropertyInfo property in typeof(EntityType).GetProperties())
            {
                if (property.PropertyType.Name == "String")
                {
                    object val = property.GetValue(Entity, null);
                    if (val != null && val.ToString() != "" && val.ToString().StartsWith("~/"))
                    {
                        //This property can be an ImageUrl field
                        
                        //trying to delete file if exists ...
                        IOUtil.DeleteFile(val.ToString(), true);

                        //trying to delete thumb file if exists ..
                        string fullname = Path.GetFileName(val.ToString());
                        string name = Path.GetFileNameWithoutExtension(val.ToString());
                        string ext = Path.GetExtension(val.ToString());
                        string folder = val.ToString().TrimEnd(fullname.ToCharArray());
                        IOUtil.DeleteFile(folder + name + "_DefaultThumb" + ext, true);
                    }
                }
            }
            //The actual record wont delete unless u return Result.Done
            //But Messages will be returnd to UI in all situations
            res.Result = ActionResult.Done;
            return res;
        }

        private string GetEntitySetFullName()
        {
            string EntityTypeName = typeof(EntityType).Name;
            var Container = Context.MetadataWorkspace.GetEntityContainer(Context.DefaultContainerName, DataSpace.CSpace);
            string EntitySetName = (from meta in Container.BaseEntitySets
                                    where meta.ElementType.Name == EntityTypeName
                                    select meta.Name).First();

            return Container.Name + "." + EntitySetName;
        }

        private OperationResult GetSucceedResult()
        {
            OperationResult Res= new OperationResult();
            Res.Result = ActionResult.Done;
            Res.AddMessage(UserMessageKey.Succeed);
            return Res;
        }

        public virtual OperationResult Validate(EntityType Data, SaveValidationMode Mode)
        {
            return new OperationResult() { Result = ActionResult.Done };
        }

        public virtual OperationResult Validate(List<long> IDs, DeleteValidationMode Mode)
        {
            return new OperationResult() { Result = ActionResult.Done };
        }
    }

    #region Validation Enums

    public enum SaveValidationMode
    {
        Add,
        Edit
    }

    public enum DeleteValidationMode
    {
        Delete,
        BulkDelete
    }

    #endregion
}