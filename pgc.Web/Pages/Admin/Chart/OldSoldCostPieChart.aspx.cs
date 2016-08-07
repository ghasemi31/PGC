using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pgc.Business;
using pgc.Model;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using kFrameWork.UI;
using kFrameWork.Util;

public partial class Pages_Admin_Chart_SoldCostPieChart : BasePage
{
    public string dataSetObj = "";
    public string dataSetArgument = "";
    public string seriesLable = "";
    public string customLabel = "";
    public string tickObj = "";



    protected void Page_Load(object sender, EventArgs e)
    {
        pgcEntities db = new pgcEntities();
        PanelPage Entity = db.PanelPages.FirstOrDefault(p => p.URL == this.AppRelativeVirtualPath);

        if (!UserSession.IsUserLogined)
        {
            UserSession.AddMessage(UserMessageKey.SessionExpired);
            Response.Redirect(GetRouteUrl("guest-default", null) + "?redirecturl=" + this.AppRelativeVirtualPath);
        }
        else if (!UserSession.User.AccessLevel.Features.Any(f => f.ID == Entity.Feature_ID))
        {
            UserSession.AddMessage(UserMessageKey.AccessDenied);
            Response.Redirect(GetRouteUrl("guest-default", null) + "?redirecturl=" + this.AppRelativeVirtualPath);
        }


        if (!IsPostBack)
        {
            toDate.PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(-1));
            fromDate.PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(-31));

            ChartInit();
        }
    }

    private void ChartInit()
    {
        try
        {
            //Datas From DB

            BranchOrderBusiness orderBusiness = new BranchOrderBusiness();
            var orderList = orderBusiness.Search_SelectPrint(
                                                  new BranchOrderPattern()
                                                  {
                                                      IsApproved=true,
                                                      OrderedPersianDate = new DateRangePattern()
                                                      {
                                                          SearchMode = DateRangePattern.SearchType.Between,
                                                          FromDate = fromDate.PersianDate,
                                                          ToDate = toDate.PersianDate
                                                      }
                                                  });


            if (orderList.Count() < 1 && !UserSession.CurrentMessages.Contains(UserMessageKey.ChartHasNoData))
            {
                UserSession.AddMessage(UserMessageKey.ChartHasNoData);
                return;
            }

            var lackList = orderList.SelectMany(f => f.BranchLackOrders);

            BranchReturnOrderBusiness returnBusiness = new BranchReturnOrderBusiness();
            returnBusiness.Context = orderBusiness.Context;

            var returnList = returnBusiness.Search_SelectPrint(
                                                    new BranchReturnOrderPattern()
                                                    {
                                                        IsApproved=true,
                                                        OrderedPersianDate = new DateRangePattern()
                                                        {
                                                            SearchMode = DateRangePattern.SearchType.Between,
                                                            FromDate = fromDate.PersianDate,
                                                            ToDate = toDate.PersianDate
                                                        }
                                                    });

         
           
            var branchs = orderList.Select(f => f.Branch).Distinct().AsQueryable();


            long totalCost = orderList.Sum(f => f.TotalPrice);
            if (returnList.Count() > 0)
                totalCost -= returnList.Sum(f => f.TotalPrice);

            //Datas Should Set For Chart

            //1.Dataset

            //Example
            //[[["none",23],["error",0],["click",5],["impression",25]]]

            costList.Value = "";

            foreach (var branch in branchs.OrderBy(f => f.Title))
            {
                //["none",23]
                long subDataSet = 0;

                var tempOrderResult = orderList.Where(f => f.Branch_ID == branch.ID);
                var tempLackResult = lackList.Where(f => f.BranchOrder.Branch_ID == branch.ID);
                var tempReturnList = returnList.Where(f => f.Branch_ID == branch.ID);

                if (tempOrderResult.Count() > 0)
                    subDataSet = tempOrderResult.Sum(f => f.TotalPrice);

                if (tempLackResult.Count() > 0)
                    subDataSet -= tempLackResult.Sum(f => f.TotalPrice);

                if (tempReturnList.Count() > 0)
                    subDataSet -= tempReturnList.Sum(f => f.TotalPrice);



                string textPercent = (((float)subDataSet / (float)totalCost) * (float)100).ToString();

                costList.Value += "+" + branch.Title + "  -  " + UIUtil.GetCommaSeparatedOf(subDataSet) + " ریال";

                dataSetObj += ", ['" + branch.Title + "', " + textPercent + "]";
            }


            dataSetObj = CheckForHasData(dataSetObj).Substring(1);



            //4.Legegnd
            //{label: 'Independent Brands'}, {label: 'Pepsi Brands'}
            seriesLable = CheckForHasData(branchs.Select(f => f.Title).OrderBy(f => f).ToList().Aggregate("", (result, next) => result + ", {label: '" + next + "'}")).Substring(1);
            //seriesLable = CheckForHasData(branchs.Select(f => f.Title).OrderBy(f => f).ToList().Aggregate("", (result, next) => result + ", '" + next + "'")).Substring(1);

            //5.Label jqPlot Argument
            customLabel = string.Format("نمودار درآمد کلی شعب مابین {0} و {1}", fromDate.PersianDate, toDate.PersianDate);

        }
        catch (Exception)
        {
            Response.Redirect(GetRouteUrl("admin-branchorder", null));
        }
    }

    private string CheckForHasData(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            if (!UserSession.CurrentMessages.Contains(UserMessageKey.ChartHasNoData))
                UserSession.AddMessage(UserMessageKey.ChartHasNoData);

            return "  ";
        }
        else
            return data;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ChartInit();
    }
}