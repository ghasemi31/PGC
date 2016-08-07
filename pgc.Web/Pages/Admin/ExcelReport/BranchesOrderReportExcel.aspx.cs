using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pgc.Model;
using pgc.Model.Enums;
using kFrameWork.Util;
using pgc.Business.General;
using pgc.Model.Patterns;
using System.Collections;
using System.Data;
using kFrameWork.UI;

public partial class Pages_Admin_ExcelReport_BranchesOrderReportExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string filePath = HttpContext.Current.Server.MapPath("~/UserFiles/Report.xls");

            BranchesOrderReportPattern pattern = (BranchesOrderReportPattern)Session["BranchesOrderReportPattern"];
            BranchesOrderReportBusiness business = new BranchesOrderReportBusiness();
            IQueryable<BranchOrderDetail> order = business.RetriveOrder(pattern);
            //List<string> headerList = order.Select(m => m.BranchOrderTitle_Title).Distinct().ToList();
            var headerList = order.Select(m => new { m.BranchOrderTitle_Title, m.BranchOrderTitle.BranchOrderTitleGroup.DisplayOrder, orderTitle = m.BranchOrderTitle.DisplayOrder }).Distinct().OrderBy(m => m.DisplayOrder).ThenBy(m => m.orderTitle);
            List<Branch> branchList = order.Select(b => b.BranchOrder.Branch).Distinct().ToList();

            System.Data.DataTable table = new System.Data.DataTable("Orders");
            int i = 2;
            table.Columns.Add("محصولات مستردیزی");
            foreach (var item in branchList)
            {
                table.Columns.Add(item.Title);
                i++;
            }
            table.Columns.Add("جمع کل");

            foreach (var item in headerList)
            {
                var query = order.Where(o => o.BranchOrderTitle_Title == item.BranchOrderTitle_Title).ToList();
                var branchRowItem = new object[i];
                int j = 0;
                branchRowItem[j] = item.BranchOrderTitle_Title;
                j++;
                foreach (var branchItem in branchList)
                {

                    long count = 0;
                    foreach (var itemCount in query.Where(q => q.BranchOrder.Branch_ID == branchItem.ID))
                    {
                        count += (itemCount.Quantity != null) ? itemCount.Quantity : 0;
                    }
                    branchRowItem[j] = (count > 0) ? kFrameWork.Util.UIUtil.GetCommaSeparatedOf(count) : "-";
                    j++;
                }
                branchRowItem[j] = kFrameWork.Util.UIUtil.GetCommaSeparatedOf(query.Sum(s => s.Quantity));
                table.Rows.Add(branchRowItem);
            }


            //var SumItems = new object[i];
            //int h = 0;
            //SumItems[h] = "جمع";
            //h++;
            //foreach (var branchItem in branchList)
            //{
            //    SumItems[h] = kFrameWork.Util.UIUtil.GetCommaSeparatedOf(order.Where(o => o.BranchOrder.Branch_ID == branchItem.ID).Sum(q => q.Quantity));
            //    h++;
            //}
            //SumItems[h] = kFrameWork.Util.UIUtil.GetCommaSeparatedOf(order.ToList().Sum(q => q.Quantity));
            //table.Rows.Add(SumItems);

            DataSet dSet = new DataSet("table");
            dSet.Tables.Add(table);

            GridView gv = new GridView();
            gv.DataSource = dSet;
            gv.DataBind();


            //Excel.Application xlApp = new Excel.Application();
            //object misValue = System.Reflection.Missing.Value;
            //Excel.Workbook xlWorkBook = xlApp.Workbooks.Add(misValue);
            //Excel.Worksheet xlWorkSheet=(Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);


            //xlWorkBook.SaveAs(filePath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //xlWorkBook.Close(true, misValue, misValue);
            //xlApp.Quit();


            //Excel.Range formatRange;
            //formatRange = xlWorkSheet.get_Range("b1", "b1");
            //formatRange.Interior.Color = System.Drawing.
            //ColorTranslator.ToOle(System.Drawing.Color.Red);
            //xlWorkSheet.Cells[1, 2] = "Red";



            ExportUtil.Export(filePath, gv, true);
        }
        catch (Exception ex)
        {

        }
    }
}