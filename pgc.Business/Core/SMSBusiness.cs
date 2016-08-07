using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Model;
using pgc.Model;
using pgc.Model.Enums;
using pgc.Model.Patterns;
using kFrameWork.Business;
using System.Text.RegularExpressions;
using kFrameWork.Util;
using pgc.Business.Magfa;
using kFrameWork.UI;

namespace pgc.Business.Core
{

    public class SMSBusiness
    {
        #region Constants

        // *** Thease Constans can be retrived from settings later on , and in the javascript using in MessageControl.asx as well

        /// <summary>
        /// Domain specified by Magfa
        /// </summary>
        //public const string MagfaDomain = "magfa";
        public static string MagfaDomain = OptionBusiness.GetText(OptionKey.MagfaDomain);

        /// <summary>
        /// Username specified by magfa
        /// </summary>
        //public const string MagfaUsername = "sepidan";
        public static string MagfaUsername = OptionBusiness.GetText(OptionKey.MagfaUsername);

        /// <summary>
        /// Password specified by magfa
        /// </summary>
        //public const string MagfaPassword = "13901390";
        public static string MagfaPassword = OptionBusiness.GetText(OptionKey.MagfaPassword);

        /// <summary>
        /// Regular expression with matchs true if a persian letter exists in a checking string
        /// </summary>
        //public const string PersianMessageRegularExpression = "[^\x00-\x7E]";
        public static string PersianMessageRegularExpression = OptionBusiness.GetNText(OptionKey.PersianMessageRegularExpression);

        /// <summary>
        /// Maximum characters in each persian message
        /// </summary>
        //public const int PersianMaxCharacterCount = 70;
        public static int PersianMaxCharacterCount = OptionBusiness.GetTinyInt(OptionKey.PersianMaxCharacterCount);

        /// <summary>
        /// Maximum character in each latin message
        /// </summary>
        //public const int LatinMaxCharacterCount = 160;
        public static int LatinMaxCharacterCount = OptionBusiness.GetTinyInt(OptionKey.LatinMaxCharacterCount);

        /// <summary>
        /// Maxmimum characters in each persian message when there is more than one message on the body
        /// </summary>
        //public const int PersianMultipleMessageReservedCharactersCount = 3;
        public static int PersianMultipleMessageReservedCharactersCount = OptionBusiness.GetTinyInt(OptionKey.PersianMultipleMessageReservedCharactersCount);

        /// <summary>
        /// Maxmimum characters in each latin message when there is more than one message on the body
        /// </summary>
        //public const int LatinMultipleMessageReservedCharactersCount = 7;
        public static int LatinMultipleMessageReservedCharactersCount = OptionBusiness.GetTinyInt(OptionKey.LatinMultipleMessageReservedCharactersCount);

        /// <summary>
        /// The size pf each packet sending to magfa
        /// </summary>
        //public const int PacketSize = 80;
        public static int PacketSize = OptionBusiness.GetTinyInt(OptionKey.PacketSize);

        #endregion Constants

        //public SMSBusiness()
        //{
        //    MagfaDomain = OptionBusiness.GetText(OptionKey.MagfaDomain);
        //    MagfaUsername = OptionBusiness.GetText(OptionKey.MagfaUsername);
        //    MagfaPassword = OptionBusiness.GetText(OptionKey.MagfaPassword);
        //    PacketSize = OptionBusiness.GetTinyInt(OptionKey.PacketSize);

        //    PersianMessageRegularExpression = OptionBusiness.GetNText(OptionKey.PersianMessageRegularExpression);

        //    PersianMaxCharacterCount = OptionBusiness.GetTinyInt(OptionKey.PersianMaxCharacterCount);
        //    LatinMaxCharacterCount = OptionBusiness.GetTinyInt(OptionKey.LatinMaxCharacterCount);

        //    PersianMultipleMessageReservedCharactersCount = OptionBusiness.GetTinyInt(OptionKey.PersianMultipleMessageReservedCharactersCount);
        //    LatinMultipleMessageReservedCharactersCount = OptionBusiness.GetTinyInt(OptionKey.LatinMultipleMessageReservedCharactersCount);
        //}

        public static OperationResult Validate(List<MessagePattern> Messages, PrivateNo PrivateNumber, List<string> Recipients)
        {
            OperationResult ValidateCountsResult = ValidateCounts(Messages, Recipients);
            if (ValidateCountsResult.Result != ActionResult.Done)
            {
                return ValidateCountsResult;
            }

            OperationResult ValidateMessagesResult = ValidateMessages(Messages);
            if (ValidateMessagesResult.Result != ActionResult.Done)
            {
                return ValidateMessagesResult;
            }

            OperationResult ValidateRecipientsResult = ValidateRecipients(Recipients);
            if (ValidateRecipientsResult.Result != ActionResult.Done)
            {
                return ValidateRecipientsResult;
            }

            //PermisionBusiness objPermision = new PermisionBusiness();
            //OperationResult ValidatePrivateNoActivenessResult = objPermision.ValidatePrivateNoActiveness(PrivateNumber);
            //if (ValidatePrivateNoActivenessResult.Result != ActionResult.Done)
            //{
            //    return ValidatePrivateNoActivenessResult;
            //}
            //OperationResult ValidateUserActivenessResult = objPermision.ValidateUserActiveness(UserID);
            //if (ValidateUserActivenessResult.Result != ActionResult.Done)
            //{
            //    return ValidateUserActivenessResult;
            //}



            //FinanceBusiness objFinance = new FinanceBusiness();
            //if (SendType != SendType.TafrihiSend)
            //{
            //    OperationResult ValidateCreditForPrivateNoRateResult = objFinance.ValidateCreditForPrivateNoRate(Rate, Messages, Recipients);
            //    if (ValidateCreditForPrivateNoRateResult.Result != ActionResult.Done)
            //    {
            //        return ValidateCreditForPrivateNoRateResult;
            //    }
            //}
            //else
            //{
            //    OperationResult ValidateCreditForUserBalanceResult = objFinance.ValidateCreditForUserBalance(UserID, Messages, Recipients);
            //    if (ValidateCreditForUserBalanceResult.Result != ActionResult.Done)
            //    {
            //        return ValidateCreditForUserBalanceResult;
            //    }

            //}

            return new OperationResult() { Result = ActionResult.Done };
        }

        private static OperationResult ValidateMessages(List<MessagePattern> Messages)
        {
            OperationResult Res = new OperationResult();

            int index;
            for (index = 0; index < Messages.Count; index++)
            {
                if (Regex.IsMatch(Messages[index].Body, PersianMessageRegularExpression, RegexOptions.IgnoreCase) &&
                    Messages[index].MessageType != MessageType.Persian)
                {
                    Res.AddMessage(UserMessageKey.MismatchMessageBodyAndType);
                    Res.Result = ActionResult.Failed;
                    return Res;
                }

                int MaxChars = (Messages[index].MessageType == MessageType.Persian) ? PersianMaxCharacterCount : LatinMaxCharacterCount;

                if (Messages[index].Body.Length > MaxChars && Messages[index].MessageType == MessageType.Persian)
                    MaxChars = MaxChars - PersianMultipleMessageReservedCharactersCount;

                if (Messages[index].Body.Length > MaxChars && Messages[index].MessageType == MessageType.Latin)
                    MaxChars = MaxChars - LatinMultipleMessageReservedCharactersCount;

                if (Math.Max(Math.Ceiling((double)Messages[index].Body.Length / MaxChars), 1) != Messages[index].SMSCount)
                {
                    Res.AddMessage(UserMessageKey.MismatchMessageBodyAndSMSCount);
                    Res.Result = ActionResult.Failed;
                    return Res;
                }
            }

            Res.Result = ActionResult.Done;
            return Res;
        }

