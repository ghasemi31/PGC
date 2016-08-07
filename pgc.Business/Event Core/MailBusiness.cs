using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using kFrameWork.Business;
using pgc.Model.Enums;
using pgc.Model;
using System.IO;
using kFrameWork.Util;
using System.Net;
using pgc.Business.Core;

namespace pgc.Business
{
    public class MailBusiness
    {
        #region Constant
        public SendEmailResult SendEmailResult;
        private List<MailAddress> MailAddressList;
        private MailAddress SenderMailAddress;
        private MailAddress DefaultRecieverMailAddress;
        private List<Attachment> AttachmentList;
        private SmtpClient smtpClient;
        
        //SMTP Parameters
        private string SmtpCredentialPassword = OptionBusiness.GetLargeText(OptionKey.SmtpCredentialPassword);
        private string SmtpCredentialUserName = OptionBusiness.GetLargeText(OptionKey.SmtpCredentialUserName);
        private bool SmtpUseDefaultCredentials = OptionBusiness.GetBoolean(OptionKey.SmtpUseDefaultCredentials);
        private int SmtpClientTimeout = OptionBusiness.GetInt(OptionKey.SmtpClientTimeout);
        private string SmtpServerName = OptionBusiness.GetLargeText(OptionKey.SmtpServerName);
        private int SmtpServerPort = OptionBusiness.GetInt(OptionKey.SmtpServerPort);
        private bool SmtpEnableSSl = OptionBusiness.GetBoolean(OptionKey.SmtpEnableSSl);

        private string MailPriority = OptionBusiness.GetLargeText(OptionKey.MailPriority);
        private string SmtpDeliveryMethod = OptionBusiness.GetLargeText(OptionKey.SmtpDeliveryMethod);
        private string MailDeliveryNotification = OptionBusiness.GetLargeText(OptionKey.MailDeliveryNotification);

        //Mail Parameters
        private string SenderDisplayName = OptionBusiness.GetLargeText(OptionKey.SenderDisplayName);

        private int EmailBlockSize = OptionBusiness.GetInt(OptionKey.EmailBlockSize);
        private int EmailBlockDelay = OptionBusiness.GetInt(OptionKey.EmailBlockDelayInMiliSecond);

        
        #endregion



        public MailBusiness()
        {
            SendEmailResult = new SendEmailResult();
            MailAddressList = new List<MailAddress>();
            AttachmentList=new List<Attachment>();            
        }



        public SendEmailResult Send(string subject, string body, List<string> Recipients, EventType EventType, List<string> FilePaths, long? OccuredEventID, string SenderDisplayName,string RecieverAddress,string RecieverDisplayName,bool UseTemplate)
        {

            #region Validation And Initiate Parameters
            //validate for files
            AttachmentList = CheckValidFilesAndCreateAttachment(FilePaths);

            //validate for body
            CheckValidContents(subject, body);
            body = ManipulateContent(body);

            //validate for emails recipient
            Recipients = RemoveJunkCharacters(Recipients);
            MailAddressList = CheckValidMailAddressesAndCreateList(Recipients);

            //validate for emails sender 
            SenderMailAddress = CheckValidSenderMailAddressesAndCreateIt(SenderDisplayName);
            DefaultRecieverMailAddress = CheckValidRecieverMailAddressesAndCreateIt(RecieverAddress, RecieverDisplayName);
            #endregion

            if (SendEmailResult.ValidAddressMailCount == 0)
            {
                SendEmailResult.MustBeCancled = true;
                SendEmailResult.UserMessages.Add(UserMessageKey.InvalidAllRecipientEmails);
            }

            if (SendEmailResult.MustBeCancled)
                return SendEmailResult;
            


            #region Blocking
            List<MailMessage> MailBlocks = Blocking();

            SendEmailResult.TotalBlockCount = MailBlocks.Count;
            SendEmailResult.BlockSize = EmailBlockSize;
            #endregion

            if (SendEmailResult.MustBeCancled)
                return SendEmailResult;
            

            SetSmtpClient();


            if (SendEmailResult.MustBeCancled)
                return SendEmailResult;


            #region Create EmailSendAttempt Object For DB

            EmailSendAttemptBusiness EmailSendAttemptBusiness = new EmailSendAttemptBusiness();
            EmailSendAttempt EmailSendaAttemptLog = new EmailSendAttempt();

            EmailSendaAttemptLog.Body = body;
            EmailSendaAttemptLog.Subject = subject;
            EmailSendaAttemptLog.BlockSize = EmailBlockSize;

            EmailSendaAttemptLog.Recipients ="";
            foreach (string recipient in Recipients)
            {
                EmailSendaAttemptLog.Recipients += "," + recipient;
            }
            EmailSendaAttemptLog.Recipients = EmailSendaAttemptLog.Recipients.Substring(1);

            EmailSendaAttemptLog.EventType = (int)EventType;
            EmailSendaAttemptLog.OccuredEvent_ID = OccuredEventID;

            EmailSendaAttemptLog.Date = DateTime.Now;
            EmailSendaAttemptLog.PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);

