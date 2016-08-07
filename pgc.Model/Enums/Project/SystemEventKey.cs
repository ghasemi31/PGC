using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    public enum SystemEventKey
    {
        //Guest
        Email_Manual_Sending,
        SMS_Manual_Sending,
        BranchRequest_New,
        UserComment_New,

        //User
        User_New,
        Password_Change,
        Login,
        Order_New,
        Comment,
        Branch_Contact,
        EmailAndPassConfirmation,


        //Agent
        Demand_Action,
        BranchPage_Edit,
        Order_Remove_Agent,
        Order_Change_Branch,
        Order_Change_Status,
        BranchOnlineOrderChangeTime,
        BranchContact_ReadMessage,

        BranchOrder_EditedBeforeConfirm,
        //Admin

        Order_Remove_Admin,
        Order_Change_Branch_Admin,
        Order_Change_Status_Admin,

        User_Action_Admin,
        User_RstPassword_Admin,

        UserComment_Change_Branch,

        Product_Action,

        Demand_Remove_Admin,
        Demand_New_Admin,
        Demand_Change_Admin,

        News_Action,

        BranchRequest_Remove_Admin,
        BranchRequest_Change_Admin,

        Branch_New_Admin,
        Branch_Remove_Admin,
        Branch_Change_Admin,

        Advertisement_Action,
        Lottery_Action,
        Lottery_Register,
        Poll_Register,
        Profile_Change,
        BranchPic_Action_Agent,
        Help_Action,
        DemandTitle_Action,

        OnlinePayment_New,
        OnlinePayment_Verify,
        OnlinePayment_Reverse,

        BranchOrder_EditedBeforeConfirmByCentralBranch,
        //Branch Actiovation Business
        Branch_Activation,
        Branch_DeActivation,


        //Finance Cycle
        BranchOrder_Cancelation,
        BranchOrder_Confirm,
        BranchOrderTitle_Action,
        BranchPayment_Online_Verify,
        BranchPayment_Offline_Action,
        BranchPayment_Offline_Confirm,
        BranchPayment_Offline_UnConfirm,
        BranchReturnOrder_Confirm,
        BranchReturnOrder_Cancelation,
        BranchReturnOrder_RollBackCancle,
        BranchReturnOrder_Insert,
        BranchMinimumCredit_Change,
        BranchCurrentCredit_Change,
        BranchTransaction_CustomerOnline_Failed,
        BranchReturnOrder_Finalize,
        BranchReturnOrder_RollBackFinalized,
        BranchReturnOrder_AdminDelete,
        BranchReturnOrder_AdminEdit,
        BranchReturnOrder_AgentDelete,
        BranchOrder_AdminFinalize,
        BranchOrder_RollBackCancle,
        BranchOrder_AdminEdit,
        BranchOrder_AdminUpdateShipment,
        BranchOrder_AdminRollBackFinalized,
        BranchOrder_AdminDelete,
        BranchOrder_Insert,
        BranchOrder_AgentDelete,
        BranchLackOrder_Confirm,
        BranchLackOrder_AdminCancelation,
        BranchLackOrder_AdminRollBackCancle,
        BranchLackOrder_AdminEdit,
        BranchLackOrder_AdminDelete,
        BranchLackOrder_AgentInsert,
        BranchLackOrder_AgentDelete,



    }
}
