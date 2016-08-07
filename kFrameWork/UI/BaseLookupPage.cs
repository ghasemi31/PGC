using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kFrameWork.UI;
using System.Web.UI.WebControls;
using System.Web.UI;


public class BaseLookupPage:BasePage
{
	public BaseLookupPage()
	{
		
	}

    protected virtual void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            long ID = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "ID"));
            e.Row.Attributes.Add("ondblclick", "RowdblClick(this," + ID.ToString() + ");");
            e.Row.Attributes.Add("onclick", "RowClick(this," + ID.ToString() + ");");
        }
    }
}