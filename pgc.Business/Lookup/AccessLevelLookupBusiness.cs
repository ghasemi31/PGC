using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Enums;
using kFrameWork.Util;

namespace pgc.Business.Lookup
{
    public class AccessLevelLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();


        public IQueryable GetLookupList()
        {
            var list = from P in Context.AccessLevels
                       orderby P.ID descending
                       select new
                       {
                           P.ID,
                           P.Title
                       };

            return list;
        }

        public IQueryable GetLookupList(object Role)
        {
            if (Role == null)
                return null;

            var list = from P in Context.AccessLevels.Where(c=>c.Role == (int)Role)
                       orderby P.ID descending
                       select new
                       {
                           P.ID,
                           P.Title
                       };

            return list;
        }

        public IQueryable GetLookupList(bool ForEventAction)
        {
            var list = Context.AccessLevels.OrderBy(p => p.Role).ToList();

            Dictionary<long, string> resultUser = new Dictionary<long, string>();
            Dictionary<long, string> resultAdmin = new Dictionary<long, string>();
            Dictionary<long, string> resultAgent = new Dictionary<long, string>();

            for (int i = 0; i < list.Count; i++)
            {
                switch ((Role)list[i].Role)
                {
                    case Role.Admin:
                        resultAdmin.Add(list[i].ID, "(" + EnumUtil.GetEnumElementPersianTitle((Role)list[i].Role) + ")-" + list[i].Title);
                        break;
                    case Role.Agent:
                        resultAgent.Add(list[i].ID, "(" + EnumUtil.GetEnumElementPersianTitle((Role)list[i].Role) + ")---------" + list[i].Title);
                        break;
                    case Role.User:
                    default:
                        resultUser.Add(list[i].ID, "(" + EnumUtil.GetEnumElementPersianTitle((Role)list[i].Role) + ")-----------" + list[i].Title);
                        break;
                }

            }

            return resultAdmin.Union(resultAgent).Union(resultUser).AsQueryable().Select(f => new { ID = f.Key, Title = f.Value });
        }
    }
}
