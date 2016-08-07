using System;
using System.Linq;
using System.Web;
using System.Xml;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using System.Collections.Generic;
using System.Globalization;
using kFrameWork.UI;
using pgc.Model.Enums;

namespace pgc.Business
{
    public class NewsBusiness : BaseEntityManagementBusiness<News, pgcEntities>
    {
        public NewsBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, NewsPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.News, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.Summary
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(NewsPattern Pattern)
        {
            return Search_Where(Context.News, Pattern).Count();
        }

        public IQueryable<News> Search_Where(IQueryable<News> list, NewsPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title) ||
                    f.Summary.Contains(Pattern.Title) || f.Body.Contains(Pattern.Title));

            if (BasePattern.IsEnumAssigned(Pattern.Status))
                list = list.Where(f => f.Status == (int)Pattern.Status);

            return list;
        }

        #region NewsRss

        public override OperationResult Insert(News Data)
        {
            //Data.Status = (int)NewsStatus.Hide;
            OperationResult res = base.Insert(Data);
            if (res.Result == ActionResult.Done)
            {
                UpdateXML();


                //News_Action
                #region News_Action

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);


                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%body%", Data.Body);
                eArg.EventVariables.Add("%summary%", Data.Summary);
                eArg.EventVariables.Add("%action%", "ایجاد");

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.News_Action, eArg);

                #endregion

            }

            //if (res.Messages.Contains(UserMessageKey.Succeed))
            //{
            //    res.Messages.Clear();
            //    res.AddMessage(UserMessageKey.NewsShowAfterConfirm);
            //}
            return res;

        }

        public override OperationResult Update(News Data)
        {
            
            //IF News IS Realy CHANGE Then The Status Get Hide
            News OldNews=new NewsBusiness().Retrieve(Data.ID);
            //if ((Data.Body != OldNews.Body) ||
            //    (Data.KeyWords != OldNews.KeyWords) ||
            //    (Data.NewsDate != OldNews.NewsDate) ||
            //    (Data.NewsPersianDate != OldNews.NewsPersianDate) ||
            //    (Data.NewsPicPath != OldNews.NewsPicPath) ||
            //    (Data.Summary != OldNews.Summary) ||
            //    (Data.Title != OldNews.Title))
            
                //Data.Status = (int)NewsStatus.Hide;
            
            
            
            
            OperationResult res = base.Update(Data);

            if (res.Result == ActionResult.Done)
            {
                UpdateXML();

                //News_Action
                #region News_Action

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);


                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%body%", Data.Body);
                eArg.EventVariables.Add("%summary%", Data.Summary);
                eArg.EventVariables.Add("%action%", "بروزرسانی");

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.News_Action, eArg);

                #endregion
            }


            //if (res.Messages.Contains(UserMessageKey.Succeed)&& Data.Status==(int)NewsStatus.Hide)
            //{
            //    res.Messages.Clear();
            //    res.AddMessage(UserMessageKey.NewsShowAfterConfirm);
            //}

            return res;
        }

        public override OperationResult Delete(long ID)
        {
            News Data = new NewsBusiness().Retrieve(ID);

            OperationResult res = base.Delete(ID);
            if (res.Result == ActionResult.Done)
            {
                UpdateXML();
                //News_Action
                #region News_Action

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);


                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%body%", Data.Body);
                eArg.EventVariables.Add("%summary%", Data.Summary);
                eArg.EventVariables.Add("%action%", "حذف");

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.News_Action, eArg);

                #endregion
            }
            return res;

            //return base.Delete(ID);
        }

        public override OperationResult BulkDelete(List<long> IDs)
        {
            OperationResult res = base.BulkDelete(IDs);
            if (res.Result == ActionResult.Done || res.Result==ActionResult.DonWithFailure)
                UpdateXML();
            return res;

            //return base.BulkDelete(IDs);
        }

        private void UpdateXML()
        {
            string FilePath = HttpContext.Current.Server.MapPath("~/Pages/Guest/RSS.xml");
            XmlWriter Writer = XmlWriter.Create(FilePath);

            //// -- Generate General Information

            Writer.WriteStartDocument();
            Writer.WriteStartElement("rss");
            Writer.WriteAttributeString("version", "2.0");
            Writer.WriteStartElement("channel");

            Writer.WriteStartElement("title");
            Writer.WriteRaw("بخش خبری رستوران مستردیزی");
            Writer.WriteEndElement();

            Writer.WriteStartElement("description");
            Writer.WriteRaw("بخش خبری رستوران مستردیزی");
            Writer.WriteEndElement();

            Writer.WriteStartElement("link");
            Writer.WriteRaw("NewsList.aspx");
            Writer.WriteEndElement();

            // -- Generate Items
            int ShowStatus = (int)NewsStatus.Show;
            foreach (News news in Context.News.OrderByDescending(n => n.NewsDate).Where(f=>f.Status==ShowStatus))
            {
                Writer.WriteStartElement("item");

                    Writer.WriteStartElement("title");
                    Writer.WriteRaw(ByPassFiveCharacterForXML(news.Title));
                    Writer.WriteEndElement();

                    Writer.WriteStartElement("description");
                    Writer.WriteRaw(ByPassFiveCharacterForXML(news.Summary));
                    Writer.WriteEndElement();

                    Writer.WriteStartElement("link");
                    Writer.WriteRaw(string.Format("http://www.pgcizi.com/newsdetails.aspx?NewsId={0}", news.ID));
                    //Writer.WriteRaw(string.Format("NewsDetail.aspx?id={0}", news.ID));
                    Writer.WriteEndElement();

                    Writer.WriteStartElement("newsDate");
                    Writer.WriteRaw(ToRFC822DateString(news.NewsDate));
                    Writer.WriteEndElement();

                Writer.WriteEndElement();
            }


            //// -- End of General Information
            Writer.WriteEndElement();
            Writer.WriteEndElement();
            Writer.WriteEndDocument();

            Writer.Close();
        }

        private static string ToRFC822DateString(DateTime dateTime)
        {
            int offset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;
            string timeZone = "+" + offset.ToString().PadLeft(2, '0');
            if (offset < 0)
            {
                int i = offset * -1;
                timeZone = "-" + i.ToString().PadLeft(2, '0');
            }

            return dateTime.ToString("ddd, dd MMM yyyy HH:mm:ss" + timeZone.PadRight(5, '0'), CultureInfo.GetCultureInfo("en-US"));
        }

        private string ByPassFiveCharacterForXML(string word)
        {
            return string.IsNullOrEmpty(word) ?
                ""
                :
                word.Replace("\"", "&quot;").Replace("'", "&apos;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;");
        }

        #endregion NewsRss
    }
}