        private static OperationResult ValidateCounts(List<MessagePattern> Messages, List<string> Recipients)
        {
            OperationResult Res = new OperationResult();
            if (Messages == null || Messages.Count == 0)
            {
                Res.AddMessage(UserMessageKey.NoMessageBody);
                Res.Result = ActionResult.Failed;
                return Res;
            }

            if (Recipients == null || Recipients.Count == 0)
            {
                Res.AddMessage(UserMessageKey.NoRecipientNumber);
                Res.Result = ActionResult.Failed;
                return Res;
            }

            if (Messages.Count != Recipients.Count && (Messages.Count != 1 && Recipients.Count != 1))
            {
                Res.AddMessage(UserMessageKey.CountMismatchForRecipientsAndMessage);
                Res.Result = ActionResult.Failed;
                return Res;
            }

            Res.Result = ActionResult.Done;
            return Res;
        }

        private static OperationResult ValidateRecipients(List<string> Recipients)
        {
            OperationResult Res = new OperationResult();

            foreach (string Recipient in Recipients)
            {
                if (string.IsNullOrEmpty(Recipient) ||
                    Recipient.Length < 5) // Later on , Can Check with Regular Expression as well
                {
                    Res.AddMessage(UserMessageKey.InvalidRecipientNumber);
                    Res.Result = ActionResult.Failed;
                    return Res;
                }
            }

            Res.Result = ActionResult.Done;
            return Res;
        }

        //public static SendResult SendMessagesWithFinanceTransaction(List<MessagePattern> Messages, PrivateNo PrivateNumber, List<string> Recipients, SendType SendType, PrivateNoRate Rate, long? UserID,bool ReturnUserMessage,long? GroupMessageRequestID)
        //{
        //    SendResult Res = new SendResult();
        //    long? PayableCharge = null;
        //    try
        //    {
        //        if (SendType != SendType.TafrihiSend)
        //            Res = SendMessages(Messages, PrivateNumber, Recipients, SendType, Rate.PersianMessageFee, Rate.LatinMessageFee, ReturnUserMessage, GroupMessageRequestID);
        //        else
        //        {
        //            long LatinMessageFee = OptionBusiness.GetMoney(OptionKey.TafrihiLatinMessageFee);
        //            long PersianMessageFee = OptionBusiness.GetMoney(OptionKey.TafrihiPersianMessageFee);

        //            Res = SendMessages(Messages, PrivateNumber, Recipients, SendType, PersianMessageFee, LatinMessageFee, ReturnUserMessage, GroupMessageRequestID);
        //        }
        //        PayableCharge = Res.SentMessages.Where(s => s.GatewayMessageID != null).Sum(s => s.SMSCount * s.Fee);
        //        if (PayableCharge != null && PayableCharge > 0)
        //        {
        //            spbsEntities Context = new spbsEntities();
        //            if (SendType != SendType.TafrihiSend)
        //            {
        //                PrivateNoRate UpdatingRate = Context.PrivateNoRates.FirstOrDefault(r => r.ID == Rate.ID);
        //                UpdatingRate.Balance = UpdatingRate.Balance - (long)PayableCharge;
        //                PrivateNoRateTransaction RateTransaction = new PrivateNoRateTransaction()
        //                {
        //                    Balance = UpdatingRate.Balance,
        //                    ChargeAmount = -(long)PayableCharge,
        //                    Date = DateTime.Now,
        //                    PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now),
        //                    PrivateNoRate_ID = UpdatingRate.ID,
        //                    TransactionChargeType = (int)SendType,
        //                    User_ID = UserID,
        //                    Description = string.Format("بابت {0} {1} از مجموع {2} پیامک",
        //                        EnumUtil.GetEnumElementPersianTitle(SendType),
        //                        Res.SucceedCount,
        //                        Res.SumCount
        //                    )
        //                };
        //                Context.PrivateNoRateTransactions.AddObject(RateTransaction);
        //                Context.SaveChanges();

        //                Res.ChargeAmount = (decimal)PayableCharge;
        //                Res.CurrentBalance = UpdatingRate.Balance;
        //                return Res;
        //            }
        //            else
        //            {
        //                Model.User user = Context.Users.Where(u => u.ID == UserID).SingleOrDefault();
        //                user.Balance = user.Balance - (long)PayableCharge;
        //                UserTransaction transaction = new UserTransaction() 
        //                {
        //                    Balance=user.Balance,
        //                    ChargeAmount = -(long)PayableCharge,
        //                    Date=DateTime.Now,
        //                    PersianDate=DateUtil.GetPersianDateShortString(DateTime.Now),
        //                    TransactionChargeType =(int)SendType,
        //                    User_ID=user.ID,
        //                    Description=string.Format("بابت {0} {1} از مجموع {2} پیامک",
        //                        EnumUtil.GetEnumElementPersianTitle(SendType),
        //                        Res.SucceedCount,
        //                        Res.SumCount)
        //                };

        //                Context.UserTransactions.AddObject(transaction);
        //                Context.SaveChanges();

        //                Res.ChargeAmount = (decimal)PayableCharge;
        //                Res.CurrentBalance = user.Balance;
        //                return Res;
        //            }

        //        }
        //        else
        //        {
        //            Res.ChargeAmount = 0;
        //            Res.CurrentBalance = Rate.Balance;
        //            return Res;
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        HandleException(Ex, "SMSSendBusiness.SendMessagesWithFinanceTransaction");
        //        SendNotificationForFinanceFailure(PrivateNumber, Rate, PayableCharge, Res.SucceedCount, UserID);
        //        if (ReturnUserMessage && Res.SucceedCount > 0)
        //            Res.UserMessages.Add(UserMessageKey.FinanceFailureOccuredInSend);
        //        return Res;
        //    }
        //}

