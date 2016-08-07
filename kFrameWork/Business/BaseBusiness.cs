using System;
using System.Data;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Linq;
using kFrameWork.Model;
using System.Data.Objects;
using kFrameWork.Enums;
using pgc.Model.Enums;

namespace kFrameWork.Business
{
    public class BaseBusiness
    {
        public ObjectContext Context;
        public BaseBusiness(ObjectContext entities)
        {
            Context = entities;
        }

        public OperationResult Insert(EntityObject Data)
        {
            OperationResult MsgResult = new OperationResult();

            using (Context)
            {
                try
                {
                    Context.AddObject(GetEntitySetFullName(Data), Data);
                    Context.SaveChanges();

                    MsgResult.AddMessage(UserMessageKey.Succeed);
                    MsgResult.Result = ActionResult.Done;

                    return MsgResult;
                }
                catch (Exception ex)
                {
                    MsgResult.AddMessage(UserMessageKey.Failed);
                    MsgResult.Result = ActionResult.Failed;
                    if (ex.InnerException != null)
                        MsgResult.Data.Add("InnerException", ex.InnerException.Message);

                    return MsgResult;
                }
            }
        }

        public OperationResult Update(EntityObject Data)
        {
            OperationResult MsgResult = new OperationResult();

            using (Context)
            {
                try
                {
                    Context.GetObjectByKey(Data.EntityKey);
                    Context.ApplyCurrentValues(GetEntitySetFullName(Data), Data);
                    Context.SaveChanges();

                    MsgResult.AddMessage(UserMessageKey.Succeed);
                    MsgResult.Result = ActionResult.Done;

                    return MsgResult;
                }
                catch (Exception ex)
                {
                    MsgResult.AddMessage(UserMessageKey.Failed);
                    MsgResult.Result = ActionResult.Failed;
                    if (ex.InnerException != null)
                        MsgResult.Data.Add("InnerException", ex.InnerException.Message);

                    return MsgResult;
                }
            }
        }

        public OperationResult Delete(long ID, EntityObject EmptyObj)
        {
            OperationResult MsgResult = new OperationResult();

            using (Context)
            {
                try
                {
                    EntityKey Key = new EntityKey(GetEntitySetFullName(EmptyObj), "ID", ID);
                    EntityObject Obj = (EntityObject)Context.GetObjectByKey(Key);
                    Context.ObjectStateManager.ChangeObjectState(Obj, EntityState.Deleted);
                    Context.SaveChanges();

                    MsgResult.AddMessage(UserMessageKey.Succeed);
                    MsgResult.Result = ActionResult.Done;

                    return MsgResult;
                }
                catch (Exception ex)
                {
                    MsgResult.AddMessage(UserMessageKey.Failed);
                    MsgResult.Result = ActionResult.Failed;
                    if (ex.InnerException != null)
                        MsgResult.Data.Add("InnerException", ex.InnerException.Message);

                    return MsgResult;
                }
            }
        }

        public EntityObject Retrieve(long ID, Type type)
        {
            try
            {
                EntityKey Key = new EntityKey(GetEntitySetFullName((EntityObject)Activator.CreateInstance(type)), "ID", ID);
                return (EntityObject)Context.GetObjectByKey(Key);
            }
            catch
            {
                //ExceptionHandler.HandleManualException(ex);
                return null;
            }
        }

        private string GetEntitySetFullName(EntityObject Entity)
        {
            if (Entity.EntityKey != null)
            {
                return Entity.EntityKey.EntitySetName;
            }
            else
            {
                string EntityTypeName = Entity.GetType().Name;
                var Container = Context.MetadataWorkspace.GetEntityContainer(Context.DefaultContainerName, DataSpace.CSpace);
                string EntitySetName = (from meta in Container.BaseEntitySets
                                        where meta.ElementType.Name == EntityTypeName
                                        select meta.Name).First();

                return Container.Name + "." + EntitySetName;
            }
        }
    }
}