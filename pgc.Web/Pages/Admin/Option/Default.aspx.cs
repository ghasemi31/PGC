using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using kFrameWork.Business;
using kFrameWork.Util;
using pgc.Model;
using pgc.Model.Enums;
using kFrameWork.Model;

public partial class Pages_Admin_Option_Default : BasePage
{
    OptionBusiness Business = new OptionBusiness();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            BindCategories(Business.GetCategories(), trvCat.Nodes);
        }
    }

    protected Int64 SelectedID
    {
        get { return ConvertorUtil.ToInt64(ViewState["SelectedID"]); }
        set { ViewState["SelectedID"] = value; }
    }

    //protected void OnCancel(object sender, EventArgs e)
    //{
    //    tblControl.Visible = false;
    //    rightcol.Visible = true;
    //    leftcol.Visible = false;
    //}

    protected void OnSave(object sender, EventArgs e)
    {
        if (SelectedID == 0)
            return;
        //long OptionID = ConvertorUtil.ToInt64(trvCat.SelectedValue);
        Option option = Business.RetriveOption(SelectedID);
        switch ((OptionType)option.Type)
        {
            case OptionType.BigInt:
                option.Value = nur_BigInt.GetNumber<long>().ToString();
                break;
            case OptionType.Boolean:
                option.Value = (rdb_Boolean_True.Checked) ? "1" : "0";
                break;
            case OptionType.Date:
                option.Value = cln_Date.SelectedDate.ToString();
                break;
            case OptionType.Double:
                option.Value = hkt_Double.Text;
                break;
            case OptionType.Email:
                option.Value = hkt_Email.Text;
                break;
            case OptionType.Html:
                //option.Value = UIUtil.ReplaceEnter(txt_Html.Value, true);
                option.Value = UIUtil.ReplaceEnter(ckHtml.GetValue(), true);
                break;
            case OptionType.Int:
                option.Value = nur_Int.GetNumber<int>().ToString();
                break;
            case OptionType.LargeText:
                option.Value = UIUtil.ReplaceEnter(txt_LargeText.Text, false);
                break;
            case OptionType.Lookup:
                option.Value = cbo_Lookup.SelectedValue;
                break;
            case OptionType.Money:
                option.Value = nur_Money.GetNumber<long>().ToString();
                break;
            case OptionType.NText:
                option.Value = UIUtil.ReplaceEnter(txt_NText.Text, false);
                break;
            case OptionType.PersianDate:
                option.Value = pdp_PersianDate.PersianDate;
                break;
            case OptionType.Phone:
                option.Value = hkt_Phone.Text;
                break;
            case OptionType.SmallInt:
                option.Value = nurSmallInt.GetNumber<short>().ToString();
                break;
            case OptionType.SmallText:
                option.Value = txt_SmallText.Text;
                break;
            case OptionType.Text:
                option.Value = txt_Text.Text;
                break;
            case OptionType.TinyInt:
                option.Value = nur_TinyInt.GetNumber<short>().ToString();
                break;
            case OptionType.Time:
                option.Value = tp_time.SelectedTime;
                break;
            case OptionType.FilePath:
                option.Value = fup_Path.FilePath;
                break;
        }

        OperationResult Res = Business.SaveOption();
        UserSession.AddMessage(Res.Messages);
        if (Res.Result == ActionResult.Done)
        {
            ResetControl(option);
            lblParameterName.Text = "";
            SelectedID = 0;
            //trvCat.SelectedNode.ImageUrl = "~/Styles/Images/tv_item.png";
            trvCat.SelectedNode.Selected = false;
            tblControl.Visible = false;
            //rightcol.Visible = true;
            //leftcol.Visible = false;
        }
    }

    protected void trvCat_SelectedNodeChanged(object sender, EventArgs e)
    {
        SelectedID = ConvertorUtil.ToInt64(trvCat.SelectedValue);
        if (SelectedID == 0)
            return;
        //trvCat.SelectedNode.ImageUrl = "~/Styles/Images/tv_edit.png";
        Option option = Business.RetriveOption(SelectedID);
        lblParameterName.Text = option.Title;
        mlvControl.ActiveViewIndex = option.Type - 1;
        switch ((OptionType)option.Type)
        {
            case OptionType.BigInt:
                nur_BigInt.SetNumber(ConvertorUtil.ToInt64(option.Value));
                break;
            case OptionType.Boolean:
                if (ConvertorUtil.ToBoolean(option.Value))
                {
                    rdb_Boolean_True.Checked = true;
                    rdb_Boolean_False.Checked = false;
                }
                else
                {
                    rdb_Boolean_True.Checked = false;
                    rdb_Boolean_False.Checked = true;
                }
                break;
            case OptionType.Date:
                if (option.Value != "")
                    cln_Date.SelectedDate = ConvertorUtil.Convert<DateTime>(option.Value);
                break;
            case OptionType.Double:
                hkt_Double.Text = option.Value;
                break;
            case OptionType.Email:
                hkt_Email.Text = option.Value;
                break;
            case OptionType.Html:
                //txt_Html.Value = option.Value;
                ckHtml.SetValue(option.Value);
                break;
            case OptionType.Int:
                nur_Int.SetNumber(ConvertorUtil.ToInt32(option.Value));
                break;
            case OptionType.LargeText:
                txt_LargeText.Text = option.Value;
                break;
            case OptionType.Lookup:
                if (option.OptionLookup != null)
                {
                    cbo_Lookup.DataSource = option.OptionLookup.OptionLookupDetails;
                    cbo_Lookup.DataTextField = "Title";
                    cbo_Lookup.DataValueField = "Value";
                    cbo_Lookup.DataBind();
                    try { cbo_Lookup.SelectedValue = option.Value; }
                    catch { }
                }
                break;
            case OptionType.Money:
                nur_Money.SetNumber(ConvertorUtil.ToInt64(option.Value));
                break;
            case OptionType.NText:
                txt_NText.Text = option.Value;
                break;
            case OptionType.PersianDate:
                pdp_PersianDate.PersianDate = option.Value;
                break;
            case OptionType.Phone:
                hkt_Phone.Text = option.Value;
                break;
            case OptionType.SmallInt:
                nurSmallInt.SetNumber(ConvertorUtil.ToInt16(option.Value));
                break;
            case OptionType.SmallText:
                txt_SmallText.Text = option.Value;
                break;
            case OptionType.Text:
                txt_Text.Text = option.Value;
                break;
            case OptionType.TinyInt:
                nur_TinyInt.SetNumber(option.Value);
                break;
            case OptionType.Time:
                tp_time.SelectedTime = option.Value;
                break;
            case OptionType.FilePath:
                fup_Path.FilePath = option.Value;
                break;
        }
        tblControl.Visible = true;
        //rightcol.Visible = false;
        //leftcol.Visible = true;
    }

    protected void ResetControl(Option option)
    {
        switch ((OptionType)option.Type)
        {
            case OptionType.BigInt:
                nur_BigInt.SetNumber("");
                break;
            case OptionType.Boolean:
                rdb_Boolean_True.Checked = true;
                rdb_Boolean_False.Checked = false;
                break;
            case OptionType.Date:
                cln_Date.SelectedDate = DateTime.Now;
                break;
            case OptionType.Double:
                hkt_Double.Text = "";
                break;
            case OptionType.Email:
                hkt_Email.Text = "";
                break;
            case OptionType.Html:
                //txt_Html.Value = "";
                ckHtml.SetValue("");
                break;
            case OptionType.Int:
                nur_Int.SetNumber("");
                break;
            case OptionType.LargeText:
                txt_LargeText.Text = "";
                break;
            case OptionType.Lookup:
                cbo_Lookup.Items.Clear();
                break;
            case OptionType.Money:
                nur_Money.SetNumber("");
                break;
            case OptionType.NText:
                txt_NText.Text = "";
                break;
            case OptionType.PersianDate:
                pdp_PersianDate.PersianDate = "";
                break;
            case OptionType.Phone:
                hkt_Phone.Text = "";
                break;
            case OptionType.SmallInt:
                nurSmallInt.SetNumber("");
                break;
            case OptionType.SmallText:
                txt_SmallText.Text = "";
                break;
            case OptionType.Text:
                txt_Text.Text = "";
                break;
            case OptionType.TinyInt:
                nur_TinyInt.SetNumber("");
                break;
            case OptionType.Time:
                tp_time.SelectedTime = DateTime.Now.TimeOfDay.ToString().Substring(0, 5);
                break;
            case OptionType.FilePath:
                fup_Path.FilePath = "";
                break;
        }
    }

    protected void BindCategories(List<OptionCategory> Cats, TreeNodeCollection Nodes)
    {
        foreach (OptionCategory Cat in Cats.Where(o => o.Active).OrderBy(c => c.DisplayOrder))
        {
            if (IsMetaRequest || Cat.GrantAdmin)
            {
                TreeNode node = new TreeNode();
                node.Text = Cat.Title;
                node.Value = Cat.ID.ToString();
                //node.ImageUrl = "~/Styles/images/tv_cat.png";
                if (Cat.OptionCategory1.Count > 0)
                {
                    node.SelectAction = TreeNodeSelectAction.Expand;
                    BindCategories(Cat.OptionCategory1.ToList(), node.ChildNodes);
                    if (node.ChildNodes.Count > 0)
                        Nodes.Add(node);
                }
                else if (Cat.Options.Count > 0)
                {
                    foreach (Option option in Cat.Options.Where(o=>o.Active).OrderBy(o => o.DisplayOrder))
                    {
                        TreeNode optionNode = new TreeNode()
                        {
                            Text = option.Title,
                            Value = option.ID.ToString()
                        };

                        //optionNode.ImageUrl = "~/Styles/images/tv_item.png"; ;
                        optionNode.SelectAction = TreeNodeSelectAction.Select;
                        node.ChildNodes.Add(optionNode);
                    }

                    node.SelectAction = TreeNodeSelectAction.Expand;
                    Nodes.Add(node);
                }
                else
                {
                    //node.SelectAction = TreeNodeSelectAction.None;
                }
            }
        }
    }

    protected bool IsMetaRequest
    {
        get
        {
            return (Request.QueryString["meta"] != null);
        }
    }

}