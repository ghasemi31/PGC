using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;

namespace pgc.Business.Lookup
{
    public class SampleLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        //normal lookup
        public IQueryable GetLookupList()
        {
            var list = from P in Context.Samples
                       orderby P.ID descending
                       select new
                       {
                           P.ID,
                           P.Title
                       };

            return list;
        }

        //add default user id
        public IQueryable GetLookupList(long UserID)
        {
            var list = from P in Context.Samples.Top("3")
                       orderby P.ID descending
                       select new
                       {
                           P.ID,
                           P.Title
                       };

            return list;
        }

        //dependon lookup
        public IQueryable GetLookupList_City(object SampleID)
        {
            if (SampleID == null)
                return null;

            var list = from P in Context.Samples.Top(SampleID.ToString())
                       orderby P.ID descending
                       select new
                       {
                           P.ID,
                           P.Title
                       };

            return list;
        }

        //run time param sample
        public IQueryable GetLookupList_RunTimeParam(bool TestBool)
        {
            if (TestBool)
            {
                var list = from P in Context.Samples
                           orderby P.ID descending
                           select new
                           {
                               P.ID,
                               Title = P.EndPersianDate
                           };
                return list;
            }
            else
            {
                var list = from PN in Context.Samples
                           select new
                           {
                               ID = PN.ID,
                               Title = PN.StartPersianDate
                           };
                return list;
            }
        }
    }
}
