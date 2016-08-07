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

public partial class Pages_Admin_Chart_ProductSoldChartByBranch : BasePage
{
    public string dataSetObj = "";
    public string dataSetArgument = "";
    public string seriesLable = "";
    public string customLabel = "";
    public string tickObj = "";
    public string tickLabelList = "";

    BranchOrderBusiness orderBusiness;
    BranchReturnOrderBusiness returnBusiness;

    IQueryable<Branch> branchs;

    IQueryable<BranchOrder> orderList;
    IQueryable<BranchLackOrder> lackList;
    IQueryable<BranchReturnOrder> returnList;

    IQueryable<BranchOrderDetail> orderProductList;
    IQueryable<BranchLackOrderDetail> lackProductList;
    IQueryable<BranchReturnOrderDetail> returnProductList;

    long selectedorderID;

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


    private void ChartInit()
    {
        try
        {
            orderBusiness = new BranchOrderBusiness();
            orderList = orderBusiness.Search_SelectPrint(
                                                  new BranchOrderPattern()
                                                  {
                                                      IsApproved = true,
                                                      OrderedPersianDate = new DateRangePattern()
                                                      {
                                                          SearchMode = DateRangePattern.SearchType.Between,
                                                          FromDate = fromDate.PersianDate,
                                                          ToDate = toDate.PersianDate
                                                      }
                                                  });


            lackList = orderList.SelectMany(f => f.BranchLackOrders);

            returnBusiness = new BranchReturnOrderBusiness();

            returnBusiness.Context = orderBusiness.Context;


            returnList = returnBusiness.Search_SelectPrint(
                                                    new BranchReturnOrderPattern()
                                                    {
                                                        IsApproved = true,
                                                        OrderedPersianDate = new DateRangePattern()
                                                        {
                                                            SearchMode = DateRangePattern.SearchType.Between,
                                                            FromDate = fromDate.PersianDate,
                                                            ToDate = toDate.PersianDate
                                                        }
                                                    });




            //var products = orderProductList.Select(f => f.BranchOrderTitle).Union(returnProductList.Select(f => f.BranchOrderTitle)).Distinct().AsQueryable();

            selectedorderID = lkpOrderTitle.GetSelectedValue<long>();
            selectedorderID = (selectedorderID < 1) ? new pgcEntities().BranchOrderTitles.OrderBy(f => f.DisplayOrder).First().ID : selectedorderID;
            //products = products.Where(f => f.ID == selectedorderID);

            orderProductList = orderList.SelectMany(f => f.BranchOrderDetails).Where(f => f.BranchOrderTitle_ID == selectedorderID);
            lackProductList = lackList.SelectMany(f => f.BranchLackOrderDetails).Where(f => f.BranchOrderTitle_ID == selectedorderID);
            returnProductList = returnList.SelectMany(f => f.BranchReturnOrderDetails).Where(f => f.BranchOrderTitle_ID == selectedorderID);
            
            branchs = orderProductList.Select(f => f.BranchOrder.Branch).Union(returnList.Select(f => f.Branch)).Distinct().AsQueryable();
            branchs = branchs.OrderBy(f => f.DispOrder);



            switch (timeType.SelectedValue)
            {
                case "1":
                    SetDayliChart();
                    break;
                case "2":
                    SetWeeklyChart();
                    break;
                case "3":
                    SetMonthlyChart();
                    break;
                default:
                    break;
            }
        }
        catch (Exception)
        {
            Response.Redirect(GetRouteUrl("admin-branchorder", null));
        }
    }