        public static SendSMSResult SendMessages(List<MessagePattern> Messages, PrivateNo PrivateNumber, List<string> Recipients, bool ReturnUserMessage, long? OccuredEvent_ID, EventType EventType)
        {
            SMSSendAttemptBusiness SMSSendAttemptBusiness = new Business.SMSSendAttemptBusiness();
            SMSSendAttempt SMSSendAttemptLog = new SMSSendAttempt();
            SMSSendAttemptLog.Date = DateTime.Now;
            SMSSendAttemptLog.OccuredEvent_ID = OccuredEvent_ID;
            SMSSendAttemptLog.EventType = (int)EventType;
            SMSSendAttemptLog.MessageType = (int)Messages.SingleOrDefault().MessageType;
            SMSSendAttemptLog.PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
            SMSSendAttemptLog.Total_ErrorCount = 0;
            SMSSendAttemptLog.Total_FailedCount = 0;
            SMSSendAttemptLog.Total_SucceedCount = 0;
            SMSSendAttemptLog.Total_SumCount = 0;
            SMSSendAttemptLog.Total_UnknownCount = 0;

            SMSSendAttemptLog.Body = Messages[0].Body;
            SMSSendAttemptLog.CharCount = Messages[0].CharCount;
            SMSSendAttemptLog.SMSCount = Messages[0].SMSCount;

            foreach (string item in Recipients)
                SMSSendAttemptLog.Recipients += "," + item;
            if (SMSSendAttemptLog.Recipients.Length > 2)
                SMSSendAttemptLog.Recipients.Substring(2);


            SendSMSResult Res = new SendSMSResult();
            List<Packet> Packets = new List<Packet>();

            try
            {
                SMSSendAttemptBusiness.Insert(SMSSendAttemptLog);

                if (Messages.Count == 1 && Recipients.Count == 1)
                {
                    Packets.Add(new Packet() { Messages = Messages, Recipients = Recipients });
                }
                else if (Messages.Count == 1 && Recipients.Count > 1)
                {
                    if (Recipients.Count > PacketSize)
                    {
                        int PacketCount = (int)Math.Ceiling((decimal)Recipients.Count / PacketSize);
                        for (int PacketIndex = 0; PacketIndex < PacketCount; PacketIndex++)
                        {
                            Packet packet = new Packet();
                            packet.Messages = Messages;

                            if (PacketIndex == PacketCount - 1)
                                packet.Recipients = Recipients.GetRange(PacketIndex * PacketSize, Recipients.Count - (PacketIndex * PacketSize));
                            else
                                packet.Recipients = Recipients.GetRange(PacketIndex * PacketSize, PacketSize);

                            Packets.Add(packet);
                        }
                    }
                    else
                    {
                        Packets.Add(new Packet() { Messages = Messages, Recipients = Recipients });
                    }
                }
                else if (Messages.Count > 1 && Recipients.Count == 1)
                {
                    if (Messages.Count > PacketSize)
                    {
                        int PacketCount = (int)Math.Ceiling((decimal)Messages.Count / PacketSize);
                        for (int PacketIndex = 0; PacketIndex < PacketCount; PacketIndex++)
                        {
                            Packet packet = new Packet();
                            packet.Recipients = Recipients;

                            if (PacketIndex == PacketCount - 1)
                                packet.Messages = Messages.GetRange(PacketIndex * PacketSize, Messages.Count - (PacketIndex * PacketSize));
                            else
                                packet.Messages = Messages.GetRange(PacketIndex * PacketSize, PacketSize);

                            Packets.Add(packet);
                        }
                    }
                    else
                    {
                        Packets.Add(new Packet() { Messages = Messages, Recipients = Recipients });
                    }
                }
                else if (Messages.Count > 1 && Recipients.Count > 1)
                {
                    if (Messages.Count > PacketSize)
                    {
                        int PacketCount = (int)Math.Ceiling((decimal)Messages.Count / PacketSize);
                        for (int PacketIndex = 0; PacketIndex < PacketCount; PacketIndex++)
                        {
                            Packet packet = new Packet();
                            if (PacketIndex == PacketCount - 1)
                            {
                                packet.Messages = Messages.GetRange(PacketIndex * PacketSize, Messages.Count - (PacketIndex * PacketSize));
                                packet.Recipients = Recipients.GetRange(PacketIndex * PacketSize, Recipients.Count - (PacketIndex * PacketSize));
                            }
                            else
                            {
                                packet.Messages = Messages.GetRange(PacketIndex * PacketSize, PacketSize);
                                packet.Recipients = Recipients.GetRange(PacketIndex * PacketSize, PacketSize);
                            }

                            Packets.Add(packet);
                        }
                    }
                    else
                    {
                        Packets.Add(new Packet() { Messages = Messages, Recipients = Recipients });
                    }
                }

                foreach (Packet packet in Packets)
                {
                    packet.Result = SendPacket(packet.Messages, PrivateNumber, packet.Recipients.ToArray(), SMSSendAttemptLog.ID);

                    Res.SentMessages.AddRange(packet.Result.SentMessages);
                }

                SMSSendAttemptLog.Total_SucceedCount = Res.Total_SucceedCount = Res.SentMessages.Count(sm => sm.GatewayMessageID != null);
                SMSSendAttemptLog.Total_FailedCount = Res.Total_FailedCount = Res.SentMessages.Count(sm => sm.FaultType != null);
                SMSSendAttemptLog.Total_UnknownCount = Res.Total_UnknownCount = Res.SentMessages.Count(sm => sm.FaultType == null && sm.GatewayMessageID == null);
                SMSSendAttemptLog.Total_SumCount = Res.Total_SumCount = Math.Max(Messages.Count, Recipients.Count);
                SMSSendAttemptLog.Total_ErrorCount = Res.Total_ErrorCount = Res.Total_SumCount - Res.SentMessages.Count;

                SMSSendAttemptBusiness.Update(SMSSendAttemptLog);
            }
            catch (Exception Ex)
            {
                HandleException(Ex, "SMSSendBusiness.SendMessages");
            }


            if (Res.Total_UnknownCount > 0)
                SendNotificationForUnknownSend(Res.Total_UnknownCount, PrivateNumber);

            if (ReturnUserMessage && Res.Total_SucceedCount > 0)
                Res.UserMessages.Add(UserMessageKey.Succeed);

            if (ReturnUserMessage && Res.Total_ErrorCount > 0)
                Res.UserMessages.Add(UserMessageKey.ErrorOccuredInSend);

            if (ReturnUserMessage && Res.Total_UnknownCount > 0)
                Res.UserMessages.Add(UserMessageKey.UnknownOccuredInSend);

            Res.SMSSendAttempt = SMSSendAttemptLog;



            //SMS_Manual_Sending
            #region Event Rising
            if (!OccuredEvent_ID.HasValue && EventType == Model.Enums.EventType.Manual)
            {
                string body = Messages.First().Body;
                User user = new UserBusiness().Retrieve(kFrameWork.UI.UserSession.UserID);

                SystemEventArgs e = new SystemEventArgs();

                e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                e.EventVariables.Add("%user%", user.FullName);
                e.EventVariables.Add("%mobile%", user.Mobile);
                e.EventVariables.Add("%phone%", user.Tel);
                e.EventVariables.Add("%email%", user.Email);
                e.EventVariables.Add("%username%", user.Username);

                e.EventVariables.Add("%body%", body);
                e.EventVariables.Add("%summary%", ((body.Length > 50) ? body.Substring(0, 50) : body));

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.SMS_Manual_Sending, e);
            }
            #endregion



            return Res;
        }

