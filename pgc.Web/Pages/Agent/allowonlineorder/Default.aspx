<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Agent_AllowOnlineOrder_Default" validateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">
<fieldset>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table class="branchform">
    <tr>        
        <td class="control" colspan="2">
            <asp:CheckBox Text="پذیرای سفارش بصورت آنلاین" runat="server" ID="chbAllow" ClientIDMode="Static" />
        </td>
    </tr>
    <tr>
        <td class="caption" style="width:100px">پذیرش از ساعت</td>
        <td class="control"><kfk:TimePicker ID="timeFrom" runat="server" /></td>
    </tr>
    <tr>
        <td class="caption"  style="width:100px">پذیرش تا ساعت</td>
        <td class="control"><kfk:TimePicker ID="timeTo" runat="server" /></td>
    </tr>   
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large"  OnClick="btnSave_Click" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="btnCancel_Click"  CausesValidation="false" />
</div>
    <script type="text/javascript">
        $(document).ready(function () {
            if (!$('#chbAllow').attr('checked'))
                $('select[id*=timeTo],select[id*=timeFrom]').attr('disabled', 'disabled');

            $('#chbAllow').click(function () {
                if (!$('#chbAllow').attr('checked'))
                    $('select[id*=timeTo],select[id*=timeFrom]').attr('disabled', 'disabled');
                else
                    $('select[id*=timeTo],select[id*=timeFrom]').removeAttr('disabled');
            });
        });
    </script>
</fieldset>
</asp:Content>

