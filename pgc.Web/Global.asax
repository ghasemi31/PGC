<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Http" %>
<script RunAt="server">

    void RegisterRoutes(System.Web.Routing.RouteCollection routes)
    {
        //routes.MapPageRoute(
        //    "adsearch",
        //    "search/{ProductType}/{*Params}",
        //    "~/AdvancedSearch.aspx"
        //    );

        //routes.MapPageRoute(
        //    "services",
        //    "services",
        //    "~/pages/guest/services.aspx"
        //    );



    }

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

        //register routse from panelpage table of databse (using kFrameWork)
        kFrameWork.UI.BasePage.RegisterRoutes(System.Web.Routing.RouteTable.Routes);
        //RegisterRoutes(System.Web.Routing.RouteTable.Routes);
        
        Newtonsoft.Json.JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
        GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;
        
        System.Web.Routing.RouteTable.Routes.MapHttpRoute(
             name: "DefaultApi",
              routeTemplate: "api/{controller}/{action}/{id}",
            defaults: new { id = System.Web.Http.RouteParameter.Optional }
            );

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs


    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

        //Temp Login
        pgc.Model.pgcEntities db = new pgc.Model.pgcEntities();
        //HttpContext.Current.Session["CurrentUser"] = db.Users.SingleOrDefault(u => u.ID == 1);
        // HttpContext.Current.Session["CurrentUser"] = db.Users.SingleOrDefault(u => u.ID == 2);
        // HttpContext.Current.Session["CurrentUser"] = db.Users.SingleOrDefault(u => u.ID == 11);
        Session["DisplayMobileAppBox"] = "Show";
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
