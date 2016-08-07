//using pgc.Business.General;
//using pgc.Model;
//using pgc.Model.Enums;
//using pgc.Model.Other.Project;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//public class PostController : ApiController
//{
//    #region User
//    [HttpPost]
//    public JsonActionResult Login(UserObject user, string accesskey, Device device = Device.WebApp)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().isUserValid(user, device);
//    }


//    [HttpPost]
//    public JsonActionResult Register(UserObject user, string accesskey, Device device = Device.WebApp)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().RegisterNewUser(user, device);
//    }

//    [HttpPost]
//    public JsonActionResult ChangePass(UserObject user, string accesskey, Device device = Device.WebApp)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().ChangeUserPassword(user, device);
//    }


//    [HttpPost]
//    public JsonActionResult ModifyProfile(UserObject user, string accesskey, Device device = Device.WebApp)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().ModifyUserProfile(user, device);
//    } 
//    #endregion

//    #region Contact us
//    [HttpPost]
//    public JsonActionResult Feedback(UserComment feedback, string accesskey, Device device = Device.WebApp)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().AddFeedBack(feedback, device);
//    }
//    [HttpPost]
//    public JsonActionResult BranchRequest(BranchRequest request, string accesskey, Device device = Device.WebApp)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().AddNewBranchrequest(request, device);
//    } 
//    #endregion

//    #region Order
//    [HttpPost]
//    public JsonActionResult NewOrder(OrderObject order, string accesskey, Device device = Device.WebApp)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().RegisterNewOrder(order, device);
//    }

//    [HttpPost]
//    public JsonActionResult NewOnlinePayment(long orderId, string accesskey, Device device = Device.WebApp)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().RegisterNewOnlinePayment(orderId, device);
//    }
//    #endregion

//    private void CheckAccessKey(string accesskey)
//    {
//        if (accesskey != Constants.WebApi_Accesskey)
//        {
//            throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NonAuthoritativeInformation, String.Format("access denied")));
//        }
//    }

//}
