<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SoldCostPie.aspx.cs" Inherits="Pages_Admin_Chart_SoldCostPie" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=chartTitle %></title>
    <link href="/assets/css/ControlPanel/Chart/BranchSummary.css" rel="stylesheet" />
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
        console.log(result);
        $(function () {
            var seriesOptions = [],
            names = [];

            Highcharts.setOptions({
                lang: {
                    thousandsSep: ',',
                    downloadJPEG: "jpgدانلود نمودار با فرمت ",
                    downloadPDF: "pdf دانلود نمودار با فرمت",
                    downloadPNG: "png دانلود نمودار با فرمت",
                    downloadSVG: "svg دانلود نمودار با فرمت",
                    printChart: "پرینت نمودار"
                }
            });
            function createChart() {
                $('#container').highcharts({
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false,
                        type: 'pie',
                        style: {
                            fontFamily: 'Tahoma'
                        }
                    },
                    legend: {
                        rtl: true,
                        align: 'right',
                        verticalAlign: 'top',
                        layout: 'vertical',
                        x: 0,
                        y: 100
                    },
                    title: {
                        text: '<span style="color:#B81716; font-size:13px;font-family:Tahoma"><%=chartTitle %></span>'
                    },
                    tooltip: {
                        //pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                        shared: true,
                        useHTML: true,
                        headerFormat: '<b>{point.key}</b><table>',
                        pointFormat: '<tr style="direction:rtl"><td style="text-align: left"><b style="direction:rtl;color: {series.color}">{point.y} %</b></td>' +
                            '<td style="color: {series.color};text-align: right">{series.name}: </td></tr>',
                        footerFormat: '</table>',
                        valueDecimals: 2
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            showInLegend: true,
                            dataLabels: {
                                enabled: true,
                                format: '<b>{point.name}</b>:  % {point.percentage:.1f}',
                                style: {
                                    color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                                }
                            }
                        }
                    },
                    series:[{
                        name:'درصد درآمد',
                        colorByPoint: true,
                        data: seriesOptions
                    }]
                });
            }
            var sum = 0;
            $.each(result, function (key, value) {
                sum += value.Sold;
            });
            $.each(result, function (key, value) {
                seriesOptions[key] = {
                    name: value.BranchName,
                    y: (value.Sold)/sum*100
                };
                names.push(value.BranchName);
            });
            createChart();
            console.log(seriesOptions);

        });
    })
</script>
</html>
