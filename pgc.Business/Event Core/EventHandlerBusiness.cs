using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Enums;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using System.Collections;
using System.Data.Objects.DataClasses;
using pgc.Business.Core;
using System.Threading;
using kFrameWork.UI;

namespace pgc.Business
{

    public class EventHandlerBusiness
    {
        public static void HandelSystemEvent(SystemEventKey Key, SystemEventArgs e)
        {
            Thread newThread = new Thread(new ParameterizedThreadStart(SubHandelSystemEvent));
            List<object> obj = new List<object>();

            obj.Add(Key);
            obj.Add(e);

            newThread.Start(obj);
        }

        private static void SubHandelSystemEvent(object obj)
        {
            try
            {
                SystemEventKey Key = (SystemEventKey)((List<object>)obj)[0];
                SystemEventArgs e = (SystemEventArgs)((List<object>)obj)[1];

                OperationResult op = new OperationResult();
                SystemEvent SystemEvent = new SystemEventBusiness().RetrieveByKey(Key);
                OccuredSystemEventBusiness OccuredBusiness = new OccuredSystemEventBusiness();
                MailBusiness mailBusiness = new MailBusiness();
                OccuredSystemEvent occured = new OccuredSystemEvent();
                occured.Date = DateTime.Now;
                occured.PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
                occured.SystemEvent_ID = SystemEvent.ID;
                occured.Description = "";
                occured.DeviceType_Enum = (int)e.Device_Type;

                if (e.Related_User != null)
                    occured.Actor = e.Related_User.Email;
                else
                    if (e.Related_Doer != null)
                        occured.Actor = e.Related_Doer.Email;
                    else
                        if (e.Related_Branch != null)
                            occured.Actor = e.Related_Branch.Title;
                        else
                            occured.Actor = "-";

                OperationResult dbRES = OccuredBusiness.Insert(occured);

                if (dbRES.Result != ActionResult.Done)
                    op.AddMessage(UserMessageKey.DBInsertingProblem);


                List<string> RecipientMailAdmin = GetRecipientsList(SystemEvent, e, true, true);
                List<string> RecipientSMSAdmin = GetRecipientsList(SystemEvent, e, true, false);
                List<string> RecipientMailUser = GetRecipientsList(SystemEvent, e, false, true);
                List<string> RecipientSMSUser = GetRecipientsList(SystemEvent, e, false, false);


                #region Set Text Templates

                string BodyEmailAdmin = SystemEvent.Template_Admin_Email;
                string BodySmsAdmin = SystemEvent.Template_Admin_SMS;
                string BodyEmailUser = SystemEvent.Template_User_Email;
                string BodySmsUser = SystemEvent.Template_User_SMS;

                foreach (var item in e.EventVariables)
                {
                    BodyEmailAdmin = BodyEmailAdmin.Replace(item.Key, item.Value);
                    BodySmsAdmin = BodySmsAdmin.Replace(item.Key, item.Value);
                    BodyEmailUser = BodyEmailUser.Replace(item.Key, item.Value);
                    BodySmsUser = BodySmsUser.Replace(item.Key, item.Value);
                }
                #endregion



                #region Mail Admin Sending
                OperationResult opMailAdmin = new OperationResult();
                opMailAdmin.Result = ActionResult.Done;
                try
                {
                    if (RecipientMailAdmin.Count != 0)
                    {
                        SendEmailResult ser = mailBusiness.Send(SystemEvent.Title, BodyEmailAdmin, RecipientMailAdmin, EventType.System, new List<string>(), occured.ID, "", "", "", true);

                        if (ser.UserMessages.Contains(UserMessageKey.Succeed))
                            opMailAdmin.Result = ActionResult.Done;

                        else if (ser.UserMessages.Contains(UserMessageKey.SomeEmailSent))
                        {
                            opMailAdmin.Result = ActionResult.DonWithFailure;
                            opMailAdmin.Messages.AddRange(ser.UserMessages);
                        }

                        else if (ser.UserMessages.Contains(UserMessageKey.NoEmailSent))
                        {
                            opMailAdmin.Result = ActionResult.Failed;
                            opMailAdmin.Messages.AddRange(ser.UserMessages);
                        }
                    }
                }
                catch (Exception)
                {
                    opMailAdmin.AddMessage(UserMessageKey.EmailSendingFailed);
                    opMailAdmin.Result = ActionResult.Failed;
                }
                #endregion



                #region SMS Admin Sending
                OperationResult opSMSAdmin = new OperationResult();
                opSMSAdmin.Result = ActionResult.Done;
                try
                {
                    if (RecipientSMSAdmin.Count != 0)
                    {

                        MessagePattern msp = new MessagePattern();
                        msp.Body = BodySmsAdmin;
                        msp.CharCount = MessagePattern.RetriveCharCount(msp.Body);
                        msp.MessageType = MessagePattern.RetriveMessageType(msp.Body);
                        msp.SMSCount = MessagePattern.RetriveSMSCount(msp.CharCount, msp.MessageType);

                        SendSMSBusiness Business;

                        if (SystemEvent.SystemEventAction.PrivateNo_ID.HasValue &&
                            new PrivateNoBusiness().Retrieve(SystemEvent.SystemEventAction.PrivateNo_ID.Value).Status == (int)PrivateNoStatus.Enabled)
                        {
                            Business = new SendSMSBusiness(msp, RecipientSMSAdmin, SystemEvent.SystemEventAction.PrivateNo_ID.Value);

                            OperationResult sendRES = Business.ValidateForSend();

                            SendSMSResult ssr = new SendSMSResult();

                            if (sendRES.Result == ActionResult.Done)
                            {
                                //If valid Send & add send messages
                                ssr = Business.Send(occured.ID, EventType.System);

                                if (ssr.UserMessages.Contains(UserMessageKey.Succeed))
                                {
                                    opSMSAdmin.Result = ActionResult.Done;
                                    opSMSAdmin.Messages.AddRange(ssr.UserMessages);
                                }
                                else
                                {
                                    opSMSAdmin.Result = ActionResult.Failed;
                                    opSMSAdmin.Messages.AddRange(ssr.UserMessages);
                                }
                            }
                            else
                            {
                                //If Not Valid & add validation messages
                                opSMSAdmin.Result = ActionResult.Failed;
                                opSMSAdmin.Messages.AddRange(sendRES.Messages);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    opSMSAdmin.AddMessage(UserMessageKey.SMSSendingFailed);
                    opSMSAdmin.Result = ActionResult.DonWithFailure;
                }
                #endregion



                #region Mail User Sending
                OperationResult opMailUser = new OperationResult();
                opMailUser.Result = ActionResult.Done;
                try
                {
                    if (RecipientMailUser.Count != 0)
                    {
                        SendEmailResult ser = mailBusiness.Send(SystemEvent.Title, BodyEmailUser, RecipientMailUser, EventType.System, new List<string>(), occured.ID, "", "", "", true);

                        if (ser.UserMessages.Contains(UserMessageKey.Succeed))
                            opMailUser.Result = ActionResult.Done;

                        else if (ser.UserMessages.Contains(UserMessageKey.SomeEmailSent))
                        {
                            opMailUser.Result = ActionResult.DonWithFailure;
                            opMailUser.Messages.AddRange(ser.UserMessages);
                        }

                        else if (ser.UserMessages.Contains(UserMessageKey.NoEmailSent))
                        {
                            opMailUser.Result = ActionResult.Failed;
                            opMailUser.Messages.AddRange(ser.UserMessages);
                        }
                    }
                }
                catch (Exception)
                {
                    opMailUser.AddMessage(UserMessageKey.EmailSendingFailed);
                    opMailUser.Result = ActionResult.Failed;
                }
                #endregion



                #region SMS User Sending
                OperationResult opSMSUser = new OperationResult();
                opSMSUser.Result = ActionResult.Done;
                try
                {
                    if (RecipientSMSUser.Count != 0)
                    {

                        MessagePattern msp = new MessagePattern();
                        msp.Body = BodySmsUser;
                        msp.CharCount = MessagePattern.RetriveCharCount(msp.Body);
                        msp.MessageType = MessagePattern.RetriveMessageType(msp.Body);
                        msp.SMSCount = MessagePattern.RetriveSMSCount(msp.CharCount, msp.MessageType);

                        SendSMSBusiness Business;
                        if (SystemEvent.SystemEventAction.PrivateNo_ID.HasValue &&
                            new PrivateNoBusiness().Retrieve(SystemEvent.SystemEventAction.PrivateNo_ID.Value).Status == (int)PrivateNoStatus.Enabled)
                        {
                            Business = new SendSMSBusiness(msp, RecipientSMSUser, SystemEvent.SystemEventAction.PrivateNo_ID.Value);

                            OperationResult sendRES = Business.ValidateForSend();

                            SendSMSResult ssr = new SendSMSResult();

                            if (sendRES.Result == ActionResult.Done)
                            {
                                //If valid Send & add send messages
                                ssr = Business.Send(occured.ID, EventType.System);

                                if (ssr.UserMessages.Contains(UserMessageKey.Succeed))
                                {
                                    opSMSUser.Result = ActionResult.Done;
                                    opSMSUser.Messages.AddRange(ssr.UserMessages);
                                }
                                else
                                {
                                    opSMSUser.Result = ActionResult.Failed;
                                    opSMSUser.Messages.AddRange(ssr.UserMessages);
                                }
                            }
                            else
                            {
                                //If Not Valid & add validation messages
                                opSMSUser.Result = ActionResult.Failed;
                                opSMSUser.Messages.AddRange(sendRES.Messages);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    opSMSUser.AddMessage(UserMessageKey.SMSSendingFailed);
                    opSMSUser.Result = ActionResult.DonWithFailure;
                }
                #endregion

                if (opSMSUser.Result == ActionResult.Done &&
                    opSMSAdmin.Result == ActionResult.Done &&
                    opMailAdmin.Result == ActionResult.Done &&
                    opMailUser.Result == ActionResult.Done)

                    op.Result = ActionResult.Done;

                else if (opSMSAdmin.Result == ActionResult.Done ||
                    opSMSUser.Result == ActionResult.Done ||
                    opMailAdmin.Result == ActionResult.Done ||
                    opMailUser.Result == ActionResult.Done)

                    op.Result = ActionResult.DonWithFailure;

                else
                    op.Result = ActionResult.Failed;

                op.Messages.AddRange(opMailAdmin.Messages);
                op.Messages.AddRange(opSMSAdmin.Messages);
                op.Messages.AddRange(opMailUser.Messages);
                op.Messages.AddRange(opSMSUser.Messages);

                try
                {
                    occured.Description = EventResult(SystemEvent.Title, op);
                    OccuredBusiness.Update(occured);
                }
                catch (Exception)
                {
                    if (!op.Messages.Contains(UserMessageKey.DBInsertingProblem))
                        op.AddMessage(UserMessageKey.DBInsertingProblem);
                    op.Result = ActionResult.Failed;
                }


                if (op.Result != ActionResult.Done)
                    ExceptionHandler.HandleManualException(new Exception("Failed EventName:" + Key.ToString()), "pgc.Business.EventHandlerBusiness.HandelSystemEvent");


            }
            catch (Exception exc)
            {
                try
                {
                    ExceptionHandler.HandleManualException(new Exception("Failed EventName:" + exc.Message.ToString()), "pgc.Business.EventHandlerBusiness.HandelSystemEvent");
                }
                catch (Exception exc1)
                {
                    //Please GOD Help Meee
                }

            }
        }


        private static List<string> GetRecipientsList(SystemEvent SystemEvent, SystemEventArgs e, bool isAdmin, bool isEmail)
        {
            UserBusiness uB = new UserBusiness();
            UserPattern uP = new UserPattern();
            List<User> UserList = new List<User>();

            List<string> result = new List<string>();

            // Check 1.Event Support Action 2.Admin Mark Action 3.the Object Exist Then Insert To Proper List

            if (isAdmin)
            {
                if (isEmail)
                {
                    #region Admin Email List
                    //Manual Admin Email
                    if ((SystemEvent.Support_Manual_Email) &&
                        (SystemEvent.SystemEventAction != null) &&
                        (!string.IsNullOrEmpty(SystemEvent.SystemEventAction.ManualEmail)))
                    {
                        foreach (string item in SystemEvent.SystemEventAction.ManualEmail.Split('\n'))
                        {
                            string text = item;
                            text = text.Replace(" ", "");
                            text = text.Replace("\r", "");
                            if (!string.IsNullOrEmpty(text))
                                result.Add(text);
                        }
                    }

                    //Custom Role Email
                    if ((SystemEvent.Support_Custom_Role_Email) &&
                        (SystemEvent.SystemEventAction != null) &&
                        (SystemEvent.SystemEventAction.Custom_Role_Email) &&
                        SystemEvent.SystemEventAction.Custom_Role_ID.HasValue &&
                            ((Role)SystemEvent.SystemEventAction.Custom_Role_ID.Value == Role.Admin ||
                             (Role)SystemEvent.SystemEventAction.Custom_Role_ID.Value == Role.Agent)
                        )
                    {
                        uP = new UserPattern();
                        uP.Role = (Role)SystemEvent.SystemEventAction.Custom_Role_ID.Value;

                        UserList = uB.Search_Select(uP);
                        foreach (User item in UserList)
                            if (!string.IsNullOrEmpty(item.Email))
                                result.Add(item.Email);
                    }


                    //Custom AccessLevel Email
                    if ((SystemEvent.Support_Custom_AccessLevel_Email) &&
                        (SystemEvent.SystemEventAction != null) &&
                        (SystemEvent.SystemEventAction.Custom_AccessLevel_Email) &&
                        SystemEvent.SystemEventAction.Custom_AccessLevel_ID.HasValue)
                    {
                        uP = new UserPattern();
                        uP.AccessLevel_ID = SystemEvent.SystemEventAction.Custom_AccessLevel_ID.Value;

                        UserList = uB.Search_Select(uP);
                        foreach (User item in UserList)
                            if (!string.IsNullOrEmpty(item.Email))
                                result.Add(item.Email);
                    }


                    //Custom User Email
                    if ((SystemEvent.Support_Custom_User_Email) &&
                        (SystemEvent.SystemEventAction != null) &&
                        (SystemEvent.SystemEventAction.Custom_User_Email) &&
                        (SystemEvent.SystemEventAction.Custom_User_ID.HasValue))
                    {
                        string UserEmail = new UserBusiness().Retrieve(SystemEvent.SystemEventAction.Custom_User_ID.Value).Email;
                        if (!string.IsNullOrEmpty(UserEmail))
                            result.Add(UserEmail);
                    }

                    //Related Doer Email
                    if ((SystemEvent.Support_Related_Doer_Email) &&
                        (SystemEvent.SystemEventAction != null) &&
                        (SystemEvent.SystemEventAction.Related_Doer_Email) &&
                        (e.Related_Doer != null && e.Related_Doer.ID > 0))
                    {
                        string DoerEmail = new UserBusiness().Retrieve(e.Related_Doer.ID).Email;
                        if (!string.IsNullOrEmpty(DoerEmail))
                            result.Add(DoerEmail);
                    }


                    //Related Branch Email
                    if ((SystemEvent.Support_Related_Branch_Email) &&
                        (SystemEvent.SystemEventAction != null) &&
                        (SystemEvent.SystemEventAction.Related_Branch_Email) &&
                        (e.Related_Branch != null && e.Related_Branch.ID > 0))
                    {
                        UserList = new BranchBusiness().Retrieve(e.Related_Branch.ID).Users.ToList();
                        foreach (User item in UserList)
                            if (!string.IsNullOrEmpty(item.Email))
                                result.Add(item.Email);
                    }
                    #endregion
                }
                else
                {
                    #region Admin SMS List
                    //Manual Manual SMS
                    if ((SystemEvent.Support_Manual_SMS) &&
                        (SystemEvent.SystemEventAction != null) &&
                        (!string.IsNullOrEmpty(SystemEvent.SystemEventAction.ManualSMS)))
                    {
                        foreach (string item in SystemEvent.SystemEventAction.ManualSMS.Split('\n'))
                        {
                            string text = item;
                            text = text.Replace(" ", "");
                            text = text.Replace("\r", "");
                            if (!string.IsNullOrEmpty(text))
                                result.Add(text);
                        }
                    }

                    //Custom Role SMS
                    if ((SystemEvent.Support_Custom_Role_SMS) &&
                        (SystemEvent.SystemEventAction != null) &&
                        (SystemEvent.SystemEventAction.Custom_Role_SMS) &&
                        SystemEvent.SystemEventAction.Custom_Role_ID.HasValue &&
                            ((Role)SystemEvent.SystemEventAction.Custom_Role_ID.Value == Role.Admin ||
                            (Role)SystemEvent.SystemEventAction.Custom_Role_ID.Value == Role.Agent)
                        )
                    {
                        uP = new UserPattern();
                        uP.Role = (Role)SystemEvent.SystemEventAction.Custom_Role_ID.Value;

                        UserList = uB.Search_Select(uP);
                        foreach (User item in UserList)
                            if (!string.IsNullOrEmpty(item.Mobile))
                                result.Add(item.Mobile);
                    }


                    //Custom AccessLevel SMS
                    if ((SystemEvent.Support_Custom_AccessLevel_SMS) &&
                        (SystemEvent.SystemEventAction != null) &&
                        (SystemEvent.SystemEventAction.Custom_AccessLevel_SMS) &&
                        SystemEvent.SystemEventAction.Custom_AccessLevel_ID.HasValue)
                    {
                        uP = new UserPattern();
                        uP.AccessLevel_ID = SystemEvent.SystemEventAction.Custom_AccessLevel_ID.Value;

                        UserList = uB.Search_Select(uP);
                        foreach (User item in UserList)
                            if (!string.IsNullOrEmpty(item.Mobile))
                                result.Add(item.Mobile);
                    }

                    //Custom User SMS
                    if ((SystemEvent.Support_Custom_User_SMS) &&
                        (SystemEvent.SystemEventAction != null) &&
                        (SystemEvent.SystemEventAction.Custom_User_SMS) &&
                        (SystemEvent.SystemEventAction.Custom_User_ID.HasValue))
                    {
                        string UserSMS = new UserBusiness().Retrieve(SystemEvent.SystemEventAction.Custom_User_ID.Value).Mobile;
                        if (!string.IsNullOrEmpty(UserSMS))
                            result.Add(UserSMS);
                    }


                    //Related Doer SMS
                    if ((SystemEvent.Support_Related_Doer_SMS) &&
                        (SystemEvent.SystemEventAction != null) &&
                        (SystemEvent.SystemEventAction.Related_Doer_SMS) &&
                        (e.Related_Doer != null && e.Related_Doer.ID > 0))
                    {
                        string DoerSMS = new UserBusiness().Retrieve(e.Related_Doer.ID).Mobile;
                        if (!string.IsNullOrEmpty(DoerSMS))
                            result.Add(DoerSMS);
                    }


                    //Related Branch SMS
                    if ((SystemEvent.Support_Related_Branch_SMS) &&
                        (SystemEvent.SystemEventAction != null) &&
                        (SystemEvent.SystemEventAction.Related_Branch_SMS) &&
                        (e.Related_Branch != null && e.Related_Branch.ID > 0))
                    {
                        UserList = new BranchBusiness().Retrieve(e.Related_Branch.ID).Users.ToList();
                        foreach (User item in UserList)
                            if (!string.IsNullOrEmpty(item.Mobile))
                                result.Add(item.Mobile);
                    }
                    #endregion
                }
            }
            else
            {
                if (isEmail)
                {
                    #region User Email List
                    //Custom Role Email
                    if ((SystemEvent.Support_Custom_Role_Email) &&
                        (SystemEvent.SystemEventAction != null) &&
                        (SystemEvent.SystemEventAction.Custom_Role_Email) &&
                        SystemEvent.SystemEventAction.Custom_Role_ID.HasValue &&
                        (Role)SystemEvent.SystemEventAction.Custom_Role_ID.Value == Role.User)
                    {
                        uP = new UserPattern();
                        uP.Role = (Role)SystemEvent.SystemEventAction.Custom_Role_ID.Value;

                        UserList = uB.Search_Select(uP);
                        foreach (User item in UserList)
                            if (!string.IsNullOrEmpty(item.Email))
                                result.Add(item.Email);
                    }

                    //Related User Email
                    if ((SystemEvent.Support_Related_User_Email) &&
                        (SystemEvent.SystemEventAction != null) &&
                        (SystemEvent.SystemEventAction.Related_User_Email) &&
                        (e.Related_User != null && e.Related_User.ID > 0))
                    {
                        string email = uB.Retrieve(e.Related_User.ID).Email;
                        if (!string.IsNullOrEmpty(email))
                            result.Add(email);
                    }

                    //Related Guest Email
                    if ((SystemEvent.Support_Related_Guest_Email) &&
                        (SystemEvent.SystemEventAction != null) &&
                        (SystemEvent.SystemEventAction.Related_Guest_Email) &&
                        (!string.IsNullOrEmpty(e.Related_Guest_Email)))

                        result.Add(e.Related_Guest_Email);

                    #endregion
                }
                else
                {
                    #region User SMS List
                    //Custom Role SMS
                    if ((SystemEvent.Support_Custom_Role_SMS) &&
                        (SystemEvent.SystemEventAction != null) &&
                        (SystemEvent.SystemEventAction.Custom_Role_SMS) &&
                        SystemEvent.SystemEventAction.Custom_Role_ID.HasValue &&
                        (Role)SystemEvent.SystemEventAction.Custom_Role_ID.Value == Role.User)
                    {
                        uP = new UserPattern();
                        uP.Role = (Role)SystemEvent.SystemEventAction.Custom_Role_ID.Value;

                        UserList = uB.Search_Select(uP);
                        foreach (User item in UserList)
                            if (!string.IsNullOrEmpty(item.Mobile))
                                result.Add(item.Mobile);
                    }


                    //Related User SMS
                    if ((SystemEvent.Support_Related_User_SMS) &&
                        (SystemEvent.SystemEventAction != null) &&
                        (SystemEvent.SystemEventAction.Related_User_SMS) &&
                        (e.Related_User != null && e.Related_User.ID > 0))
                    {
                        string mobile = uB.Retrieve(e.Related_User.ID).Mobile;
                        if (!string.IsNullOrEmpty(mobile))
                            result.Add(mobile);
                    }

                    //Related Guest SMS
                    if ((SystemEvent.Support_Related_Guest_SMS) &&
                        (SystemEvent.SystemEventAction != null) &&
                        (SystemEvent.SystemEventAction.Related_Guest_SMS) &&
                        (!string.IsNullOrEmpty(e.Related_Guest_Phone)))

                        result.Add(e.Related_Guest_Phone);
                    #endregion
                }
            }

            result = result.Distinct().ToList();

            return result;
        }

        //public static OperationResult HandelScheduleEvent(long Schedule_ID)
        //{
        //    OperationResult op = new OperationResult();
        //    ScheduleEventBusiness Business = new ScheduleEventBusiness();
        //    ScheduleEvent ScheduleEvent = new ScheduleEvent();
        //    try
        //    {
        //        ScheduleEvent = Business.Retrieve(Schedule_ID);
        //    }
        //    catch (Exception)
        //    {
        //        op.AddMessage(UserMessageKey.NotRetrieveScheduleEvent);
        //        op.Result = ActionResult.Failed;
        //        return op;
        //    }

        //    OccuredScheduleEventBusiness OccuredBusiness = new OccuredScheduleEventBusiness();
        //    OccuredScheduleEvent occuredScheduleEvent = new OccuredScheduleEvent();
        //    occuredScheduleEvent.Date = DateTime.Now;
        //    occuredScheduleEvent.PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
        //    occuredScheduleEvent.ScheduleEvent_ID = ScheduleEvent.ID;
        //    occuredScheduleEvent.Description = "";
        //    OperationResult dbRES = OccuredBusiness.Insert(occuredScheduleEvent);

        //    if (!dbRES.Messages.Contains(UserMessageKey.Succeed))
        //    {
        //        op.AddMessage(UserMessageKey.DBInsertingProblem);
        //        op.Result = ActionResult.Failed;
        //    }

        //    List<ScheduleEventAction> EmailRecipientList = ScheduleEvent.ScheduleEventActions.Where(f => f.ActionType == (int)EventActionType.Email).ToList();
        //    List<ScheduleEventAction> SMSRecipientList = ScheduleEvent.ScheduleEventActions.Where(f => f.ActionType == (int)EventActionType.SMS).ToList();


        //    #region Mail Sending
        //    OperationResult opMail = new OperationResult();
        //    opMail.Result = ActionResult.Done;
        //    try
        //    {
        //        if (EmailRecipientList.Count != 0)
        //        {
        //            List<string> recipientList = EmailRecipientList.Where(f => f.User_ID == null).Select(g => g.Destination).ToList();
        //            recipientList.AddRange(EmailRecipientList.Where(f => f.User_ID != null).Select(g => g.User.Email).ToList());

        //            SendEmailResult ser = new MailBusiness().Send(ScheduleEvent.Title, ScheduleEvent.EmailTemplate, recipientList, EventType.Schedule, new List<string>(), occuredScheduleEvent.ID, "", "", "", "", true);

        //            if (ser.UserMessages.Contains(UserMessageKey.Succeed))
        //                opMail.Result = ActionResult.Done;

        //            else if (ser.UserMessages.Contains(UserMessageKey.SomeEmailSent))
        //            {
        //                opMail.Result = ActionResult.DonWithFailure;
        //                opMail.Messages.AddRange(ser.UserMessages);
        //            }

        //            else if (ser.UserMessages.Contains(UserMessageKey.NoEmailSent))
        //            {
        //                opMail.Result = ActionResult.Failed;
        //                opMail.Messages.AddRange(ser.UserMessages);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        opMail.AddMessage(UserMessageKey.EmailSendingFailed);
        //        opMail.Result = ActionResult.Failed;
        //    }
        //    #endregion

        //    #region SMS Sending
        //    OperationResult opSMS = new OperationResult();
        //    opSMS.Result = ActionResult.Done;

        //    try
        //    {
        //        if (SMSRecipientList.Count != 0)
        //        {
        //            List<string> recipientList = SMSRecipientList.Where(f => f.User_ID == null).Select(g => g.Destination).ToList();
        //            recipientList.AddRange(SMSRecipientList.Where(f => f.User_ID != null).Select(g => g.User.Mobile).ToList());

        //            MessagePattern msp = new MessagePattern();
        //            msp.Body = ScheduleEvent.SMSTemplate;
        //            msp.CharCount = MessagePattern.RetriveCharCount(msp.Body);
        //            msp.MessageType = MessagePattern.RetriveMessageType(msp.Body);
        //            msp.SMSCount = MessagePattern.RetriveSMSCount(msp.CharCount, msp.MessageType);

        //            SendSMSBusiness SMSBusiness = new SendSMSBusiness(msp, recipientList, ScheduleEvent.PrivateNo_ID);
        //            OperationResult sendRES = SMSBusiness.ValidateForSend();

        //            SendSMSResult ssr = new SendSMSResult();

        //            if (sendRES.Result == ActionResult.Done)
        //            {
        //                //If valid Send & add send messages
        //                ssr = SMSBusiness.Send(occuredScheduleEvent.ID, EventType.Schedule);

        //                if (ssr.UserMessages.Contains(UserMessageKey.Succeed))
        //                {
        //                    opSMS.Result = ActionResult.Done;
        //                    opSMS.Messages.AddRange(ssr.UserMessages);
        //                }
        //                else
        //                {
        //                    opSMS.Result = ActionResult.Failed;
        //                    opSMS.Messages.AddRange(ssr.UserMessages);
        //                }
        //            }
        //            else
        //            {
        //                //If Not Valid & add validation messages
        //                opSMS.Result = ActionResult.Failed;
        //                opSMS.Messages.AddRange(sendRES.Messages);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        opSMS.AddMessage(UserMessageKey.SMSSendingFailed);
        //        opSMS.Result = ActionResult.DonWithFailure;
        //    }
        //    #endregion


        //    if (opSMS.Result == ActionResult.Done && opMail.Result == ActionResult.Done)
        //        op.Result = ActionResult.Done;
        //    else if (opSMS.Result == ActionResult.Done || opMail.Result == ActionResult.Done)
        //        op.Result = ActionResult.DonWithFailure;
        //    else
        //        op.Result = ActionResult.Failed;

        //    op.Messages.AddRange(opMail.Messages);
        //    op.Messages.AddRange(opSMS.Messages);

        //    try
        //    {
        //        occuredScheduleEvent.Description = EventResult(ScheduleEvent.Title, op);
        //        OccuredBusiness.Update(occuredScheduleEvent);
        //    }
        //    catch (Exception)
        //    {
        //        if (!op.Messages.Contains(UserMessageKey.DBInsertingProblem))
        //            op.AddMessage(UserMessageKey.DBInsertingProblem);
        //        op.Result = ActionResult.Failed;
        //    }
        //    return op;
        //}


        private static string EventResult(string title, OperationResult op)
        {
            string TotalResult = ""; //string.Format("اجرای اقدامات رخداد \"{0}\" ", title);

            if (op.Result == ActionResult.Done)
            {
                TotalResult += "با موفقیت انجام شد ";
            }
            else if (op.Result == ActionResult.DonWithFailure)
            {
                TotalResult += "بطور ناقص انجام شد ";
            }
            else if (op.Result == ActionResult.Failed)
            {
                TotalResult += "با مشکل مواجه شد ";
            }
            pgcEntities UsermessageContext = new pgcEntities();
            if (op.Result != ActionResult.Done)
            {
                string msg = "";
                foreach (UserMessageKey key in op.Messages)
                {
                    string stringKey = key.ToString();
                    msg += ", " + UsermessageContext.UserMessages.Single(f => f.Key == stringKey).Description;
                }
                if (msg.Length > 2)
                    TotalResult += msg.Substring(2);
            }
            return TotalResult;
        }
    }
}
