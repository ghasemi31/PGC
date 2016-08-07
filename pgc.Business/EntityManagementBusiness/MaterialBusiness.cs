using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business
{
    public class MaterialBusiness :BaseEntityManagementBusiness<Material,pgcEntities>
    {
        public MaterialBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, MaterialPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.Materials, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.MaterialPicPath
                 });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(MaterialPattern Pattern)
        {
            return Search_Where(Context.Materials, Pattern).Count();
        }

        public IQueryable<Material> Search_Where(IQueryable<Material> list, MaterialPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title));
                      
            return list;
        }

        public List<Material> GetAllMaterial()
        {
            return Context.Materials.OrderBy(m => m.Title).ToList();
        }

       
    }
}
   