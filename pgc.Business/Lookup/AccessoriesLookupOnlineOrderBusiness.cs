using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;
using kFrameWork.Util;

namespace pgc.Business.Lookup
{
    public class AccessoriesOnlineOrderLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        //normal lookup
        public IQueryable GetLookupList()
        {
            var list = Context.Products.Where(p => p.Accessories == true && p.AllowOnlineOrder).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                string pureTitle = list[i].Title.TrimEnd();
                if (pureTitle.Contains("("))
                {
                    pureTitle = pureTitle.Substring(0, pureTitle.IndexOf("(")).TrimEnd();
                }
                list[i].Title = string.Format("{0} ({1} {2})",
                                    pureTitle,
                                    UIUtil.GetCommaSeparatedOf((list[i].Price / 10)),
                                    "تومان");
            }



            return list.AsQueryable();
        }

        
    }
}
