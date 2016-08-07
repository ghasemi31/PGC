//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using kFrameWork.Business;
//using kFrameWork.Model;
//using pgc.Model;
//using pgc.Model.Enums;
//using kFrameWork.Util;
//using pgc.Model.Other.Project;
//using kFrameWork.UI;
//using System.Threading;
//using pgc.Business.Core;
//using System.Device.Location;
//using System.IO;

//namespace pgc.Business.General
//{
//    public class ApiBusiness
//    {
//        pgcEntities db;
     
//        public ApiBusiness()
//        {
//            db = new pgcEntities();

//        }

//        #region GET

//        #region Home
//        public HomeObject RetrieveHomeObject()
//        {
//            AppSetting sett = db.AppSettings.FirstOrDefault();
//            HomeObject obj = new HomeObject();
//            obj.Products = db.Products.Where(p => p.DisplayInSlider == true && p.Accessories == false && p.IsActive == true).OrderBy(p => p.DispOrder).Select(p => new ProductObject() { ID = p.ID, Title = p.Title, Price = p.Price, ImageUrl = p.SliderProductPicPath.Replace("~", ""), BackImageUrl = p.ProductPicPath.Replace("~", ""), IsAccessories = p.Accessories }).ToList();
//            obj.Prices = db.Products.Where(p => p.IsActive == true).ToDictionary(p => p.ID, p => p.Price);
//            obj.QualityCharter = sett.QualityCharter;
//            obj.NavHeaderImageUrl = sett.NavHeaderImage.Replace("~", "");
//            obj.OrderIsSuspendedMessage = OptionBusiness.GetText(OptionKey.MessageForOnlineOrderIsSuspended);
//            List<NewsObject> News = new List<NewsObject>();
//            foreach (var item in db.News.Where(n => n.Status == (int)NewsStatus.Show).OrderByDescending(n => n.NewsDate).Take(sett.NewsCount).ToList())
//            {
//                NewsObject news = new NewsObject();
//                news.ID = item.ID;
//                news.ImageUrl = item.NewsPicPath.Replace("~", "");
//                StringBuilder sb = new StringBuilder();
//                sb.Append(item.Title + "\n");
//                sb.Append(kFrameWork.Util.DateUtil.GetPersianDateWithTimeRaw(item.NewsDate) + "\n");
//                sb.Append(item.Summary);
//                news.Desc = sb.ToString();
                
//                News.Add(news);
//            }

//            obj.News = News;

//            obj.Slider = db.MainSliders.Where(r => r.IsVisible).OrderBy(o => o.DispOrder).Select(s => (s.ImgPathMobile!=null&&s.ImgPathMobile!="")?s.ImgPathMobile.Replace("~", ""):s.ImgPath.Replace("~", "")).ToList();
//            return obj;
//        }
//        #endregion

//        #region User
//        public UserObject RetrieveUser(string email)
//        {

//            User user = db.Users.SingleOrDefault(u => u.Email == email);
//            if (user == null)
//                return null;
//            else
//                return new UserObject() { ID = user.ID, Name = user.FullName, Email = user.Email, Address = user.Address, Mobile = user.Mobile, Phone = user.Tel, Password = user.pwd };
//        }
//        #endregion

//        #region Contact Us
//        public ContactusObject RetrieveContactusObject()
//        {
//            ContactusObject res = new ContactusObject();
//            res.ContactUsText = OptionBusiness.GetLargeText(pgc.Model.Enums.OptionKey.Contact_Specification);
//            res.BranchesList = new Dictionary<long, string> { { -1, "مدیریت" } };
//            foreach (var item in db.Branches.Where(b => b.IsActive).OrderBy(b => b.DispOrder).ToDictionary(b => b.ID, b => b.Title))
//            {
//                res.BranchesList.Add(item.Key, item.Value);
//            }

//            res.FeedbackTypesList = new Dictionary<long, string> { { 1, "انتقاد" }, { 2, "پیشنهاد" }, { 3, "پرسش" }, { 4, "درخواست" } };


