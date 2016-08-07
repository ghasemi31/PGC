<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OldSoldCostPieChart.aspx.cs" Inherits="Pages_Admin_Chart_SoldCostPieChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%#this.Entity.Title %></title>
    <link id="page_favicon" href="<%#ResolveClientUrl("~/Styles/Images/favicon.ico") %>" rel="icon" type="image/x-icon" />

    <script type="text/javascript" src="<%#ResolveUrl("~/Scripts/Shared/jquery-1.7.2.min.js") %>"></script>
    
    <script type="text/javascript" src="<%#ResolveClientUrl("~/Scripts/Shared/jquery.blockUI.min.js") %>"></script>
    <script type="text/javascript" src="<%#ResolveClientUrl("~/Scripts/UserControls/Common/PersianDatePicker.js") %>"></script>
    
    <!--[if lt IE 9]><script language="javascript" type="text/javascript" src="<%#ResolveUrl("~/Scripts/Shared/jqPlot/excanvas.js") %>" ></script><![endif]-->
    <script type="text/javascript" src="<%#ResolveUrl("~/Scripts/Shared/jqPlot/jquery.jqplot.js") %>"></script>
    <%--<script type="text/javascript" src="<%#ResolveUrl("~/Scripts/Shared/jqPlot/jqplot.canvasAxisTickRenderer.js") %>"></script>
    <script type="text/javascript" src="<%#ResolveUrl("~/Scripts/Shared/jqPlot/jqplot.canvasTextRenderer.min.js") %>"></script>  --%>
    <%--<script type="text/javascript" src="<%#ResolveUrl("~/Scripts/Shared/jqPlot/jqplot.categoryAxisRenderer.min.js") %>"></script>--%>
    <script type="text/javascript" src="<%#ResolveUrl("~/Scripts/Shared/jqPlot/jqplot.pieRenderer.min.js") %>"></script>
    <%--<script type="text/javascript" src="<%#ResolveUrl("~/Scripts/Shared/jqPlot/jqplot.pointLabels.min.js") %>"></script>--%>
    <script type="text/javascript" src="<%#ResolveUrl("~/Scripts/UserControls/Common/Loading.js") %>"></script>
    <script type="text/javascript" src="<%#ResolveUrl("~/Scripts/UserControls/Common/UserMessageViewer.js") %>"></script>
                               
    <link rel="Stylesheet" type="text/css" href="<%#ResolveUrl("~/Styles/form.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%#ResolveUrl("~/Styles/Pages/Admin/Chart/ChartPage.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%#ResolveUrl("~/Styles/UserControls/Common/PersianDatePicker.css")%>" />
    <link rel="stylesheet" type="text/css" href="<%#ResolveUrl("~/Styles/Pages/Admin/Chart/jquery.jqplot.css") %>" />
    <link rel="Stylesheet" type="text/css" href="<%#ResolveUrl("~/Styles/UserControls/Common/UserMessageViewer.css") %>" />
    
    
</head>
<body>
    <script type="text/javascript">
        $(document).ready(function () {
                
            //1.    Create DataSets               
            var dataset = [[ <%=dataSetObj %> ]];

            //2.    Create jqPlot
            var plot1 = $.jqplot('chart1', dataset, {
                
                //2.0   Set Title
                title: '<%=customLabel %>',                        

                //2.1   Set DefaultSeries
                seriesDefaults:{ 
                    renderer: $.jqplot.PieRenderer ,
                    rendererOptions: {
                        showDataLabels: true
                    }
                }
                 
                ,
                series:[
                    {pointLabels:{
                        show: true,
                        labels:[<%=seriesLable %>]
                        }
                    }
                ],
                //2.2   Set Legend
                legend:{ show:true } , 
                
                //2.3   Set Background
                grid: {
                    drawBorder: false, 
                    drawGridlines: false,
                    background: 'none',
                    shadow:false
                }
            });
            //3.    Bind Some Function To it


            //Click For More Information
                $('#chart1').bind('jqplotDataClick', 
                function (ev, seriesIndex, pointIndex, data) {
                    //$('#info1').html('درصد: '+parseInt( data.toString().split(',')[1]).toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")+' %');                    
                    $('#info1').html($('#costList').val().split('+')[pointIndex+1]) ;
                }
            );


            $('.jqplot-title').css('width','')
            //So Dont SHow Sum of All at the first
            //$($($('tr.jqplot-table-legend').last()).find('td')).first().click()
        });
    </script>
    <form id="form1" runat="server">
        <div id="frame">
            <div id="wrapper">
                <div id="maincontent">
                    <div id="form">
                        <div id="wrapper">
                            <input type="hidden" id="costList" runat="server" clientidmode="Static" />
                            <fieldset class="fldSearch">
                            <legend><%=this.Entity.Title %> (با اعمال مرجوعی ها و کسری ها)</legend>
                                <table class="Table srchPanel">
                                    <tr>
                                        <td class="caption">از تاریخ</td>
                                        <td class="control"><kfk:PersianDatePicker ID="fromDate" runat="server" Required="true" /></td>
                                    
                                        <td class="caption">تا تاریخ</td>
                                        <td class="control"><kfk:PersianDatePicker ID="toDate" runat="server" Required="true" /></td>
                                    </tr>
                                    
                                    <tr><td colspan="4">&nbsp;</td></tr>
                                    
                                    <tr>
                                        <td colspan="4" class="commands">
                                            <a class="backbtn" href="<%=ResolveClientUrl(GetRouteUrl("admin-default",null)) %>">&laquo;&nbsp;&nbsp;پنل مدیریتی</a>
                                            <asp:Button ID="btnSearch" CssClass="large" runat="server" Text="تولید نمودار" onclick="btnSearch_Click" CausesValidation="true" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset class="chartPanel">
                                <table class="infowrapper">
                                    <tr>
                                        <td class="caption" id="info1">بر روی یکی از نقاط کلیک نمایید</td>
                                    </tr>
                                </table>
                                <div id="chart1">
                                </div>                            
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </div>        
        <kfk:Loading runat="server" ID="loading" />
        <kfk:UserMessageViewer runat="server" ID="umv"></kfk:UserMessageViewer>
    </form>
</body>
</html>
