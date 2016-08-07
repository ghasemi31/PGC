using kFrameWork.UI;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_BranchContact_Search : BaseSearchControl<BranchContactPattern>
{
    public override BranchContactPattern Pattern
    {
        get
        {
            return new BranchContactPattern()
            {
                Name = txtName.Text,
                PersianDate = pdrUCPersianDate.DateRange,

            };
        }
        set
        {
            txtName.Text = value.Name;
            pdrUCPersianDate.DateRange = value.PersianDate;

        }
    }
}