using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;
using pgc.Business.Core;
using System.Collections.Generic;

namespace pgc.Business
{
    public class SendSMSBusiness
    {
        #region Properties

        pgcEntities Context;

        public MessagePattern Message { get; set ; }

        public List<string> Recipients{ get; set ; }

        public PrivateNo PrivateNumber { get; set ; }

        #endregion Properties


        #region Constructor

        public SendSMSBusiness(MessagePattern Message,List<string> Recipients,long PrivateNumberID)
        {
            Context = new pgcEntities();
            this.Message = Message;
            this.Recipients = Recipients;
            this.PrivateNumber= Context.PrivateNoes.FirstOrDefault(p => p.ID == PrivateNumberID);
        }

        #endregion Constructor


        #region Functionalities

        public OperationResult ValidateForSend()
        {
            OperationResult Res = new OperationResult();

            if (this.PrivateNumber == null)
            {
                Res.AddMessage(UserMessageKey.NoPrivateNo);
                Res.Result = ActionResult.Failed;
                return Res;
            }

            if (string.IsNullOrEmpty(this.Message.Body))
            {
                Res.AddMessage(UserMessageKey.NoMessageBody);
                Res.Result = ActionResult.Failed;
                return Res;
            }

            return SMSBusiness.Validate(
                new List<MessagePattern>() {Message },
                PrivateNumber,
                Recipients);
        }

        public SendSMSResult Send(long? OccuredEvent_ID, EventType EventType)
        {
            return SMSBusiness.SendMessages(
              new List<MessagePattern>() { this.Message },
              this.PrivateNumber,
              this.Recipients,
              true,
              OccuredEvent_ID,
              EventType);
        }


        #endregion Functionalities
    }
}