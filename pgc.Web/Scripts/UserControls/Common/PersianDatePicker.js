function SearchModeSelectorChanged(SelectorID, FromPanelID, AndPanelID, ToPanelID) {

    var Selector = document.getElementById(SelectorID);
    var FromPanel = document.getElementById(FromPanelID);
    var AndPanel = document.getElementById(AndPanelID);
    var ToPanel = document.getElementById(ToPanelID);

    var Index = Selector.selectedIndex;

    //alert(Index);

    if (Index == 0) {
        FromPanel.style.display = 'none';
        AndPanel.style.display = 'none';
        ToPanel.style.display = 'none';
    }
    if (Index == 1 || Index == 2 || Index == 3) {
        FromPanel.style.display = 'block';
        AndPanel.style.display = 'none';
        ToPanel.style.display = 'none';
    }
    else if (Index == 4) {
        FromPanel.style.display = 'block';
        AndPanel.style.display = 'block';
        ToPanel.style.display = 'block';
    }
}

var datePickerDivID = "datepicker";
var iFrameDivID = "datepickeriframe";

var dayArrayShort = new Array('&#1588;', '&#1740;', '&#1583;', '&#1587;', '&#1670;', '&#1662;', '&#1580;');
var dayArrayMed = new Array('&#1588;&#1606;&#1576;&#1607;', '&#1740;&#1705;&#1588;&#1606;&#1576;&#1607;', '&#1583;&#1608;&#1588;&#1606;&#1576;&#1607;', '&#1587;&#1607;&#32;&#1588;&#1606;&#1576;&#1607;', '&#1670;&#1607;&#1575;&#1585;&#1588;&#1606;&#1576;&#1607;', '&#1662;&#1606;&#1580;&#1588;&#1606;&#1576;&#1607;', '&#1580;&#1605;&#1593;&#1607;');
var dayArrayLong = dayArrayMed;
var monthArrayShort = new Array('&#1601;&#1585;&#1608;&#1585;&#1583;&#1740;&#1606;', '&#1575;&#1585;&#1583;&#1740;&#1576;&#1607;&#1588;&#1578;', '&#1582;&#1585;&#1583;&#1575;&#1583;', '&#1578;&#1740;&#1585;', '&#1605;&#1585;&#1583;&#1575;&#1583;', '&#1588;&#1607;&#1585;&#1740;&#1608;&#1585;', '&#1605;&#1607;&#1585;', '&#1570;&#1576;&#1575;&#1606;', '&#1570;&#1584;&#1585;', '&#1583;&#1740;', '&#1576;&#1607;&#1605;&#1606;', '&#1575;&#1587;&#1601;&#1606;&#1583;');
var monthArrayMed = monthArrayShort;
var monthArrayLong = monthArrayShort;

var defaultDateSeparator = "/";
var defaultDateFormat = "ymd"
var dateSeparator = defaultDateSeparator;
var dateFormat = defaultDateFormat;

function displayDatePicker(dateFieldName, displayBelowThisObject, dtFormat, dtSep) {

    var targetDateField = document.getElementById(dateFieldName);
    targetDateField.select();

    if (!displayBelowThisObject)
        displayBelowThisObject = targetDateField;

    if (dtSep)
        dateSeparator = dtSep;
    else
        dateSeparator = defaultDateSeparator;

    if (dtFormat)
        dateFormat = dtFormat;
    else
        dateFormat = defaultDateFormat;

    var x = displayBelowThisObject.offsetLeft - 132;
    var y = displayBelowThisObject.offsetTop + displayBelowThisObject.offsetHeight - 20;

    var parent = displayBelowThisObject;
    while (parent.offsetParent) {
        parent = parent.offsetParent;
        x += parent.offsetLeft;
        y += parent.offsetTop;
    }

    drawDatePicker(targetDateField, x, y);


}

function drawDatePicker(targetDateField, x, y) {
    var dt = getFieldDate(targetDateField.value);

    if (!document.getElementById(datePickerDivID)) {
        var newNode = document.createElement("div");
        newNode.setAttribute("id", datePickerDivID);
        newNode.setAttribute("class", "dpDiv");
        newNode.setAttribute("style", "visibility: hidden;");
        document.body.appendChild(newNode);
    }

    var pickerDiv = document.getElementById(datePickerDivID);
    pickerDiv.style.position = "absolute";
    pickerDiv.style.left = x + "px";
    pickerDiv.style.top = y + "px";
    pickerDiv.style.visibility = (pickerDiv.style.visibility == "visible" ? "hidden" : "visible");
    pickerDiv.style.display = (pickerDiv.style.display == "block" ? "none" : "block");

    pickerDiv.style.zIndex = 10000000;

    refreshDatePicker(targetDateField.name, dt[0], dt[1], dt[2]);
}


