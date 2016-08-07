using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Agent_BranchPic_Search : BaseSearchControl<AgentBranchPicPattern>
{
    public override AgentBranchPicPattern Pattern
    {
        get
        {
            return new AgentBranchPicPattern()
            {
                FileName=txtFileName.Text,
                
                
            };
        }
        set
        {
            txtFileName.Text = value.FileName;

        }
    }

    //public override AgentBranchPicPattern DefaultPattern
    //{
    //    get
    //    {
    //            long BranchID = 
    //            BranchPicPattern pat = new BranchPicPattern()
    //            {
    //                Branch_ID = BranchID
    //            };
    //            return pat;
    //        }

    //        return base.DefaultPattern;
    //    }
    //}
}