            if (FilePaths.Count < 10)
                for (; FilePaths.Count < 10; )
                    FilePaths.Add("");

            EmailSendaAttemptLog.FilePath1 = (string.IsNullOrEmpty(FilePaths[0]) ? "" : FilePaths[0]);
            EmailSendaAttemptLog.FilePath2 = (string.IsNullOrEmpty(FilePaths[1]) ? "" : FilePaths[1]);
            EmailSendaAttemptLog.FilePath3 = (string.IsNullOrEmpty(FilePaths[2]) ? "" : FilePaths[2]);
            EmailSendaAttemptLog.FilePath4 = (string.IsNullOrEmpty(FilePaths[3]) ? "" : FilePaths[3]);
            EmailSendaAttemptLog.FilePath5 = (string.IsNullOrEmpty(FilePaths[4]) ? "" : FilePaths[4]);
            EmailSendaAttemptLog.FilePath6 = (string.IsNullOrEmpty(FilePaths[5]) ? "" : FilePaths[5]);
            EmailSendaAttemptLog.FilePath7 = (string.IsNullOrEmpty(FilePaths[6]) ? "" : FilePaths[6]);
            EmailSendaAttemptLog.FilePath8 = (string.IsNullOrEmpty(FilePaths[7]) ? "" : FilePaths[7]);
            EmailSendaAttemptLog.FilePath9 = (string.IsNullOrEmpty(FilePaths[8]) ? "" : FilePaths[8]);
            EmailSendaAttemptLog.FilePath10 = (string.IsNullOrEmpty(FilePaths[9]) ? "" : FilePaths[9]);


            #endregion


            //Sending Blocks
            #region Sending Blocks
            foreach (MailMessage Mail in MailBlocks)
            {
                if (MailBlocks.First() != Mail && EmailBlockDelay > 0)
                    System.Threading.Thread.Sleep(EmailBlockDelay);

                SentEmailBlock SentBlock=new SentEmailBlock();
                SentBlock.Date=DateTime.Now;
                SentBlock.PersianDate=DateUtil.GetPersianDateShortString(DateTime.Now);
                SentBlock.Size = Mail.Bcc.Count() + Mail.To.Count();

                SentBlock.RecipientMailAddress="";
                foreach (MailAddress item in Mail.Bcc.Union(Mail.To))
                {
                    SentBlock.RecipientMailAddress += "," + item.Address;
                }
                SentBlock.RecipientMailAddress = SentBlock.RecipientMailAddress.Substring(1);

                try
                {
                    string mailBody = body;
                    if (UseTemplate)
                    {
                        string template=kFrameWork.Business.OptionBusiness.GetHtml(OptionKey.EmailTemplate);
                        if (!string.IsNullOrEmpty(template))
                            mailBody = template.Replace("%content%", body);
                    }

                    SendMail(Mail, subject, mailBody, SmtpCredentialUserName, SenderDisplayName);

                    SentBlock.IsSent = true;

                    SendEmailResult.SentBlockCount++;
                    SendEmailResult.SentEmailCount += Mail.Bcc.Count() + Mail.To.Count;

                    EmailSendaAttemptLog.SentBlock_Count++;
                    EmailSendaAttemptLog.SentEmail_Count += Mail.Bcc.Count() + Mail.To.Count;
                    
                }                
                catch (Exception Ex)
                {
                    SentBlock.IsSent = false;

                    SendEmailResult.FailedBlockCount++;
                    SendEmailResult.FailedEmailCount += Mail.Bcc.Count() + Mail.To.Count;

                    EmailSendaAttemptLog.FailedBlock_Count++;
                    EmailSendaAttemptLog.FailedEmail_Count += Mail.Bcc.Count() + Mail.To.Count;
                }

                EmailSendaAttemptLog.SentEmailBlocks.Add(SentBlock);

                EmailSendaAttemptLog.TotalBlock_Count++;
                EmailSendaAttemptLog.TotalEmail_Count += Mail.Bcc.Count() + Mail.To.Count;                
            }
            #endregion

