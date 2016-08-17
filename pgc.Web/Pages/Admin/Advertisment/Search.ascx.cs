using kFrameWork.UI;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Advertisment_Search : BaseSearchControl<AdvertismentPattern>
{
    public override AdvertismentPattern Pattern
    {
        get
        {
            return new AdvertismentPattern()
            {
                Title = txtTitle.Text,

            };
        }
        set
        {
            txtTitle.Text = value.Title;

        }
    }
}