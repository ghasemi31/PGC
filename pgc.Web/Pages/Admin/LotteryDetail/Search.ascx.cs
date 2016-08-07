using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_LotteryDetail_Search : BaseSearchControl<LotteryDetailPattern>
{
    public override LotteryDetailPattern Pattern
    {
        get
        {
            return new LotteryDetailPattern()
            {
                Lottery_ID=lkpLottery.GetSelectedValue<long>(),
                Name=txtName.Text,
                Code=txtCode.GetNumber<int>(),
                
            };
        }
        set
        {
            lkpLottery.SetSelectedValue(value.Lottery_ID);
            txtName.Text = value.Name;
            txtCode.SetNumber(value.Code);
        }
    }

    public override LotteryDetailPattern DefaultPattern
    {
        get
        {

            if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.id))
            {
                long LotteryID = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.id);
                LotteryDetailPattern pat = new LotteryDetailPattern()
                {
                    Lottery_ID = LotteryID
                };
                return pat;
            }

            return base.DefaultPattern;
        }
    }
}