//            List<LocationObject> locations = new List<LocationObject>();

//            List<string> loclist = new List<string>(
//                           db.AppSettings.FirstOrDefault().pgcizi_LatLng.Split(new string[] { "\n" },
//                           StringSplitOptions.RemoveEmptyEntries));

//            foreach (var item in loclist)
//            {
//                string[] loc = item.Split(',');
//                locations.Add(new LocationObject()
//                {
//                    Latitude = ConvertorUtil.ToDouble(loc[0]), Longitude = ConvertorUtil.ToDouble(loc[1]),
//                    Title = loc[2],
//                    Description = loc[3]
//                });
//            }

//            res.LocationsList = locations;

//            return res;
//        }

//        public BranchAgreementObject RetrieveBranchAgreementObject()
//        {
//            BranchAgreementObject res = new BranchAgreementObject();

//            var sett = db.AppSettings.FirstOrDefault();


//            res.ImageUrl = sett.BranchAgreement_Image.Replace("~", "");
//            res.Content = sett.BranchAgreement_Content;

//            return res;
//        }
//        #endregion

//        #region News
//        public NewsObject RetrieveNewsObject(long id)
//        {
//            NewsObject res = new NewsObject();
//            News news = db.News.SingleOrDefault(n => n.ID == id);
//            if (news != null)
//            {
//                res.ImageUrl = news.NewsPicPath.Replace("~", "");
//                res.Title = news.Title;
//                res.Body = news.Body;
//                res.Date = kFrameWork.Util.DateUtil.GetPersianDateWithMonthName(news.NewsDate);
//            }

//            return res;
//        }
//        #endregion

//        #region Product
//        public ProductObject RetrieveProductObject(long id)
//        {
//            ProductObject res = new ProductObject();
//            Product product = db.Products.SingleOrDefault(n => n.ID == id);
//            if (product != null)
//            {
//                res.ID = product.ID;
//                res.ImageUrl = product.ProductPicPath.Replace("~", "");
//                res.Title = product.Title;
//                res.Price = product.Price;
//                res.Desc = product.Body;
//                res.IsAccessories = product.Accessories;
//                res.AllowOrdering = OptionBusiness.GetBoolean(OptionKey.AllowOnlineOrdering)&&product.AllowOnlineOrder;
//                res.Materials = new List<string>();
//                foreach (var item in product.Materials.Select(m => m.MaterialPicPath))
//                {
//                    res.Materials.Add(item.Replace("~", ""));
//                }
//            }

//            return res;
//        }

//        public List<ProductObject> RetrieveProductList()
//        {
//            if (!OptionBusiness.GetBoolean(OptionKey.AllowOnlineOrdering))
//                return new List<ProductObject>();

//            return db.Products.Where(p =>  p.Accessories == false && p.IsActive == true&&p.AllowOnlineOrder==true).OrderBy(p => p.DispOrder).Select(p => new ProductObject() { ID = p.ID, Title = p.Title, Price = p.Price, ImageUrl = p.SliderProductPicPath.Replace("~", ""), BackImageUrl = p.ProductPicPath.Replace("~", ""), IsAccessories = p.Accessories }).ToList(); ;
//        }

//        public List<ProductObject> RetrieveAccessoriesList()
//        {
//            return db.Products.Where(p => p.Accessories == true && p.IsActive == true && p.AllowOnlineOrder == true).OrderBy(p => p.DispOrder).Select(p => new ProductObject() { ID = p.ID, Title = p.Title, Price = p.Price, ImageUrl = p.SliderProductPicPath.Replace("~", ""), BackImageUrl = p.ProductPicPath.Replace("~", ""), IsAccessories = p.Accessories }).ToList(); ;
//        }
//        #endregion

//        #region Order

//        public OrderObject RetrieveOrderObject(long id)
//        {
//            OrderObject res = new OrderObject();

//            Order order = db.Orders.SingleOrDefault(o => o.ID == id);
//            if (order == null)
//                return null;

