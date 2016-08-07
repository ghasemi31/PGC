<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mgmt.aspx.cs" Inherits="mgmt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Mgmt</title>
</head>
<body>
<form id="form1" runat="server">
    <div style ="font-family:Tahoma;font-size:8pt">
        <table style="width:100%">
            <tr>
                <td colspan="2">
                    <fieldset title="Login" id="LoginPanel" runat="server" style="height: 50px">
                    <legend><b>Login</b></legend>
                        <table style="width: 100%" cellpadding="5" cellspacing="0">
                            <tr>
                                <td style="width: 100px">
                                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" AutoPostBack="True" OnTextChanged="cmdLogin_Click" ></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="cmdLogin" runat="server" Text="Login" OnClick="cmdLogin_Click" /></td>
                            </tr>
                        </table>
                     </fieldset>
                </td>
            </tr>
            <tr>
                <td style="width:50%" valign="top">
                    <fieldset title="Connection" id="Table1" runat="server" style="height: 180px">
                    <legend><b>Connection String</b></legend>
                        <table style="width: 100%" cellpadding="5" cellspacing="0">
                        <tr>
                            <td style="width: 100px">Connection String</td>
                            <td>
                                <asp:TextBox ID="txtConnectionString" runat="server" Width="95%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="cmdConnect" runat="server" Text="Connect" OnClick="cmdConnect_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblConnectionString" runat="server" ForeColor="Gray" BorderStyle="None" EnableViewState="False"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbConnectionMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
                        </tr>
                    </table>
                </fieldset>        
                </td>
                <td style="width:50%" valign="top">
                    <fieldset title="T-Sql Generation" id="Table2" runat="server" style="height: 180px">
                        <legend><b>T-Sql Generation</b></legend>
                            <table style="width: 100%" cellpadding="5" cellspacing="0" >
                            <tr>
                                <td style="width: 100px">
                                    Table</td>
                                <td>
                                    <asp:DropDownList ID="cboTables" runat="server">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Procedure Type</td>
                                <td>
                                    <asp:DropDownList ID="cboTypes" runat="server" Width="150px">
                                        <asp:ListItem Value="0">Retrieve</asp:ListItem>
                                        <asp:ListItem Value="1">Delete</asp:ListItem>
                                        <asp:ListItem Value="2">Save</asp:ListItem>
                                        <asp:ListItem Value="3">Lookup</asp:ListItem>
                                        <asp:ListItem Value="4" Selected="True">List</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:RadioButton ID="rdbCreate" runat="server" Checked="True" GroupName="rdbg1" Text="Create" />
                                    <asp:RadioButton ID="rdbAlter" runat="server" GroupName="rdbg1" Text="Alter" /></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;<asp:Button ID="cmdGenerate" runat="server" Text="Generate T-Sql" OnClick="cmdGenerate_Click" /></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblGenerationMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
                            </tr>
                        </table>
                    </fieldset>        
                </td>
            </tr>
        </table>
        <fieldset title="Query" id="Table3" runat="server">
            <legend><b>T-Sql Query</b></legend>
                <table style="width: 100%" cellpadding="5">
            <tr>
                <td valign="top" style="width: 80px">Query</td>
                <td>
                    <asp:TextBox ID="txtQuery" runat="server" Rows="4" TextMode="MultiLine" Width="100%" Height="250px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Execution Type:</td>
                <td>
                    <asp:RadioButton ID="rdbReader" runat="server" Checked="True" GroupName="grpExecuteType" Text="Reader" />
                    <asp:RadioButton ID="rdbNonQuery" runat="server" Text="NonQuery" GroupName="grpExecuteType" />
                    <asp:RadioButton ID="rdbScalar" runat="server" Text="Scalar" GroupName="grpExecuteType" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblExecuteMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Button ID="cmdExecute" runat="server" Text="Execute" OnClick="cmdExecute_Click" /></td>
            </tr>
        </table>
        </fieldset>
        <br />
        <fieldset title="Result" id="Table4" runat="server">
            <legend><b>Result</b></legend>
                <table style="width: 100%" cellpadding="5">
            <tr>
                <td>
                    <asp:GridView ID="QueryGridView" runat="server" CellPadding="3" OnPageIndexChanging="QueryGridView_PageIndexChanging" PageSize="30" AllowPaging="True" Width="100%" EnableViewState="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellSpacing="2">
                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <RowStyle BackColor="#FFF7E7" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#8C4510" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="Lavender" />
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="25" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblQuery" runat="server" BorderStyle="None" EnableViewState="False"
                        ForeColor="Gray"></asp:Label>
                </td>
            </tr>
        </table>
        </fieldset>
    </div>
    </form>
</body>
</html>
