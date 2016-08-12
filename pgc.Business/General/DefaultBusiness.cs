using kFrameWork.UI;
using pgc.Model;
using pgc.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business.General
{
    public class DefaultBusiness
    {
        pgcEntities db = new pgcEntities();

        public List<Product> GetDefaultFoodSlider()
        {
            return db.Products.Where(p => p.DisplayInSlider == true && p.Accessories == false && p.IsActive == true).OrderBy(p => p.DispOrder).ToList();
        }

        public IQueryable<News> GetLastNews()
        {
           return db.News.Where(n => n.Status == (int)NewsStatus.Show).OrderByDescending(n => n.NewsDate).Take(kFrameWork.Business.OptionBusiness.GetInt(OptionKey.NewsNumberInHomePage));
        }

        public string GetRandomImage()
        {
            IQueryable<MainSlider> list = db.MainSliders.Where(f => f.IsVisible);
            int count = list.Count();
            int index = new Random().Next(count);
            return list.OrderBy(r => r.ID).Skip(index).FirstOrDefault().ImgPath;
        }

        public List<MainMenu> GetMainMenu()
        {
            return db.MainMenus.Where(m => m.DispInHome == true).OrderBy(m => m.DisplayOrder).ToList();
        }

        public List<SocialIcon> GetSocialIcon()
        {
            return db.SocialIcons.OrderBy(s => s.DispOrder).Take(4).ToList();
        }

        public IQueryable<MainSlider> GetMainSlider()
        {
            return db.MainSliders.Where(r => r.IsVisible).OrderBy(o => o.DispOrder);
        }

        public IQueryable<Game> GetGameLogos()
        {
            return db.Games.Where(r => !string.IsNullOrEmpty(r.LogoPath)).OrderBy(o => o.DispOrder);
        }

    }
}