//            res.ID = order.ID;
//            res.IsPaid = order.IsPaid;
//            res.Mobile = order.Mobile;
//            res.Tel = order.Tel;
//            res.Address = order.Address;
//            res.BranchTitle = order.BranchTitle;
//            res.Comment = order.Comment;
//            res.Date = kFrameWork.Util.DateUtil.GetPersianDate(Convert.ToDateTime(order.OrderDate));
//            res.Name = order.Name;
//            res.OrderStatus = kFrameWork.Util.EnumUtil.GetEnumElementPersianTitle((pgc.Model.Enums.OrderStatus)order.OrderStatus);
//            res.PayableAmount = order.PayableAmount;
//            res.PaymentType = order.PaymentType;
//            res.RefNum = (order.OnlinePayments.Any(o => o.TransactionState == "OK")) ? order.OnlinePayments.FirstOrDefault(o => o.TransactionState == "OK").RefNum : "";
//            res.UserID = order.User_ID;

//            foreach (var item in order.OrderDetails)
//            {
//                OrderDetailObject detail = new OrderDetailObject()
//                {
//                    ImageUrl = (item.Product != null) ? item.Product.SliderProductPicPath.Replace("~", "") : "",
//                    Quantity = item.Quantity,
//                    SumPrice = item.SumPrice,
//                    Title = item.ProductTitle,
//                    UnitPrice = item.UnitPrice
//                };
//                res.OrderDetails.Add(detail);
//            }
//            return res;
//        }

//        public List<OrderObject> RetrieveOrderList(string email)
//        {
//            User user = db.Users.SingleOrDefault(u => u.Email == email);
//            if (user == null)
//                return null;

//            List<OrderObject> res = new List<OrderObject>();

//            foreach (var item in user.Orders.OrderByDescending(f => f.OrderDate))
//            {
//                res.Add(new OrderObject()
//                {
//                    ID = item.ID,
//                    OrderStatus = kFrameWork.Util.EnumUtil.GetEnumElementPersianTitle((pgc.Model.Enums.OrderStatus)item.OrderStatus),
//                    Date = item.OrderPersianDate,
//                    PayableAmount = item.PayableAmount,
//                    PaymentType = item.PaymentType,
//                    IsPaid = item.IsPaid
//                });
//            }

//            return res;
//        }
//        #endregion

//        #region Branch
//        public Dictionary<long, string> RetrieveBranchesList()
//        {
//            Dictionary<long, string> res = new Dictionary<long, string>();
//            string TimeNow = DateTime.Now.TimeOfDay.ToString().Substring(0, 5);
//            foreach (var item in db.Branches.Where(P => P.AllowOnlineOrder && P.IsActive && P.AllowOnlineOrderTimeFrom.CompareTo(TimeNow) <= 0 && P.AllowOnlineOrderTimeTo.CompareTo(TimeNow) >= 0).OrderBy(p => p.DispOrder).ToDictionary(b => b.ID, b => b.Title))
//            {
//                res.Add(item.Key, item.Value);
//            }

//            return res;
//        }

//        public List<BranchObject> RetrieveBranches(double lat, double lng)
//        {
//            List<BranchObject> res = new List<BranchObject>();

//                var coord = new GeoCoordinate(lat, lng);
//                var nearest = db.Branches.Where(x => x.IsActive)
//                    .AsEnumerable()
//                   .Select(x => new
//                   {
//                       branch = x,
//                       coord = new GeoCoordinate { Latitude = x.latitude, Longitude = x.Longitude }
//                   })
//                   .OrderBy(x => x.coord.GetDistanceTo(coord));


                

//                foreach (var item in nearest)
//                {
//                    string tel = "";
//                    string code = !string.IsNullOrEmpty(item.branch.CityCode) ? item.branch.CityCode : "";
//                    List<string> phones = new List<string>();
//                    if (!string.IsNullOrEmpty(item.branch.PhoneNumbers))
//                    {
//                        phones = new List<string>(
//                                   item.branch.PhoneNumbers.Split(new string[] { "\n" },
//                                   StringSplitOptions.RemoveEmptyEntries));
//                        tel=code+"-"+string.Join(", ",phones);
//                    }
                    

