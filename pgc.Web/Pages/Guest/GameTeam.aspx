<%@ Page title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="GameTeam.aspx.cs" Inherits="Pages_User_GameDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <link href="/assets/User/UserProfile.css?v=2" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbdy" Runat="Server">
    <section class="main-body">
            <div class="container">
            <div class="row">
                
                    <input type="hidden" id="SelectedOrder" runat="server" clientidmode="Static" />
                    <div class="col-lg-offset-1 col-md-offset-1 col-sm-offset-0 col-xs-offset-0 col-lg-10 col-md-10 col-sm-12 col-xs-12">
                        
                        <div class="alert alert-info fade in">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                           لطفا اعضای تیم <%=order.GroupName %> را مشخص کرده و سپس برروی دکمه تکمیل ثبت نام کلیک کنید.
                          </div>
                        
                        

                        <div class="user-info" >
                            <header>
                                <i class="fa fa-list" aria-hidden="true"></i>
                                <span>اعضای گروه</span>
                                
                            </header>
                             <div class="row margin0" style="padding: 0 20px;">
                                <table class="table">
                                    <tr class="order-title table-header">
                                        <td><asp:TextBox ID="txtName" CssClass="form-control" placeholder="نام و نام خانوادگی بازیکن" ClientIDMode="Static" runat="server" autocomplete="off" ></asp:TextBox>
                            <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2" runat="server"
                                        ValidationGroup="add"
                                        ErrorMessage="لطفا نام و نام خانوادگی بازیکن را وارد نمایید" ControlToValidate="txtName"
                                Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator></td>
                                        <td><asp:TextBox ID="txtFather" CssClass="form-control" placeholder="نام پدر" ClientIDMode="Static" runat="server" autocomplete="off" ></asp:TextBox>
                            <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator3" runat="server"
                                        ValidationGroup="add"
                                        ErrorMessage="لطفا نام پدر را وارد نمایید" ControlToValidate="txtFather"
                                Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator></td>
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


                         <div class="col-lg-offset-1 col-md-offset-1 col-sm-offset-0 col-xs-offset-0 col-lg-10 col-md-10 col-sm-12 col-xs-12">
                             <asp:Button ID="btnSubmit"  runat="server" Text="تکمیل ثبت نام" OnClick="btnSubmit_Click" CssClass="btn-profile" />
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