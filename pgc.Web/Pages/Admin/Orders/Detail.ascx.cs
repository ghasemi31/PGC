using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;

using pgc.Model.Patterns;
using kFrameWork.Util;
using System.Web.UI.WebControls;

using pgc.Business;

public partial class Pages_Admin_GameOrder_Detail : BaseDetailControl<GameOrder>
{
    public pgc.Business.General.GameBusiness business = new pgc.Business.General.GameBusiness();

    public GameOrder GameOrder = new GameOrder();

    public override GameOrder GetEntity(GameOrder Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new GameOrder();
        

        return Data;
    }

    public override void SetEntity(GameOrder Data, ManagementPageMode Mode)
    {
        GameOrder = Data;
        lblGameOrderID.Text = Data.ID.ToString();
        lblAddress.Text = Data.Address;
        lblGameTitle.Text = Data.GameTitle;
        lblFullName.Text = Data.Name;
        lblGameOrderPersianDate.Text = DateUtil.GetPersianDateWithTime(Data.OrderDate).ToString();
        lblPayableAmount.Text = UIUtil.GetCommaSeparatedOf(Data.PayableAmount) + " ریال";
        //lblPaymentType.Text = EnumUtil.GetEnumElementPersianTitle((PaymentType)Data.PaymentType);
        lblGameOrderPaymentStatus.Text = Data.IsPaid ? "پرداخت شده" : "پرداخت نشده";
        lblGameType.Text=!string.IsNullOrEmpty(Data.GroupName)?"تیمی":"انفرادی";
        lblUserTel.Text = string.IsNullOrEmpty(Data.Mobile) ? Data.Tel : Data.Mobile;

        if (Data.User != null)
        {
            lblEmail.Text = Data.User.Email;
            lblNationalCode.Text = Data.User.NationalCode;
        }

        lblTeamName.Text = Data.GroupName;

        if (!string.IsNullOrEmpty(Data.GroupName))
        {
            lsvGroup.DataSource = business.gamer_List(Data.ID);
            lsvGroup.DataBind();
        }

        lblgameCenter.Text = Data.GameCenterTitle ?? "";
       

    }

    public override void EndMode(ManagementPageMode Mode)
    {
        (this.Page as BaseManagementPage<GameOrdersBusiness, GameOrder, GameOrdersPattern, pgcEntities>).ListControl.Grid.DataBind();
        base.EndMode(Mode);
    }

    protected void btnOnline_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("admin-onlinepayment", null) + "?fid=" + (this.Page as BaseManagementPage<GameOrdersBusiness, GameOrder, GameOrdersPattern, pgcEntities>).SelectedID.ToString());
    }


    
}