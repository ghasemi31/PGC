using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_BranchRequest_Search : BaseSearchControl<BranchRequestPattern>
{
    public override BranchRequestPattern Pattern
    {
        get
        {
            return new BranchRequestPattern()
            {
                
                
 
                Name = txtName.Text,
                Contact=txtContact.Text,
                Description=txtDesc.Text,
                Status=lkcStatus.GetSelectedValue<UserCommentStatus>(),
                BRPersianDate=pdrBRPersianDate.DateRange

            };
        }
        set
        {
            

            txtName.Text = value.Name;
            txtDesc.Text = value.Description;
            txtContact.Text = value.Contact;
            lkcStatus.SetSelectedValue(value.Status);
            pdrBRPersianDate.DateRange = value.BRPersianDate;

        }
    }

    public override BranchRequestPattern DefaultPattern
    {
        get
        {
            BranchRequestPattern p = new BranchRequestPattern();
            p.Status = UserCommentStatus.UnRead;
            p.BRPersianDate = pdrBRPersianDate.DateRange;
            return p; 


            //return base.DefaultPattern;
            
        }
    }
}