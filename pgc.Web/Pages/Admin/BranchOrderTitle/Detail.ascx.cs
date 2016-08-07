using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;
using pgc.Business.General;

public partial class Pages_Admin_BranchOrderTitle_Detail : BaseDetailControl<BranchOrderTitle>
{
    private BranchBusiness business = new BranchBusiness();
    public override BranchOrderTitle GetEntity(BranchOrderTitle Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new BranchOrderTitle();

        Data.Title = txtTitle.Text;
        Data.Status = (int)lkpStatus.GetSelectedValue<BranchOrderTitleStatus>();
        Data.Price = nmrPrice.GetNumber<long>();
        Data.DisplayOrder = Convert.ToInt32(txtDispOrder.Text);
        Data.BranchOrderTitleGroup_ID = lkpGroup.GetSelectedValue<long>();
        Data.ImagePath = fupProductPic.FilePath;
        Data.IsOrderForAllBranch = Convert.ToBoolean(chbAllBranches.Checked);
        if (Data.IsOrderForAllBranch)
        {
            Data.Branch_BranchOrderTitle.Clear();
           
            foreach (var item in business.GetBranchList())
            {
                Branch_BranchOrderTitle model = new Branch_BranchOrderTitle();
                model.MinimumQuantity = Convert.ToInt32(txtMinimumCount.Text);
                model.Branch_ID = item.ID;
                Data.Branch_BranchOrderTitle.Add(model);
            }
           
        }
        return Data;
    }

    public override void SetEntity(BranchOrderTitle Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        lkpStatus.SetSelectedValue(Data.Status);
        nmrPrice.SetNumber(Data.Price);
        txtDispOrder.Text = Data.DisplayOrder.ToString();
        lkpGroup.SetSelectedValue(Data.BranchOrderTitleGroup_ID);
        fupProductPic.FilePath = Data.ImagePath;
        chbAllBranches.Checked = Data.IsOrderForAllBranch;
    }
}