function refreshDatePicker(dateFieldName, year, month, day) {
    var thisDay = getTodayPersian();
    var weekday = (thisDay[3] - thisDay[2] + 1) % 7;
    if (!day)
        day = 1;
    if ((month >= 1) && (year > 0)) {
        thisDay = calcPersian(year, month, 1);
        weekday = thisDay[3];
        thisDay = new Array(year, month, day, weekday);
        thisDay[2] = 1;
    } else {
        day = thisDay[2];
        thisDay[2] = 1;
    }

    var crlf = "\r\n";
    var TABLE = "<table cols='7' class='dpTable'  cellspacing='2px' cellpadding='2px'>" + crlf;
    var xTABLE = "</table>" + crlf;
    var TR = "<tr class='dpTR'>";
    var TR_title = "<tr>";
    var TR_days = "<tr class='dpDayTR'>";
    var TR_todaybutton = "<tr class='dpTodayButtonTR'>";
    var xTR = "</tr>" + crlf;
    var TD = "<td class='dpTD' onMouseOut='this.className=\"dpTD\";' onMouseOver=' this.className=\"dpTDHover\";' ";    // leave this tag open, because we'll be adding an onClick event
    var TD_Blank = "<td class='dpTD_Blank' onMouseOut='this.className=\"dpTD_Blank\";' onMouseOver=' this.className=\"dpTD_Blank\";' ";    // leave this tag open, because we'll be adding an onClick event
    var TD_title = "<td colspan=5 class='dpTitleTD'>";
    var TD_buttons = "<td class='dpButtonTD' width='10%'>";
    var TD_todaybutton = "<td colspan=7 class='dpTodayButtonTD'><hr/>";
    var TD_days = "<td class='dpDayTD'>";
    var TD_selected = "<td class='dpDayHighlightTD' onMouseOut='this.className=\"dpDayHighlightTD\";' onMouseOver='this.className=\"dpTDHover\";' ";    // leave this tag open, because we'll be adding an onClick event
    var xTD = "</td>" + crlf;
    var DIV_title = "<div class='dpTitleText'>";
    var DIV_selected = "<div class='dpDayHighlight'>";
    var xDIV = "</div>";

    var html = TABLE;

    var today = getTodayPersian();
    html += '<tr class="dpTitleBarTR"><td colspan="7" valign="top">';
    html += '<div class="dptbCloseButton" ' + 'onClick="updateDateField(\'' + dateFieldName + '\');"' + ' title="بستن"></div>';
    html += '<div class="dptbResetButton" ' + 'onClick="resetDataField(\'' + dateFieldName + '\');"' + ' title="خالی کردن"></div>';
    html += '<div class="dptbTodayButton" ' + 'onClick="refreshDatePicker(\'' + dateFieldName + '\', ' + today[0] + ', ' + today[1] + ', ' + today[2] + ');"' + ' >&#1575;&#1605;&#1585;&#1608;&#1586;</div>';
    html += '</td></tr>'


    html += "<tr class='dpTitleTR'><td colspan='7' valign='top'>"
    html += '<div class="YearPrev" title="سال قبل" ' + getButtonCodeYearPrev(dateFieldName, thisDay, -1, "") + '><div class="dpBtnYearPrev" ></div></div>';
    html += '<div class="Prev" title="ماه قبل" ' + getButtonCodePrev(dateFieldName, thisDay, -1, "") + '><div class="dpBtnPrev" ></div></div>';
    html += '<div class="dpTitleText">' + monthArrayLong[thisDay[1] - 1] + '&nbsp;' + thisDay[0] + '</div>';
    html += '<div class="Next" title="ماه بعد" ' + getButtonCodeNext(dateFieldName, thisDay, 1, "") + ' ><div class="dpBtnNext"></div></div>';
    html += '<div class="YearNext" title="سال بعد" ' + getButtonCodeYearNext(dateFieldName, thisDay, 1, "") + '><div class="dpBtnYearNext" ></div></div>';
    html += "</td></tr>"


    html += TR_days;
    var i;
    for (i = 0; i < dayArrayShort.length; i++)
        html += TD_days + dayArrayShort[i] + xTD;
    html += xTR;
    html += TR;
    if (weekday != 6)
        for (i = 0; i <= weekday; i++)
            html += TD_Blank + "&nbsp;" + xTD;

    var len = 31;
    if (thisDay[1] > 6)
        len = 30;
    if (thisDay[1] == 12 && !leap_persian(thisDay[0]))
        len = 29;

    for (var dayNum = thisDay[2]; dayNum <= len; dayNum++) {
        TD_onclick = " onclick=\"updateDateField('" + dateFieldName + "', '" + getDateString(thisDay) + "');\">";

        if (dayNum == day)
            html += TD_selected + TD_onclick + DIV_selected + dayNum + xDIV + xTD;
        else
            html += TD + TD_onclick + dayNum + xTD;

        if (weekday == 5)
            html += xTR + TR;
        weekday++;
        weekday = weekday % 7;

        thisDay[2]++;
    }

    if (weekday > 0) {
        for (i = 6; i > weekday; i--)
            html += TD_Blank + "&nbsp;" + xTD;
    }
    html += xTR;



    html += xTABLE;

    document.getElementById(datePickerDivID).innerHTML = html;
    adjustiFrame();
}


