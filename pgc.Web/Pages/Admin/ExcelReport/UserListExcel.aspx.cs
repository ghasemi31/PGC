using kFrameWork.Util;
using pgc.Business;
using pgc.Model;
using pgc.Model.Enums;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_ExcelReport_UserListExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string filePath = HttpContext.Current.Server.MapPath("~/UserFiles/Report.xls");

            UserPattern pattern = (UserPattern)Session["UserListPattern"];
            UserBusiness business = new UserBusiness();
            IQueryable<User> OrdersList = business.Search_SelectPrint(pattern);


            DataTable table = new DataTable("Orders");

            table.Columns.Add("ردیف");
            table.Columns.Add("نام و نام خانوادگی");
            table.Columns.Add("نام پدر");
            table.Columns.Add("نقش");
            table.Columns.Add("وضعیت");
            table.Columns.Add("پست الکترونیک");
            table.Columns.Add("شماره تماس");
            table.Columns.Add("تلفن همراه");
            table.Columns.Add("کد ملی");           
            table.Columns.Add("کد پستی");
            table.Columns.Add("آدرس");




            int i = 0;
            foreach (var user in OrdersList.OrderByDescending(f => f.ID))
            {
                i++;
                table.Rows.Add(
                    i,
                    user.FullName,
                    user.FatherName,
                    EnumUtil.GetEnumElementPersianTitle((Role)user.AccessLevel.Role),
                    EnumUtil.GetEnumElementPersianTitle((UserActivityStatus)user.ActivityStatus),
                    user.Email,         
                    user.Tel,
                    user.Mobile,
                    user.NationalCode,
                    user.PostalCode,
                    user.Address
                    );
            }


            DataSet dSet = new DataSet("table");
            dSet.Tables.Add(table);

            GridView gv = new GridView();
            gv.DataSource = dSet;
            gv.DataBind();

            ExportUtil.Export(filePath, gv, true);
        }
        catch (Exception ex)
        {

        }
    }
}