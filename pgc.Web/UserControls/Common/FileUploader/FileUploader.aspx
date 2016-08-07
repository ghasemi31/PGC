<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileUploader.aspx.cs" Inherits="_FileUploader" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en" class="no-js">
<head>
    <title>jQuery File Upload Example</title>
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/Styles/UserControls/Common/FileUploader/fileupload-ui.css") %>" type="text/css"/>
</head>
<body style="background:transparent;overflow:hidden;" scroll="no">

<div id="fileupload">
    <form action="Handler.ashx" method="post" enctype="multipart/form-data">
        <input type="hidden" id="hdf_contId" runat="server" clientidmode="Static" value="" enableviewstate="true"/>
        <input type="hidden" value="<%=Request.QueryString["SaveFolder"] %>" id="relative_path" name="relative_path"/>

        <div class="fileupload-buttonbar">
            
            <label class="fileinput-button">
                <span>آپلود فایل</span>
                <input id="file" type="file" name="files[]">
            </label>
        </div>
    
        <div class="fileupload-content">
            <table class="files" cellpadding="0" cellspacing="0"></table>
        </div>
    </form>
</div>
<script id="template-upload" type="text/x-jquery-tmpl">
    <tr class="template-upload{{if error}} ui-state-error{{/if}}">
        <td>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td class="preview" rowspan="3"></td>    
                    <td class="filename" colspan="3"><div class="name-holder">${name}<div></td>
                </tr>
                <tr>
                    <td class="size" colspan="3">${sizef}</td>
                </tr>
                <tr>
                {{if error}}
                    <td class="error" colspan="2">Error:
                        {{if error === 'maxFileSize'}}File is too big
                        {{else error === 'minFileSize'}}File is too small
                        {{else error === 'acceptFileTypes'}}Filetype not allowed
                        {{else error === 'maxNumberOfFiles'}}Max number of files exceeded
                        {{else}}${error}
                        {{/if}}
                    </td>
                    <td></td>
                {{else}}
                    <td class="progress"><div></div></td>
                    <td class="start"><button>آپلود</button></td>
                    <td class="cancel"><button>لغو</button></td>
                {{/if}}
                </tr>
            </table>
        </td>
    </tr>
</script>
<script id="template-download" type="text/x-jquery-tmpl">
    <tr class="template-download{{if error}} ui-state-error{{/if}}">
        {{if error}}
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="name">${namefdsa}</td>
                        <td class="size">${sizef}</td>
                    </tr>
                    <tr>
                        <td class="error" colspan="2">Error:
                            {{if error === 1}}File exceeds upload_max_filesize (php.ini directive)
                            {{else error === 2}}File exceeds MAX_FILE_SIZE (HTML form directive)
                            {{else error === 3}}File was only partially uploaded
                            {{else error === 4}}No File was uploaded
                            {{else error === 5}}Missing a temporary folder
                            {{else error === 6}}Failed to write file to disk
                            {{else error === 7}}File upload stopped by extension
                            {{else error === 'maxFileSize'}}File is too big
                            {{else error === 'minFileSize'}}File is too small
                            {{else error === 'acceptFileTypes'}}Filetype not allowed
                            {{else error === 'maxNumberOfFiles'}}Max number of files exceeded
                            {{else error === 'uploadedBytes'}}Uploaded bytes exceed file size
                            {{else error === 'emptyResult'}}Empty file upload result
                            {{else}}${error}
                            {{/if}}
                        </td>
                    </tr>
                    <tr>
                        <td class="delete" colspan="2">
                            <button data-type="${delete_type}" data-url="${delete_url}">Delete</button>
                        </td>
                    </tr>
                </table>
            </td>
        {{else}}
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="preview" rowspan="3">
                            {{if Thumbnail_url}}
                                <a href="${Global_Path}" target="_blank"><img src="${Thumbnail_url}"></a>
                            {{/if}}
                        </td>
                        <td class="filename" colspan="3">
                            <a href="${Global_Path}" target="_blank" class="name-holder">${File_Name}</a>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="size" colspan="3">
                            ${Length} KB
                        </td>
                    </tr>
                    <tr>
                    {{if error}}
                        <td class="error" colspan="2">Error:
                            {{if error === 'maxFileSize'}}File is too big
                            {{else error === 'minFileSize'}}File is too small
                            {{else error === 'acceptFileTypes'}}Filetype not allowed
                            {{else error === 'maxNumberOfFiles'}}Max number of files exceeded
                            {{else}}${error}
                            {{/if}}
                        </td>
                        <td></td>
                        <td></td>
                    {{else}}
                        <td class="delete">
                            <button data-type="${delete_type}" data-url="${delete_url}">حذف</button>
                        </td>
                        <td class="name" colspan="2">
                            <a href="${Global_Path}" target="_blank" class="name-holder">${Name}</a>
                        </td>
                    {{/if}}
                    </tr>
                </table>
            </td>
         {{/if}}
    </tr>
</script>


<!-- can be shared -->
<!--<script src="//ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js"></script>-->
<script src="<%=ResolveClientUrl("~/Scripts/UserControls/Common/FileUploader/jquery-1.7.2.min.js")%>" language="javascript" type="text/javascript"></script>
<!--<script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.8.13/jquery-ui.min.js"></script>-->
<script src="<%=ResolveClientUrl("~/Scripts/UserControls/Common/FileUploader/jquery-ui.min.js")%>" language="javascript" type="text/javascript"></script>

<!-- can be custom -->
<!--<script src="//ajax.aspnetcdn.com/ajax/jquery.templates/beta1/jquery.tmpl.min.js"></script>-->
<script src="<%=ResolveClientUrl("~/Scripts/UserControls/Common/FileUploader/jquery.tmpl.min.js")%>" language="javascript" type="text/javascript"></script>
<script src="<%=ResolveClientUrl("~/Scripts/UserControls/Common/FileUploader/jquery.iframe-transport.js")%>" language="javascript" type="text/javascript"></script>
<script src="<%=ResolveClientUrl("~/Scripts/UserControls/Common/FileUploader/jquery.fileupload.js")%>" language="javascript" type="text/javascript"></script>
<script src="<%=ResolveClientUrl("~/Scripts/UserControls/Common/FileUploader/jquery.fileupload-ui.js")%>" language="javascript" type="text/javascript"></script>
<script src="<%=ResolveClientUrl("~/Scripts/UserControls/Common/FileUploader/application.js")%>" language="javascript" type="text/javascript"></script>

</html>