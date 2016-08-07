<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllProductSoldChart.aspx.cs" Inherits="Pages_Admin_Chart_AllProductSoldChart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=chartTitle %></title>
    <link href="/assets/css/ControlPanel/Chart/BranchSummary.css" rel="stylesheet" />
    <script src="/assets/js/pace/pace.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="hidden" id="json-list" value='<%=jsonList %>' />
            <div id="container" style="height: 600px; min-width: 100%; direction: ltr"></div>
        </div>
    </form>
</body>
<script src="/assets/global/plugins/jquery/jquery-2.2.0.min.js"></script>
<script src="/assets/global/plugins/Highstock-4.2.5/js/highstock.js"></script>
<script src="/assets/global/plugins/Highstock-4.2.5/js/modules/exporting.js"></script>
<script src="/assets/global/plugins/Highstock-4.2.5/js/themes/grid.js"></script>
<script src="/assets/global/plugins/PersianDate/PersianDate.js"></script>
<script type="text/javascript">

    $(document).ready(function () {
        var result = jQuery.parseJSON($('#json-list').val());
        var data = [];
        var category = [];
        $.each(result, function (key, value) {
            var dateStr = value.Date;
            //var date = new Date(parseInt(dateStr.replace(/\/Date\((.*?)\)\//gi, "$1")));
            var date2 = dateStr.replace(/\/Date\((.*?)\)\//gi, "$1");
            var serie = new Array(parseFloat(date2), value.ProductSold);
            category.push(value.Date);
            data.push(serie);
        });

        $(function () {
            Highcharts.dateFormats = {
                'a': function (ts) {
                    return new persianDate(ts).format('dddd')
                },
                'A': function (ts) {
                    return new persianDate(ts).format('dddd')
                },
                'd': function (ts) {
                    return new persianDate(ts).format('DD')
                },
                'e': function (ts) {
                    return new persianDate(ts).format('D')
                },
                'b': function (ts) {
                    return new persianDate(ts).format('MMMM')
                },
                'B': function (ts) {
                    return new persianDate(ts).format('MMMM')
                },
                'm': function (ts) {
                    return new persianDate(ts).format('MM')
                },
                'y': function (ts) {
                    return new persianDate(ts).format('YY')
                },
                'Y': function (ts) {
                    return new persianDate(ts).format('YYYY')
                },
                'W': function (ts) {
                    return new persianDate(ts).format('ww')
                }
            }
            Highcharts.setOptions({
                lang: {
                    thousandsSep: ',',
                    downloadJPEG: "jpgدانلود نمودار با فرمت ",
                    downloadPDF: "pdf دانلود نمودار با فرمت",
                    downloadPNG: "png دانلود نمودار با فرمت",
                    downloadSVG: "svg دانلود نمودار با فرمت",
                    printChart: "پرینت نمودار",
                    rangeSelectorZoom: 'بازه های نمایش',
                    rangeSelectorFrom: 'از',
                    rangeSelectorTo: 'تا'
                }
            });
            $('#container').highcharts('StockChart', {
                chart: {
                    type: 'line',
                    style: {
                        fontFamily: 'Tahoma'
                    }
                },
                rangeSelector:{
                    buttons: [{
                        type: 'month',
                        count: 1,
                        text: 'یک ماه'
                    }, {
                        type: 'month',
                        count: 3,
                        text: 'سه ماه'
                    }, {
                        type: 'month',
                        count: 6,
                        text: 'شش ماه'
                    },  {
                        type: 'year',
                        count: 1,
                        text: 'یک سال'
                    }, {
                        type: 'all',
                        text: 'همه'
                    }],
                    inputBoxWidth: 150,
                    buttonTheme: { 
                        width: 60
                    },
                },
                //rangeSelector: {
                //    allButtonsEnabled: true,
                //    buttons: [{
                //        type: 'month',
                //        count: 3,
                //        text: 'روزانه',
                //        dataGrouping: {
                //            forced: true,
                //            units: [['day', [1]]]
                //        }
                //    }, {
                //        type: 'month',
                //        count: 6,
                //        text: 'هفتگی',
                //        dataGrouping: {
                //            forced: true,
                //            units: [['week', [1]]]
                //        }
                //    }, {
                //        type: 'year',
                //        count: 2,
                //        text: 'ماهیانه',
                //        dataGrouping: {
                //            forced: true,
                //            units: [['month', [1]]]
                //        }
                //    }],
                //    buttonTheme: {
                //        width: 60
                //    },
                //    selected:0
                //},


                title: {
                    text: '<span style="color:#B81716; font-size:13px;font-family:Tahoma"><%=chartTitle %></span>'
                },
                xAxis: {
                    type: 'datetime',
                    labels: {
                        formatter: function () {
                            return Highcharts.dateFormat('%a  %y/%m/%d', this.value);
                        },
                        rotation: 310
                    },
                    
                },
                yAxis: {
                    title: {
                        text: 'ریال'
                    },
                    labels: {
                        format: '{value:.,0f}'
                    },
                },
                tooltip: {
                    shared: true,
                    useHTML: true,
                    xDateFormat: '%a  %y/%m/%d',
                    headerFormat: '<b>{point.key}</b><table>',
                    pointFormat: '<tr style="direction:rtl"><td style="text-align: left"><b style="direction:rtl;color: {series.color}">{point.y} ریال</b></td>' +
                        '<td style="color: {series.color};text-align: right">{series.name}: </td></tr>',
                    footerFormat: '</table>',
                    valueDecimals: 0

                },
                navigator: {
                    series: {
                        type: 'areaspline',
                        color: '#4572A7',
                        fillOpacity: 0.05,
                        dataGrouping: {
                            smoothed: false
                        },
                        lineWidth: 1,

                    }
                },
                series: [{
                    name: 'فروش',
                    data: data,
                    marker: {
                        enabled: true,
                        radius: 3
                    },
                }]
            });

        });
        
    })
    window.onload(function () {
        alert('test');
    });

</script>
</html>