//                    res.Add(new BranchObject()
//                    {
//                        ID=item.branch.ID,
      
//                        Name = item.branch.Title,
                      
              
                        
//                        ImagePath = (!string.IsNullOrEmpty(item.branch.ThumbListPath))?item.branch.ThumbListPath.Replace("~",""):"" ,
//                        Latitude=item.branch.latitude,
//                        Longitude=item.branch.Longitude,
//                        Address=item.branch.Address,
                   
//                       Tel =tel

//                    });
//                }             

//            return res;
//        }



//        public BranchObject RetrieveBranch(long branchID)
//        {
//            Branch branch = db.Branches.SingleOrDefault(b => b.ID == branchID);
//            if (branch == null)
//                return null;

//            List<string> slides=new List<string>();
//            string TimeNow = DateTime.Now.TimeOfDay.ToString().Substring(0, 5);

//            List<string> phones = new List<string>();
//            if (!string.IsNullOrEmpty(branch.PhoneNumbers))
//            {
//                phones = new List<string>(
//                           branch.PhoneNumbers.Split(new string[] { "\n" },
//                           StringSplitOptions.RemoveEmptyEntries));
//            }


//            return new BranchObject()
//            {
//                ID = branch.ID,
//                Name = branch.Title,
              
//                ImagePath = (!string.IsNullOrEmpty(branch.ThumbListPath)) ? branch.ThumbListPath.Replace("~", "") : "",
//                Address = branch.Address,
//                 Latitude = branch.latitude, Longitude = branch.Longitude ,
//                HoursOrdering = branch.HoursOrdering,
//                HoursServingFood = branch.HoursServingFood,
//                NumberOfChair = branch.NumberOfChair.ToString(),
//                TransportCost = branch.TransportCost,
//                Slides = branch.BranchPics.Select(x =>{ return x.ImagePath.Replace("~","");}).ToList(),
//                CanOrder = branch.AllowOnlineOrder && branch.IsActive && branch.AllowOnlineOrderTimeFrom.CompareTo(TimeNow) <= 0 && branch.AllowOnlineOrderTimeTo.CompareTo(TimeNow) >= 0,
//                CityCode=string.IsNullOrEmpty(branch.CityCode)?"":branch.CityCode,
//                PhoneNumbers=phones
//            };

//        }

//        #endregion

//        #endregion

//        #region POST

//        #region User
//        public JsonActionResult isUserValid(UserObject user, Device device = Device.WebApp)
//        {
//            JsonActionResult res = new JsonActionResult();

//            User CurrentUser = null;
//            CurrentUser = db.Users.SingleOrDefault(u => u.Email == user.Email && u.pwd == user.Password);
//            if (CurrentUser == null)
//            {
//                res.Result = false;
//                res.Message = LoadMessage(UserMessageKey.InvalidUsernameOrPassword.ToString());
//                return res;
//            }


//            else if ((UserActivityStatus)CurrentUser.ActivityStatus != UserActivityStatus.Enabled)
//            {
//                res.Result = false;
//                res.Message = LoadMessage(UserMessageKey.ContactAdminInErrorPersistance.ToString());
//                return res;
//            }



//            #region Event Raising

//            SystemEventArgs eARGS = new SystemEventArgs();

//            eARGS.Related_User = CurrentUser;
//            eARGS.Device_Type = device;

//            eARGS.EventVariables.Add("%user%",CurrentUser.FullName);
//            //eARGS.EventVariables.Add("%username%", user.Username);
//            eARGS.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
//            eARGS.EventVariables.Add("%mobile%", CurrentUser.Mobile);
//            eARGS.EventVariables.Add("%email%", CurrentUser.Email);
//            eARGS.EventVariables.Add("%phone%", CurrentUser.Tel);

