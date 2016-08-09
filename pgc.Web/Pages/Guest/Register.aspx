<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Pages_Guest_Register" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="kfk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/assets/Guest/css/Register.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbdy" runat="Server">
    <section id="page">
        <div class="container">
            <div class="row">

                <div id="main-content" class="col-lg-offset-2 col-md-offset-2 col-sm-offset-0 col-sm-offset-0 col-lg-8 col-md-8 col-sm-12 col-sm-12">
                    <header>
                        <h1>ثبت نام</h1>
                        <hr />
                    </header>
                   
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <kfk:LookupCombo ID="lkpSexStatus" runat="server" CssClass="form-control" EnumParameterType="pgc.Model.Enums.Gender" />
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txtFullName" CssClass="form-control" placeholder="نام و نام خانوادگی" ClientIDMode="Static" runat="server" autocomplete="off" ToolTip="نام و نام خانوادگی"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server"
                                    ErrorMessage="لطفا نام و نام خانوادگی خود را وارد نمایید" ControlToValidate="txtFullName"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txtNationalCode" CssClass="form-control" placeholder="کد ملی" ClientIDMode="Static" runat="server" autocomplete="off" ToolTip="کد ملی"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server"
                                    ErrorMessage="لطفا کد ملی خود را وارد نمایید" ControlToValidate="txtNationalCode"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txtFatherName" CssClass="form-control" placeholder="نام پدر" ClientIDMode="Static" runat="server" autocomplete="off" ToolTip="نام پدر"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server"
                                    ErrorMessage="لطفا نام پدر خود را وارد نمایید" ControlToValidate="txtFatherName"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <kfk:LookupCombo ID="lkcProvince" class="form-control width100"
                                    runat="server"
                                    BusinessTypeName="pgc.Business.Lookup.ProvinceLookupBusiness"
                                    AutoPostBack="true"
                                    DependantControl="lkcCity"
                                    Required="true" />
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <kfk:LookupCombo ID="lkcCity" class="form-control width100"
                                    runat="server"
                                    BusinessTypeName="pgc.Business.Lookup.CityLookupBusiness"
                                    DependOnParameterName="Province_ID"
                                    DependOnParameterType="Int64"
                                    Required="true" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txtTel" CssClass="form-control" placeholder="تلفن ثابت" ClientIDMode="Static" runat="server" autocomplete="off" ToolTip="تلفن ثابت"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator4" runat="server"
                                    ErrorMessage="لطفا تلفن ثابت خود را وارد نمایید" ControlToValidate="txtTel"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txtMobile" CssClass="form-control" placeholder="تلفن همراه" ClientIDMode="Static" runat="server" autocomplete="off" ToolTip="تلفن همراه"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator5" runat="server"
                                    ErrorMessage="لطفا تلفن همراه خود را وارد نمایید" ControlToValidate="txtMobile"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txtAddress" CssClass="form-control" placeholder="آدرس" ClientIDMode="Static" runat="server" autocomplete="off" ToolTip="آدرس"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator6" runat="server"
                                    ErrorMessage="لطفا آدرس خود را وارد نمایید" ControlToValidate="txtAddress"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txtEmail" ClientIDMode="Static" CssClass="form-control" placeholder="پست الکترونیک" runat="server" autocomplete="off" ToolTip="پست الکترونیک"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredEmail" runat="server"
                                    ErrorMessage="لطفا پست الکترونیک خود را وارد نمایید" ControlToValidate="txtEmail"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ErrorMessage="پست الکترونیک معتبر نمی باشد"
                                    Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" ControlToValidate="txtEmail"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    Display="Dynamic">
                                    </asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txtPassword" TextMode="Password" ClientIDMode="Static" placeholder="کلمه عبور" CssClass="form-control" runat="server" autocomplete="off" ToolTip="کلمه عبور"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredPassword" runat="server"
                                    ErrorMessage="لطفا کلمه عبور خود را وارد نمایید" ControlToValidate="txtPassword"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txtRePassword" TextMode="Password" ClientIDMode="Static" placeholder="تکرار کلمه عبور" CssClass="form-control" runat="server" autocomplete="off" ToolTip="تکرار کلمه عبور"></asp:TextBox>
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
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <div class="row col-md-12">
                                        <asp:ScriptManager ID="sm" runat="server">
                                        </asp:ScriptManager>
                                        <asp:UpdatePanel ID="uplCaptcha" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtCaptcha" CssClass="form-control" placeholder="کد امنیتی" ClientIDMode="Static" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator7" runat="server"
                                                        ErrorMessage="لطفا کد امنیتی را وارد نمایید" ControlToValidate="txtCaptcha"
                                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                                </asp:RequiredFieldValidator>
                                                    <asp:CustomValidator ErrorMessage="کد امنیتی اشتباه است" OnServerValidate="ValidateCaptcha"
                                                        runat="server" Visible="True" Font-Names="Tahoma" ForeColor="#CC0000" Font-Size="10px" Display="Dynamic" />
                                                </div>
                                                <div class="row col-md-6">
                                                    <div id="captcha">
                                                        <kfk:CaptchaControl ID="Captcha6" runat="server"
                                                            CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                                            CaptchaHeight="40" CaptchaWidth="120"
                                                            CaptchaLineNoise="None" CaptchaMinTimeout="5"
                                                            CaptchaMaxTimeout="240" FontColor="#ea7c1b" />
                                                        <input type="submit" runat="server" class="btn-captch" value="&#xf021;" onclick="" causesvalidation="false" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <asp:Button CssClass="btnRegisterForm" runat="server" Text="ثبت نام" OnClick="btnSave_Click" />
                                </div>
                            </div>
                        </div>
                   
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphfoot" runat="Server">
</asp:Content>

