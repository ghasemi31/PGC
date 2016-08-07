using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using kFrameWork.Model;
using pgc.Model.Enums;

public partial class Pages_Admin_DynPage_Detail : BaseDetailControl<DynPage>
{
    public override DynPage GetEntity(DynPage Data, ManagementPageMode Mode)
    {

        if (Data == null)
            Data = new DynPage();
       
        Data.Title = txtTitle.Text;
        Data.UrlKey = txtUrlKey.Text.Trim();
        Data.MetaKeyWords = txtMetaKeyWords.Text.Replace("\n", ", ");
        Data.MetaDescription = txtMetaDesc.Text;
        Data.Heading = txtHeading.Text;
     
        Data.Body = txtBody.Text;
        
        return Data;

    }

    public override void SetEntity(DynPage Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtUrlKey.Text = Data.UrlKey;
        txtMetaDesc.Text = Data.MetaDescription;
        txtMetaKeyWords.Text = Data.MetaKeyWords.Replace(", ", "\n");
        txtHeading.Text = Data.Heading;
      
        txtBody.Text = Data.Body;

    }

    public override bool Validate(ManagementPageMode Mode)
    {
        string str = txtUrlKey.Text.Trim();

        if (str.Contains("/"))
        {
            UserSession.AddMessage(UserMessageKey.ValidateUrlKey);
            return false;
        }

        return base.Validate(Mode);
    }


}