            //Insert Logs In DataBase            
            try
            {
                EmailSendAttemptBusiness.Insert(EmailSendaAttemptLog);
            }
            catch (Exception)
            {
                SendEmailResult.UserMessages.Add(UserMessageKey.DBInsertingProblem);
            }


            SendEmailResult.EmailSendAttempt = EmailSendaAttemptLog;
            SendEmailResult.TotalEmailCount = SendEmailResult.FailedEmailCount + SendEmailResult.SentEmailCount;


            if (SendEmailResult.SentEmailCount == SendEmailResult.ValidAddressMailCount)
              SendEmailResult.UserMessages.Add(UserMessageKey.Succeed);
            
            else if ((SendEmailResult.SentEmailCount < SendEmailResult.ValidAddressMailCount)&&(SendEmailResult.SentEmailCount > 0))
                SendEmailResult.UserMessages.Add(UserMessageKey.SomeEmailSent);
            
            else if (SendEmailResult.SentEmailCount == 0)
                SendEmailResult.UserMessages.Add(UserMessageKey.NoEmailSent);


            //Email_Manual_Sending
            #region Event Rising
            if (!OccuredEventID.HasValue && EventType == Model.Enums.EventType.Manual)
            {
                User user = new UserBusiness().Retrieve(kFrameWork.UI.UserSession.UserID);

                SystemEventArgs e = new SystemEventArgs();

                e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                e.EventVariables.Add("%user%", user.FullName);
                e.EventVariables.Add("%mobile%", user.Mobile);
                e.EventVariables.Add("%phone%", user.Tel);
                e.EventVariables.Add("%email%", user.Email);
                e.EventVariables.Add("%username%", user.Username);

                e.EventVariables.Add("%body%", body);
                e.EventVariables.Add("%subject%", subject);
                e.EventVariables.Add("%summary%", ((body.Length > 50) ? body.Substring(0, 50) : body));

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Email_Manual_Sending, e);
            }
            #endregion


