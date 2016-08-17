<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="GameCenter.aspx.cs" Inherits="Pages_Guest_GameCenter" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <%this.Title = "مراکز بازی "; %>
    <link href="/assets/User/UserProfile.css?v=2" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbdy" runat="Server">
    <section>
        <header id="awards-header">
        </header>
        <div class="container">
            <div class="row">
                <div>
                    <div class="order-code">
                    </div>
                    <div class="user-info">
                        <header>
                            <i class="fa fa-list" aria-hidden="true"></i>
                            <span>جدول مراکز بازی</span>
                            <hr />
                        </header>
                        <div class="row margin0" style="padding: 0 20px;">
                            <table class="table">
                                <tr class="order-title table-header">
                                    <td>نام مرکز</td>
                                    <td>استان</td>
                                    <td>شهر</td>
                                    <td>توضیحات</td>
                                </tr>
                                <%foreach (var item in business.GetGameCenterList())
                                  {%>
                                <tr class="order-tb-row">
                                    <td><%=item.TItle %></td>
                                    <td><%=item.City.Province.Title %></td>
                                    <td><%=item.City.Title %></td>
                                    <td><%=(item.Description.Length>200)?item.Description.Substring(0,200):item.Description%></td>                                    
                                </tr>
                                <%} %>
                            </table>
                        </div>
                    </div>
                </div>


            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphfoot" runat="Server">
    </asp:Content>


