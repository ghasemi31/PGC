using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using pgc.Model;
using kFrameWork.Util;
using pgc.Model.Patterns;
using kFrameWork.Model;
using pgc.Model.Enums;

namespace pgc.Business.Core
{
    public class RelayBusiness
    {
        public static void Log(
            string LogTitle,
            HttpRequest Request,
            Exception Ex,
            string From,
            string To,
            string Text,
            long? MessageID,
            int? Status,
            int? Count,
            DateTime? EnteredDate)
        {
            ExceptionHandler.HandleManualException(Ex, "pgc.Business.Core.RelayBusiness");
            return;
        }

        public static bool ReciveMessage(string From, string To, string Text)
        {

            pgcEntities Context = new pgcEntities();
            ReceivedMessage SavedMessage = SaveReceivedMessage(Context, From, To, Text);
            if (SavedMessage == null || SavedMessage.ID == 0)
                return false;

            return true;
        }

        public static ReceivedMessage SaveReceivedMessage(pgcEntities Context, string From, string To, string Text)
        {
            try
            {
                PrivateNo PrivateNumber = Context.PrivateNoes.FirstOrDefault(pn => pn.Number == To);
                if (PrivateNumber == null)
                {
                    try
                    {
                        throw new Exception("ReciveMessage Null PrivateNo ");
                    }
                    catch (Exception Ex)
                    {
                        RelayBusiness.Log("ReciveMessage Null PrivateNo", HttpContext.Current.Request, Ex, From, To, Text, null, null, null, null);
                    }
                    return null;
                }

                ReceivedMessage Message = new ReceivedMessage();
                Message.Body = Text;
                Message.Date = DateTime.Now;
                Message.PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
                Message.PrivateNo_ID = PrivateNumber.ID;
                Message.SenderNumber = From;

                Context.ReceivedMessages.AddObject(Message);
                Context.SaveChanges();

                return Message;
            }
            catch (Exception Ex)
            {
                RelayBusiness.Log("ReciveMessage Exception", HttpContext.Current.Request, Ex, From, To, Text, null, null, null, null);
                return null;
            }
        }
        public static bool UpdateStatus(long MessageID, int Status, int Count, DateTime EnteredDate)
        {
            try
            {
                pgcEntities Context = new pgcEntities();
                SentSMS Message = Context.SentSMSList.FirstOrDefault(sm => sm.GatewayMessageID == MessageID);
                int DatabaseSynchTryCounter = 12;
                while (Message == null && DatabaseSynchTryCounter > 0)
                {
                    System.Threading.Thread.Sleep(4000);
                    Message = Context.SentSMSList.FirstOrDefault(sm => sm.GatewayMessageID == MessageID);
                    DatabaseSynchTryCounter--;
                }

                if (Message == null)
                {
                    try
                    {
                        throw new Exception("RelayBusiness : GatewayMessageID does not exists in db : " + MessageID);
                    }
                    catch (Exception Ex)
                    {
                        RelayBusiness.Log("UpdateStatus Null SentMessage", HttpContext.Current.Request, Ex, null, null, null, MessageID, Status, Count, EnteredDate);
                    }
                    return false;
                }

                switch ((SendSMSStatus)Status)
                {
                    case (SendSMSStatus.DeliveredToPhone):
                        Message.PhoneDeliveryCount++;
                        break;
                    case (SendSMSStatus.NotDeliveredToPhone):
                        Message.PhoneFailCount++;
                        break;
                    case (SendSMSStatus.DeliveredToTelecommunication):
                        Message.TeleDeliveryCount++;
                        break;
                    case (SendSMSStatus.NotDeliveredToTelecommunication):
                        Message.TeleFailCount++;
                        break;
                }

                if (Message.PhoneDeliveryCount >= Message.SMSCount)
                {
                    Message.SendStatus = (int)SendSMSStatus.DeliveredToPhone;
                }
                else if (Message.PhoneFailCount > 0)
                {
                    Message.SendStatus = (int)SendSMSStatus.NotDeliveredToPhone;
                }
                else if (Message.TeleDeliveryCount >= Message.SMSCount)
                {
                    Message.SendStatus = (int)SendSMSStatus.DeliveredToTelecommunication;
                }
                else if (Message.TeleFailCount > 0)
                {
                    Message.SendStatus = (int)SendSMSStatus.NotDeliveredToTelecommunication;
                    // u can roll back financial ,.... be carefull of duplicate call for every page of sms
                }

                Context.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                RelayBusiness.Log("UpdateStatus Exception", HttpContext.Current.Request, Ex, null, null, null, MessageID, Status, Count, EnteredDate);
                return false;
            }
        }
        public static string GetPersianNumberOfMessage(string body)
        {
            string res = body;
            res = res.Replace('0', '٠');
            res = res.Replace('1', '١');
            res = res.Replace('2', '٢');
            res = res.Replace('3', '٣');
            res = res.Replace('4', '٤');
            res = res.Replace('5', '٥');
            res = res.Replace('6', '٦');
            res = res.Replace('7', '٧');
            res = res.Replace('8', '٨');
            res = res.Replace('9', '٩');
            return res;
        }
        public static string GetEnglishNumbersOfMessage(string body)
        {
            string res = body;
            res = res.Replace('٠', '0');
            res = res.Replace('١', '1');
            res = res.Replace('٢', '2');
            res = res.Replace('٣', '3');
            res = res.Replace('٤', '4');
            res = res.Replace('٥', '5');
            res = res.Replace('٦', '6');
            res = res.Replace('٧', '7');
            res = res.Replace('٨', '8');
            res = res.Replace('٩', '9');
            return res;
        }
    }
}
