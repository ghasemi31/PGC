<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HTMLEditor.ascx.cs" Inherits="UserControls_Project_HTMLEditoe" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>


<div class="icon-picker">



    <CKEditor:CKEditorControl ID="CKEditor" BasePath="/ckeditor/" runat="server" Width="100%" Toolbar="Basic" ToolbarStartupExpanded="false"
ToolbarBasic="|Source|-|Bold|Italic|Underline|-|NumberedList|BulletedList|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|
|Link|Unlink|-|TextColor|-|Undo|Redo|Cut|Copy|Paste|PasteText|PasteFromWord|-|Maximize|-|BidiLtr|BidiRtl|
/
|Find|Replace|-|Image|Flash|Table|HorizontalRule|SpecialChar|-|Format|" ></CKEditor:CKEditorControl>

    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
ControlToValidate="CKEditor" ErrorMessage="*" 
 ForeColor="Red">*</asp:RequiredFieldValidator>

</div>


