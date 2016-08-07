using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_BranchOrderShipmentState_Search : BaseSearchControl<BranchOrderShipmentStatePattern>
{
    public override BranchOrderShipmentStatePattern Pattern
    {
        get
        {
            return new BranchOrderShipmentStatePattern()
            {
                ID= lkpState.GetSelectedValue<long>()
            };
        }
        set
        {
            lkpState.SetSelectedValue(value.ID);
        }
    }
}