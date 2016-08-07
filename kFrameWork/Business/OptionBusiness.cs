using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pgc.Model.Enums;
using pgc.Model;
using kFrameWork.Util;
using kFrameWork.Model;

namespace kFrameWork.Business
{
    public class OptionBusiness
    {
        pgcEntities Context = new pgcEntities();
        public List<OptionCategory> GetCategories()
        {
            return Context.OptionCategories.Where(c => c.Parent_OptionCategory_ID == null).ToList();
        }
        public Option RetriveOption(long OptionID)
        {
            return Context.Options.FirstOrDefault(o => o.ID == OptionID);
        }
        public OperationResult SaveOption()
        {
            OperationResult Res = new OperationResult();
            Context.SaveChanges();
            Res.Result = ActionResult.Done;
            Res.AddMessage(UserMessageKey.Succeed);
            return Res;
        }


        public static string GetValue(OptionKey Key)
        {
            pgcEntities Context = new pgcEntities();
            string strKey = Key.ToString();
            Option option = Context.Options.FirstOrDefault(o => o.Key == strKey);
            if (option == null)
                return "";
            return option.Value;
        }

        public static string GetSmallText(OptionKey Key)
        {
            return GetValue(Key);
        }
        public static string GetText(OptionKey Key)
        {
            return GetValue(Key);
        }
        public static string GetLargeText(OptionKey Key)
        {
            return GetValue(Key);
        }
        public static string GetNText(OptionKey Key)
        {
            return GetValue(Key);
        }
        public static string GetHtml(OptionKey Key)
        {
            return GetValue(Key);
        }
        public static short GetSmallInt(OptionKey Key)
        {
            return ConvertorUtil.ToInt16(GetValue(Key));
        }
        public static int GetInt(OptionKey Key)
        {
            return ConvertorUtil.ToInt32(GetValue(Key));
        }
        public static long GetBigInt(OptionKey Key)
        {
            return ConvertorUtil.ToInt64(GetValue(Key));
        }
        public static byte GetTinyInt(OptionKey Key)
        {
            return ConvertorUtil.ToByte(GetValue(Key));
        }
        public static double GetDouble(OptionKey Key)
        {
            return ConvertorUtil.ToDouble(GetValue(Key));
        }
        public static string GetPhone(OptionKey Key)
        {
            return GetValue(Key);
        }
        public static string GetEmail(OptionKey Key)
        {
            return GetValue(Key);
        }
        public static long GetMoney(OptionKey Key)
        {
            return ConvertorUtil.ToInt64(GetValue(Key));
        }
        public static bool GetBoolean(OptionKey Key)
        {
            return ConvertorUtil.ToBoolean(GetValue(Key));
        }
        public static DateTime? GetDate(OptionKey Key)
        {
            string res = GetValue(Key);
            try{return DateTime.Parse(res);}
            catch {return null;}
        }
        public static string GetPersianDate(OptionKey Key)
        {
            return GetValue(Key);
        }
        public static OptionLookupKey GetLookup(OptionKey Key)
        {
            string res =  GetValue(Key);
            return (OptionLookupKey)ConvertorUtil.ToInt64(res);
        }
        public static string GetFilePath(OptionKey Key)
        {
            return GetValue(Key);
        }
    }
}
