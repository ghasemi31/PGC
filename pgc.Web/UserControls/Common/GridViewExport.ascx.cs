using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.Util;
using kFrameWork.UI;

public partial class UserControl_GridViewExport :BaseUserControl
{
    public string GridViewID { get; set; }
    public string FileName { get; set; }
    public bool IncludeReportDateInFileName { get; set; }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (ScriptManager.GetCurrent(this.Page) != null)
        {
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(lnkExportToExcel);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(lnkExportToWord);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(lnkExportToHtml);
        }
    }


    protected void lnkExportToExcel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(FileName))
            FileName = "ExcelReport";
        if (IncludeReportDateInFileName)
            FileName += GetDateTime();
        ExportUtil.Export(FileName + ".xls", Parent.FindControl(GridViewID) as GridView, true);
    }

    protected void lnkExportToWord_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(FileName))
            FileName = "WordReport";
        if (IncludeReportDateInFileName)
            FileName += GetDateTime();
        ExportUtil.Export(FileName + ".doc", Parent.FindControl(GridViewID) as GridView, true);
    }

    protected void lnkExportToHtml_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(FileName))
            FileName = "HtmlReport";
        if (IncludeReportDateInFileName)
            FileName += GetDateTime();
        ExportUtil.Export(FileName + ".html", Parent.FindControl(GridViewID) as GridView, true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //UIUtil.AddCSSReference("~/Styles/gridviewexport.css", this, "DynamicHeader");
        //UIUtil.AddScriptReference("~/Scripts/gridviewexport.js", this, ScriptManager.GetCurrent(this.Page));
    }

    protected string GetDateTime()
    {
        string Res = "";
        Res += DateUtil.GetPersianDateShortString(DateTime.Now);
        Res = Res.Replace("/", "-");
        Res += "_";
        Res += DateTime.Now.Hour.ToString().PadLeft(2) + "-";
        Res += DateTime.Now.Minute.ToString().PadLeft(2) ;
        return Res;
    }
}