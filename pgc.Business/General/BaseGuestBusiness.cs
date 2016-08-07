using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.UI;

namespace pgc.Business
{
    public class BaseGuestBusiness
    {
        pgcEntities db = new pgcEntities();
        public IQueryable<SiteMapCat> GetSiteMapCategory()
        {
            return db.SiteMapCats.Where(c=>c.IsVisible).OrderBy(c => c.DispOrder); 
        }

        public IQueryable<SiteMapItem> GetSiteMapItem(long id)
        {
            return db.SiteMapItems.Where(s => s.SiteMapCat_ID == id && s.IsVisible).OrderBy(s=>s.DispOrder);
        }

        public List<SideMenuCat> GetSideMenuCategory()
        {
            return db.SideMenuCats.Where(s => s.IsVisible == true).OrderBy(s => s.DisplayOrder).ToList();
        }

        public List<SideMenuItem> GetSideSubMenu(long id)
        {
            return db.SideMenuItems.Where(s => s.SideMenuCat_ID == id && s.IsVisible == true).OrderBy(s => s.DispOrder).ToList();
        }

        public List<SocialIcon> GetSocialIcon()
        {
            return db.SocialIcons.OrderBy(s => s.DispOrder).ToList();
        }

        public List<SocialIcon> GetSideNavSocialIcon()
        {
            return db.SocialIcons.OrderBy(s => s.DispOrder).Take(4).ToList();
        }

      
    }
}
