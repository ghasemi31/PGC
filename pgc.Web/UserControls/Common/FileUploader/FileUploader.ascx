<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileUploader.ascx.cs" Inherits="FileUploader" %>
<div id="<%=this.ID %>">
<%if (!this.ReadOnly){ %>
<div class="defaultview" style="<%=(FilePath == "")?"":"display:none;" %>" >
    <iframe src="<%=ResolveClientUrl("~/UserControls/Common/FileUploader/FileUploader.aspx?contId=" + this.ID + "&SaveFolder=" + SaveFolder) %>" 
            allowTransparency="true"
            style="overflow:hidden;" 
            marginheight="0" 
            marginwidth="0" 
            frameborder="0">
    </iframe>
</div>
<%} %>
<div class="loadedview" style="<%=(FilePath != "")?"":"display:none;" %>" >
    <%--<table class="files" cellpadding="0" cellspacing="0" >
        <tr class="template-download">--%>
            <div class="new-preview">
                <a href="<%=ResolveClientUrl(FilePath) %>" target="_blank"><img src="<%=ResolveClientUrl(ThumbPath) %>"></a>
            </div>
            <div class="new-filename">
                <a href="<%=ResolveClientUrl(FilePath) %>" target="_blank" class="name-holder"><%=System.IO.Path.GetFileName(FilePath)%></a>
            </div>
            <div class="new-size">
                <%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(FileSize)%> KB
            </div>
            <div class="new-delete">
                <%if (!this.ReadOnly){ %>
                <button type="button" class="btn-delete" onclick="btndel(this,'<%=FilePath %>','<%=ResolveClientUrl("~/UserControls/Common/FileUploader/DeleteFile.aspx") %>')" title="حذف" ></button>
                <%} %>
            </div>
            <div class="new-name">
                <a href="<%=ResolveClientUrl(FilePath) %>" target="_blank" class="name-holder"><%=FilePath %></a>
            </div>
        <%--</tr>
    </table>--%>
</div>
<input type="text" id="hdf_FilePath" class="hdf_FilePath" runat="server" enableviewstate="true" value="" style="display:none"/>
<%if(this.Required){ %>
    <asp:RequiredFieldValidator runat="server"
                                id="rfvFilePath"
                                ForeColor="Red"
                                CssClass="validator-fileupload"
                                ControlToValidate="hdf_FilePath"
                                Text="*"
                                ToolTip="واردن کردن این مشخصه الزامی است">
    </asp:RequiredFieldValidator>
<%} %>
</div>