    private void SetMonthlyChart()
    {
        //Datas From DB
        var tickList = orderList.Select(f => f.OrderedPersianDate.Substring(0, 7)).Union(returnList.Select(f => f.OrderedPersianDate.Substring(0, 7))).Distinct().OrderBy(f => f);



        //Datas Should Set For Chart

        //1.Dataset

        //Example
        //var dataSets = {
        //dset1: [['1392/01/01', 250000], [0.034259, 0.000122], [0.036908, 0.000271], [0.039957, 0.000457], [0.043623, 0.000699], [0.048008, 0.001005], [0.053278, 0.001404], [0.059665, 0.001916], [0.067405, 0.002587], [0.076951, 0.003482], [0.088909, 0.004676], [0.103706, 0.006234], [0.121406, 0.008198], [0.142376, 0.010698], [0.167456, 0.013945], [0.198392, 0.018266], [0.237371, 0.024152], [0.287096, 0.03217], [0.350664, 0.043128], [0.43322, 0.058856], [0.546538, 0.08196], [0.703419, 0.116382], [0.921534, 0.167573], [1.225405, 0.244101], [1.658248, 0.362099], [2.286919, 0.545089], [3.168531, 0.811148], [4.338121, 1.180312], [5.828025, 1.677699], [7.674282, 2.317937], [9.856185, 3.124879], [12.427952, 4.135038], [15.374906, 5.337835], [18.559853, 6.704187], [21.938501, 8.240167], [25.480211, 9.968066], [29.195872, 11.896316], [33.040813, 13.995922], [36.929108, 16.268393], [40.883239, 18.753271], [44.934876, 21.456742], [48.979949, 24.281483], [52.874709, 27.163823], [56.569122, 30.090586], [60.143349, 33.155912], [63.632698, 36.332609], [67.019887, 39.647977], [70.272329, 42.982924], [73.31569, 46.301522], [76.137996, 49.580067], [78.734309, 52.80729], [81.144203, 56.013671], [83.364771, 59.154237], [85.40072, 62.237023], [87.276365, 65.274088], [88.994984, 68.231226], [90.547773, 71.063226], [91.930153, 73.740825], [93.143588, 76.235257], [94.19365, 78.526502], [95.093093, 80.626967], [95.863261, 82.534767], [96.518119, 84.276606], [97.07861, 85.871008], [97.555507, 87.313822], [97.958887, 88.612275], [98.29631, 89.76839], [98.57645, 90.788966], [98.805892, 91.680096], [98.994988, 92.471419], [99.15183, 93.168954], [99.279882, 93.771844], [99.384116, 94.303287], [99.470272, 94.773123], [99.541871, 95.191639], [99.601871, 95.572501], [99.653556, 95.924822], [99.698334, 96.25129], [99.737113, 96.553878], [99.770733, 96.834228], [99.800151, 97.099569], [99.826487, 97.355328], [99.85018, 97.600349], [99.871291, 97.832168], [99.889939, 98.050737], [99.906286, 98.254414], [99.92045, 98.442305], [99.932702, 98.616245], [99.9433, 98.776877], [99.952485, 98.926517], [99.960528, 99.066855], [99.96755, 99.196967], [99.9736, 99.316087], [99.978742, 99.423943], [99.983126, 99.523155], [99.986929, 99.615949], [99.990275, 99.703425], [99.993221, 99.785398], [99.995794, 99.861721], [99.998031, 99.932812], [100, 100]],
        //dset3: [[0, 0], [0.044856, 0.00021], [0.048153, 0.000449], [0.051764, 0.000737], [0.055907, 0.001092], [0.060801, 0.00155], [0.066762, 0.002138], [0.073899, 0.002894], [0.082612, 0.003876], [0.093168, 0.005148], [0.106164, 0.006835], [0.122508, 0.009095], [0.142886, 0.012069], [0.16758, 0.015833], [0.19669, 0.020566], [0.23118, 0.026579], [0.273098, 0.034447], [0.325019, 0.044958], [0.390807, 0.059161], [0.474177, 0.078318], [0.579565, 0.104359], [0.719638, 0.14226], [0.911325, 0.197784], [1.175054, 0.278915], [1.537895, 0.397967], [2.041596, 0.57646], [2.758826, 0.848809], [3.776691, 1.250019], [5.133703, 1.8048], [6.852621, 2.537016], [8.948744, 3.478047], [11.417648, 4.640509], [14.293846, 6.085098], [17.634387, 7.851123], [21.360828, 9.886831], [25.29956, 12.160142], [29.428878, 14.674388], [33.707138, 17.452533], [38.129755, 20.506174], [42.671778, 23.796002], [47.229102, 27.303349], [51.800588, 31.060857], [56.383771, 35.053496], [60.886558, 39.145526], [65.12792, 43.181575], [69.030354, 47.116849], [72.617033, 51.025646], [75.986887, 54.923636], [79.112304, 58.743632], [81.975561, 62.443223], [84.537139, 65.922443], [86.782817, 69.135506], [88.71901, 72.103335], [90.413815, 74.878966], [91.908817, 77.500639], [93.228911, 79.953304], [94.381653, 82.20716], [95.359352, 84.209133], [96.170082, 85.973719], [96.837248, 87.5051], [97.378991, 88.838761], [97.8273, 90.020506], [98.200135, 91.065991], [98.506646, 91.96714], [98.752685, 92.735267], [98.949784, 93.390185], [99.10766, 93.94721], [99.232528, 94.417506], [99.334335, 94.838423], [99.42121, 95.226477], [99.497035, 95.589049], [99.563629, 95.927413], [99.621726, 96.240868], [99.672192, 96.530566], [99.715746, 96.794464], [99.752893, 97.034788], [99.784601, 97.251679], [99.811139, 97.442528], [99.833203, 97.614678], [99.852181, 97.774104], [99.868723, 97.92227], [99.883153, 98.05955], [99.89576, 98.188276], [99.906951, 98.310902], [99.917064, 98.429897], [99.926265, 98.545001], [99.9346, 98.656238], [99.942191, 98.76409], [99.949086, 98.868769], [99.955426, 98.971691], [99.961279, 99.072938], [99.966677, 99.172455], [99.971673, 99.270765], [99.976302, 99.367541], [99.980575, 99.462525], [99.984496, 99.554946], [99.988063, 99.644199], [99.991314, 99.731953], [99.994366, 99.820708], [99.997264, 99.910225], [100, 100]]
        //} 

        foreach (var branch in branchs)
        {
            string subDataSet = "";

            foreach (var persianDate in tickList)
            {
                var tempOrderProductResult = orderProductList.Where(f => f.BranchOrder.Branch_ID == branch.ID && f.BranchOrder.OrderedPersianDate.Substring(0, 7) == persianDate);
                var tempLackProductResult = lackProductList.Where(f => f.BranchLackOrder.BranchOrder.Branch_ID == branch.ID && f.BranchLackOrder.BranchOrder.OrderedPersianDate.Substring(0, 7) == persianDate);
                var tempReturnProductList = returnProductList.Where(f => f.BranchReturnOrder.Branch_ID == branch.ID && f.BranchReturnOrder.OrderedPersianDate.Substring(0, 7) == persianDate );

                long subTempTotalPrice = 0;

                if (tempOrderProductResult.Count() > 0)
                    subTempTotalPrice = tempOrderProductResult.Sum(f => f.Quantity);

                if (tempLackProductResult.Count() > 0)
                    subTempTotalPrice -= tempLackProductResult.Sum(f => f.Quantity);

                if (tempReturnProductList.Count() > 0)
                    subTempTotalPrice -= tempReturnProductList.Sum(f => f.Quantity);

                subDataSet += "," + subTempTotalPrice.ToString();
            }

            subDataSet = subDataSet.Substring(1);
            subDataSet = ", dset" + branch.ID.ToString() + " : [" + subDataSet + "]";

            dataSetObj += subDataSet;
        }


        dataSetObj = CheckForHasData(dataSetObj).Substring(1);


        //2.DataArgument
        dataSetArgument = CheckForHasData(branchs.Select(f => f.ID).ToList().Aggregate<long, string>("", (result, next) => result + ", dataset.dset" + next.ToString())).Substring(1);

        //3.Tick 

        if (tickList.Count() > 30)
        {
            int rowIndex = 0;
            int period = tickList.Count() / 10;
            tickObj = CheckForHasData(tickList.ToList().Aggregate("", (result, next) => result + (((rowIndex++) % period == 0) ? ", '" + next + "'" : ",' '"))).Substring(1);
        }
        else
            tickObj = CheckForHasData(tickList.ToList().Aggregate("", (result, next) => result + ", '" + next + "'")).Substring(1);

        tickLabelList = CheckForHasData(tickList.ToList().Aggregate("", (result, next) => result + "," + next + "")).Substring(1);



        //4.Legegnd
        //{label: 'Independent Brands'}, {label: 'Pepsi Brands'}
        seriesLable = CheckForHasData(branchs.Select(f => f.Title).ToList().Aggregate("", (result, next) => result + ", {label: '" + next + "'}")).Substring(1);
        //seriesLable += ",{label: 'کل مجموعه'}";

        //5.Label jqPlot Argument
        customLabel = string.Format("نمودار فروش {2} مابین {0} و {1}", fromDate.PersianDate, toDate.PersianDate, new BranchOrderTitleBusiness().Retrieve(selectedorderID).Title);

    }

