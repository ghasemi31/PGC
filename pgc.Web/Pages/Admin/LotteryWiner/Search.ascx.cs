using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_LotteryWiner_Search : BaseSearchControl<LotteryWinerPattern>
{
    public override LotteryWinerPattern Pattern
    {
        get
        {
            return new LotteryWinerPattern()
            {
                Lottery_ID=lkpLottery.GetSelectedValue<long>(),
                Name=txtName.Text,
                Rank=txtRank.GetNumber<int>(),
                
            };
        }
        set
        {
            lkpLottery.SetSelectedValue(value.Lottery_ID);
            txtName.Text = value.Name;
            txtRank.SetNumber(value.Rank);
        }
    }

    public override LotteryWinerPattern DefaultPattern
    {
        get
        {

            if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.id))
            {
                long LotteryID = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.id);
                LotteryWinerPattern pat = new LotteryWinerPattern()
                {
                    Lottery_ID = LotteryID
                };
                return pat;
            }

            return base.DefaultPattern;
        }
    }
}