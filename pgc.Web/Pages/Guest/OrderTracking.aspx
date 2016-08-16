<%@ Page title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="OrderTracking.aspx.cs" Inherits="Pages_Guest_OrderTracking" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <link href="/assets/User/UserProfile.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbdy" Runat="Server">
    <section class="main-body">
            <div class="container">
            <div class="row">
                
                    <input type="hidden" id="SelectedOrder" runat="server" clientidmode="Static" />
                    <div class="col-lg-offset-1 col-md-offset-1 col-sm-offset-0 col-xs-offset-0 col-lg-10 col-md-10 col-sm-12 col-xs-12">
                        <%if(order.IsPaid){ %>
                         <div class="alert alert-success fade in">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                            شما با موفقیت در بازی <%=order.GameTitle %> ثبت نام شدید، به منظور پیگیری مراحل بعدی باید کد رهگیری زیر را در اختیار داشته باشید<br />
    <div style="display: table;margin-right: auto;margin-left: auto;margin-top: 20px;">
    کد رهگیری: <span style="font-size: 28px;"" ><%=order.ID %> </span></div>
                          </div>
                        <%}else{ %>
                        <div class="alert alert-danger fade in">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                            عملیات پرداخت با موفقیت همراه نبود، در صورت تمایل مجددا در بازی <%=order.GameTitle %> ثبت نام کنید.
                          </div>
                        <%} %>

                        <div class="user-info">              
                            <div class="row">
                                <%var game = order.Game; %>
                                <%if (game != null)
                                  { %>
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
                            
                            <div class="clearfix"></div>
                </div>
                        

                        <div class="user-info" <%=string.IsNullOrEmpty(order.GroupName)?"style='display:none'":"" %>">
                            <header>
                                <i class="fa fa-list" aria-hidden="true"></i>
                                <span>اعضای گروه</span>
                                
                            </header>
                            
                         
                            <div class="row margin0" style="padding: 0 20px;margin-top:30px">
                                <table class="table">
                                    <tr class="order-title table-header">
                                        <td>نام بازیکن</td>
                                        <td>نام پدر</td>
                                        <td>کد ملی</td>                                  
                                    </tr>
                                   <asp:ObjectDataSource ID="odsOrder"
                                        runat="server"
                                        EnablePaging="false"
                                                                               SelectMethod="gamer_List"
                                        OnSelecting="odsOrder_Selecting"
                                        TypeName="pgc.Business.General.GameBusiness"
                                        EnableViewState="false" />
                                    <asp:ListView ID="lsvOrder" runat="server" DataSourceID="odsOrder" EnableViewState="false">
                                        <ItemTemplate>
                                            <tr class="order-tb-row">
                                                <td><%#Eval("FullName") %></td>
                                                <td><%#Eval("FatherName") %></td>
                                                <td><%#Eval("NationalCode") %></td> 
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </table>
                    </div>
                    </div>
                </div>

               
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