            return SendEmailResult;
        }



        private void SendMail(MailMessage mailMessage,string subject,string body,string senderEmail,string senderName)
        {
            mailMessage.From = SenderMailAddress;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            foreach (Attachment Attach in AttachmentList)
            {
                mailMessage.Attachments.Add(Attach);
            }
            
            mailMessage.Body = body;
            mailMessage.Subject = subject;

            switch (MailPriority)
            {
                case ("High"):
                    mailMessage.Priority = System.Net.Mail.MailPriority.High;
                    break;
                case ("Low"):
                    mailMessage.Priority = System.Net.Mail.MailPriority.Low;
                    break;
                case ("Normal"):
                    mailMessage.Priority = System.Net.Mail.MailPriority.Normal;
                    break;
            }
            
            switch (MailDeliveryNotification)
            {
                case ("Delay"):
                    mailMessage.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.Delay;
                    break;
                case ("Never"):
                    mailMessage.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.Never;
                    break;
                case ("None"):
                    mailMessage.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.None;
                    break;
                case ("OnFailure"):
                    mailMessage.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnFailure;
                    break;
                case ("OnSuccess"):
                    mailMessage.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
                    break;
            }

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (TimeoutException ex)
            {
                ExceptionHandler.HandleManualException(ex,"pgc.Business.EventCore.MailBusiness.SendMail(Timeout)");
                throw ex;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleManualException(ex, "pgc.Business.EventCore.MailBusiness.SendMail");
                throw ex;
            }
        }



        private void SetSmtpClient()
        {
            try
            {

                smtpClient = new SmtpClient();
                smtpClient.Host = SmtpServerName;
                smtpClient.Port = SmtpServerPort;
                smtpClient.Timeout = (SmtpClientTimeout == 0) ? smtpClient.Timeout : SmtpClientTimeout;
                smtpClient.UseDefaultCredentials = SmtpUseDefaultCredentials;


                switch (SmtpDeliveryMethod)
                {
                    case ("Network"):
                        smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                        break;
                    case ("PickupDirectoryFromIis"):
                        smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis;
                        break;
                    case ("SpecifiedPickupDirectory"):
                        smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        break;
                }


                NetworkCredential SmtpCredential = new NetworkCredential();
                SmtpCredential.UserName = SmtpCredentialUserName;
                SmtpCredential.Password = SmtpCredentialPassword;

                smtpClient.Credentials = SmtpCredential;
                smtpClient.EnableSsl = SmtpEnableSSl;

                //Check The Important Properties Initiate Correctly
                if ((string.IsNullOrEmpty(SmtpCredentialUserName.Trim())) ||
                    (string.IsNullOrEmpty(SmtpCredentialPassword.Trim())) ||
                    (string.IsNullOrEmpty(smtpClient.Host.Trim())) ||
                    (smtpClient.Port < (int)1))
                {
                    SendEmailResult.UserMessages.Add(UserMessageKey.SMTPParametersNotInitiate);
                    SendEmailResult.MustBeCancled = true;
                }
            }
            catch (Exception)
            {
                SendEmailResult.UserMessages.Add(UserMessageKey.SMTPError);
                SendEmailResult.MustBeCancled = true;
            }
        }



        private List<MailMessage> Blocking()
        {
            List<MailMessage> RecipientEmails = new List<MailMessage>();

            try
            {
                MailMessage MailBlock = new MailMessage();

                int BlockSize = EmailBlockSize;
                int BlockDelay = EmailBlockDelay;

                foreach (MailAddress recipient in MailAddressList)
                {
                    if (BlockSize == 1 || MailAddressList.Count == 1)
                    {
                        MailBlock.To.Add(recipient);
                        RecipientEmails.Add(MailBlock);
                        MailBlock = new MailMessage();
                        continue;
                    }
                    else
                    {
                        if (DefaultRecieverMailAddress != null)
                            MailBlock.To.Add(DefaultRecieverMailAddress);

                        MailBlock.Bcc.Add(recipient);

                        if (BlockSize > 0 && MailBlock.Bcc.Count >= BlockSize)
                        {
                            RecipientEmails.Add(MailBlock);
                            MailBlock = new MailMessage();
                        }
                    }
                }

                //Last Block
                if (MailBlock.Bcc.Count > 0)
                    RecipientEmails.Add(MailBlock);
            }
            catch (Exception)
            {
                SendEmailResult.MustBeCancled = true;
                SendEmailResult.UserMessages.Add(UserMessageKey.BlockingFailed);
            }

            return RecipientEmails;
        }



        #region Validations

        private MailAddress CheckValidSenderMailAddressesAndCreateIt(string displayName)
        {
            try
            {
                if (!string.IsNullOrEmpty(displayName))
                    SenderDisplayName = displayName;

                return new MailAddress(SmtpCredentialUserName, SenderDisplayName);
            }
            catch (Exception)
            {
                SendEmailResult.UserMessages.Add(UserMessageKey.InvalidSenderEmail);
                SendEmailResult.MustBeCancled = true;
                return null;
            }
        }



        private MailAddress CheckValidRecieverMailAddressesAndCreateIt(string address, string displayName)
        {
            try
            {
                return new MailAddress(address, displayName);
            }
            catch (Exception)
            {
                //SendEmailResult.UserMessages.Add(UserMessageKey.InvalidRecieverEmail);
                //SendEmailResult.MustBeCancled = true;
                return null;
            }
        }



        private List<MailAddress> CheckValidMailAddressesAndCreateList(List<string> RecipientsList)
        {
            List<MailAddress> result = new List<MailAddress>();

            foreach (string recipient in RecipientsList)
            {
                try
                {                    
                    MailAddress mail = new MailAddress(recipient, recipient, System.Text.Encoding.UTF8);
                    SendEmailResult.ValidAddressMailCount++;
                    result.Add(mail);
                }
                catch (Exception Ex)
                {
                    SendEmailResult.InvalidAddressMailCount++;
                }
            }

            if (SendEmailResult.InvalidAddressMailCount > 0)
                SendEmailResult.UserMessages.Add(UserMessageKey.InvalidRecipientEmail);

            return result;
        }



        private List<Attachment> CheckValidFilesAndCreateAttachment(List<string> Files)
        {
            string[] extensions = ".ade.adp.bat.chm.cmd.com.cpl.exe.hta.ins.isp.jse.lib.mde.msc.msp.mst.pif.scr.sct.shb.sys.vb.vbe.vbs.vxd.wsc.wsf.wsh".Split('.');
            List<Attachment> result = new List<Attachment>();

            foreach (string filePath in Files)
            {
                FileInfo file = new FileInfo(filePath);
                if (extensions.Contains(file.Extension))
                {
                    SendEmailResult.UserMessages.Add(UserMessageKey.FileExtensionIsBlocked);
                    SendEmailResult.MustBeCancled = true;
                    return null;
                }

                try
                {
                    Attachment attachment = new Attachment(System.Web.HttpContext.Current.Server.MapPath(filePath));
                    result.Add(attachment);
                }
                catch (Exception ex)
                {
                    SendEmailResult.UserMessages.Add(UserMessageKey.IncompatibleFile);
                    SendEmailResult.MustBeCancled = true;
                    return result;
                }
            }

            return result;
        }



        private List<string> RemoveJunkCharacters(List<string> Recipients)
        {
            List<string> result = new List<string>();

            foreach (string recipient in Recipients)
            {
                string item = recipient;
                item = item.Trim();
                item = item.Replace("\r", "");
                item = item.Replace(" ", "");

                if (result.Contains(item) && !SendEmailResult.UserMessages.Contains(UserMessageKey.DupliacteEmailAddress))
                    SendEmailResult.UserMessages.Add(UserMessageKey.DupliacteEmailAddress);

                if (!string.IsNullOrEmpty(item) && !result.Contains(item))
                    result.Add(item);
            }
            return result;
        }
        


        private void CheckValidContents(string Subject, string body)
        {
            if (string.IsNullOrEmpty(body))
            {
                SendEmailResult.UserMessages.Add(UserMessageKey.NoEmailBody);
                SendEmailResult.MustBeCancled = true;
            }

            if (string.IsNullOrEmpty(Subject))
                SendEmailResult.UserMessages.Add(UserMessageKey.NoEmailSubject);
        }



        private string ManipulateContent(string body)
        {
            if (!string.IsNullOrEmpty(body))
            {
                body = body.Replace("\r\n", "<br />"); // Return Key.
                body = body.Replace(System.Convert.ToChar(9).ToString(), "&nbsp;&nbsp;&nbsp; "); // TAB Key.
            }
            return body;
        }
        
        #endregion

    }




    public class SendEmailResult
    {
        public int FailedEmailCount { get; set; }
        public int SentEmailCount { get; set; }
        public int TotalEmailCount { get; set; }

        public int FailedBlockCount { get; set; }
        public int SentBlockCount { get; set; }
        public int TotalBlockCount { get; set; }

        public int BlockSize { get; set; }

        public int InvalidAddressMailCount { get; set; }
        public int ValidAddressMailCount { get; set; }

        public List<UserMessageKey> UserMessages { get; set; }
        public EmailSendAttempt EmailSendAttempt { get; set; }

        public bool MustBeCancled { get; set; }
        
        public SendEmailResult()
        {
            UserMessages = new List<UserMessageKey>();
            EmailSendAttempt = new EmailSendAttempt();
            MustBeCancled = false;
        }
    }
}