//            EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Login, eARGS);

//            #endregion

//            res.Result = true;
//            res.Message = LoadMessage(UserMessageKey.Succeed);
//            res.Parameters = new Dictionary<string, string> { { "name", CurrentUser.FullName }, { "phone", CurrentUser.Tel }, { "mobile", CurrentUser.Mobile }, { "address", CurrentUser.Address }, { "user_id", CurrentUser.ID.ToString() } };
//            return res;
//        }

//        public JsonActionResult ChangeUserPassword(UserObject user, Device device)
//        {

//            JsonActionResult res = new JsonActionResult();
//            try
//            {

//                User currentUser = db.Users.SingleOrDefault(u => u.Email == user.Email);


//                currentUser.pwd = user.Password;

//                db.SaveChanges();


//                //Password_Change
//                #region Event Raising

//                if (user.Password != currentUser.pwd)
//                {
//                    SystemEventArgs e = new SystemEventArgs();
//                    e.Related_User = currentUser;
//                    e.Device_Type = device;
//                    e.EventVariables.Add("%user%", currentUser.FullName);
//                    e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
//                    //e.EventVariables.Add("%mobile%", user.Mobile);
//                    e.EventVariables.Add("%email%", currentUser.Email);
//                    //e.EventVariables.Add("%username%", user.Username);
//                    //e.EventVariables.Add("%phone%", user.Tel);
//                    e.EventVariables.Add("%password%", currentUser.pwd);

//                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Password_Change, e);
//                }
//                #endregion

//                res.Result = true;
//                res.Message = LoadMessage(UserMessageKey.Succeed);
//                return res;
//            }

//            catch
//            {
//                res.Result = false;
//                res.Message = LoadMessage(UserMessageKey.Failed);
//                return res;
//            }

//        }

//        public JsonActionResult RegisterNewUser(UserObject user, Device device = Device.WebApp)
//        {

//            JsonActionResult res = new JsonActionResult();
//            try
//            {
//                if (db.Users.Where(u => u.Email == user.Email).Count() > 0)
//                {
//                    res.Result = false;
//                    res.Message = LoadMessage(UserMessageKey.DuplicateEmail);
//                    return res;
//                }

//                User newUser = new User();
//                newUser.FullName = user.Name;
//                newUser.pwd = user.Password;
//                newUser.Email = user.Email;
//                newUser.Username = user.Email;
//                newUser.AccessLevel_ID = 2;
//                newUser.ActivityStatus = Convert.ToInt32(UserActivityStatus.Enabled);
//                newUser.SignUpPersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);


//                db.Users.AddObject(newUser);
//                db.SaveChanges();


//                //User_New
//                #region Event Raising

//                SystemEventArgs e = new SystemEventArgs();
//                e.Related_User = newUser;
//                e.Device_Type = device;
//                e.EventVariables.Add("%user%", newUser.FullName);
//                e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
//                e.EventVariables.Add("%email%", newUser.Email);
//                //e.EventVariables.Add("%phone%", user.Tel);
//                //e.EventVariables.Add("%mobile%", user.Mobile);
//                //e.EventVariables.Add("%username%", user.Username);
//                //e.EventVariables.Add("%regcode%", user.ID.ToString());
//                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.User_New, e);

//                #endregion


//                res.Result = true;
//                res.Message = LoadMessage(UserMessageKey.RegisterGreeting);
//                res.Parameters = new Dictionary<string, string> { { "phone", newUser.Tel }, { "mobile", newUser.Mobile }, { "address", newUser.Address }, { "user_id", newUser.ID.ToString() } };
//                return res;
//            }

//            catch
//            {
//                res.Result = false;
//                res.Message = LoadMessage(UserMessageKey.Failed);
//                return res;
//            }

//        }

//        public JsonActionResult ModifyUserProfile(UserObject user, Device device)
//        {

//            JsonActionResult res = new JsonActionResult();
//            try
//            {

//                User currentUser = db.Users.SingleOrDefault(u => u.Email == user.Email);