    private void SetWeeklyChart()
    {
        //Datas From DB
        var tempTime = orderList.Select(f => f.OrderedPersianDate).Union(returnList.Select(f => f.OrderedPersianDate)).Distinct().OrderBy(f => f);




        DateTime maxDate = DateUtil.GetEnglishDateTime(tempTime.Max());
        maxDate = maxDate.AddDays(7 - ((int)maxDate.DayOfWeek));

        DateTime minDate = DateUtil.GetEnglishDateTime(tempTime.Min());
        minDate = minDate.AddDays(-((int)minDate.DayOfWeek));

        List<string> tickMinList = new List<string>();
        List<string> tickLabel = new List<string>();

        bool beContinue = true;
        for (; beContinue; )
        {
            tickLabel.Add(string.Format("از {0} تا {1}",
                    DateUtil.GetPersianDateShortString(minDate).Substring(5),
                    DateUtil.GetPersianDateShortString(minDate.AddDays(6)).Substring(5)
                ));


            tickMinList.Add(DateUtil.GetPersianDateShortString(minDate));
            minDate = minDate.AddDays(7);

            if (maxDate.CompareTo(minDate) < 0)
                beContinue = false;
        }





        //Datas Should Set For Chart

        //1.Dataset

        //Example
        //var dataSets = {
        //dset1: [['1392/01/01', 250000], [0.034259, 0.000122], [0.036908, 0.000271], [0.039957, 0.000457], [0.043623, 0.000699], [0.048008, 0.001005], [0.053278, 0.001404], [0.059665, 0.001916], [0.067405, 0.002587], [0.076951, 0.003482], [0.088909, 0.004676], [0.103706, 0.006234], [0.121406, 0.008198], [0.142376, 0.010698], [0.167456, 0.013945], [0.198392, 0.018266], [0.237371, 0.024152], [0.287096, 0.03217], [0.350664, 0.043128], [0.43322, 0.058856], [0.546538, 0.08196], [0.703419, 0.116382], [0.921534, 0.167573], [1.225405, 0.244101], [1.658248, 0.362099], [2.286919, 0.545089], [3.168531, 0.811148], [4.338121, 1.180312], [5.828025, 1.677699], [7.674282, 2.317937], [9.856185, 3.124879], [12.427952, 4.135038], [15.374906, 5.337835], [18.559853, 6.704187], [21.938501, 8.240167], [25.480211, 9.968066], [29.195872, 11.896316], [33.040813, 13.995922], [36.929108, 16.268393], [40.883239, 18.753271], [44.934876, 21.456742], [48.979949, 24.281483], [52.874709, 27.163823], [56.569122, 30.090586], [60.143349, 33.155912], [63.632698, 36.332609], [67.019887, 39.647977], [70.272329, 42.982924], [73.31569, 46.301522], [76.137996, 49.580067], [78.734309, 52.80729], [81.144203, 56.013671], [83.364771, 59.154237], [85.40072, 62.237023], [87.276365, 65.274088], [88.994984, 68.231226], [90.547773, 71.063226], [91.930153, 73.740825], [93.143588, 76.235257], [94.19365, 78.526502], [95.093093, 80.626967], [95.863261, 82.534767], [96.518119, 84.276606], [97.07861, 85.871008], [97.555507, 87.313822], [97.958887, 88.612275], [98.29631, 89.76839], [98.57645, 90.788966], [98.805892, 91.680096], [98.994988, 92.471419], [99.15183, 93.168954], [99.279882, 93.771844], [99.384116, 94.303287], [99.470272, 94.773123], [99.541871, 95.191639], [99.601871, 95.572501], [99.653556, 95.924822], [99.698334, 96.25129], [99.737113, 96.553878], [99.770733, 96.834228], [99.800151, 97.099569], [99.826487, 97.355328], [99.85018, 97.600349], [99.871291, 97.832168], [99.889939, 98.050737], [99.906286, 98.254414], [99.92045, 98.442305], [99.932702, 98.616245], [99.9433, 98.776877], [99.952485, 98.926517], [99.960528, 99.066855], [99.96755, 99.196967], [99.9736, 99.316087], [99.978742, 99.423943], [99.983126, 99.523155], [99.986929, 99.615949], [99.990275, 99.703425], [99.993221, 99.785398], [99.995794, 99.861721], [99.998031, 99.932812], [100, 100]],
        //dset3: [[0, 0], [0.044856, 0.00021], [0.048153, 0.000449], [0.051764, 0.000737], [0.055907, 0.001092], [0.060801, 0.00155], [0.066762, 0.002138], [0.073899, 0.002894], [0.082612, 0.003876], [0.093168, 0.005148], [0.106164, 0.006835], [0.122508, 0.009095], [0.142886, 0.012069], [0.16758, 0.015833], [0.19669, 0.020566], [0.23118, 0.026579], [0.273098, 0.034447], [0.325019, 0.044958], [0.390807, 0.059161], [0.474177, 0.078318], [0.579565, 0.104359], [0.719638, 0.14226], [0.911325, 0.197784], [1.175054, 0.278915], [1.537895, 0.397967], [2.041596, 0.57646], [2.758826, 0.848809], [3.776691, 1.250019], [5.133703, 1.8048], [6.852621, 2.537016], [8.948744, 3.478047], [11.417648, 4.640509], [14.293846, 6.085098], [17.634387, 7.851123], [21.360828, 9.886831], [25.29956, 12.160142], [29.428878, 14.674388], [33.707138, 17.452533], [38.129755, 20.506174], [42.671778, 23.796002], [47.229102, 27.303349], [51.800588, 31.060857], [56.383771, 35.053496], [60.886558, 39.145526], [65.12792, 43.181575], [69.030354, 47.116849], [72.617033, 51.025646], [75.986887, 54.923636], [79.112304, 58.743632], [81.975561, 62.443223], [84.537139, 65.922443], [86.782817, 69.135506], [88.71901, 72.103335], [90.413815, 74.878966], [91.908817, 77.500639], [93.228911, 79.953304], [94.381653, 82.20716], [95.359352, 84.209133], [96.170082, 85.973719], [96.837248, 87.5051], [97.378991, 88.838761], [97.8273, 90.020506], [98.200135, 91.065991], [98.506646, 91.96714], [98.752685, 92.735267], [98.949784, 93.390185], [99.10766, 93.94721], [99.232528, 94.417506], [99.334335, 94.838423], [99.42121, 95.226477], [99.497035, 95.589049], [99.563629, 95.927413], [99.621726, 96.240868], [99.672192, 96.530566], [99.715746, 96.794464], [99.752893, 97.034788], [99.784601, 97.251679], [99.811139, 97.442528], [99.833203, 97.614678], [99.852181, 97.774104], [99.868723, 97.92227], [99.883153, 98.05955], [99.89576, 98.188276], [99.906951, 98.310902], [99.917064, 98.429897], [99.926265, 98.545001], [99.9346, 98.656238], [99.942191, 98.76409], [99.949086, 98.868769], [99.955426, 98.971691], [99.961279, 99.072938], [99.966677, 99.172455], [99.971673, 99.270765], [99.976302, 99.367541], [99.980575, 99.462525], [99.984496, 99.554946], [99.988063, 99.644199], [99.991314, 99.731953], [99.994366, 99.820708], [99.997264, 99.910225], [100, 100]]
        //} 

        foreach (var branch in branchs)
        {
            string subDataSet = "";

            foreach (var minPersianDate in tickMinList)
            {
                string maxPersianDate = DateUtil.GetPersianDateShortString(DateUtil.GetEnglishDateTime(minPersianDate).AddDays(6));

                var tempOrderProductResult = orderProductList.Where(f => f.BranchOrder.Branch_ID == branch.ID && f.BranchOrder.OrderedPersianDate.CompareTo(maxPersianDate) <= 0 && 0 <= f.BranchOrder.OrderedPersianDate.CompareTo(minPersianDate));
                var tempLackProductResult = lackProductList.Where(f => f.BranchLackOrder.BranchOrder.Branch_ID == branch.ID && f.BranchLackOrder.BranchOrder.OrderedPersianDate.CompareTo(maxPersianDate) <= 0 && 0 <= f.BranchLackOrder.BranchOrder.OrderedPersianDate.CompareTo(minPersianDate));
                var tempReturnProductList = returnProductList.Where(f => f.BranchReturnOrder.Branch_ID == branch.ID && f.BranchReturnOrder.OrderedPersianDate.CompareTo(maxPersianDate) <= 0 && 0 <= f.BranchReturnOrder.OrderedPersianDate.CompareTo(minPersianDate));

                long subTempTotalPrice = 0;

                if (tempOrderProductResult.Count() > 0)
                    subTempTotalPrice = tempOrderProductResult.Sum(f => f.Quantity);

                if (tempLackProductResult.Count() > 0)
                    subTempTotalPrice -= tempLackProductResult.Sum(f => f.Quantity);

                if (tempReturnProductList.Count() > 0)
                    subTempTotalPrice -= tempReturnProductList.Sum(f => f.Quantity);

                subDataSet += "," + subTempTotalPrice.ToString();
            }

            subDataSet = subDataSet.Substring(1);
            subDataSet = ", dset" + branch.ID.ToString() + " : [" + subDataSet + "]";

            dataSetObj += subDataSet;
        }

       
       



        dataSetObj = CheckForHasData(dataSetObj).Substring(1);


        //2.DataArgument
        dataSetArgument = CheckForHasData(branchs.Select(f => f.ID).ToList().Aggregate<long, string>("", (result, next) => result + ", dataset.dset" + next.ToString())).Substring(1);

        //3.Tick 

        if (tickLabel.Count() > 30)
        {
            int rowIndex = 0;
            int period = tickMinList.Count() / 10;
            tickObj = CheckForHasData(tickLabel.ToList().Aggregate("", (result, next) => result + (((rowIndex++) % period == 0) ? ", '" + next + "'" : ",' '"))).Substring(1);
        }
        else
            tickObj = CheckForHasData(tickLabel.ToList().Aggregate("", (result, next) => result + ", '" + next + "'")).Substring(1);

        tickLabelList = CheckForHasData(tickLabel.ToList().Aggregate("", (result, next) => result + ", " + next)).Substring(1); ;


        //4.Legegnd
        //{label: 'Independent Brands'}, {label: 'Pepsi Brands'}
        seriesLable = CheckForHasData(branchs.Select(f => f.Title).ToList().Aggregate("", (result, next) => result + ", {label: '" + next + "'}")).Substring(1);
        //seriesLable += ",{label: 'کل مجموعه'}";

        //5.Label jqPlot Argument
        customLabel = string.Format("نمودار فروش {2} مابین {0} و {1}", fromDate.PersianDate, toDate.PersianDate, new BranchOrderTitleBusiness().Retrieve(selectedorderID).Title);

    }

