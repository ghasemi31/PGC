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