//                currentUser.FullName = user.Name;
//                currentUser.Tel = user.Phone;
//                currentUser.Mobile = user.Mobile;
//                currentUser.Address = user.Address;


//                db.SaveChanges();

//                //Profile_Change
//                #region Profile_Change

//                SystemEventArgs eArg = new SystemEventArgs();


//                eArg.Related_User = currentUser;
//                eArg.Device_Type = device;

//                eArg.EventVariables.Add("%user%", currentUser.FullName);
//                eArg.EventVariables.Add("%username%", currentUser.Username);
//                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
//                eArg.EventVariables.Add("%mobile%", currentUser.Mobile);
//                eArg.EventVariables.Add("%email%", currentUser.Email);

//                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Profile_Change, eArg);

//                #endregion


//                res.Result = true;
//                res.Message = LoadMessage(UserMessageKey.Succeed);
//                return res;
//            }

//            catch
//            {
//                res.Result = false;
//                res.Message = LoadMessage(UserMessageKey.Failed);
//                return res;
//            }
//        }
//        #endregion

//        #region Contact us
//        public JsonActionResult AddFeedBack(UserComment feedback, Device device)
//        {
//            JsonActionResult res = new JsonActionResult();
//            try
//            {

//                feedback.Status = (int)UserCommentStatus.UnRead;
//                feedback.UCDate = DateTime.Now;
//                feedback.UCPersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
//                if (feedback.Branch_ID == -1)
//                {
//                    feedback.Branch_ID = null;
//                    feedback.BranchTitle = "";
//                }
//                else
//                {
//                    feedback.BranchTitle = new BranchBusiness().RetirveBranchID(feedback.Branch_ID.Value).Title;
//                }



//                db.UserComments.AddObject(feedback);
//                db.SaveChanges();


//                //UserComment_New
//                #region Event Raising

//                SystemEventArgs e = new SystemEventArgs();
//                e.Related_Guest_Email = feedback.Email;
//                e.Device_Type = device;
//                if (feedback.Branch_ID.HasValue)
//                    e.Related_Branch = new BranchBusiness().RetirveBranchID(feedback.Branch_ID.Value);

//                e.EventVariables.Add("%user%", feedback.Name);
//                e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
//                e.EventVariables.Add("%email%", feedback.Email);
//                e.EventVariables.Add("%phone%", feedback.Phone);
//                e.EventVariables.Add("%body%", feedback.Body);
//                e.EventVariables.Add("%type%", EnumUtil.GetEnumElementPersianTitle((UserCommentType)feedback.Type));

//                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.UserComment_New, e);

//                #endregion

//                res.Result = true;
//                res.Message = LoadMessage(UserMessageKey.ThankUserForComment);
//                return res;
//            }

//            catch
//            {
//                res.Result = false;
//                res.Message = LoadMessage(UserMessageKey.Failed);
//                return res;
//            }
//        }

//        public JsonActionResult AddNewBranchrequest(BranchRequest request, Device device)
//        {
//            JsonActionResult res = new JsonActionResult();
//            try
//            {
//                request.ApplicatorName = request.ApplicatorName ?? "";
//                request.Description = request.Description ?? "";
//                request.Tel = request.Tel ?? "";
//                request.Address = request.Address ?? "";

//                request.Status = Convert.ToInt32(UserCommentStatus.UnRead);
//                request.BRPersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
//                request.BRDate = DateTime.Now;

//                db.BranchRequests.AddObject(request);
//                db.SaveChanges();


//                //BranchRequest_New
//                #region Event Raising

//                SystemEventArgs e = new SystemEventArgs();
//                e.Related_Guest_Email = request.Email;
//                e.Related_Guest_Phone = request.Mobile;
//                e.Device_Type = device;
//                e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));

//                //e.EventVariables.Add("%lname%", request.Lname);
//                //e.EventVariables.Add("%fname%", request.Fname);
//                e.EventVariables.Add("%fullname%", request.FullName);
//                e.EventVariables.Add("%applicator%", request.ApplicatorName);

