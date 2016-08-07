using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business.General
{
    public class GuestBusiness
    {
        pgcEntities db = new pgcEntities();
        public List<MainMenu> GetMainMenu()
        {
            return db.MainMenus.Where(m => m.DispInOtherPage == true).OrderBy(m => m.DisplayOrder).ToList();
        }

        public List<Product> GetDefaultFoodSlider()
        {
            return db.Products.Where(p => p.DisplayInSlider == true && p.Accessories == false && p.IsActive == true).OrderBy(p => p.DispOrder).ToList();
        }
    }
}