        private static SendPacketResult SendPacket(List<MessagePattern> Messages, PrivateNo PrivateNumber, string[] Recipients, long SMSAttempt_ID)
        {
            SendPacketResult Res = new SendPacketResult();
            List<SentSMS> SavingMessages = new List<SentSMS>();
            pgcEntities Context = new pgcEntities();
            try
            {
                int PacketSize = Math.Max(Messages.Count, Recipients.Length);
                string PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
                for (int index = 0; index < PacketSize; index++)
                {
                    SentSMS SavingMessage = new SentSMS();
                    SavingMessage.Body = (Messages.Count > 1) ? Messages[index].Body : Messages[0].Body;
                    SavingMessage.RecipientNumber = (Recipients.Length > 1) ? Recipients[index] : Recipients[0];
                    SavingMessage.PrivateNo_ID = PrivateNumber.ID;
                    SavingMessage.Date = DateTime.Now;
                    SavingMessage.PersianDate = PersianDate;
                    SavingMessage.PhoneDeliveryCount = 0;
                    SavingMessage.PhoneFailCount = 0;
                    SavingMessage.SendStatus = 0;
                    SavingMessage.SMSCount = (Messages.Count > 1) ? Messages[index].SMSCount : Messages[0].SMSCount;
                    SavingMessage.MessageType = (Messages.Count > 1) ? (int)Messages[index].MessageType : (int)Messages[0].MessageType;
                    SavingMessage.TeleDeliveryCount = 0;
                    SavingMessage.TeleFailCount = 0;
                    SavingMessage.SMSSendAttempt_ID = SMSAttempt_ID;


                    SavingMessages.Add(SavingMessage);
                    Context.SentSMSList.AddObject(SavingMessage);
                }
                Context.SaveChanges();
            }
            catch (Exception Ex)
            {
                Res.Status = SendPacketResultStatus.Failed;
                HandleException(Ex, "SMSSendBusiness.SendPacket.InsertSentMessages");
                return Res;
            }

            //SendToMagfaResult MagfaRes = SendToMagfa(Messages.Select(m => m.Body).ToArray(), Recipients, new string[] { PrivateNumber.Number });
            SendToMagfaResult MagfaRes = SendToPayamakPanel(Messages.Select(m => m.Body).ToArray(), Recipients, new string[] { PrivateNumber.Number });


            #region Change For Unknown To be FaultType
            if (MagfaRes.Status == SendToMagfaResultStatus.Failed)
            {
                Res.Status = SendPacketResultStatus.Failed;
                foreach (var item in SavingMessages)
                    item.FaultType = 0;

                Context.SaveChanges();
                Res.SentMessages = SavingMessages;
                return Res;
            }

            if (MagfaRes.Status == SendToMagfaResultStatus.SentWithoutResult ||
                MagfaRes.IDs.Length != SavingMessages.Count)
            {
                Res.Status = SendPacketResultStatus.Unknown;
                Res.SentMessages = SavingMessages;
                return Res;
            }


            #endregion

            try
            {
                int index = 0;
                foreach (SentSMS SavingMessage in SavingMessages)
                {
                    if (MagfaRes.IDs[index] > 1000)
                        SavingMessage.GatewayMessageID = MagfaRes.IDs[index];   //MessageID
                    else
                        SavingMessage.FaultType = (int)MagfaRes.IDs[index];     //Fault

                    index++;
                }
                Context.SaveChanges();
                Res.Status = SendPacketResultStatus.Succeed;
                Res.SentMessages = SavingMessages;
            }
            catch (Exception Ex)
            {
                Res.Status = SendPacketResultStatus.Unknown;
                Res.SentMessages = SavingMessages;
                HandleException(Ex, "SMSSendBusiness.SendPacket.UpdateSentMessages");
                return Res;
            }

            return Res;
        }

        public static SendToMagfaResult SendToMagfa(string[] Bodies, string[] Recipients, string[] Senders)
        {
            SendToMagfaResult Res = new SendToMagfaResult();
            try
            {

                SoapSmsQueuableImplementationService ServiceProvider = new SoapSmsQueuableImplementationService();
                ServiceProvider.Credentials = new System.Net.NetworkCredential(MagfaUsername, MagfaPassword);
                ServiceProvider.PreAuthenticate = true;
                Res.IDs = ServiceProvider.enqueue(
                    MagfaDomain,
                    Bodies,
                    Recipients,
                    Senders,
                    null,//new int[]{-1},
                    null,//new string[] {"" },
                    null,//new int[] { -1},
                    null,//new int[] { -1},
                    null//new long[] { 2002}
                    );
                if (Res.IDs != null)
                    Res.Status = SendToMagfaResultStatus.SentWithResult;
                // ServiceProvider.getRealMessageStatuses(Res.IDs);
                else
                    Res.Status = SendToMagfaResultStatus.SentWithoutResult;
            }
            catch (TimeoutException Ex)
            {
                Res.Status = SendToMagfaResultStatus.Failed;
                HandleException(Ex, "SMSSendBusiness.SendToMagfa(Timeout)");
            }
            catch (Exception Ex)
            {
                Res.Status = SendToMagfaResultStatus.Failed;
                HandleException(Ex, "SMSSendBusiness.SendToMagfa");
            }
            return Res;
        }

        public static SendToMagfaResult SendToPayamakPanel(string[] Bodies, string[] Recipients, string[] Senders)
        {
            SendToMagfaResult Res = new SendToMagfaResult();
            try
            {
                com.ppanel.api.Send ServiceProvider = new com.ppanel.api.Send();
                //ServiceProvider.Credentials = new System.Net.NetworkCredential(MagfaUsername, MagfaPassword);
                //ServiceProvider.PreAuthenticate = true;

                long[] recIDs = null;
                byte[] status = null;
                //Res.IDs !!!

                int Result = ServiceProvider.SendSms(
                   MagfaUsername, //actually gatway(currently payamek panel) username
                   MagfaPassword, //actually gatway(currently payamek panel) password
                   Recipients,
                   Senders[0],
                   Bodies[0],
                   false,
                   "",
                   ref recIDs,
                   ref status);

                Res.IDs = recIDs;

                //Result: 1 succed, Result 0, ...12 failed messages... 
                if (Result != 1 && Result >= 0 && Result <= 12)
                    UserSession.AddMessage(GetMessageKeyBySendSMSResult(Result));

                if (Res.IDs != null)
                    Res.Status = SendToMagfaResultStatus.SentWithResult;
                else
                    Res.Status = SendToMagfaResultStatus.SentWithoutResult;

            }
            catch (Exception Ex)
            {
                Res.Status = SendToMagfaResultStatus.Failed;
                HandleException(Ex, "SMSSendBusiness.SendToPayamakPanel");
            }
            return Res;
        }

        private static UserMessageKey GetMessageKeyBySendSMSResult(int Result)
        {
            switch (Result)
            {
                case 0: return UserMessageKey.PayamakPanel_InavalidUsernameOrPassword;
                case 2: return UserMessageKey.PayamakPanel_InsufficientCredit;
                case 3: return UserMessageKey.PayamakPanel_LimitationOnDailySend;
                case 4: return UserMessageKey.PayamakPanel_LimitationOnSendVolume;
                case 5: return UserMessageKey.PayamakPanel_InvalidSenderNumber;
                case 6: return UserMessageKey.PayamakPanel_UpdatingPPServer;
                case 7: return UserMessageKey.PayamakPanel_TextIncludedFilteredWord;
                case 9: return UserMessageKey.PayamakPanel_ImpossibleByPublicNumber;
                case 10: return UserMessageKey.PayamakPanel_DisableUser;
                case 11: return UserMessageKey.PayamakPanel_DidNotSent;
                case 12: return UserMessageKey.PayamakPanel_DocumentsInComplete;
            }
            return UserMessageKey.Succeed;
        }

        private static void HandleException(Exception Ex, string Event)
        {
            ExceptionHandler.HandleManualException(Ex,Event);
        }

        private static void SendNotificationForUnknownSend(int UnknownCount, PrivateNo PrivateNumber)
        {
            //dont forget to handle exception

            //NotificationBusiness objNotification = new NotificationBusiness();
            //List<NotificationMessageDetail> NotificationDetails = new List<NotificationMessageDetail>();
            //NotificationDetails.Add(new NotificationMessageDetail()
            //{
            //    Key = NotificationMessageDetailKey.RequestedMessagesCount.ToString(),
            //    KeyCaption = PersianTitleService.GetEnumElementPersianTitle(NotificationMessageDetailKey.RequestedMessagesCount),
            //    Value = SentMessages.Count.ToString()
            //});
            //NotificationDetails.Add(new NotificationMessageDetail()
            //{
            //    Key = NotificationMessageDetailKey.SendDate.ToString(),
            //    KeyCaption = PersianTitleService.GetEnumElementPersianTitle(NotificationMessageDetailKey.SendDate),
            //    Value = DateTime.Now.ToString()
            //});
            //NotificationDetails.Add(new NotificationMessageDetail()
            //{
            //    Key = NotificationMessageDetailKey.PrivateNo.ToString(),
            //    KeyCaption = PersianTitleService.GetEnumElementPersianTitle(NotificationMessageDetailKey.PrivateNo),
            //    Value = PrivateNumber.Number
            //});
            //objNotification.AddCriticalNotification(UserMessageKey.Notification_SMSSendFailure, null, (int)Role.Admin, NotificationDetails);
        }

