<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RightPanel.ascx.cs" Inherits="UserControls_Project_RightPanel" %>
<div id="right-panel">

    <div class="wrapper">
        <div class="tab">
            <a href="<%=GetRouteUrl("guest-default",null) %>">
                <img src="/assets/global/images/logo/pgcizi.title.red.svg" alt="صفحه نخست" style="width: 100%" />
            </a>
        </div>
        <%--<asp:Button runat="server" Text="خروج" CssClass="exitbtn" OnClick="LogOut" UseSubmitBehavior="false"/>--%>
        <%--<input type="button" value="خروج" class="exitbtn"/>--%>

        <%--<div class="welcome"> <%=((pgc.Model.Enums.Gender)kFrameWork.UI.UserSession.User.Gender == pgc.Model.Enums.Gender.Female) ? "خانم" : "آقای" %> <%=kFrameWork.UI.UserSession.User.Fname + " " + kFrameWork.UI.UserSession.User.Lname%><br />به مستر دیزی خوش آمدید.--%>

        <div class="welcome">
            <span style="font-size: 20px;"><i class="fa fa-user" aria-hidden="true"></i></span>
            <%=(!string.IsNullOrEmpty(kFrameWork.UI.UserSession.User.FullName)?kFrameWork.UI.UserSession.User.FullName:"کاربر") + "  عزیز،" %><br />
            به مستر دیزی خوش آمدید.
          <%if ((pgc.Model.Enums.Role)kFrameWork.UI.UserSession.User.AccessLevel.Role == pgc.Model.Enums.Role.User)
            {%>
            <br />
            <b style="color: #f7e302">کد اشتراک شما : <%=kFrameWork.UI.UserSession.UserID%></b>

            <%} %>
            <%if ((pgc.Model.Enums.Role)kFrameWork.UI.UserSession.User.AccessLevel.Role == pgc.Model.Enums.Role.Agent)
              {
                  long minimumCredit = pgc.Business.BranchCreditBusiness.GetBranchMinimumCredit(kFrameWork.UI.UserSession.User.Branch_ID.Value) * (-1);
            %>
            <div>اعتبار شما :
                <br />
                <%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(minimumCredit)%> ریال</div>
            <%long Credit = pgc.Business.BranchCreditBusiness.GetBranchCredit(kFrameWork.UI.UserSession.User.Branch_ID.Value); %>
            <%if (Credit < 0)
              {%>
            <div>حساب شما :
                <br />
                <span class="minuscrdt"><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(Credit)%> ریال</span></div>
            <%}
              else
              { %>
            <div>حساب شما :
                <br />
                <%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(Credit)%> ریال</div>
            <%} %>
            <%} %>
        </div>


        <div class="info" style="background: none;"><%--آخرین مراجعه: <br /><%=kFrameWork.Util.DateUtil.GetPersianDateWithTime(DateTime.Now) %>--%></div>

        <%--<div class="cat">
			<div class="title">ویرایش نمایه</div>
			<div class="sub">
                <a href="#" class="arrow item" >102 تور خارجی</a>
                <a href="#" class="arrow item" >102 تور خارجی</a>
                <a href="#" class="arrow item" >102 تور خارجی</a>
                <a href="#" class="arrow item" >102 تور خارجی</a>
                <a href="#" class="arrow item" >102 تور خارجی</a>
            </div>
		</div>
		<div class="cat cat-open">
			<div class="title">تنظیمات</div>
            <div class="sub" style="display:block">
			    <a href="#" class="arrow item" >102 تور خارجی</a>
                <a href="#" class="star item" >3 تور ویژه</a>
                <a href="#" class="btn" >ویرایش اطلاعات شخصی</a>
			    <div class="hsplitter"></div>
			    <a href="#" class="add item" >افزودن هتل</a>
            </div>
		</div>--%>

        <%=GetHtmlOfToolBoxMenu() %>

        <%--<div class="toggle opened"></div>--%>
        <div>
            <a href="javascript:;" runat="server" class="exitbtn" id="btnExite" style="float: left">خروج<i class="fa fa-sign-out" aria-hidden="true"></i></a>
            <a href="<%=GetRouteUrl("guest-default",null) %>" class="exitbtn" style="float: right"><i class="fa fa-arrow-right" aria-hidden="true"></i>بازگشت</a>
        </div>
        <div style="clear:both;float: none;"></div>


    </div>
</div>
