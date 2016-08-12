using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Model;
using System.Web.Routing;
using pgc.Business;
using pgc.Model.Enums;

public partial class UserControls_Project_RightPanel :BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnExite.ServerClick += new EventHandler(LogOut);
    }

    protected void LogOut(object sender, EventArgs e)
    {
        UserSession.LogOut();
        Session["login"] = null;
        //Response.Redirect("~/Pages/Guest/Default.aspx");
        Response.Redirect(this.Page.GetRouteUrl("guest-default", null));
    }

    private void GetPhysicalPath()
    {
        
    }

    protected string GetHtmlOfToolBoxMenu()
    {
        pgcEntities db = new pgcEntities();
        string Res = "";
        List<long> Features = UserSession.User.AccessLevel.Features.Select(f => f.ID).ToList();
        List<pgc.Model.MenuItem> Items = db.MenuItems.Where(m => Features.Contains(m.Feature_ID)).Where(m => m.MenuCategory_ID != null).OrderBy(m => m.DisplayOrder).ToList();
        List<long?> CatIDs = Items.Select(i => i.MenuCategory_ID).ToList();
        List<MenuCategory> Cats = db.MenuCategories.Where(c => c.Active&& CatIDs.Contains(c.ID)).OrderBy(c => c.DisplayOrder).ToList();

        foreach (MenuCategory Cat in Cats)
        {
            List<pgc.Model.MenuItem> CatItems = Items.Where(i => i.MenuCategory_ID == Cat.ID && i.Active).ToList();

            if (CatItems.Count > 0)
            {
                bool IsActiveCat = false;
                string ItemsHtml = "";


                foreach (pgc.Model.MenuItem item in CatItems)
                {
                    bool IsActiveItem = false;

                    if (item.NavigateURL == this.Page.AppRelativeVirtualPath)
                    {
                        IsActiveItem = true;
                        IsActiveCat = true;
                    }

                    
                    
                    #region customized For Financial Items

                    if (item.RouteName == "admin-branchorder")
                    {
                        long numbersOfPendingBranchOrder = new BranchOrderBusiness().GetNumberOfStatus(BranchOrderStatus.Pending);

                        if (numbersOfPendingBranchOrder > 0)
                            item.Title += string.Format(" ({0}) {1}", numbersOfPendingBranchOrder, item.IsNew ? "<span style='color:red;font-size: 8px;'> (جدید)</span>" : "");

                    }
                    else if (item.RouteName == "admin-branchlackorder")
                    {
                        long numbersOfPendingBranchLackOrder = new BranchLackOrderBusiness().GetNumberOfStatus(BranchLackOrderStatus.Pending);

                        if (numbersOfPendingBranchLackOrder > 0)
                            item.Title += string.Format(" ({0}) {1}", numbersOfPendingBranchLackOrder, item.IsNew ? "<span style='color:red;font-size: 8px;'> (جدید)</span>" : "");

                    }
                    else if (item.RouteName == "admin-branchreturnorder")
                    {
                        long numbersOfPendingBranchReturnOrder = new BranchReturnOrderBusiness().GetNumberOfStatus(BranchReturnOrderStatus.Pending);

                        if (numbersOfPendingBranchReturnOrder > 0)
                            item.Title += string.Format(" ({0}) {1}", numbersOfPendingBranchReturnOrder, item.IsNew ? "<span style='color:red;font-size: 8px;'> (جدید)</span>" : "");

                    }

                        

                    #endregion


                    

                    //render item
                    ItemsHtml += string.Format("<a href='{0}' Class='{1} {2}' target='{4}'>{3}</a>",
                        this.Page.GetRouteUrl(item.RouteName, null),
                        item.UIClass,
                        ((IsActiveItem) ? "arrow item" : "star item"),
                        item.IsNew ? item.Title + "<span style='color:red;font-size: 8px;'> (جدید)</span>" : item.Title, item.Target);
                }

                //render cat
                string str = (IsActiveCat) ? "fa fa-caret-down" : "fa fa-caret-left";
                Res += string.Format("<div class='{0} {1}'><div class='title'><i class='"+str+"' aria-hidden='true' style='float:right;font-size: 14px'></i>{2}</div><div class='sub' {3}>{4}</div></div>",
                    Cat.UIClass,
                    ((IsActiveCat) ? "cat cat-open" : "cat"),
                    Cat.Title,
                    ((IsActiveCat) ? "style='display:block'" : ""),
                    ItemsHtml);
            }
        }
        return Res;
    }

}