function getButtonCodeNext(dateFieldName, dateVal, adjust, label) {
    var newMonth = (dateVal[1] + adjust) % 12;
    var newYear = dateVal[0] + parseInt((dateVal[1] + adjust) / 12);
    if (newMonth < 1) {
        newMonth += 12;
        newYear += -1;
    }
    return 'onClick="refreshDatePicker(\'' + dateFieldName + '\', ' + newYear + ', ' + newMonth + ');"';
}

function getButtonCodePrev(dateFieldName, dateVal, adjust, label) {
    var newMonth = (dateVal[1] + adjust) % 12;
    var newYear = dateVal[0] + parseInt((dateVal[1] + adjust) / 12);
    if (newMonth < 1) {
        newMonth += 12;
        newYear += -1;
    }
    return 'onClick="refreshDatePicker(\'' + dateFieldName + '\', ' + newYear + ', ' + newMonth + ');"';
}

function getButtonCodeYearPrev(dateFieldName, dateVal, adjust, label) {
    var newMonth = dateVal[1];
    var newYear = (dateVal[0] + adjust);

    return 'onClick = "refreshDatePicker(\'' + dateFieldName + '\', ' + newYear + ', ' + newMonth + ');"';
}

function getButtonCodeYearNext(dateFieldName, dateVal, adjust, label) {
    var newMonth = dateVal[1];
    var newYear = (dateVal[0] + adjust);

    return 'onClick = "refreshDatePicker(\'' + dateFieldName + '\', ' + newYear + ', ' + newMonth + ');"';
}
function getDateString(dateVal) {
    var dayString = "00" + dateVal[2];
    var monthString = "00" + (dateVal[1]);
    dayString = dayString.substring(dayString.length - 2);
    monthString = monthString.substring(monthString.length - 2);

    switch (dateFormat) {
        case "dmy":
            return dayString + dateSeparator + monthString + dateSeparator + dateVal[0];
        case "ymd":
            return dateVal[0] + dateSeparator + monthString + dateSeparator + dayString;
        case "mdy":
        default:
            return monthString + dateSeparator + dayString + dateSeparator + dateVal[0];
    }
}


function getFieldDate(dateString) {
    var dateVal;
    var dArray;
    var d, m, y;

    try {
        dArray = splitDateString(dateString);
        if (dArray) {
            switch (dateFormat) {
                case "dmy":
                    d = parseInt(dArray[0], 10);
                    m = parseInt(dArray[1], 10);
                    y = parseInt(dArray[2], 10);
                    break;
                case "ymd":
                    d = parseInt(dArray[2], 10);
                    m = parseInt(dArray[1], 10);
                    y = parseInt(dArray[0], 10);
                    break;
                case "mdy":
                default:
                    d = parseInt(dArray[1], 10);
                    m = parseInt(dArray[0], 10);
                    y = parseInt(dArray[2], 10);
                    break;
            }
            dateVal = new Array(y, m, d);
        } else if (dateString) {
            dateVal = getTodayPersian();
        } else {
            dateVal = getTodayPersian();
        }
    } catch (e) {
        dateVal = getTodayPersian();
    }

    return dateVal;
}


