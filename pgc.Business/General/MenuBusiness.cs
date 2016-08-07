using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business.General
{
    public class MenuBusiness
    {
        pgcEntities db = new pgcEntities();
        public List<MainMenu> PageMenu()
        {
            return db.MainMenus.Where(m => m.DispInOtherPage == true).OrderBy(m => m.DisplayOrder).ToList();
        }
    }
}
