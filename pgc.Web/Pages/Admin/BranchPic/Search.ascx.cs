using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_BranchPic_Search : BaseSearchControl<BranchPicPattern>
{
    public override BranchPicPattern Pattern
    {
        get
        {
            return new BranchPicPattern()
            {
                
                Branch_ID=lkpBranch.GetSelectedValue<long>(),
                
                
            };
        }
        set
        {
            
            lkpBranch.SetSelectedValue(value.Branch_ID);

        }
    }

    public override BranchPicPattern DefaultPattern
    {
        get
        {

            if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.id))
            {
                long BranchID = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.id);
                BranchPicPattern pat = new BranchPicPattern()
                {
                    Branch_ID = BranchID
                };
                return pat;
            }

            return base.DefaultPattern;
        }
    }
}