function splitDateString(dateString) {
    var dArray;
    if (dateString.indexOf("/") >= 0)
        dArray = dateString.split("/");
    else if (dateString.indexOf(".") >= 0)
        dArray = dateString.split(".");
    else if (dateString.indexOf("-") >= 0)
        dArray = dateString.split("-");
    else if (dateString.indexOf("\\") >= 0)
        dArray = dateString.split("\\");
    else
        dArray = false;

    return dArray;
}

function resetDataField(dateFieldName) {
    var targetDateField = document.getElementsByName(dateFieldName).item(0);
    targetDateField.value = '';

    var pickerDiv = document.getElementById(datePickerDivID);
    pickerDiv.style.visibility = "hidden";
    pickerDiv.style.display = "none";

    adjustiFrame();
}

function closeDP() {
    var pickerDiv = document.getElementById(datePickerDivID);
    pickerDiv.style.visibility = "hidden";
    pickerDiv.style.display = "none";

    adjustiFrame();
}

function updateDateField(dateFieldName, dateString) {
    var targetDateField = document.getElementsByName(dateFieldName).item(0);
    if (dateString)
        targetDateField.value = dateString;

    var pickerDiv = document.getElementById(datePickerDivID);
    pickerDiv.style.visibility = "hidden";
    pickerDiv.style.display = "none";

    adjustiFrame();

    //  if (dateString)
    //    targetDateField.focus();

    if ((dateString) && (typeof (datePickerClosed) == "function"))
        datePickerClosed(targetDateField);
}


function adjustiFrame(pickerDiv, iFrameDiv) {
    var is_opera = (navigator.userAgent.toLowerCase().indexOf("opera") != -1);
    if (is_opera)
        return;
    try {
        if (!document.getElementById(iFrameDivID)) {
            var newNode = document.createElement("iFrame");
            newNode.setAttribute("id", iFrameDivID);
            newNode.setAttribute("src", "javascript:false;");
            newNode.setAttribute("scrolling", "no");
            newNode.setAttribute("frameborder", "0");
            document.body.appendChild(newNode);
        }

        if (!pickerDiv)
            pickerDiv = document.getElementById(datePickerDivID);
        if (!iFrameDiv)
            iFrameDiv = document.getElementById(iFrameDivID);

        try {
            iFrameDiv.style.position = "absolute";
            iFrameDiv.style.width = pickerDiv.offsetWidth;
            iFrameDiv.style.height = pickerDiv.offsetHeight;
            iFrameDiv.style.top = pickerDiv.style.top;
            iFrameDiv.style.left = pickerDiv.style.left;
            iFrameDiv.style.zIndex = pickerDiv.style.zIndex - 1;
            iFrameDiv.style.visibility = pickerDiv.style.visibility;
            iFrameDiv.style.display = pickerDiv.style.display;
        } catch (e) {
        }

    } catch (ee) {
    }

}

function mod(a, b) {
    return a - (b * Math.floor(a / b));
}

function jwday(j) {
    return mod(Math.floor((j + 1.5)), 7);
}

var Weekdays = new Array("Sunday", "Monday", "Tuesday", "Wednesday",
                          "Thursday", "Friday", "Saturday");

function leap_gregorian(year) {
    return ((year % 4) == 0) &&
            (!(((year % 100) == 0) && ((year % 400) != 0)));
}

var GREGORIAN_EPOCH = 1721425.5;

function gregorian_to_jd(year, month, day) {
    return (GREGORIAN_EPOCH - 1) +
           (365 * (year - 1)) +
           Math.floor((year - 1) / 4) +
           (-Math.floor((year - 1) / 100)) +
           Math.floor((year - 1) / 400) +
           Math.floor((((367 * month) - 362) / 12) +
           ((month <= 2) ? 0 :
                               (leap_gregorian(year) ? -1 : -2)
           ) +
           day);
}

