using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_Lottery_Search : BaseSearchControl<LotteryPattern>
{
    public override LotteryPattern Pattern
    {
        get
        {
            return new LotteryPattern()
            {
                Title = txtDesc.Text,
                Status = lkcStatus.GetSelectedValue<LotteryStatus>(),
                RegPersianDate = pdrRegPersianDate.DateRange

            };
        }
        set
        {

            txtDesc.Text = value.Title;
            lkcStatus.SetSelectedValue(value.Status);
            pdrRegPersianDate.DateRange = value.RegPersianDate;

        }
    }
}