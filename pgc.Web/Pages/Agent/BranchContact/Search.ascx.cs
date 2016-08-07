using kFrameWork.UI;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Agent_BranchContact_Search : BaseSearchControl<AgentBranchContactPattern>
{
    public override AgentBranchContactPattern Pattern
    {
        get
        {
            return new AgentBranchContactPattern()
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