function jd_to_gregorian(jd) {
    var wjd, depoch, quadricent, dqc, cent, dcent, quad, dquad,
        yindex, dyindex, year, yearday, leapadj;

    wjd = Math.floor(jd - 0.5) + 0.5;
    depoch = wjd - GREGORIAN_EPOCH;
    quadricent = Math.floor(depoch / 146097);
    dqc = mod(depoch, 146097);
    cent = Math.floor(dqc / 36524);
    dcent = mod(dqc, 36524);
    quad = Math.floor(dcent / 1461);
    dquad = mod(dcent, 1461);
    yindex = Math.floor(dquad / 365);
    year = (quadricent * 400) + (cent * 100) + (quad * 4) + yindex;
    if (!((cent == 4) || (yindex == 4))) {
        year++;
    }
    yearday = wjd - gregorian_to_jd(year, 1, 1);
    leapadj = ((wjd < gregorian_to_jd(year, 3, 1)) ? 0
                                                  :
                  (leap_gregorian(year) ? 1 : 2)
              );
    month = Math.floor((((yearday + leapadj) * 12) + 373) / 367);
    day = (wjd - gregorian_to_jd(year, month, 1)) + 1;

    return new Array(year, month, day);
}

function leap_persian(year) {
    return ((((((year - ((year > 0) ? 474 : 473)) % 2820) + 474) + 38) * 682) % 2816) < 682;
}

var PERSIAN_EPOCH = 1948320.5;
var PERSIAN_WEEKDAYS = new Array("í˜ÔäÈå", "ÏæÔäÈå",
                                 "Óå ÔäÈå", "åÇÑÔäÈå",
                                 "äÌ ÔäÈå", "ÌãÚå", "ÔäÈå");

function persian_to_jd(year, month, day) {
    var epbase, epyear;

    epbase = year - ((year >= 0) ? 474 : 473);
    epyear = 474 + mod(epbase, 2820);

    return day +
            ((month <= 7) ?
                ((month - 1) * 31) :
                (((month - 1) * 30) + 6)
            ) +
            Math.floor(((epyear * 682) - 110) / 2816) +
            (epyear - 1) * 365 +
            Math.floor(epbase / 2820) * 1029983 +
            (PERSIAN_EPOCH - 1);
}

function jd_to_persian(jd) {
    var year, month, day, depoch, cycle, cyear, ycycle,
        aux1, aux2, yday;


    jd = Math.floor(jd) + 0.5;

    depoch = jd - persian_to_jd(475, 1, 1);
    cycle = Math.floor(depoch / 1029983);
    cyear = mod(depoch, 1029983);
    if (cyear == 1029982) {
        ycycle = 2820;
    } else {
        aux1 = Math.floor(cyear / 366);
        aux2 = mod(cyear, 366);
        ycycle = Math.floor(((2134 * aux1) + (2816 * aux2) + 2815) / 1028522) +
                    aux1 + 1;
    }
    year = ycycle + (2820 * cycle) + 474;
    if (year <= 0) {
        year--;
    }
    yday = (jd - persian_to_jd(year, 1, 1)) + 1;
    month = (yday <= 186) ? Math.ceil(yday / 31) : Math.ceil((yday - 6) / 30);
    day = (jd - persian_to_jd(year, month, 1)) + 1;
    return new Array(year, month, day);
}

function calcPersian(year, month, day) {
    var date, j;

    j = persian_to_jd(year, month, day);
    date = jd_to_gregorian(j);
    weekday = jwday(j);
    return new Array(date[0], date[1], date[2], weekday);
}

function calcGregorian(year, month, day) {
    month--;

    var j, weekday;

    j = gregorian_to_jd(year, month + 1, day) +
           (Math.floor(0 + 60 * (0 + 60 * 0) + 0.5) / 86400.0);

    perscal = jd_to_persian(j);
    weekday = jwday(j);
    return new Array(perscal[0], perscal[1], perscal[2], weekday);
}

function getTodayGregorian() {
    var t = new Date();
    var today = new Date();

    var y = today.getYear();
    if (y < 1000) {
        y += 1900;
    }

    return new Array(y, today.getMonth() + 1, today.getDate(), t.getDay());
}

function getTodayPersian() {
    var t = new Date();
    var today = getTodayGregorian();

    var persian = calcGregorian(today[0], today[1], today[2]);
    return new Array(persian[0], persian[1], persian[2], t.getDay());
}


function lockkeypress(e) {
    var key = (window.event) ? event.keyCode : e.which;
    if (key == 0) return;
    if (window.event) { window.event.returnValue = null; e.preventDefault(); }
    else { e.preventDefault(); }
}