//                e.EventVariables.Add("%mobile%", request.Mobile);
//                e.EventVariables.Add("%email%", request.Email);
//                e.EventVariables.Add("%phone%", request.Tel);
//                e.EventVariables.Add("%address%", request.Address);

//                e.EventVariables.Add("%location%", request.BranchLocation);
//                e.EventVariables.Add("%locationtype%", EnumUtil.GetEnumElementPersianTitle((LocationType)request.LocationType));
//                e.EventVariables.Add("%description%", request.Description);

//                e.EventVariables.Add("%hasexperience%", (request.HaveBackgroung) ? "دارم" : "ندارم");

//                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchRequest_New, e);

//                #endregion

//                res.Result = true;
//                res.Message = LoadMessage(UserMessageKey.ThankBranchRequest);
//                return res;
//            }

//            catch
//            {
//                res.Result = false;
//                res.Message = LoadMessage(UserMessageKey.Failed);
//                return res;
//            }
//        }
//        #endregion


//        #region Order
//        public JsonActionResult RegisterNewOrder(OrderObject newOrder, Device device)
//        {

//            JsonActionResult res = new JsonActionResult();
//            try
//            {

//                Order model = new Order();
//                model.Name = newOrder.Name;
//                model.Tel = newOrder.Tel;
//                model.Mobile = newOrder.Mobile;
//                model.Address = newOrder.Address;
//                model.Comment = newOrder.Comment;
//                model.TotalAmount = newOrder.PayableAmount;
//                model.PayableAmount = model.TotalAmount;
//                model.PaymentType = newOrder.PaymentType;
//                model.Branch_ID = newOrder.Branch_ID;
//                model.User_ID = newOrder.UserID;

//                model.OrderDate = DateTime.Now;
//                model.OrderPersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
//                model.OrderStatus = (int)OrderStatus.New;
//                model.IsPaid = false;
//                model.BranchTitle = model.Branch_ID != null ? db.Branches.FirstOrDefault(b => b.ID == model.Branch_ID).Title : "";
//                model.DeviceType_Enum = (int)device;
//                db.Orders.AddObject(model);
//                db.SaveChanges();


//                long orderID = model.ID;


//                foreach (var item in newOrder.Details)
//                {

//                    OrderDetail order = new OrderDetail();
//                    order.Order_ID = orderID;
//                    order.Product_ID = item.Key;
//                    order.Quantity = item.Value;
//                    var product = db.Products.FirstOrDefault(d => d.ID == order.Product_ID);
//                    order.UnitPrice = product.Price;
//                    order.ProductTitle = product.Title;
//                    order.SumPrice = order.Quantity * order.UnitPrice;
//                    db.OrderDetails.AddObject(order);
//                    db.SaveChanges();
//                }


//                //Order_New
//                #region Event Raising

//                SystemEventArgs e = new SystemEventArgs();
//                User user = new pgc.Business.General.UserBusiness().RetriveUser(model.User_ID);
//                e.Device_Type = device;
//                e.Related_User = user;
//                if (model.Branch_ID.HasValue)
//                    e.Related_Branch = new pgc.Business.General.BranchBusiness().RetirveBranchID(model.Branch_ID.Value);

//                //Details Of User
//                e.EventVariables.Add("%user%", e.Related_User.FullName);
//                e.EventVariables.Add("%username%", e.Related_User.Username);
//                e.EventVariables.Add("%email%", e.Related_User.Email);
//                e.EventVariables.Add("%mobile%", string.IsNullOrEmpty(e.Related_User.Mobile) ? e.Related_User.Tel : e.Related_User.Mobile);