    private void SetDayliChart()
    {
        //Datas From DB
        var tickList = orderList.Select(f => f.OrderedPersianDate).Union(returnList.Select(f => f.OrderedPersianDate)).Distinct().OrderBy(f => f);



        //Datas Should Set For Chart

        //1.Dataset

        //Example
        //var dataSets = {
        //dset1: [['1392/01/01', 250000], [0.034259, 0.000122], [0.036908, 0.000271], [0.039957, 0.000457], [0.043623, 0.000699], [0.048008, 0.001005], [0.053278, 0.001404], [0.059665, 0.001916], [0.067405, 0.002587], [0.076951, 0.003482], [0.088909, 0.004676], [0.103706, 0.006234], [0.121406, 0.008198], [0.142376, 0.010698], [0.167456, 0.013945], [0.198392, 0.018266], [0.237371, 0.024152], [0.287096, 0.03217], [0.350664, 0.043128], [0.43322, 0.058856], [0.546538, 0.08196], [0.703419, 0.116382], [0.921534, 0.167573], [1.225405, 0.244101], [1.658248, 0.362099], [2.286919, 0.545089], [3.168531, 0.811148], [4.338121, 1.180312], [5.828025, 1.677699], [7.674282, 2.317937], [9.856185, 3.124879], [12.427952, 4.135038], [15.374906, 5.337835], [18.559853, 6.704187], [21.938501, 8.240167], [25.480211, 9.968066], [29.195872, 11.896316], [33.040813, 13.995922], [36.929108, 16.268393], [40.883239, 18.753271], [44.934876, 21.456742], [48.979949, 24.281483], [52.874709, 27.163823], [56.569122, 30.090586], [60.143349, 33.155912], [63.632698, 36.332609], [67.019887, 39.647977], [70.272329, 42.982924], [73.31569, 46.301522], [76.137996, 49.580067], [78.734309, 52.80729], [81.144203, 56.013671], [83.364771, 59.154237], [85.40072, 62.237023], [87.276365, 65.274088], [88.994984, 68.231226], [90.547773, 71.063226], [91.930153, 73.740825], [93.143588, 76.235257], [94.19365, 78.526502], [95.093093, 80.626967], [95.863261, 82.534767], [96.518119, 84.276606], [97.07861, 85.871008], [97.555507, 87.313822], [97.958887, 88.612275], [98.29631, 89.76839], [98.57645, 90.788966], [98.805892, 91.680096], [98.994988, 92.471419], [99.15183, 93.168954], [99.279882, 93.771844], [99.384116, 94.303287], [99.470272, 94.773123], [99.541871, 95.191639], [99.601871, 95.572501], [99.653556, 95.924822], [99.698334, 96.25129], [99.737113, 96.553878], [99.770733, 96.834228], [99.800151, 97.099569], [99.826487, 97.355328], [99.85018, 97.600349], [99.871291, 97.832168], [99.889939, 98.050737], [99.906286, 98.254414], [99.92045, 98.442305], [99.932702, 98.616245], [99.9433, 98.776877], [99.952485, 98.926517], [99.960528, 99.066855], [99.96755, 99.196967], [99.9736, 99.316087], [99.978742, 99.423943], [99.983126, 99.523155], [99.986929, 99.615949], [99.990275, 99.703425], [99.993221, 99.785398], [99.995794, 99.861721], [99.998031, 99.932812], [100, 100]],
        //dset3: [[0, 0], [0.044856, 0.00021], [0.048153, 0.000449], [0.051764, 0.000737], [0.055907, 0.001092], [0.060801, 0.00155], [0.066762, 0.002138], [0.073899, 0.002894], [0.082612, 0.003876], [0.093168, 0.005148], [0.106164, 0.006835], [0.122508, 0.009095], [0.142886, 0.012069], [0.16758, 0.015833], [0.19669, 0.020566], [0.23118, 0.026579], [0.273098, 0.034447], [0.325019, 0.044958], [0.390807, 0.059161], [0.474177, 0.078318], [0.579565, 0.104359], [0.719638, 0.14226], [0.911325, 0.197784], [1.175054, 0.278915], [1.537895, 0.397967], [2.041596, 0.57646], [2.758826, 0.848809], [3.776691, 1.250019], [5.133703, 1.8048], [6.852621, 2.537016], [8.948744, 3.478047], [11.417648, 4.640509], [14.293846, 6.085098], [17.634387, 7.851123], [21.360828, 9.886831], [25.29956, 12.160142], [29.428878, 14.674388], [33.707138, 17.452533], [38.129755, 20.506174], [42.671778, 23.796002], [47.229102, 27.303349], [51.800588, 31.060857], [56.383771, 35.053496], [60.886558, 39.145526], [65.12792, 43.181575], [69.030354, 47.116849], [72.617033, 51.025646], [75.986887, 54.923636], [79.112304, 58.743632], [81.975561, 62.443223], [84.537139, 65.922443], [86.782817, 69.135506], [88.71901, 72.103335], [90.413815, 74.878966], [91.908817, 77.500639], [93.228911, 79.953304], [94.381653, 82.20716], [95.359352, 84.209133], [96.170082, 85.973719], [96.837248, 87.5051], [97.378991, 88.838761], [97.8273, 90.020506], [98.200135, 91.065991], [98.506646, 91.96714], [98.752685, 92.735267], [98.949784, 93.390185], [99.10766, 93.94721], [99.232528, 94.417506], [99.334335, 94.838423], [99.42121, 95.226477], [99.497035, 95.589049], [99.563629, 95.927413], [99.621726, 96.240868], [99.672192, 96.530566], [99.715746, 96.794464], [99.752893, 97.034788], [99.784601, 97.251679], [99.811139, 97.442528], [99.833203, 97.614678], [99.852181, 97.774104], [99.868723, 97.92227], [99.883153, 98.05955], [99.89576, 98.188276], [99.906951, 98.310902], [99.917064, 98.429897], [99.926265, 98.545001], [99.9346, 98.656238], [99.942191, 98.76409], [99.949086, 98.868769], [99.955426, 98.971691], [99.961279, 99.072938], [99.966677, 99.172455], [99.971673, 99.270765], [99.976302, 99.367541], [99.980575, 99.462525], [99.984496, 99.554946], [99.988063, 99.644199], [99.991314, 99.731953], [99.994366, 99.820708], [99.997264, 99.910225], [100, 100]]
        //} 

        foreach (var branch in branchs)
        {
            string subDataSet = "";

            foreach (var persianDate in tickList)
            {
                var tempOrderProductResult = orderProductList.Where(f => f.BranchOrder.Branch_ID == branch.ID && f.BranchOrder.OrderedPersianDate == persianDate);
                var tempLackProductResult = lackProductList.Where(f => f.BranchLackOrder.BranchOrder.Branch_ID == branch.ID && f.BranchLackOrder.BranchOrder.OrderedPersianDate == persianDate);
                var tempReturnProductList = returnProductList.Where(f => f.BranchReturnOrder.Branch_ID == branch.ID && f.BranchReturnOrder.OrderedPersianDate == persianDate);

                long subTempTotalPrice = 0;

                if (tempOrderProductResult.Count() > 0)
                    subTempTotalPrice = tempOrderProductResult.Sum(f => f.Quantity);

                if (tempLackProductResult.Count() > 0)
                    subTempTotalPrice -= tempLackProductResult.Sum(f => f.Quantity);

                if (tempReturnProductList.Count() > 0)
                    subTempTotalPrice -= tempReturnProductList.Sum(f => f.Quantity);

                subDataSet += "," + subTempTotalPrice.ToString();
            }


            subDataSet = subDataSet.Substring(1);
            subDataSet = ", dset" + branch.ID.ToString() + " : [" + subDataSet + "]";

            dataSetObj += subDataSet;
        }


        dataSetObj = CheckForHasData(dataSetObj).Substring(1);


        //2.DataArgument
        dataSetArgument = CheckForHasData(branchs.Select(f => f.ID).ToList().Aggregate<long, string>("", (result, next) => result + ", dataset.dset" + next.ToString())).Substring(1);

        //3.Tick 

        if (tickList.Count() > 31)
        {
            int rowIndex = 0;
            int period = tickList.Count() / 10;
            tickObj = CheckForHasData(tickList.ToList().Aggregate("", (result, next) => result + (((rowIndex++) % period == 0) ? ", '" + next + "'" : ",' '"))).Substring(1);
        }
        else
            tickObj = CheckForHasData(tickList.ToList().Aggregate("", (result, next) => result + ", '" + next + "'")).Substring(1);

        tickLabelList = CheckForHasData(tickList.ToList().Aggregate("", (result, next) => result + "," + next + "")).Substring(1);



        //4.Legegnd
        //{label: 'Independent Brands'}, {label: 'Pepsi Brands'}
        seriesLable = CheckForHasData(branchs.Select(f => f.Title).ToList().Aggregate("", (result, next) => result + ", {label: '" + next + "'}")).Substring(1);
        //seriesLable += ",{label: 'کل مجموعه'}";

        //5.Label jqPlot Argument
        customLabel = string.Format("نمودار فروش {2} مابین {0} و {1}", fromDate.PersianDate, toDate.PersianDate, new BranchOrderTitleBusiness().Retrieve(selectedorderID).Title);

    }

    
    public void Province_Changed(object sender, EventArgs e)
    {
        ChartInit();
    }

