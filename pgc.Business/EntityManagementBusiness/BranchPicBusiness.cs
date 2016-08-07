using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class BranchPicBusiness:BaseEntityManagementBusiness<BranchPic,pgcEntities>
    {
        public BranchPicBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, BranchPicPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.BranchPics, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    Branch = f.Branch.Title
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(BranchPicPattern Pattern)
        {
            return Search_Where(Context.BranchPics, Pattern).Count();
        }

        public IQueryable<BranchPic> Search_Where(IQueryable<BranchPic> list, BranchPicPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern

            if (Pattern.Branch_ID > 0)
                list = list.Where(f => f.Branch_ID == Pattern.Branch_ID);

            return list;
        }
        
        public override kFrameWork.Model.OperationResult Insert(pgc.Model.BranchPic Data)
        {
            Data.ThumbPath = IOUtil.MakeThumbnailOf(Data.ImagePath, 58, 54, "BranchThumb");
            return base.Insert(Data);
        }

        public override OperationResult Update(BranchPic Data)
        {
            IOUtil.DeleteFile(Data.ThumbPath,true);

            Data.ThumbPath = IOUtil.MakeThumbnailOf(Data.ImagePath, 58, 54, "BranchThumb");
            return base.Update(Data);
        }


    }
}