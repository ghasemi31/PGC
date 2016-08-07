//using pgc.Business.General;
//using pgc.Model;
//using pgc.Model.Other.Project;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//public class GetController : ApiController
//{

//    #region Home
//    [HttpGet]
//    public HomeObject Home(string accesskey)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().RetrieveHomeObject();
//    }

//    #endregion

//    #region User
//    [HttpGet]
//    public UserObject User(string accesskey, string email)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().RetrieveUser(email);
//    } 
//    #endregion

//    #region ContactUs
//    [HttpGet]
//    public ContactusObject ContactUs(string accesskey)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().RetrieveContactusObject();
//    }

//    [HttpGet]
//    public BranchAgreementObject BranchAgreement(string accesskey)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().RetrieveBranchAgreementObject();
//    }
//    #endregion

//    #region News
//    [HttpGet]
//    public NewsObject News(string accesskey, long id)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().RetrieveNewsObject(id);
//    } 
//    #endregion

//    #region Product
//    [HttpGet]
//    public ProductObject Product(string accesskey, long id)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().RetrieveProductObject(id);
//    }
//    [HttpGet]
//    public List<ProductObject> Products(string accesskey)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().RetrieveProductList();
//    }
//    [HttpGet]
//    public List<ProductObject> Accessories(string accesskey)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().RetrieveAccessoriesList();
//    }
//    #endregion

//    #region Order


//    [HttpGet]
//    public OrderObject Order(string accesskey, long OrderId)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().RetrieveOrderObject(OrderId);
//    }
//     [HttpGet]
//    public List<OrderObject> OrderList(string accesskey, string email)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().RetrieveOrderList(email);
//    }
//    #endregion

//    #region branches
//    [HttpGet]
//    public Dictionary<long, string> BranchesList(string accesskey)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().RetrieveBranchesList();
//    }
//    [HttpGet]
//    public List<BranchObject> Branches(string accesskey, double latitude, double longitude)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().RetrieveBranches(latitude, longitude);
//    }

//    [HttpGet]
//    public BranchObject Branch(string accesskey, long branchID)
//    {
//        CheckAccessKey(accesskey);

//        return new ApiBusiness().RetrieveBranch(branchID);
//    }
//    #endregion

//    private void CheckAccessKey(string accesskey)
//    {
//        if (accesskey != Constants.WebApi_Accesskey)
//        {
//            throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NonAuthoritativeInformation, String.Format("you have not accesskey")));
//        }
//    }

//}
