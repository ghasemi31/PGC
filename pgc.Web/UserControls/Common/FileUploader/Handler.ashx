<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using kFrameWork.Util;

public class Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";//"application/json";
        var r = new System.Collections.Generic.List<ViewDataUploadFilesResult>();
        JavaScriptSerializer js = new JavaScriptSerializer();
        foreach (string file in context.Request.Files)
        {
            HttpPostedFile hpf = context.Request.Files[file] as HttpPostedFile;
            string FileName = string.Empty;
            if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE")
            {
                string[] files = hpf.FileName.Split(new char[] { '\\' });
                FileName = files[files.Length - 1];
            }
            else
            {
                FileName = hpf.FileName;
            }
            if (hpf.ContentLength == 0)
                continue;

            string RelativePath = IOUtil.SaveFile(hpf, context.Request.Form["relative_path"]);
            string GlobalPath = VirtualPathUtility.ToAbsolute(RelativePath);
            
            //Thumbnail Handling -----------------------------------------------------------------
            string Thumb_RelativePath ="";
            string Thumb_GlobalPath = "";
            if (IOUtil.IsImageFormat(RelativePath))
            {
                Thumb_RelativePath = IOUtil.MakeThumbnailOf(RelativePath, 80, 80, "");
                Thumb_GlobalPath = VirtualPathUtility.ToAbsolute(Thumb_RelativePath);
            }
            else
            {
                Thumb_RelativePath = GetFileIconRelativePath(RelativePath);
                Thumb_GlobalPath = VirtualPathUtility.ToAbsolute(Thumb_RelativePath);
            }
            //------------------------------------------------------------------------------------
            
            string File_Name = System.IO.Path.GetFileName(RelativePath);
            int Len = (int)(hpf.ContentLength / 1000);
            //if (File_Name.Length > 25)
            //    File_Name = File_Name.Substring(0, 24) + "...";

            r.Add(new ViewDataUploadFilesResult()
            {
                File_Name = File_Name,
                Global_Path = GlobalPath,
                Thumbnail_url = Thumb_GlobalPath,
                Name = RelativePath,
                Length = Len,
                Type = hpf.ContentType,
                
            });
            var uploadedFiles = new
            {
                files = r.ToArray()
            };
            var jsonObj = js.Serialize(uploadedFiles);
            //jsonObj.ContentEncoding = System.Text.Encoding.UTF8;
            //jsonObj.ContentType = "application/json;";
            context.Response.Write(jsonObj.ToString());
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }


    private string GetFileIconRelativePath(string path)
    {
        string ext = System.IO.Path.GetExtension(path).ToLower().Trim('.');
        string icon_FileName = "icon_" + ext + ".png";
        string icon_RelativePath = "~/UserFiles/icons/" + icon_FileName;
        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(icon_RelativePath)))
            return icon_RelativePath;
        else
            return "~/UserFiles/icons/icon_default.png";
    }
}

public class ViewDataUploadFilesResult
{
    public string File_Name { get; set; }
    public string Global_Path { get; set; }
    public string Thumbnail_url { get; set; }
    public string Name { get; set; }
    public int Length { get; set; }
    public string Type { get; set; }
}