//using kFrameWork.UI;
//using pgc.Model.Patterns;
//using pgc.Model.Enums;

//public partial class Pages_Admin_SentMessage_Search : BaseSearchControl<SentSMSPattern>
//{
//    public override SentSMSPattern Pattern
//    {
//        get
//        {
//            return new SentSMSPattern()
//            {
//                Message=txtBody.Text,
//                MessageType=lkcMessageType.GetSelectedValue<MessageType>(),
//                RecipientNumber=txtRecipient.Text,
//                SendDate=pdrSendPersianDate.DateRange,
//                SendStatus=lkcSendStatus.GetSelectedValue<SendSMSStatus>()
//            };
//        }
//        set
//        {
//            txtBody.Text = value.Message;
//            txtRecipient.Text = value.RecipientNumber;
//            lkcMessageType.SetSelectedValue(value.MessageType);
//            lkcSendStatus.SetSelectedValue(value.SendStatus);
//            pdrSendPersianDate.DateRange = value.SendDate;
//        }
//    }
//}