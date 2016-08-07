using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Model.Enums;
using kFrameWork.Model;
using pgc.Business.General;
using pgc.Model;
using kFrameWork.Util;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using pgc.Business.Core;
using pgc.Business;
using System.Text.RegularExpressions;

public partial class Pages_Guest_Order : BasePage
{
    public OrderBusiness business = new OrderBusiness();
    public List<Branch> branch;
    public List<Product> product;
    public long id;
    public int count;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            id = this.GetQueryStringValue<long>(QueryStringKeys.id);
            count = this.GetQueryStringValue<int>(QueryStringKeys.count);
        }

        branch = business.GetBranchList();
        product = business.GetProducts();
        if (Request.Form["action"] != null && Request.Form["action"] == "orderForm")
        {
            SubmitOrder();
        }
    }

    public void SubmitOrder()
    {
        OrderBusiness business = new OrderBusiness();
        Order model = new Order();
        model.DeviceType_Enum = (int)pgc.Model.Enums.Device.WebApp;
        model.Name = Request.Form["txtTransferee"];
        model.Tel = Request.Form["txtPhone"];
        model.Mobile = Request.Form["txtMobile"];
        model.Address = Request.Form["txtAddress"];
        model.Comment = Request.Form["txtDescription"];
        model.TotalAmount = ConvertorUtil.ToInt32(Request.Form["total-amount"].Trim());
        model.PayableAmount = model.TotalAmount;
        model.PaymentType = (Request.Form["peymentType"] == "1") ? (int)PaymentType.Presence : (int)PaymentType.Online;
        model.Branch_ID = ConvertorUtil.ToInt64(Request.Form["branchID"]);
        long orderID = business.AddNewOrder(model);
        OperationResult result = new OperationResult();
        result = business.AddOrderDetail(Request.Form["item-selected"], orderID);
        UserSession.AddMessage(result.Messages);

        //Order_New
        #region Event Raising

        SystemEventArgs e = new SystemEventArgs();
        User user = new pgc.Business.General.UserBusiness().RetriveUser(UserSession.UserID);

        e.Related_User = user;
        if (model.Branch_ID.HasValue)
            e.Related_Branch = new pgc.Business.General.BranchBusiness().RetirveBranchID(model.Branch_ID.Value);

        //Details Of User
        e.EventVariables.Add("%user%", e.Related_User.FullName);
        e.EventVariables.Add("%username%", e.Related_User.Username);
        e.EventVariables.Add("%email%", e.Related_User.Email);
        e.EventVariables.Add("%mobile%", string.IsNullOrEmpty(e.Related_User.Mobile) ? e.Related_User.Tel : e.Related_User.Mobile);

        //Details Of Order
        e.EventVariables.Add("%phone%", model.Tel);
        e.EventVariables.Add("%address%", string.IsNullOrEmpty(model.Address) ? e.Related_User.Address : model.Address);
        e.EventVariables.Add("%comment%", model.Comment);
        e.EventVariables.Add("%orderid%", model.ID.ToString());
        e.EventVariables.Add("%amount%", UIUtil.GetCommaSeparatedOf(model.TotalAmount) + " ریال");
        e.EventVariables.Add("%branch%", model.Branch_ID.HasValue ? model.Branch.Title : "ندارد");
        e.EventVariables.Add("%type%", ((PaymentType)model.PaymentType == PaymentType.Online) ? EnumUtil.GetEnumElementPersianTitle((PaymentType)model.PaymentType) + "(تا این لحظه عملیات پرداخت صورت نگرفته)" : EnumUtil.GetEnumElementPersianTitle((PaymentType)model.PaymentType));

        string productlist = "";

        foreach (var item in model.OrderDetails)
        {
            string temp = string.Format(",{0}({1}عدد)", item.Product.Title, item.Quantity);
            productlist += temp;
        }
        if (productlist.Length > 1)
            productlist = productlist.Substring(1);

        e.EventVariables.Add("%productlist%", productlist);


        e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(model.OrderDate));
        e.EventVariables.Add("%time%", model.OrderDate.TimeOfDay.ToString().Substring(0, 8));


        EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Order_New, e);

        #endregion

        if (result.Result == ActionResult.Done)
        {
            if (Request.Form["peymentType"] == "2")
            {
                string ResNum = new SamanOnlinePayment().CreateReservationNumber(orderID);
                Response.Redirect(GetRouteUrl("guest-onlinepayment", null) + "?id=" + ResNum);
            }
            if (Request.Form["peymentType"] == "1")
            {
                Response.Redirect(GetRouteUrl("guest-orderlist", null));
            }
        }
    }

    [WebMethod]
    public static long Login(string email, string password)
    {
        pgc.Business.General.UserBusiness uBusiness = new pgc.Business.General.UserBusiness();
        ///////////
        OperationResult Res;
        bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        if (isEmail)
        {
            OperationResult ResEmailPass = uBusiness.ValidEmailAndPass(email, password);
            //is email format check for email and pass validation
            if (ResEmailPass.Result == ActionResult.Done)
            {
                //email $ pass is valid so login user
                Res = UserSession.LogIn(email, password);
                //UserSession.AddMessage(Res.Messages);
            }
            else
            {
                //email & pass is not valid show message               
                //UserSession.AddMessage(ResEmailPass.Messages);
                return 0;
            }
        }
        else
        {
            OperationResult ResUserNamePass = uBusiness.ValidUserNameAndPass(email, password);
            //is not email format maby its valid usename
            if (ResUserNamePass.Result == ActionResult.Done)
            {
                //username & pass is valid and not lock redirect to email page

                 return uBusiness.RetriveUserID(email, password)*(-1);;                
            }
            else
            {
                //username $ pass is not valid or username is lock show message

                //UserSession.AddMessage(ResUserNamePass.Messages);
                return 0;
            }
        }

        /////////////////
        ///Res = UserSession.LogIn(email, password);
        //UserSession.AddMessage(Res.Messages);

        //Login
        #region Event Raising

        SystemEventArgs eARGS = new SystemEventArgs();
        User user = new pgc.Business.UserBusiness().RetirveUser(UserSession.UserID);

        eARGS.Related_User = UserSession.User;

        eARGS.EventVariables.Add("%user%", user.FullName);
        eARGS.EventVariables.Add("%username%", user.Username);
        eARGS.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
        eARGS.EventVariables.Add("%mobile%", user.Mobile);
        eARGS.EventVariables.Add("%email%", user.Email);
        eARGS.EventVariables.Add("%phone%", user.Tel);

        EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Login, eARGS);

        #endregion

        if (UserSession.IsUserLogined)
        {
            return uBusiness.GetUserID(email);
        }
        return 0;
    }

    [WebMethod]
    public static long Register(string email, string password, string fullname)
    {
        pgc.Business.General.UserBusiness uBusiness = new pgc.Business.General.UserBusiness();
        if (uBusiness.IsExistUser(email))
        {
            return 0;
        }
        User user = new User();
        OperationResult result = new OperationResult();
        user.Email = email;
        user.pwd = password;
        user.FullName = fullname;
        user.Username = email;

        result = uBusiness.RegisterUser(user);
        //UserSession.AddMessage(result.Messages);

        if (result.Result == ActionResult.Done)
        {
            UserSession.LogIn(user.Username, user.pwd);
            return uBusiness.GetUserID(email);
        }
        return -1;
    }

    [WebMethod]
    public static UserInformation UserInfo(string id)
    {
        pgc.Business.General.UserBusiness uBusiness = new pgc.Business.General.UserBusiness();
        User u = uBusiness.RetriveUser(ConvertorUtil.ToInt64(id));
        UserInformation userinfo = new UserInformation();
        userinfo.Fullname = u.FullName;
        userinfo.Tel = u.Tel;
        userinfo.Mobile = u.Mobile;
        userinfo.Address = u.Address;
        return userinfo;
    }

    public struct UserInformation
    {
        public string Fullname { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
    }
}