        private static void SendNotificationForFinanceFailure(PrivateNo PrivateNumber, int SucceedCount, long? UserID)
        {
            //dont forget to handle exception
        }
    }

    #region Results Structures

    public class SendToMagfaResult
    {
        public SendToMagfaResultStatus Status { get; set; }

        public long[] IDs { get; set; }
    }

    public class SendPacketResult
    {
        public SendPacketResult()
        {
            SentMessages = new List<SentSMS>();
        }

        public SendPacketResultStatus Status { get; set; }

        public List<SentSMS> SentMessages { get; set; }
    }

    public class SendSMSResult
    {
        public SendSMSResult()
        {
            SentMessages = new List<SentSMS>();
            UserMessages = new List<UserMessageKey>();
            SMSSendAttempt = new SMSSendAttempt();
        }

        public List<UserMessageKey> UserMessages { get; set; }

        public List<SentSMS> SentMessages { get; set; } // = SucceedCount + FailedCount + UnknownCount

        public SMSSendAttempt SMSSendAttempt { get; set; }

        public int Total_SucceedCount { get; set; }

        public int Total_FailedCount { get; set; }

        public int Total_UnknownCount { get; set; }

        public int Total_ErrorCount { get; set; }

        public int Total_SumCount { get; set; }

        public decimal ChargeAmount { get; set; }

        public decimal CurrentBalance { get; set; }
    }

    public class Packet
    {
        public Packet()
        {
            Messages = new List<MessagePattern>();
            Recipients = new List<string>();
        }

        public List<MessagePattern> Messages { get; set; }

        public List<string> Recipients { get; set; }

        public SendPacketResult Result { get; set; }
    }

    public enum SendToMagfaResultStatus
    {
        SentWithResult, //with IDs
        SentWithoutResult,
        Failed
    }

    public enum SendPacketResultStatus
    {
        Succeed,//with sent messages
        Unknown,//with sent messages
        Failed
    }

    #endregion Results Structures


    //------------------------------------OLD CODES B4 EVENT CYCLE DISIGNING------------------------------------//
    //----------------------------------------------------------------------------------------------------------//
    //----------------------------------------------------------------------------------------------------------//

    //public class SMSBusiness
    //{
    //    #region Constants

    //    // *** Thease Constans can be retrived from settings later on , and in the javascript using in MessageControl.asx as well

    //    /// <summary>
    //    /// Domain specified by Magfa
    //    /// </summary>
    //    //public const string MagfaDomain = "magfa";
    //    public static string MagfaDomain = OptionBusiness.GetText(OptionKey.MagfaDomain);

    //    /// <summary>
    //    /// Username specified by magfa
    //    /// </summary>
    //    //public const string MagfaUsername = "sepidan";
    //    public static string MagfaUsername = OptionBusiness.GetText(OptionKey.MagfaUsername);

    //    /// <summary>
    //    /// Password specified by magfa
    //    /// </summary>
    //    //public const string MagfaPassword = "13901390";
    //    public static string MagfaPassword = OptionBusiness.GetText(OptionKey.MagfaPassword);

    //    /// <summary>
    //    /// Regular expression with matchs true if a persian letter exists in a checking string
    //    /// </summary>
    //    //public const string PersianMessageRegularExpression = "[^\x00-\x7E]";
    //    public static string PersianMessageRegularExpression = OptionBusiness.GetNText(OptionKey.PersianMessageRegularExpression);

    //    /// <summary>
    //    /// Maximum characters in each persian message
    //    /// </summary>
    //    //public const int PersianMaxCharacterCount = 70;
    //    public static int PersianMaxCharacterCount = OptionBusiness.GetTinyInt(OptionKey.PersianMaxCharacterCount);

    //    /// <summary>
    //    /// Maximum character in each latin message
    //    /// </summary>
    //    //public const int LatinMaxCharacterCount = 160;
    //    public static int LatinMaxCharacterCount = OptionBusiness.GetTinyInt(OptionKey.LatinMaxCharacterCount);

    //    /// <summary>
    //    /// Maxmimum characters in each persian message when there is more than one message on the body
    //    /// </summary>
    //    //public const int PersianMultipleMessageReservedCharactersCount = 3;
    //    public static int PersianMultipleMessageReservedCharactersCount = OptionBusiness.GetTinyInt(OptionKey.PersianMultipleMessageReservedCharactersCount);

    //    /// <summary>
    //    /// Maxmimum characters in each latin message when there is more than one message on the body
    //    /// </summary>
    //    //public const int LatinMultipleMessageReservedCharactersCount = 7;
    //    public static int LatinMultipleMessageReservedCharactersCount = OptionBusiness.GetTinyInt(OptionKey.LatinMultipleMessageReservedCharactersCount);

    //    /// <summary>
    //    /// The size pf each packet sending to magfa
    //    /// </summary>
    //    //public const int PacketSize = 80;
    //    public static int PacketSize = OptionBusiness.GetTinyInt(OptionKey.PacketSize);

    //    #endregion Constants

    //    //public SMSBusiness()
    //    //{
    //    //    MagfaDomain = OptionBusiness.GetText(OptionKey.MagfaDomain);
    //    //    MagfaUsername = OptionBusiness.GetText(OptionKey.MagfaUsername);
    //    //    MagfaPassword = OptionBusiness.GetText(OptionKey.MagfaPassword);
    //    //    PacketSize = OptionBusiness.GetTinyInt(OptionKey.PacketSize);

    //    //    PersianMessageRegularExpression = OptionBusiness.GetNText(OptionKey.PersianMessageRegularExpression);

    //    //    PersianMaxCharacterCount = OptionBusiness.GetTinyInt(OptionKey.PersianMaxCharacterCount);
    //    //    LatinMaxCharacterCount = OptionBusiness.GetTinyInt(OptionKey.LatinMaxCharacterCount);

    //    //    PersianMultipleMessageReservedCharactersCount = OptionBusiness.GetTinyInt(OptionKey.PersianMultipleMessageReservedCharactersCount);
    //    //    LatinMultipleMessageReservedCharactersCount = OptionBusiness.GetTinyInt(OptionKey.LatinMultipleMessageReservedCharactersCount);
    //    //}

    //    public static OperationResult Validate(List<MessagePattern> Messages, PrivateNo PrivateNumber, List<string> Recipients)
    //    {
    //        OperationResult ValidateCountsResult = ValidateCounts(Messages,Recipients);
    //        if (ValidateCountsResult.Result != ActionResult.Done)
    //        {
    //            return ValidateCountsResult;
    //        }

    //        OperationResult ValidateMessagesResult = ValidateMessages(Messages);
    //        if (ValidateMessagesResult.Result != ActionResult.Done)
    //        {
    //            return ValidateMessagesResult;
    //        }

    //        OperationResult ValidateRecipientsResult = ValidateRecipients(Recipients);
    //        if (ValidateRecipientsResult.Result != ActionResult.Done)
    //        {
    //            return ValidateRecipientsResult;
    //        }

    //        //PermisionBusiness objPermision = new PermisionBusiness();
    //        //OperationResult ValidatePrivateNoActivenessResult = objPermision.ValidatePrivateNoActiveness(PrivateNumber);
    //        //if (ValidatePrivateNoActivenessResult.Result != ActionResult.Done)
    //        //{
    //        //    return ValidatePrivateNoActivenessResult;
    //        //}
    //        //OperationResult ValidateUserActivenessResult = objPermision.ValidateUserActiveness(UserID);
    //        //if (ValidateUserActivenessResult.Result != ActionResult.Done)
    //        //{
    //        //    return ValidateUserActivenessResult;
    //        //}



    //        //FinanceBusiness objFinance = new FinanceBusiness();
    //        //if (SendType != SendType.TafrihiSend)
    //        //{
    //        //    OperationResult ValidateCreditForPrivateNoRateResult = objFinance.ValidateCreditForPrivateNoRate(Rate, Messages, Recipients);
    //        //    if (ValidateCreditForPrivateNoRateResult.Result != ActionResult.Done)
    //        //    {
    //        //        return ValidateCreditForPrivateNoRateResult;
    //        //    }
    //        //}
    //        //else
    //        //{
    //        //    OperationResult ValidateCreditForUserBalanceResult = objFinance.ValidateCreditForUserBalance(UserID, Messages, Recipients);
    //        //    if (ValidateCreditForUserBalanceResult.Result != ActionResult.Done)
    //        //    {
    //        //        return ValidateCreditForUserBalanceResult;
    //        //    }

    //        //}

    //        return new OperationResult() { Result = ActionResult.Done };
    //    }

    //    private static OperationResult ValidateMessages(List<MessagePattern> Messages)
    //    {
    //        OperationResult Res = new OperationResult();

    //        int index;
    //        for (index = 0; index < Messages.Count; index++)
    //        {
    //            if (Regex.IsMatch(Messages[index].Body, PersianMessageRegularExpression, RegexOptions.IgnoreCase) &&
    //                Messages[index].MessageType != MessageType.Persian)
    //            {
    //                Res.AddMessage(UserMessageKey.MismatchMessageBodyAndType);
    //                Res.Result = ActionResult.Failed;
    //                return Res;
    //            }

    //            int MaxChars = (Messages[index].MessageType == MessageType.Persian) ? PersianMaxCharacterCount : LatinMaxCharacterCount;

    //            if (Messages[index].Body.Length > MaxChars && Messages[index].MessageType == MessageType.Persian)
    //                MaxChars = MaxChars - PersianMultipleMessageReservedCharactersCount;

    //            if (Messages[index].Body.Length > MaxChars && Messages[index].MessageType == MessageType.Latin)
    //                MaxChars = MaxChars - LatinMultipleMessageReservedCharactersCount;

    //            if (Math.Max(Math.Ceiling((double)Messages[index].Body.Length / MaxChars), 1) != Messages[index].SMSCount)
    //            {
    //                Res.AddMessage(UserMessageKey.MismatchMessageBodyAndSMSCount);
    //                Res.Result = ActionResult.Failed;
    //                return Res;
    //            }
    //        }

    //        Res.Result = ActionResult.Done;
    //        return Res;
    //    }

    //    private static OperationResult ValidateCounts(List<MessagePattern> Messages, List<string> Recipients)
    //    {
    //        OperationResult Res = new OperationResult();
    //        if (Messages == null || Messages.Count == 0 )
    //        {
    //            Res.AddMessage(UserMessageKey.NoMessageBody);
    //            Res.Result = ActionResult.Failed;
    //            return Res;
    //        }

    //        if (Recipients == null || Recipients.Count == 0)
    //        {
    //            Res.AddMessage(UserMessageKey.NoRecipientNumber);
    //            Res.Result = ActionResult.Failed;
    //            return Res;
    //        }

    //        if (Messages.Count != Recipients.Count && (Messages.Count != 1 && Recipients.Count != 1))
    //        {
    //            Res.AddMessage(UserMessageKey.CountMismatchForRecipientsAndMessage);
    //            Res.Result = ActionResult.Failed;
    //            return Res;
    //        }

    //        Res.Result = ActionResult.Done;
    //        return Res;
    //    }

    //    private static OperationResult ValidateRecipients(List<string> Recipients)
    //    {
    //        OperationResult Res = new OperationResult();

    //        foreach (string Recipient in Recipients)
    //        {
    //            if (string.IsNullOrEmpty(Recipient) ||
    //                Recipient.Length < 5) // Later on , Can Check with Regular Expression as well
    //            {
    //                Res.AddMessage(UserMessageKey.InvalidRecipientNumber);
    //                Res.Result = ActionResult.Failed;
    //                return Res;
    //            }
    //        }

    //        Res.Result = ActionResult.Done;
    //        return Res;
    //    }

    //    //public static SendResult SendMessagesWithFinanceTransaction(List<MessagePattern> Messages, PrivateNo PrivateNumber, List<string> Recipients, SendType SendType, PrivateNoRate Rate, long? UserID,bool ReturnUserMessage,long? GroupMessageRequestID)
    //    //{
    //    //    SendResult Res = new SendResult();
    //    //    long? PayableCharge = null;
    //    //    try
    //    //    {
    //    //        if (SendType != SendType.TafrihiSend)
    //    //            Res = SendMessages(Messages, PrivateNumber, Recipients, SendType, Rate.PersianMessageFee, Rate.LatinMessageFee, ReturnUserMessage, GroupMessageRequestID);
    //    //        else
    //    //        {
    //    //            long LatinMessageFee = OptionBusiness.GetMoney(OptionKey.TafrihiLatinMessageFee);
    //    //            long PersianMessageFee = OptionBusiness.GetMoney(OptionKey.TafrihiPersianMessageFee);

    //    //            Res = SendMessages(Messages, PrivateNumber, Recipients, SendType, PersianMessageFee, LatinMessageFee, ReturnUserMessage, GroupMessageRequestID);
    //    //        }
    //    //        PayableCharge = Res.SentMessages.Where(s => s.GatewayMessageID != null).Sum(s => s.SMSCount * s.Fee);
    //    //        if (PayableCharge != null && PayableCharge > 0)
    //    //        {
    //    //            spbsEntities Context = new spbsEntities();
    //    //            if (SendType != SendType.TafrihiSend)
    //    //            {
    //    //                PrivateNoRate UpdatingRate = Context.PrivateNoRates.FirstOrDefault(r => r.ID == Rate.ID);
    //    //                UpdatingRate.Balance = UpdatingRate.Balance - (long)PayableCharge;
    //    //                PrivateNoRateTransaction RateTransaction = new PrivateNoRateTransaction()
    //    //                {
    //    //                    Balance = UpdatingRate.Balance,
    //    //                    ChargeAmount = -(long)PayableCharge,
    //    //                    Date = DateTime.Now,
    //    //                    PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now),
    //    //                    PrivateNoRate_ID = UpdatingRate.ID,
    //    //                    TransactionChargeType = (int)SendType,
    //    //                    User_ID = UserID,
    //    //                    Description = string.Format("بابت {0} {1} از مجموع {2} پیامک",
    //    //                        EnumUtil.GetEnumElementPersianTitle(SendType),
    //    //                        Res.SucceedCount,
    //    //                        Res.SumCount
    //    //                    )
    //    //                };
    //    //                Context.PrivateNoRateTransactions.AddObject(RateTransaction);
    //    //                Context.SaveChanges();

    //    //                Res.ChargeAmount = (decimal)PayableCharge;
    //    //                Res.CurrentBalance = UpdatingRate.Balance;
    //    //                return Res;
    //    //            }
    //    //            else
    //    //            {
    //    //                Model.User user = Context.Users.Where(u => u.ID == UserID).SingleOrDefault();
    //    //                user.Balance = user.Balance - (long)PayableCharge;
    //    //                UserTransaction transaction = new UserTransaction() 
    //    //                {
    //    //                    Balance=user.Balance,
    //    //                    ChargeAmount = -(long)PayableCharge,
    //    //                    Date=DateTime.Now,
    //    //                    PersianDate=DateUtil.GetPersianDateShortString(DateTime.Now),
    //    //                    TransactionChargeType =(int)SendType,
    //    //                    User_ID=user.ID,
    //    //                    Description=string.Format("بابت {0} {1} از مجموع {2} پیامک",
    //    //                        EnumUtil.GetEnumElementPersianTitle(SendType),
    //    //                        Res.SucceedCount,
    //    //                        Res.SumCount)
    //    //                };

    //    //                Context.UserTransactions.AddObject(transaction);
    //    //                Context.SaveChanges();

    //    //                Res.ChargeAmount = (decimal)PayableCharge;
    //    //                Res.CurrentBalance = user.Balance;
    //    //                return Res;
    //    //            }

    //    //        }
    //    //        else
    //    //        {
    //    //            Res.ChargeAmount = 0;
    //    //            Res.CurrentBalance = Rate.Balance;
    //    //            return Res;
    //    //        }
    //    //    }
    //    //    catch (Exception Ex)
    //    //    {
    //    //        HandleException(Ex, "SMSSendBusiness.SendMessagesWithFinanceTransaction");
    //    //        SendNotificationForFinanceFailure(PrivateNumber, Rate, PayableCharge, Res.SucceedCount, UserID);
    //    //        if (ReturnUserMessage && Res.SucceedCount > 0)
    //    //            Res.UserMessages.Add(UserMessageKey.FinanceFailureOccuredInSend);
    //    //        return Res;
    //    //    }
    //    //}

    //    public static SendResult SendMessages(List<MessagePattern> Messages, PrivateNo PrivateNumber, List<string> Recipients, bool ReturnUserMessage)
    //    {
    //        SendResult Res = new SendResult();
    //        List<Packet> Packets = new List<Packet>();

    //        try
    //        {
    //            if (Messages.Count == 1 && Recipients.Count == 1)
    //            {
    //                Packets.Add(new Packet() { Messages = Messages, Recipients = Recipients });
    //            }
    //            else if (Messages.Count == 1 && Recipients.Count > 1)
    //            {
    //                if (Recipients.Count > PacketSize)
    //                {
    //                    int PacketCount = (int)Math.Ceiling((decimal)Recipients.Count / PacketSize);
    //                    for (int PacketIndex = 0; PacketIndex < PacketCount; PacketIndex++)
    //                    {
    //                        Packet packet = new Packet();
    //                        packet.Messages = Messages;

    //                        if (PacketIndex == PacketCount - 1)
    //                            packet.Recipients = Recipients.GetRange(PacketIndex * PacketSize, Recipients.Count - (PacketIndex * PacketSize));
    //                        else
    //                            packet.Recipients = Recipients.GetRange(PacketIndex * PacketSize, PacketSize);

    //                        Packets.Add(packet);
    //                    }
    //                }
    //                else
    //                {
    //                    Packets.Add(new Packet() { Messages = Messages, Recipients = Recipients });
    //                }
    //            }
    //            else if (Messages.Count > 1 && Recipients.Count == 1)
    //            {
    //                if (Messages.Count > PacketSize)
    //                {
    //                    int PacketCount = (int)Math.Ceiling((decimal)Messages.Count / PacketSize);
    //                    for (int PacketIndex = 0; PacketIndex < PacketCount; PacketIndex++)
    //                    {
    //                        Packet packet = new Packet();
    //                        packet.Recipients = Recipients;

    //                        if (PacketIndex == PacketCount - 1)
    //                            packet.Messages = Messages.GetRange(PacketIndex * PacketSize, Messages.Count - (PacketIndex * PacketSize));
    //                        else
    //                            packet.Messages = Messages.GetRange(PacketIndex * PacketSize, PacketSize);

    //                        Packets.Add(packet);
    //                    }
    //                }
    //                else
    //                {
    //                    Packets.Add(new Packet() { Messages = Messages, Recipients = Recipients });
    //                }
    //            }
    //            else if (Messages.Count > 1 && Recipients.Count > 1)
    //            {
    //                if (Messages.Count > PacketSize)
    //                {
    //                    int PacketCount = (int)Math.Ceiling((decimal)Messages.Count / PacketSize);
    //                    for (int PacketIndex = 0; PacketIndex < PacketCount; PacketIndex++)
    //                    {
    //                        Packet packet = new Packet();
    //                        if (PacketIndex == PacketCount - 1)
    //                        {
    //                            packet.Messages = Messages.GetRange(PacketIndex * PacketSize, Messages.Count - (PacketIndex * PacketSize));
    //                            packet.Recipients = Recipients.GetRange(PacketIndex * PacketSize, Recipients.Count - (PacketIndex * PacketSize));
    //                        }
    //                        else
    //                        {
    //                            packet.Messages = Messages.GetRange(PacketIndex * PacketSize, PacketSize);
    //                            packet.Recipients = Recipients.GetRange(PacketIndex * PacketSize, PacketSize);
    //                        }

    //                        Packets.Add(packet);
    //                    }
    //                }
    //                else
    //                {
    //                    Packets.Add(new Packet() { Messages = Messages, Recipients = Recipients });
    //                }
    //            }

    //            foreach (Packet packet in Packets)
    //            {
    //                packet.Result = SendPacket(packet.Messages, PrivateNumber, packet.Recipients.ToArray());
    //                Res.SentMessages.AddRange(packet.Result.SentMessages);
    //            }
    //        }
    //        catch (Exception Ex)
    //        {
    //            HandleException(Ex, "SMSSendBusiness.SendMessages");
    //        }

    //        Res.SucceedCount = Res.SentMessages.Count(sm => sm.GatewayMessageID != null);
    //        Res.FailedCount = Res.SentMessages.Count(sm => sm.FaultType != null);
    //        Res.UnknownCount = Res.SentMessages.Count(sm => sm.FaultType == null && sm.GatewayMessageID == null);
    //        Res.SumCount = Math.Max(Messages.Count, Recipients.Count);
    //        Res.ErrorCount = Res.SumCount - Res.SentMessages.Count;

    //        if (Res.UnknownCount > 0)
    //            SendNotificationForUnknownSend(Res.UnknownCount,PrivateNumber);

    //        if (ReturnUserMessage && Res.SucceedCount > 0)
    //            Res.UserMessages.Add(UserMessageKey.Succeed);

    //        if (ReturnUserMessage && Res.ErrorCount > 0)
    //            Res.UserMessages.Add(UserMessageKey.ErrorOccuredInSend);

    //        if (ReturnUserMessage && Res.UnknownCount > 0)
    //            Res.UserMessages.Add(UserMessageKey.UnknownOccuredInSend);

    //        return Res;
    //    }

    //    private static SendPacketResult SendPacket(List<MessagePattern> Messages, PrivateNo PrivateNumber,string[] Recipients)
    //    {
    //        SendPacketResult Res = new SendPacketResult();
    //        List<SentMessage> SavingMessages = new List<SentMessage>();
    //        pgcEntities Context = new pgcEntities();
    //        try
    //        {
    //            int PacketSize = Math.Max(Messages.Count, Recipients.Length);
    //            string PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
    //            for (int index = 0; index < PacketSize; index++)
    //            {
    //                SentMessage SavingMessage = new SentMessage();
    //                SavingMessage.Body = (Messages.Count> 1)?Messages[index].Body:Messages[0].Body;
    //                SavingMessage.RecipientNumber = (Recipients.Length >1) ? Recipients[index] : Recipients[0];
    //                SavingMessage.PrivateNo_ID = PrivateNumber.ID;
    //                SavingMessage.Date = DateTime.Now;
    //                SavingMessage.PersianDate = PersianDate;
    //                SavingMessage.PhoneDeliveryCount = 0;
    //                SavingMessage.PhoneFailCount = 0;
    //                SavingMessage.SendStatus = 0;
    //                SavingMessage.SMSCount = (Messages.Count > 1) ? Messages[index].SMSCount : Messages[0].SMSCount;
    //                SavingMessage.MessageType = (Messages.Count > 1) ? (int)Messages[index].MessageType :(int) Messages[0].MessageType;
    //                SavingMessage.TeleDeliveryCount = 0;
    //                SavingMessage.TeleFailCount = 0;



    //                SavingMessages.Add(SavingMessage);
    //                Context.SentMessages.AddObject(SavingMessage);
    //            }
    //            Context.SaveChanges();
    //        }
    //        catch(Exception Ex)
    //        {
    //            Res.Status = SendPacketResultStatus.Failed;
    //            HandleException(Ex,"SMSSendBusiness.SendPacket.InsertSentMessages");
    //            return Res;
    //        }

    //        SendToMagfaResult MagfaRes = SendToMagfa(Messages.Select(m => m.Body).ToArray(), Recipients, new string[] { PrivateNumber.Number });

    //        if (MagfaRes.Status == SendToMagfaResultStatus.SentWithoutResult ||
    //            MagfaRes.Status == SendToMagfaResultStatus.Failed ||
    //            MagfaRes.IDs.Length  != SavingMessages.Count)
    //        {
    //            Res.Status = SendPacketResultStatus.Unknown;
    //            Res.SentMessages = SavingMessages;
    //            return Res;
    //        }

    //        try
    //        {
    //            int index = 0;
    //            foreach (SentMessage SavingMessage in SavingMessages)
    //            {
    //                if (MagfaRes.IDs[index] > 1000)
    //                    SavingMessage.GatewayMessageID = MagfaRes.IDs[index];   //MessageID
    //                else
    //                    SavingMessage.FaultType = (int)MagfaRes.IDs[index];     //Fault

    //                index ++;
    //            }
    //            Context.SaveChanges();
    //            Res.Status = SendPacketResultStatus.Succeed;
    //            Res.SentMessages = SavingMessages;
    //        }
    //        catch (Exception Ex)
    //        {
    //            Res.Status = SendPacketResultStatus.Unknown;
    //            Res.SentMessages = SavingMessages;
    //            HandleException(Ex, "SMSSendBusiness.SendPacket.UpdateSentMessages");
    //            return Res;
    //        }

    //        return Res;
    //    }

    //    public static SendToMagfaResult SendToMagfa(string[] Bodies,string[] Recipients,string[] Senders )
    //    {
    //        SendToMagfaResult Res = new SendToMagfaResult();
    //        try
    //        {

    //            Magfa.SoapSmsQueuableImplementationService ServiceProvider = new Magfa.SoapSmsQueuableImplementationService();
    //            ServiceProvider.Credentials = new System.Net.NetworkCredential(MagfaUsername, MagfaPassword);
    //            ServiceProvider.PreAuthenticate = true;
    //            Res.IDs = ServiceProvider.enqueue(
    //                MagfaDomain,
    //                Bodies,
    //                Recipients,
    //                Senders,
    //                null,//new int[]{-1},
    //                null,//new string[] {"" },
    //                null,//new int[] { -1},
    //                null,//new int[] { -1},
    //                null//new long[] { 2002}
    //                );
    //            if (Res.IDs != null)
    //                Res.Status = SendToMagfaResultStatus.SentWithResult;
    //               // ServiceProvider.getRealMessageStatuses(Res.IDs);
    //            else
    //                Res.Status = SendToMagfaResultStatus.SentWithoutResult;
    //        }
    //        catch (Exception Ex)
    //        {
    //            Res.Status = SendToMagfaResultStatus.Failed;
    //            HandleException(Ex, "SMSSendBusiness.SendToMagfa");
    //        }
    //        return Res;
    //    }

    //    private static void HandleException(Exception Ex,string Event)
    //    {
    //        ExceptionHandler.HandleManualException(Ex);
    //    }

    //    private static void SendNotificationForUnknownSend(int UnknownCount , PrivateNo PrivateNumber)
    //    {
    //        //dont forget to handle exception

    //        //NotificationBusiness objNotification = new NotificationBusiness();
    //        //List<NotificationMessageDetail> NotificationDetails = new List<NotificationMessageDetail>();
    //        //NotificationDetails.Add(new NotificationMessageDetail()
    //        //{
    //        //    Key = NotificationMessageDetailKey.RequestedMessagesCount.ToString(),
    //        //    KeyCaption = PersianTitleService.GetEnumElementPersianTitle(NotificationMessageDetailKey.RequestedMessagesCount),
    //        //    Value = SentMessages.Count.ToString()
    //        //});
    //        //NotificationDetails.Add(new NotificationMessageDetail()
    //        //{
    //        //    Key = NotificationMessageDetailKey.SendDate.ToString(),
    //        //    KeyCaption = PersianTitleService.GetEnumElementPersianTitle(NotificationMessageDetailKey.SendDate),
    //        //    Value = DateTime.Now.ToString()
    //        //});
    //        //NotificationDetails.Add(new NotificationMessageDetail()
    //        //{
    //        //    Key = NotificationMessageDetailKey.PrivateNo.ToString(),
    //        //    KeyCaption = PersianTitleService.GetEnumElementPersianTitle(NotificationMessageDetailKey.PrivateNo),
    //        //    Value = PrivateNumber.Number
    //        //});
    //        //objNotification.AddCriticalNotification(UserMessageKey.Notification_SMSSendFailure, null, (int)Role.Admin, NotificationDetails);
    //    }

    //    private static void SendNotificationForFinanceFailure(PrivateNo PrivateNumber,int SucceedCount,long? UserID)
    //    {
    //        //dont forget to handle exception
    //    }
    //}

    //#region Results Structures

    //public class SendToMagfaResult
    //{
    //    public SendToMagfaResultStatus Status { get; set; }

    //    public long[] IDs { get; set; }
    //}

    //public class SendPacketResult
    //{
    //    public SendPacketResult()
    //    {
    //        SentMessages = new List<SentMessage>();
    //    }

    //    public SendPacketResultStatus Status { get; set; }

    //    public List<SentMessage> SentMessages { get; set; }
    //}

    //public class SendResult
    //{
    //    public SendResult()
    //    {
    //        SentMessages = new List<SentMessage>();
    //        UserMessages = new List<UserMessageKey>();
    //    }

    //    public List<UserMessageKey> UserMessages { get; set; }

    //    public List<SentMessage> SentMessages { get; set; } // = SucceedCount + FailedCount + UnknownCount

    //    public int SucceedCount { get; set; }

    //    public int FailedCount { get; set; }

    //    public int UnknownCount { get; set; }

    //    public int ErrorCount { get; set; }

    //    public int SumCount { get; set; }

    //    public decimal ChargeAmount { get; set; }

    //    public decimal CurrentBalance { get; set; }
    //}

    //public class Packet
    //{
    //    public Packet()
    //    {
    //        Messages = new List<MessagePattern>();
    //        Recipients = new List<string>();
    //    }

    //    public List<MessagePattern> Messages { get; set; }

    //    public List<string> Recipients { get; set; }

    //    public SendPacketResult Result { get; set; }
    //}

    //public enum SendToMagfaResultStatus
    //{
    //    SentWithResult, //with IDs
    //    SentWithoutResult,
    //    Failed
    //}

    //public enum SendPacketResultStatus
    //{
    //    Succeed,//with sent messages
    //    Unknown,//with sent messages
    //    Failed
    //}

    //#endregion Results Structures
}
