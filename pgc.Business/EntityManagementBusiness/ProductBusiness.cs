using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using kFrameWork.UI;
using pgc.Model.Enums;
using System.Collections.Generic;

namespace pgc.Business
{
    public class ProductBusiness:BaseEntityManagementBusiness<Product,pgcEntities>
    {
        public ProductBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, ProductPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.Products, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.Body,
                    f.IsActive
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(ProductPattern Pattern)
        {
            return Search_Where(Context.Products, Pattern).Count();
        }

        public IQueryable<Product> Search_Where(IQueryable<Product> list, ProductPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title) ||
                    f.Body.Contains(Pattern.Title));

            if (BasePattern.IsEnumAssigned(Pattern.AllowOnlineOrder))
            {
                bool allow = bool.Parse(Pattern.AllowOnlineOrder.ToString());
                list = list.Where(f => f.AllowOnlineOrder == allow);
            }
          
            return list;
        }

        public override OperationResult Insert(Product Data)
        {
            OperationResult op = base.Insert(Data); ;

            if (op.Result == ActionResult.Done)
            {
                //Product_Action
                #region Product_Action

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);


                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%body%", Data.Body);
                eArg.EventVariables.Add("%title%", Data.Title);
                eArg.EventVariables.Add("%price%", Data.Price.ToString());
                eArg.EventVariables.Add("%action%", "ثبت محصول جدید");

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Product_Action, eArg);

                #endregion
            }
            return op;
        }

        public override OperationResult Update(Product Data)
        {
            OperationResult op = base.Update(Data);

            if (op.Result == ActionResult.Done)
            {
                //Product_Action
                #region Product_Action

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%body%", Data.Body);
                eArg.EventVariables.Add("%title%", Data.Title);
                eArg.EventVariables.Add("%price%", Data.Price.ToString());
                eArg.EventVariables.Add("%action%", "بروزرسانی محصول");

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Product_Action, eArg);

                #endregion
            }

            return op;
        }


        public override OperationResult Delete(long ID)
        {
            Product Data = new ProductBusiness().Retrieve(ID);
            OperationResult op = base.Delete(ID);

            if (op.Result == ActionResult.Done)
            {
                //User_Action_Admin
                #region User_RstPassword_Admin

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);


                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%",doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%body%", Data.Body);
                eArg.EventVariables.Add("%title%", Data.Title);
                eArg.EventVariables.Add("%price%", Data.Price.ToString());
                eArg.EventVariables.Add("%action%", "حذف محصول");

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Product_Action, eArg);

                #endregion
            }
            return op;
        }

        public IQueryable<Product> GetAllProduct()
        {
            return Context.Products.OrderBy(f => f.Title);
        }

        //public OperationResult UpdateIsDone(long Product_ID)
        //{
        //    Product Data = this.Retrieve(Product_ID);
        //    OperationResult Res = new OperationResult();
        //    if (Data != null)
        //    {
        //        Data.IsDone = !Data.IsDone;
        //        Res=this.Update(Data);
        //    }

        //    return Res;
        //}

        public Material retrieveMaterial(long materialid)
        {
            return Context.Materials.SingleOrDefault(m => m.ID == materialid);
        }

        public List<Material> retriveProductMaterial(Product product)
        {
            return product.Materials.ToList();
        }
    }
}