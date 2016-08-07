using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using kFrameWork.Model;
using pgc.Model;
using pgc.Model.Enums;
using kFrameWork.Util;
using pgc.Model.Other.Project;

namespace pgc.Business.General
{
    public class ProductBusiness
    {
        pgcEntities db = new pgcEntities();

        public Product RetriveProduct(long ID)
        {
            return db.Products.Where(f => f.ID == ID).SingleOrDefault();
        }

        public List<Product> GetFoodSlider()
        {
            return db.Products.Where(p => p.DisplayInSlider == true && p.Accessories == false && p.IsActive==true).OrderBy(p => p.DispOrder).ToList();
        }
        public IQueryable Product_List()
        {
            //if (startRowIndex == 0 && maximumRows == 0)
            //    return null;

            var Result = Product_Where(db.Products)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.Body,
                    f.ProductPicPath
                    
                });

             //return Result.Skip(startRowIndex).Take(maximumRows);
            return Result;
        }
        public int Product_Count()
        {
            return Product_Where(db.Products).Count();
        }
        public IQueryable<Product> Product_Where(IQueryable<Product> list)
        {
            return list=db.Products.Where(f => f.Accessories == false);
          //  return list;
        }

        public List<Product> GetProductList()
        {
            return db.Products.Where(c => c.Accessories == false).OrderByDescending(c => c.ID).ToList();
        }

        //public Product RetriveProduct(long ID)
        //{
        //    return db.Products.Where(f => f.ID == ID).SingleOrDefault();
        //}


        public List<Material> GetMaterial(Product p)
        {
            return p.Materials.ToList();
        }

        public List<Comment> GetProductComment(Product p)
        {
            return p.Comments.Where(c=>c.IsDisplay==true).OrderBy(c=>c.Date).ToList();
        }

        public void Like(int num, long id)
        {
            var query = db.Comments.FirstOrDefault(c => c.ID == id);
            query.Like += num;
            db.SaveChanges();
        }

    }
}