    //private void ChartInit()
    //{
    //    try
    //    {
    //        //Datas From DB

    //        var tickList = orderList.Select(f => f.OrderedPersianDate).Distinct().OrderBy(f => f);



    //        //Datas Should Set For Chart

    //        //1.Dataset

    //        //Example
    //        //var dataSets = {
    //        //dset1: [['1392/01/01', 250000], [0.034259, 0.000122], [0.036908, 0.000271], [0.039957, 0.000457], [0.043623, 0.000699], [0.048008, 0.001005], [0.053278, 0.001404], [0.059665, 0.001916], [0.067405, 0.002587], [0.076951, 0.003482], [0.088909, 0.004676], [0.103706, 0.006234], [0.121406, 0.008198], [0.142376, 0.010698], [0.167456, 0.013945], [0.198392, 0.018266], [0.237371, 0.024152], [0.287096, 0.03217], [0.350664, 0.043128], [0.43322, 0.058856], [0.546538, 0.08196], [0.703419, 0.116382], [0.921534, 0.167573], [1.225405, 0.244101], [1.658248, 0.362099], [2.286919, 0.545089], [3.168531, 0.811148], [4.338121, 1.180312], [5.828025, 1.677699], [7.674282, 2.317937], [9.856185, 3.124879], [12.427952, 4.135038], [15.374906, 5.337835], [18.559853, 6.704187], [21.938501, 8.240167], [25.480211, 9.968066], [29.195872, 11.896316], [33.040813, 13.995922], [36.929108, 16.268393], [40.883239, 18.753271], [44.934876, 21.456742], [48.979949, 24.281483], [52.874709, 27.163823], [56.569122, 30.090586], [60.143349, 33.155912], [63.632698, 36.332609], [67.019887, 39.647977], [70.272329, 42.982924], [73.31569, 46.301522], [76.137996, 49.580067], [78.734309, 52.80729], [81.144203, 56.013671], [83.364771, 59.154237], [85.40072, 62.237023], [87.276365, 65.274088], [88.994984, 68.231226], [90.547773, 71.063226], [91.930153, 73.740825], [93.143588, 76.235257], [94.19365, 78.526502], [95.093093, 80.626967], [95.863261, 82.534767], [96.518119, 84.276606], [97.07861, 85.871008], [97.555507, 87.313822], [97.958887, 88.612275], [98.29631, 89.76839], [98.57645, 90.788966], [98.805892, 91.680096], [98.994988, 92.471419], [99.15183, 93.168954], [99.279882, 93.771844], [99.384116, 94.303287], [99.470272, 94.773123], [99.541871, 95.191639], [99.601871, 95.572501], [99.653556, 95.924822], [99.698334, 96.25129], [99.737113, 96.553878], [99.770733, 96.834228], [99.800151, 97.099569], [99.826487, 97.355328], [99.85018, 97.600349], [99.871291, 97.832168], [99.889939, 98.050737], [99.906286, 98.254414], [99.92045, 98.442305], [99.932702, 98.616245], [99.9433, 98.776877], [99.952485, 98.926517], [99.960528, 99.066855], [99.96755, 99.196967], [99.9736, 99.316087], [99.978742, 99.423943], [99.983126, 99.523155], [99.986929, 99.615949], [99.990275, 99.703425], [99.993221, 99.785398], [99.995794, 99.861721], [99.998031, 99.932812], [100, 100]],
    //        //dset3: [[0, 0], [0.044856, 0.00021], [0.048153, 0.000449], [0.051764, 0.000737], [0.055907, 0.001092], [0.060801, 0.00155], [0.066762, 0.002138], [0.073899, 0.002894], [0.082612, 0.003876], [0.093168, 0.005148], [0.106164, 0.006835], [0.122508, 0.009095], [0.142886, 0.012069], [0.16758, 0.015833], [0.19669, 0.020566], [0.23118, 0.026579], [0.273098, 0.034447], [0.325019, 0.044958], [0.390807, 0.059161], [0.474177, 0.078318], [0.579565, 0.104359], [0.719638, 0.14226], [0.911325, 0.197784], [1.175054, 0.278915], [1.537895, 0.397967], [2.041596, 0.57646], [2.758826, 0.848809], [3.776691, 1.250019], [5.133703, 1.8048], [6.852621, 2.537016], [8.948744, 3.478047], [11.417648, 4.640509], [14.293846, 6.085098], [17.634387, 7.851123], [21.360828, 9.886831], [25.29956, 12.160142], [29.428878, 14.674388], [33.707138, 17.452533], [38.129755, 20.506174], [42.671778, 23.796002], [47.229102, 27.303349], [51.800588, 31.060857], [56.383771, 35.053496], [60.886558, 39.145526], [65.12792, 43.181575], [69.030354, 47.116849], [72.617033, 51.025646], [75.986887, 54.923636], [79.112304, 58.743632], [81.975561, 62.443223], [84.537139, 65.922443], [86.782817, 69.135506], [88.71901, 72.103335], [90.413815, 74.878966], [91.908817, 77.500639], [93.228911, 79.953304], [94.381653, 82.20716], [95.359352, 84.209133], [96.170082, 85.973719], [96.837248, 87.5051], [97.378991, 88.838761], [97.8273, 90.020506], [98.200135, 91.065991], [98.506646, 91.96714], [98.752685, 92.735267], [98.949784, 93.390185], [99.10766, 93.94721], [99.232528, 94.417506], [99.334335, 94.838423], [99.42121, 95.226477], [99.497035, 95.589049], [99.563629, 95.927413], [99.621726, 96.240868], [99.672192, 96.530566], [99.715746, 96.794464], [99.752893, 97.034788], [99.784601, 97.251679], [99.811139, 97.442528], [99.833203, 97.614678], [99.852181, 97.774104], [99.868723, 97.92227], [99.883153, 98.05955], [99.89576, 98.188276], [99.906951, 98.310902], [99.917064, 98.429897], [99.926265, 98.545001], [99.9346, 98.656238], [99.942191, 98.76409], [99.949086, 98.868769], [99.955426, 98.971691], [99.961279, 99.072938], [99.966677, 99.172455], [99.971673, 99.270765], [99.976302, 99.367541], [99.980575, 99.462525], [99.984496, 99.554946], [99.988063, 99.644199], [99.991314, 99.731953], [99.994366, 99.820708], [99.997264, 99.910225], [100, 100]]
    //        //} 

