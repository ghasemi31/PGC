//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using kFrameWork.Business;
//using pgc.Model.Enums;
//using pgc.Model.Patterns;
//using kFrameWork.Util;
//using kFrameWork.UI;

//public partial class UserControls_Project_MessageControl : BaseUserControl
//{
//    private const int MinFarsiCharCode = 1548;
//    private const int MaxFarsiCharCode = 1790;

//    /// <summary>
//    /// Maximum characters in each persian message
//    /// </summary>
//    //public const int PersianMaxCharacterCount = 70;
//    public int PersianMaxCharacterCount = OptionBusiness.GetInt(OptionKey.PersianMaxCharacterCount);
//    /// <summary>
//    /// Maximum character in each latin message
//    /// </summary>
//    //public const int LatinMaxCharacterCount = 160;
//    public int LatinMaxCharacterCount = OptionBusiness.GetInt(OptionKey.LatinMaxCharacterCount);

//    /// <summary>
//    /// Maxmimum characters in each persian message when there is more than one message on the body
//    /// </summary>
//    //public const int PersianMultipleMessageReservedCharactersCount = 3;
//    public int PersianMultipleMessageReservedCharactersCount = OptionBusiness.GetInt(OptionKey.PersianMultipleMessageReservedCharactersCount);

//    /// <summary>
//    /// Maxmimum characters in each latin message when there is more than one message on the body
//    /// </summary>
//    //public const int LatinMultipleMessageReservedCharactersCount = 7;
//    public int LatinMultipleMessageReservedCharactersCount = OptionBusiness.GetInt(OptionKey.LatinMultipleMessageReservedCharactersCount);

//    private MessagePattern _Message = new MessagePattern();

//    public MessagePattern Message
//    {
//        set
//        {
//            _Message = value;
//        }
//        get
//        {
//            return _Message;
//        }
//    }

//    private MessagePattern _TemplateMessage = new MessagePattern();

//    public MessagePattern TemplateMessage
//    {
//        set
//        {
//            _TemplateMessage = value;
//            Message = value;
//            Msg.InnerText = value.Body;
//        }
//        get
//        {
//            return _TemplateMessage;
//        }
//    }

//    public string TextBody
//    {
//        set
//        {
//            Msg.InnerText = value;
//        }
//        get
//        {
//            return Msg.InnerText;
//        }
//    }

//    protected void Page_Load(object sender, EventArgs e)
//    {
//       // UIUtil.AddStartupScript("SMSKeyPress(" + Msg + ");", this);

//        if (IsPostBack)
//        {
//            Message.MessageType = ReturnMsgType(Msg.InnerText);
//            Message.Body = Msg.InnerText;
//            Message.CharCount = ReturnMsgCharCount(Msg.InnerText);
//            Message.SMSCount = ReturnMsgCount(Message.CharCount, Message.MessageType);
//        }
//        else
//        {
//            Message = TemplateMessage;
//            Msg.InnerText = TemplateMessage.Body;
//        }
//    }

//    public MessageType ReturnMsgType(string TXT)
//    {
//        foreach (char C in TXT)
//            if (C >= MinFarsiCharCode && C <= MaxFarsiCharCode)
//                return MessageType.Persian;
//        return MessageType.Latin;

//    }

//    public int ReturnMsgCount(int CharCount, MessageType Type)
//    {
//        int MaxChars = (Type == MessageType.Persian) ? PersianMaxCharacterCount : LatinMaxCharacterCount;

//        if (CharCount > MaxChars && Type == MessageType.Persian)
//            MaxChars = MaxChars - PersianMultipleMessageReservedCharactersCount;

//        if (CharCount > MaxChars && Type == MessageType.Latin)
//            MaxChars = MaxChars - LatinMultipleMessageReservedCharactersCount;

//        return ConvertorUtil.ToInt32(Math.Max(Math.Ceiling((double)CharCount / MaxChars), 1));
//    }

//    public int ReturnMsgCharCount(string TXT)
//    {
//        int EnterCount = TXT.Where(S => S.CompareTo(Convert.ToChar(13)) == 0).Count();
//        return TXT.Length - EnterCount;

//    }
//}