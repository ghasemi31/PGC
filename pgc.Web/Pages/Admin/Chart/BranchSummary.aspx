<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchSummary.aspx.cs" Inherits="Pages_Admin_Chart_BranchSummary" %>

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
            <input type="hidden" id="minPrice" value='<%=minPrice %>' />
            <div id="container" style="width: 100%; height: 600px; direction: ltr;"></div>
        </div>
    </form>


</body>
<script src="/assets/global/plugins/jquery/jquery-2.2.0.min.js"></script>
<%-- <script src="/assets/global/plugins/Highcharts-4.2.5/js/highcharts.js"></script>--%>
<script src="/assets/global/plugins/Highstock-4.2.5/js/highstock.js"></script>
<script src="/assets/global/plugins/Highstock-4.2.5/js/modules/exporting.js"></script>
<%--<script src="/assets/global/plugins/Highstock-4.2.5/js/themes/grid-light.js"></script>--%>
<script src="/assets/global/plugins/Highstock-4.2.5/js/themes/grid.js"></script>

<script type="text/javascript">
    var category = [];
    $(document).ready(function () {

        var result = jQuery.parseJSON($('#json-list').val());


        var data_summary = [];
        var data_credit = [];
        var data_currentDebt = [];

        $.each(result, function (key, value) {
            if ((value.CurrentCredit != 0 || value.CurrentDebt != 0 || value.MinimumCredit != 0) && (Math.abs(value.CurrentCredit) >= parseFloat($('#minPrice').val()) || Math.abs(value.CurrentDebt) >= parseFloat($('#minPrice').val()) || Math.abs(value.MinimumCredit) >= parseFloat($('#minPrice').val()))) {
                category.push(value.Title);
                var serie = new Array(value.Title, value.CurrentCredit);
                var serie_credit = new Array(value.Title, value.MinimumCredit);
                var serie_debt = new Array(value.Title, value.CurrentDebt);
                data_summary.push(serie);
                data_credit.push(serie_credit);
                data_currentDebt.push(serie_debt);
            }

        });


        DreawChart(data_summary, data_credit, data_currentDebt);

    })

    function DreawChart(series_summary, series_credit, series_debt) {
        Highcharts.setOptions({
            lang: {
                thousandsSep: ',',
                downloadJPEG: "jpgدانلود نمودار با فرمت ",
                downloadPDF: "pdf دانلود نمودار با فرمت",
                downloadPNG: "png دانلود نمودار با فرمت",
                downloadSVG: "svg دانلود نمودار با فرمت",
                printChart:"پرینت نمودار"
            }
        });

        $('#container').highcharts({
            chart: {
                type: 'column',
                style: {
                    fontFamily: 'Tahoma'
                }
            },
            title: {

                text: '<span style="color:#B81716; font-size:13px;font-family:Tahoma"><%=chartTitle %></span>'

            },
            subtitle: {
                text: '<span style="color:#B81716; font-size:9px;font-family:Tahoma"><%=(minPrice!=0)?string.Format("در نمودار مقادیر کمتر از {0} ریال لحاظ نشده است",kFrameWork.Util.UIUtil.GetCommaSeparatedOf(minPrice)):"" %></span>'
            },
            xAxis: {
                categories: category,
                labels: {
                    rotation: 310
                }
            },
            yAxis: {
                title: {
                    text: 'ریال'
                },
                labels: {
                    format: '{value:.,0f}'
                },
            },
            credits: {
                enabled: false
            },
            series: [{
                name: 'بدهکار',
                data: series_debt,
                color: 'rgb(236, 37, 0)',
            }, {
                name: 'بستانکار',
                data: series_summary,
                color: 'rgb(144, 238, 126)',
            },
            {
                name: 'اعتبار',
                data: series_credit,
                color: 'rgb(124, 181, 236)',
            }],
            tooltip: {
                shared: true,
                useHTML: true,
                headerFormat: '<b>{point.key}</b><table>',
                pointFormat: '<tr style="direction:rtl"><td style="text-align: left"><b style="direction:rtl;color: {series.color}">{point.y} ریال</b></td>' +
                    '<td style="color: {series.color};text-align: right">{series.name}: </td></tr>',
                footerFormat: '</table>',

            },
        });
    }

</script>
</html>
