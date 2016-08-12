<%@ Page title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="GameDetail.aspx.cs" Inherits="Pages_User_GameDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <link href="/assets/User/UserProfile.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbdy" Runat="Server">
    <section class="main-body">
        <div class="container">
            <div class="row">
                
                    <input type="hidden" id="SelectedOrder" runat="server" clientidmode="Static" />
                    <div class="col-lg-offset-1 col-md-offset-1 col-sm-offset-0 col-xs-offset-0 col-lg-10 col-md-10 col-sm-12 col-xs-12">
                        <div class="order-code">
                            <ul class="list-inline">
                                <li><a href="/userprofile">حساب کاربری من  <i class="fa fa-angle-left" aria-hidden="true"></i></a></li>
                                 <li><a href="/user/gamelist">بازیهای من  <i class="fa fa-angle-left" aria-hidden="true"></i></a></li>
                                <li>بازی <%=order.GameTitle %></li>
                            </ul>
                        </div>

                        <div class="user-info">              
                            <div class="row">
                                <%var game = order.Game; %>
                                <%if(game!=null){ %>
                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                    <div>
                                        <div>
                                            <div class="profile-userpic">
                                                <img src="<%=game.ImagePath.Replace("~","") %>" />
                                            </div>
                                           
                                        </div>
                                    </div>
                                </div>
                                <%} %>
                                <div class="col-lg-8 col-md-8 col-sm-6 col-xs-12" style="margin-top: 2em;">
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12 profile-item">
                                        <span class="profile-title">نام بازی:  </span>
                                        <span><%=order.GameTitle %></span>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12 profile-item">
                                        <span class="profile-title">تاریخ ثبت نام:  </span>
                                        <span><%=kFrameWork.Util.DateUtil.GetPersianDateWithTime(Convert.ToDateTime(order.OrderDate)) %></span>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12 profile-item">
                                        <span class="profile-title">هزینه ثبت نام(ریال):  </span>
                                        <span><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf((Convert.ToInt64(order.PayableAmount)).ToString()) %></span>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12 profile-item">
                                        <span class="profile-title">وضعیت پرداخت:  </span>
                                        <span><i class="fa <%= order.IsPaid?"fa-check paid":"fa-times unpaid" %>" aria-hidden="true"></i></span>
                                    </div>
                                   

                                </div>
                            </div>
                            <div class="row" id="profile-button">
                                <%if(!order.IsPaid){ %>
                                <asp:Button ID="BtnPay"  runat="server" Text="پرداخت آنلاین" OnClick="Btn_Pay_Click"  CssClass="btn-profile" />
                                <%} %>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        

                        <div class="user-info" <%=order.Group==null?"style='display:none'":"" %>">
                            <header>
                                <i class="fa fa-list" aria-hidden="true"></i>
                                <span>اعضای گروه</span>
                                
                            </header>
                             <div class="row margin0" style="padding: 0 20px;">
                                <table class="table">
                                    <tr class="order-title table-header">
                                        <td><asp:TextBox ID="txtNationalCode" CssClass="form-control" placeholder="کد ملی بازیکن" ClientIDMode="Static" runat="server" autocomplete="off" ></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1" runat="server"
                                        ValidationGroup="add"
                                        ErrorMessage="لطفا کد ملی بازیکن را وارد نمایید" ControlToValidate="txtNationalCode"
                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator></td>
                                        <td> 
                                             <asp:Button ID="Button1"  runat="server" Text="افزودن به گروه" OnClick="btnAddToGroup_Click" ValidationGroup="add"  CssClass="btn-profile" />
                                        </td>
                                        
                                    </tr>
                                    </table>
                                 </div>

                            <div class="row margin0" style="padding: 0 20px;margin-top:30px">
                                <table class="table">
                                    <tr class="order-title table-header">
                                        <td>نام بازیکن</td>
                                        <td>ایمیل</td>
                                        <td>کد ملی</td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                   <asp:ObjectDataSource ID="odsOrder"
                                        runat="server"
                                        EnablePaging="True"
                                        SelectCountMethod="gamer_count"
                                        SelectMethod="gamer_List"
                                        OnSelecting="odsOrder_Selecting"
                                        TypeName="pgc.Business.General.GameBusiness"
                                        EnableViewState="false" />
                                    <asp:ListView ID="lsvOrder" runat="server" DataSourceID="odsOrder" EnableViewState="false">
                                        <ItemTemplate>
                                            <tr class="order-tb-row">
                                                <td><%#Eval("FullName") %></td>
                                                <td><%#Eval("Email") %></td>
                                                <td><%#Eval("NationalCode") %></td>
                                               

                                                <td><asp:LinkButton Text="حذف" ID="btnRemove" CommandArgument='<%# Eval("ID") %>' OnCommand="btnRemove_Command" runat="server" /></td>
                                                
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </table>
                            </div>
                        </div>
                    </div>
               
                <!-- Pager -->
                <%if (dprOrder.TotalRowCount > dprOrder.MaximumRows)
                  {%>
                <div class="pagination">
                    <span>صفحات دیگر: </span>
                    <asp:DataPager ID="dprOrder" runat="server" PagedControlID="lsvOrder" PageSize="10" QueryStringField="page">
                        <Fields>
                            <asp:NextPreviousPagerField PreviousPageText="قبلی" ButtonCssClass="button prev" ShowNextPageButton="false" />
                            <asp:TemplatePagerField>
                                <PagerTemplate><span class="pages"></PagerTemplate>
                            </asp:TemplatePagerField>
                            <asp:NumericPagerField ButtonCount="6" NumericButtonCssClass="page" NextPreviousButtonCssClass="page" CurrentPageLabelCssClass="current" />
                            <asp:TemplatePagerField>
                                <PagerTemplate></span></PagerTemplate>
                            </asp:TemplatePagerField>
                            <asp:NextPreviousPagerField NextPageText="بعدی" ButtonCssClass="button next" ShowPreviousPageButton="false" />
                        </Fields>
                    </asp:DataPager>
                </div>
                <%} %>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphfoot" Runat="Server">
    <script language="javascript" type="text/javascript">
        function setID(e) {
            $('#SelectedOrder').val($(e).prev().attr('order'));
        };

        $(document).ready(function () {
            $('input[IsPaid=True]').each(function () {
                $(this).next().hide();
                //$(this).next().next().hide();
            });
        });
    </script>
</asp:Content>