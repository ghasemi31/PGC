using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using System.Collections.Generic;
using pgc.Model.Enums;

namespace pgc.Business
{
    public class AccessLevelBusiness:BaseEntityManagementBusiness<AccessLevel,pgcEntities>
    {
        public AccessLevelBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, AccessLevelPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.AccessLevels, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Role,
                    f.Title
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(AccessLevelPattern Pattern)
        {
            return Search_Where(Context.AccessLevels, Pattern).Count();
        }

        public IQueryable<AccessLevel> Search_Where(IQueryable<AccessLevel> list, AccessLevelPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern

            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title) );

            if (BasePattern.IsEnumAssigned(Pattern.Role))
                list = list.Where(f => f.Role == (int)Pattern.Role);


            return list;
        }

        public override OperationResult Delete(long ID)
        {
            AccessLevel Data = Retrieve(ID);
            if (Data.Users != null && Data.Users.Count > 0)
            {
                OperationResult op = new OperationResult();
                op.Result = ActionResult.Failed;
                op.AddMessage(Model.Enums.UserMessageKey.FirstMoveOrDeleteUsersForAccessLevel);
                return op;
            }

            return base.Delete(ID);
        }

        public List<Feature> GetFeaturesForRole(Role role)
        {
            if (role == Role.Admin)
            {
                return Context.Features.Where(f => f.CanGrantToAdmin).OrderBy(f => f.DisplayOrder).ToList();
            }
            else if (role == Role.User)
            {
                return Context.Features.Where(f => f.CanGrantToUser).OrderBy(f=>f.DisplayOrder).ToList();
            }
            else if (role == Role.Agent)
            {
                return Context.Features.Where(f => f.CanGrantToAgent).OrderBy(f => f.DisplayOrder).ToList();
            }
            return null;
        }

        public List<Feature> GetCurrentFeatures(long AccessLevel_ID)
        {
            AccessLevel temp = Context.AccessLevels.SingleOrDefault(a => a.ID == AccessLevel_ID);
            return temp.Features.ToList();
        }

        public Feature RetriveFeature(long ID)
        {
            return Context.Features.SingleOrDefault(f => f.ID == ID);
        }
    }
}