//                //Details Of Order
//                e.EventVariables.Add("%phone%", model.Tel);
//                e.EventVariables.Add("%address%", string.IsNullOrEmpty(model.Address) ? e.Related_User.Address : model.Address);
//                e.EventVariables.Add("%comment%", model.Comment);
//                e.EventVariables.Add("%orderid%", model.ID.ToString());
//                e.EventVariables.Add("%amount%", UIUtil.GetCommaSeparatedOf(model.TotalAmount) + " ریال");
//                e.EventVariables.Add("%branch%", model.Branch_ID.HasValue ? model.Branch.Title : "ندارد");
//                e.EventVariables.Add("%type%", ((PaymentType)model.PaymentType == PaymentType.Online) ? EnumUtil.GetEnumElementPersianTitle((PaymentType)model.PaymentType) + "(تا این لحظه عملیات پرداخت صورت نگرفته)" : EnumUtil.GetEnumElementPersianTitle((PaymentType)model.PaymentType));

//                string productlist = "";

//                foreach (var item in model.OrderDetails)
//                {
//                    string temp = string.Format(",{0}({1}عدد)", item.Product.Title, item.Quantity);
//                    productlist += temp;
//                }
//                if (productlist.Length > 1)
//                    productlist = productlist.Substring(1);

//                e.EventVariables.Add("%productlist%", productlist);


//                e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(model.OrderDate));
//                e.EventVariables.Add("%time%", model.OrderDate.TimeOfDay.ToString().Substring(0, 8));


//                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Order_New, e);

//                #endregion

//                res.Result = true;
//                res.Message = LoadMessage(UserMessageKey.ThankForOrder);
//                res.Parameters = new Dictionary<string, string> { { "order_id", orderID.ToString() } };
//                return res;
//            }

//            catch(Exception ex)
//            {
//               // LogError(ex);

//                res.Result = false;
//                res.Message = LoadMessage(UserMessageKey.Failed);
//                return res;
//            }

//        }



//        public JsonActionResult RegisterNewOnlinePayment(long orderId, Device device)
//        {
//            JsonActionResult res = new JsonActionResult();
//            try
//            {
//                string ResNum = new SamanOnlinePayment().CreateReservationNumber(orderId);

//                res.Result = true;
//                res.Message = LoadMessage(UserMessageKey.ThankForOrder);
//                res.Parameters = new Dictionary<string, string> { { "resnum", ResNum } };
//                return res;
//            }

//            catch
//            {
//                res.Result = false;
//                res.Message = LoadMessage(UserMessageKey.Failed);
//                return res;
//            }
//        }
//        #endregion
//        #endregion

//        #region utils

//        public string LoadMessage(string key)
//        {
//            UserMessage Result = db.UserMessages.SingleOrDefault(u => u.Key == key);
//            if (key == UserMessageKey.OnlinePaymentSucceedText.ToString())
//            {
//                Result.Description = OptionBusiness.GetHtml(OptionKey.OnlinePaymentSucceedText);
//            }
//            else if (key == UserMessageKey.OnlineOrderIsSuspended.ToString())
//            {
//                Result.Description = OptionBusiness.GetHtml(OptionKey.MessageForOnlineOrderIsSuspended);
//            }
//            return Result.Description;
//        }

//        public string LoadMessage(UserMessageKey key)
//        {
//            return LoadMessage(key.ToString());
//        }

//        private void LogError(Exception ex)
//        {
//            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
//            message += Environment.NewLine;
//            message += "-----------------------------------------------------------";
//            message += Environment.NewLine;
//            message += string.Format("Message: {0}", ex.Message);
//            message += Environment.NewLine;
//            message += string.Format("StackTrace: {0}", ex.StackTrace);
//            message += Environment.NewLine;
//            message += string.Format("Source: {0}", ex.Source);
//            message += Environment.NewLine;
//            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
//            message += Environment.NewLine;
//            message += "-----------------------------------------------------------";
//            message += Environment.NewLine;
//            string path = System.Web.HttpContext.Current.Server.MapPath("~/UserFiles/ErrorLog.txt");
//            using (StreamWriter writer = new StreamWriter(path, true))
//            {
//                writer.WriteLine(message);
//                writer.Close();
//            }
//        }


//        #endregion







//    }
//}
