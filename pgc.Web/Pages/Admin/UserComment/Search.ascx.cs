using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_UserComment_Search : BaseSearchControl<UserCommentPattern>
{
    public override UserCommentPattern Pattern
    {
        get
        {
            return new UserCommentPattern()
            {
                Name = txtName.Text,
                Type=lkcType.GetSelectedValue<UserCommentType>(),
                Status=lkcStatus.GetSelectedValue<UserCommentStatus>(),
                UCPersianDate=pdrUCPersianDate.DateRange,
                Branch_ID = lkcBranch.GetSelectedValue<long>(),
                BranchTitle=txtBranchTitle.Text
 
            };
        }
        set
        {
            txtName.Text = value.Name;
            lkcStatus.SetSelectedValue(value.Status);
            lkcType.SetSelectedValue(value.Type);
            pdrUCPersianDate.DateRange = value.UCPersianDate;
            lkcBranch.SetSelectedValue(value.Branch_ID);
            txtBranchTitle.Text = value.BranchTitle;
        }
    }
}