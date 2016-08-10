


namespace pgc.Model.Enums
{
    [PersianTitle("")]
    public enum OptionKey
    {
        AboutUs_Title,
        AboutUs_KeyWords,
        AboutUs_Description,
        AboutUs_Content,


        Admin_GreetingMessage,
        User_GreetingMessage,
        Agent_GreetingMessage,

        contact_Title,
        contact_header_title,
        contact_header_text,
        Contact_Specification,
        Contact_Body,

        User_Title,
        User_Description,
        User_Keywords,
        User_Content,
        BranchAgreement_Title,
        BranchAgreement_Content,

        HomeGiftPic,
        HomeFooter_link,
        HomeFooter_Branch,
        Home_Menu,
        NewsNumberInHomePage,
        CopyRight,
        Quality_Charter,
        Hamrah_Dizi_Pic,
        Licence,
        HomeFullPageSliderMode,
        FullPageSliderTimer,
        FullPageSpeedSlider,
        Slider_transitionStyle,
        Enamad,
        MobileApplicationModalWindows,
        HomeTitle,

        Master_Menu,
        Master_GiftPic,
        MasterFooter_RightLink,
        MasterFooter_LeftLink,
        Master_SiteMap_Social,
        Master_Footer_Social,

        Product_Bean,
        Product_Spice,
        Product_Potato,
        Product_Meat,
        Product_Onion,
        Product_Tomato,
        OrderComment,
        OrderTitle,
        SecondOfRefreshOrderPage,
        LotteryConfig,

        Keyword_Default,
        Description_Default,

        RondSoft_License,
        BranchDemantListText,
        BranchDemantListTitle,

        //requet page
        request_title,
        request_header_title,
        request_header_text,
        request_subHeader_text,
        request_subHeader_tel,
        request_subHeader_email,
        Description_BranchRequest,
        Keywords_BranchRequest,

        //map
        TehranMap,
        IranMap,
        WorldMap,
        pgcMap,

        //OnlineOrder Allow
        AllowOnlineOrdering,
        MessageForOnlineOrderIsSuspended,
        OnlineOrderInformation,
        OrderListPrintLayoutRowNumber,

        #region Event, SMS, Email Options , Google Analitic, Online Payment

        //Magfa Options
        MagfaDomain,
        MagfaUsername,
        MagfaPassword,
        PacketSize,
        PersianMessageRegularExpression,
        PersianMaxCharacterCount,
        LatinMaxCharacterCount,
        PersianMultipleMessageReservedCharactersCount,
        LatinMultipleMessageReservedCharactersCount,


        //Email Sending Options
        SmtpCredentialPassword,
        SmtpCredentialUserName,
        SmtpUseDefaultCredentials,
        SmtpClientTimeout,
        MailPriority,
        SmtpDeliveryMethod,
        MailDeliveryNotification,
        SmtpServerName,
        SmtpServerPort,
        SenderDisplayName,
        EmailBlockSize,
        EmailBlockDelayInMiliSecond,
        EmailTemplate,

        //comment
        IsRead,

        //Meta Code e.g: Google Analitics
        PreHead,
        PostHead,
        PreBody,
        PostBody,

        //Online Payment
        MerchantID,
        MerchantPassword,
        TryNumberForVerify,
        RedirectURLForSaman,
        OnlinePaymentText,
        OnlinePaymentSucceedText,
        SmtpEnableSSl,

        #endregion

        //Finance Cycle
        BranchTransactionCustomerOnlineDesc,
        BranchPayWizardDescription,
        BranchPayWizardOnlineDescription,
        BranchPayWizardOfflineDescription,
        BranchRedirectURLForSaman,
        BranchTransactionBranchOnlineDesc,
        BranchPaymentOfflineAvaileble,
        BranchTransactionBranchOfflineDesc,
        BranchOrderNewDesc,
        BranchOrderNew_AcceptFrom,
        BranchOrderNew_AcceptTo,
        BranchTransactionFactureOfBranchOrderDesc,
        BranchTransactionFactureOfBranchReturnOrderDesc,
        BranchReturnOrderNew_AcceptFrom,
        BranchReturnOrderNew_AcceptTo,
        BranchReturnOrderNewDesc,
        BranchLackOrderNew_AcceptFrom,
        BranchLackOrderNew_AcceptTo,
        BranchLackOrderNewDesc,
        BranchLackOrderNewClosed,
        BranchOrderNewClosed,
        BranchReturnOrderNewClosed,
        BranchTransactionFactureOfBranchLackOrderDesc,

        //print layout
        BranchFinancePrintLayoutTitleHtml,
        BranchFinancePrintLayoutFooterHtml,
        BoundaryTime_For_OrdersSearch,

        ActiveAjax,

        GooglePlayLink,
        AppStoreLink,

        GalleryTitle,
        Description_Gallery,
        Keyworde_Gallery,

        BranchListTitle,
        Description_BranchList,
        Keywords_BranchList,

        NewsListTitle,
        Description_NewsList,
        Keywords_NewsList,

        Description_Order,
        Keywords_Order,

        Description_Contact,
        Keywords_Contact,

        Login_Title,
        Login_Description,
        Login_Keywords,
        Mellat_TerminalId,
        Mellat_UserName,
        Mellat_Password,
        Mellat_BankURL
    }

}