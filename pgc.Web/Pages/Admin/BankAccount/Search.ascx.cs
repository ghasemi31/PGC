using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_BankAccount_Search : BaseSearchControl<BankAccountPattern>
{
    public override BankAccountPattern Pattern
    {
        get
        {
            return new BankAccountPattern()
            {
                Title = txtTitle.Text,
                Description=txtDescription.Text,
                Status=lkpStatus.GetSelectedValue<OfflineBankAccountStatus>()
            };
        }
        set
        {
            txtTitle.Text = value.Title;
            txtDescription.Text = value.Description;
            lkpStatus.SetSelectedValue(value.Status);
        }
    }
}