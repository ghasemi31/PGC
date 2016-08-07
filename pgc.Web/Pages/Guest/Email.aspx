<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="Email.aspx.cs" Inherits="Pages_Guest_Email" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphStylePege" runat="Server">
    <link href="/assets/css/Email/Email.min.css?v=2.2" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentPage" runat="Server">
    <section class="main-body">
        <div class="container">
            <div class="row">
                <div class="tip">
                    <p>
                        کاربر گرامی، برای ورود به سایت پست الکترویک و کلمه عبور جدید را وارد کنید.
                    </p>
                </div>
                <div>
                    <form runat="server">
                        <div class="form-item row">
                            <div class="col-lg-offset-4 col-md-offset-4 col-sm-offset-3 col-xs-offset-0 col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                <label><i class="fa fa-envelope"></i></label>
                                <span class="form-item-title">پست الکترونیک جدید</span>

                                <asp:TextBox ID="txtEmail" CssClass="input-text" placeholder="پست الکترونیک" ClientIDMode="Static" runat="server"></asp:TextBox>
                                <hr />
                                <asp:RequiredFieldValidator
                                    ID="RequiredEmail" runat="server"
                                    ErrorMessage="لطفا پست الکترونیک خود را وارد نمایید" ControlToValidate="txtEmail"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ErrorMessage="پست الکترونیک معتبر نمی باشد"
                                    Font-Names="Tahoma" ForeColor="#CC0000" Font-Size="10px" ControlToValidate="txtEmail"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    Display="Dynamic">
                                </asp:RegularExpressionValidator>
                            </div>

                        </div>
                        <div class="form-item row">
                            <div class="col-lg-offset-4 col-md-offset-4 col-sm-offset-3 col-xs-offset-0 col-lg-4 col-md-4 col-sm-6 col-xs-12">
                               
                                <label><i class="fa fa-key"></i></label>
                                <span class="form-item-title">کلمه عبور جدید</span>
                                <asp:TextBox ID="txtPassword" TextMode="Password" ClientIDMode="Static" placeholder="کلمه عبور" CssClass="input-text" runat="server" autocomplete="off"></asp:TextBox>
                                <hr />
                                <asp:RequiredFieldValidator
                                    ID="RequiredPassword" runat="server"
                                    ErrorMessage="لطفا کلمه عبور خود را وارد نمایید" ControlToValidate="txtPassword"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </div>

                        </div>
                        <div class="form-item row">
                            <div class="col-lg-offset-4 col-md-offset-4 col-sm-offset-3 col-xs-offset-0 col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                
                                <label><i class="fa fa-key"></i></label>
                                <span class="form-item-title">تکرار کلمه عبور جدید</span>
                                <asp:TextBox ID="txtRePassword" TextMode="Password" ClientIDMode="Static" placeholder="تکرار کلمه عبور" CssClass="input-text" runat="server" autocomplete="off"></asp:TextBox>
                                <hr />
                                <asp:RequiredFieldValidator
                                    ID="RequiredRePassword" runat="server"
                                    ErrorMessage="لطفا کلمه عبور خود را تکرار نمایید" ControlToValidate="txtRePassword"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server"
                                    ErrorMessage="کلمه عبور یکسان نمی باشد" ControlToCompare="txtPassword"
                                    ControlToValidate="txtRePassword" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                </asp:CompareValidator>
                            </div>

                        </div>
                        <div class="row display-center">
                            <asp:Button ID="btnSave" CssClass="btn-email" runat="server" Text="ثبت اطلاعات جدید" OnClick="btnSave_Click" />
                        </div>
                        <kfk:UserMessageViewer runat="server" ID="UserMessageViewer" />
                    </form>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScriptPage" runat="Server">
</asp:Content>