    //        foreach (var branch in branchs)
    //        {
    //            string subDataSet = "";

    //            foreach (var persianDate in tickList)
    //            {
    //                var tempOrderProductResult = orderProductList.Where(f => f.BranchOrder.Branch_ID == branch.ID && f.BranchOrder.OrderedPersianDate == persianDate && f.BranchOrderTitle_ID == selectedorderID);
    //                var tempReturnProductList = returnProductList.Where(f => f.BranchReturnOrder.Branch_ID == branch.ID && f.BranchReturnOrder.OrderedPersianDate == persianDate && f.BranchOrderTitle_ID == selectedorderID);

    //                    long subTempTotalPrice = 0;

    //                    if (tempOrderProductResult.Count() > 0)
    //                        subTempTotalPrice = tempOrderProductResult.Sum(f => f.Quantity);

    //                    if (tempReturnProductList.Count() > 0)
    //                        subTempTotalPrice -= tempReturnProductList.Sum(f => f.Quantity);

    //                    subDataSet += "," + subTempTotalPrice.ToString();
    //            }




    //            subDataSet = subDataSet.Substring(1);
    //            subDataSet = ", dset" + branch.ID.ToString() + " : [" + subDataSet + "]";

    //            dataSetObj += subDataSet;
    //        }


    //        dataSetObj = CheckForHasData(dataSetObj).Substring(1);


