$(document).ready(function () {
    //So Dont SHow Sum of All at the first
    //$($($('tr.jqplot-table-legend').last()).find('td')).first().click()

    var allbranchitem = $('tr.jqplot-table-legend').last().clone();

    //allbranchitem = allbranchitem.wrap('<p/>').parent().html().replace('کل مجموعه', 'تمام گزینه ها');
    allbranchitem = allbranchitem.wrap('<p/>').parent().html().replace($('tr.jqplot-table-legend').last().find('td').last().text(), 'تمام گزینه ها');

    var hasTotal = $('tr.jqplot-table-legend').last().find('td').last().text().indexOf('کل مجموعه') > -1;

    $('table.jqplot-table-legend tbody').append(allbranchitem);

    $('tr.jqplot-table-legend').last().click(function () {

        var isTblHidden = $(this).attr('class').indexOf('jqplot-series-hidden') > 0;

        $(this).toggleClass('jqplot-series-hidden');
        var lastIndex = $(this).index();

        $('tr.jqplot-table-legend').each(function () {
            var indexNumber = (hasTotal) ? lastIndex - 1 : lastIndex;

            if ($(this).index() < indexNumber) {
                if ((isTblHidden && $($(this).find('td:nth-child(2)')).attr('class').indexOf('jqplot-series-hidden') > 0) ||
                    (!isTblHidden && $($(this).find('td:nth-child(2)')).attr('class').indexOf('jqplot-series-hidden') < 0)) {
                    $($(this).find('td')[0]).click();
                }
            }
        })

    })

    if (hasTotal) {
        $('tr.jqplot-table-legend').last().click();
    }
});


                
              
                