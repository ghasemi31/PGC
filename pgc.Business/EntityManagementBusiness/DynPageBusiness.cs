using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;

namespace pgc.Business
{
    public class DynPageBusiness:BaseEntityManagementBusiness<DynPage,pgcEntities>
    {
        public DynPageBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, DynPagePattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.DynPages, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.UrlKey
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(DynPagePattern Pattern)
        {
            return Search_Where(Context.DynPages, Pattern).Count();
        }

        public IQueryable<DynPage> Search_Where(IQueryable<DynPage> list, DynPagePattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title));

            if (!string.IsNullOrEmpty(Pattern.Meta))
                list = list.Where(f=>f.MetaDescription.Contains(Pattern.Meta)
                    || f.MetaKeyWords.Contains(Pattern.Meta ));

            if (!string.IsNullOrEmpty(Pattern.Content))
                list = list.Where(f =>f.Body.Contains(Pattern.Content));

            if (!string.IsNullOrEmpty(Pattern.UrlKey))
                list = list.Where(f => f.UrlKey.Contains(Pattern.UrlKey));

            return list;
        }

        
        public override OperationResult Validate(DynPage Data, SaveValidationMode Mode)
        {
            OperationResult res = new OperationResult();
             
            
            if (Context.DynPages.Where(f => f.UrlKey == Data.UrlKey && f.ID != Data.ID).Count() > 0)
            {
                res.Result = ActionResult.Failed;
                res.AddMessage(UserMessageKey.DuplicateUrlKey);
                return res;
            }
            return base.Validate(Data, Mode);
        }


        //public OperationResult UpdateIsDone(long DynPage_ID)
        //{
        //    DynPage Data = this.Retrieve(DynPage_ID);
        //    OperationResult Res = new OperationResult();
        //    if (Data != null)
        //    {
        //        Data.IsDone = !Data.IsDone;
        //        Res=this.Update(Data);
        //    }

        //    return Res;
        //}

    }
}