    //        //string subDataSetMax = "";

    //        //foreach (var persianDate in tickList)
    //        //{
    //        //    var tempProductOrderResult = orderProductList.Where(f => f.BranchOrder.OrderedPersianDate == persianDate);
    //        //    var tempReturnProductList = returnProductList.Where(f => f.BranchReturnOrder.OrderedPersianDate == persianDate);

    //        //    long subTempTotalPrice = 0;

    //        //    if (tempProductOrderResult.Count() > 0)
    //        //        subTempTotalPrice = tempProductOrderResult.Sum(f => f.Quantity);

    //        //    if (tempReturnProductList.Count() > 0)
    //        //        subTempTotalPrice -= tempReturnProductList.Sum(f => f.Quantity);

    //        //    subDataSetMax += "," + subTempTotalPrice.ToString();
    //        //}


    //        //subDataSetMax = CheckForHasData(subDataSetMax).Substring(1);
    //        //subDataSetMax = ", dsetMax : [" + subDataSetMax + "]";

    //        //dataSetObj += subDataSetMax;



    //        //2.DataArgument
    //        dataSetArgument = CheckForHasData(branchs.Select(f => f.ID).ToList().Aggregate<long, string>("", (result, next) => result + ", dataset.dset" + next.ToString())).Substring(1);
    //        //dataSetArgument += ",dataset.dsetMax";

