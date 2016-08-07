<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IconPicker.ascx.cs" Inherits="UserControls_Project_IconPicker" %>

<%--<kfk:SharedResources runat="server" ID="shr">--%>
    <%--<ResourceName>bootstrap.min.css</ResourceName>--%>
    <%--<ResourceName>font-awesome.min.css</ResourceName>--%>
  <%--  <resourcename>bootstrap-iconpicker.css</resourcename>--%>

    <%-- <ResourceName>jquery-1.11.2.js</ResourceName>--%>
    <%--<ResourceName>bootstrap.min.js</ResourceName>--%>
    <%--<ResourceName>iconset-all.min.js</ResourceName>--%>
<%--    <resourcename>bootstrap-iconpicker.js</resourcename>
</kfk:SharedResources>--%>

<div class="icon-picker">
    <asp:HiddenField ID="hfValue" runat="server" Value="empty" />
    <input type="hidden" class="IcnSet" value="<%=this.Iconset.ToString() %>" />


    <%--  <p><code>fontawesome</code></p>--%>
    <%if (this.Mode == pgc.Model.Enums.IconPickerMode.Inline){ %>
        <div class="inline-picker">
            <div style="width: 281px; height: 250px;" class="icn-picker"></div>
        </div>
    <%}
      else{ %>
        <div class="dropdownpicker">
            <button class="btn btn-default icn-picker"></button>
        </div>
    <%} %>
</div>


