<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReturnCostChart.aspx.cs" Inherits="Pages_Admin_Chart_ReturnCostChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=this.Entity.Title %></title>

    <link id="page_favicon" href="<%#ResolveClientUrl("~/Styles/Images/favicon.ico") %>" rel="icon" type="image/x-icon" />

    <script type="text/javascript" src="<%#ResolveUrl("~/Scripts/Shared/jquery-1.7.2.min.js") %>"></script>
    
    <script type="text/javascript" src="<%#ResolveClientUrl("~/Scripts/Shared/jquery.blockUI.min.js") %>"></script>
    <script type="text/javascript" src="<%#ResolveClientUrl("~/Scripts/UserControls/Common/PersianDatePicker.js") %>"></script>
    
    <!--[if lt IE 9]><script language="javascript" type="text/javascript" src="<%#ResolveUrl("~/Scripts/Shared/jqPlot/excanvas.js") %>" ></script><![endif]-->
    <script type="text/javascript" src="<%#ResolveUrl("~/Scripts/Shared/jqPlot/jquery.jqplot.js") %>"></script>
    <script type="text/javascript" src="<%#ResolveUrl("~/Scripts/Shared/jqPlot/jqplot.enhancedLegendRenderer.js") %>"></script>
    <script type="text/javascript" src="<%#ResolveUrl("~/Scripts/Shared/jqPlot/jqplot.canvasAxisTickRenderer.js") %>"></script>
    <script type="text/javascript" src="<%#ResolveUrl("~/Scripts/Shared/jqPlot/jqplot.canvasTextRenderer.min.js") %>"></script>
    <script type="text/javascript" src="<%#ResolveUrl("~/Scripts/Shared/jqPlot/jqplot.categoryAxisRenderer.min.js") %>"></script>
    <script type="text/javascript" src="<%#ResolveUrl("~/Scripts/Shared/jqPlot/Scripts/UserControls/Common/Loading.js") %>"></script>
    <script type="text/javascript" src="<%#ResolveUrl("~/Scripts/UserControls/Common/UserMessageViewer.js") %>"></script>

    <link rel="Stylesheet" type="text/css" href="<%#ResolveUrl("~/Styles/form.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%#ResolveUrl("~/Styles/Pages/Admin/Chart/ChartPage.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%#ResolveUrl("~/Styles/UserControls/Common/PersianDatePicker.css")%>" />
    <link rel="Stylesheet" type="text/css" href="<%#ResolveUrl("~/Styles/UserControls/Common/UserMessageViewer.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%#ResolveUrl("~/Styles/Pages/Admin/Chart/jquery.jqplot.css") %>" />
    
    
</head>
<body>
    <script type="text/javascript">
        $(document).ready(function () {
                
            //1.    Create DataSets               
            var dataset = { <%=dataSetObj %> };

            //2.    Create jqPlot
            var plot1 = $.jqplot('chart1', [<%=dataSetArgument %>], {
                
                //2.0   Set Title
                title: '<%=customLabel %>',                        

                //2.1   Set DefaultSeries
                series: [<%=seriesLable %>],

                //2.2   Set Legend
                legend: {
                    show: true,
                    placement: 'outsideGrid',
                    renderer: $.jqplot.EnhancedLegendRenderer,
                },

                //2.3   Set AXES
                axes: {

                    xaxis: {

                        renderer: $.jqplot.CategoryAxisRenderer,
                        pad: 3,                        
                        labelRenderer: $.jqplot.CanvasAxisLabelRenderer,

                        ticks: [<%=tickObj %>],
                        tickRenderer: $.jqplot.CanvasAxisTickRenderer,
                        tickOptions: {
                            angle: -30,
                            fontFamily: 'Courier New',
                            fontSize: '9pt'
                        }
                    },

                    yaxis: {
                        pad: 1.1,
                        tickOptions: { formatString: "%'d ریال" },
                        labelRenderer: $.jqplot.CanvasAxisLabelRenderer
                    }
                },


                //2.4   Set Highlighter
                highlighter: {
                    show: true,
                    showMarker: true,
                    useAxesFormatters: false,                                                
                    formatString: '<span style="display:none;">%d</span> %d ریال'

                }
            });
            //3.    Bind Some Function To it

            $('.jqplot-title').css('width','')

            //Click For More Information
                $('#chart1').bind('jqplotDataClick', 
                function (ev, seriesIndex, pointIndex, data) {
                    $('#info1').html('مبلغ: '+parseInt( data.toString().split(',')[1]).toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")+' ریال  -  '+$(tickList).val().split(',')[pointIndex]);
                    //$('#info1').html('مبلغ: '+parseInt( data.toString().split(',')[1]).toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")+' ریال');
                }
            );

            //So Dont SHow Sum of All at the first
            //$($($('tr.jqplot-table-legend').last()).find('td')).first().click()
        });
    </script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Pages/Admin/Chart/AddAllBranchClick.js") %>"></script>
    <form id="form1" runat="server">
        <div id="frame">
            <div id="wrapper">
                <div id="maincontent">
                    <div id="form">
                        <div id="wrapper">
                            <input type="hidden" id="tickList" value="<%=tickLabelList %>" />
                            <fieldset class="fldSearch">
                            <legend><%=this.Entity.Title %></legend>
                                <table class="Table srchPanel">
                                    <tr>
                                        <td class="caption">از تاریخ</td>
                                        <td class="control"><kfk:PersianDatePicker ID="fromDate" runat="server" Required="true" /></td>
                                    
                                        <td class="caption">تا تاریخ</td>
                                        <td class="control"><kfk:PersianDatePicker ID="toDate" runat="server" Required="true" /></td>
                                    </tr>
                                    <tr>
                                        <td class="caption">نوع زمانبندی</td>
                                        <td class="control">
                                            <asp:DropDownList runat="server" ID="timeType">
                                                <asp:ListItem Text="روزانه" Value="1" />
                                                <asp:ListItem Text="هفتگی" Value="2" />
                                                <asp:ListItem Text="ماهانه" Value="3" />
                                            </asp:DropDownList>
                                        </td>
                                    
                                        <td class="caption" colspan="2">
                                            <asp:CheckBox Text="نمایش نمودار جمع کل" runat="server" ID="chkTotal" Checked="true" />
                                        </td>
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