    //        //3.Tick          
    //        //tickObj = CheckForHasData(tickList.ToList().Aggregate("", (result, next) => result + ", '" + next + "'")).Substring(1);

    //        if (tickList.Count() > 30)
    //        {
    //            int rowIndex = 0;
    //            int period = tickList.Count() / 10;
    //            tickObj = CheckForHasData(tickList.ToList().Aggregate("", (result, next) => result + (((rowIndex++) % period == 0) ? ", '" + next + "'" : ",' '"))).Substring(1);
    //        }
    //        else
    //            tickObj = CheckForHasData(tickList.ToList().Aggregate("", (result, next) => result + ", '" + next + "'")).Substring(1);

    //        tickLabelList = CheckForHasData(tickList.ToList().Aggregate("", (result, next) => result + "," + next + "")).Substring(1);




    //        //4.Legegnd
    //        //{label: 'Independent Brands'}, {label: 'Pepsi Brands'}
    //        seriesLable = CheckForHasData(branchs.Select(f => f.Title).ToList().Aggregate("", (result, next) => result + ", {label: '" + next + "'}")).Substring(1);
    //        //seriesLable += ",{label: 'کل مجموعه'}";

    //        //5.Label jqPlot Argument
    //        customLabel = string.Format("نمودار فروش {2} مابین {0} و {1}", fromDate.PersianDate, toDate.PersianDate, new BranchOrderTitleBusiness().Retrieve(selectedorderID).Title);

    //    }
    //    catch (Exception)
    //    {
    //        Response.Redirect(GetRouteUrl("admin-branchorder", null));
    //    }
    //}
}