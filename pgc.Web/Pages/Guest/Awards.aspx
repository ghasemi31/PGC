<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="Awards.aspx.cs" Inherits="Pages_Guest_Awards" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <%this.Title = "جوایز بازیها"; %>
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
                            <span>جدول جوایز بازیها</span>
                            <hr />
                        </header>
                        <div class="row margin0" style="padding: 0 20px;">
                            <table class="table">
                                <tr class="order-title table-header">
                                    <td>کد</td>
                                    <td>نام بازی</td>
                                    <td>نوع</td>

                                    <td>مجموع جوایز</td>

                                    <td>جایزه نفر اول</td>
                                    <td>جایزه نفر دوم</td>
                                    <td>جایزه نفر سوم</td>
                                    <td> ورودی (هزار تومان)</td>
                                </tr>
                                <%foreach (var item in business.GameList())
                                  {%>
                                <tr class="order-tb-row">
                                    <td><%=item.ID %></td>
                                    <td><%=item.Title %></td>
                                    <td><%=item.GamerCount==1?"تک نفره":item.GamerCount+" نفره" %></td>
                                    <td><%=item.SumPresent %></td>
                                    <td><%=item.FirstPresent %></td>
                                    <td><%=item.SecondPresent %></td>
                                    <td><%=item.ThirdPresent %></td>
                                    <td><%=item.Cost/10000 %></td>
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


