using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Drawing;
using System.Security.Cryptography;

namespace kFrameWork.Util
{
    public class IOUtil
    {

        //Old Pattern Delete & Save
        public static bool DeleteFile(string FilePath,bool IsRelative)
        {
            try
            {
                if (string.IsNullOrEmpty(FilePath))
                    return false;

                if (IsRelative)
                    FilePath = HttpContext.Current.Server.MapPath(FilePath) ;


                if (System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);
                    return true;
                }

                return false;
            }
            catch 
            {
                return false;
            }
        }
        public static string SaveFile(HttpPostedFile file, string FolderPath )
        {
            try
            {
                string FilePath = HttpContext.Current.Server.MapPath(FolderPath) + System.IO.Path.GetFileName(file.FileName);

                int Counter = 1;

                while (System.IO.File.Exists(FilePath))
                {
                    if (Counter == 1)
                    {
                        FilePath = HttpContext.Current.Server.MapPath(FolderPath) + 
                            Path.GetFileNameWithoutExtension(FilePath) + 
                            "_" + 
                            Counter.ToString() + 
                            Path.GetExtension(FilePath);
                    }
                    else
                    {
                        string FileName = System.IO.Path.GetFileNameWithoutExtension(FilePath);

                        FilePath = HttpContext.Current.Server.MapPath(FolderPath) + 
                            FileName.Remove(FileName.LastIndexOf('_')) + 
                            "_" + 
                            Counter.ToString() + 
                            Path.GetExtension(FilePath);
                    }

                    Counter++;
                }
                file.SaveAs(FilePath);

                return FolderPath + Path.GetFileName(FilePath);
            }
            catch 
            {
                return string.Empty;
            }
        }

        //Image Formats
        public static System.Drawing.Imaging.ImageFormat GetImageFormat(string FilePath)
        {
            System.Drawing.Imaging.ImageFormat ImageFormat = null;

            switch (System.IO.Path.GetExtension(FilePath).ToUpper().Trim('.'))
            {
                case ("JPG"):
                case ("JPEG"):
                    {
                        ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                        break;
                    }
                case ("GIF"):
                    {
                        ImageFormat = System.Drawing.Imaging.ImageFormat.Gif;
                        break;
                    }
                case ("WMF"):
                    {
                        ImageFormat = System.Drawing.Imaging.ImageFormat.Wmf;
                        break;
                    }
                case ("TIF"):
                case ("TIFF"):
                    {
                        ImageFormat = System.Drawing.Imaging.ImageFormat.Tiff;
                        break;
                    }
                case ("ICO"):
                    {
                        ImageFormat = System.Drawing.Imaging.ImageFormat.Icon;
                        break;
                    }
                case ("BMP"):
                    {
                        ImageFormat = System.Drawing.Imaging.ImageFormat.Bmp;
                        break;
                    }
                case ("EMF"):
                    {
                        ImageFormat = System.Drawing.Imaging.ImageFormat.Emf;
                        break;
                    }
                case ("PNG"):
                    {
                        ImageFormat = System.Drawing.Imaging.ImageFormat.Png;
                        break;
                    }
            }
            return ImageFormat;
        }
        public static bool IsImageFormat(string FilePath)
        {
            return IsImageFormat(FilePath, false);
        }
        public static bool IsImageFormat(string FilePath, bool CountSwf)
        {
            System.Drawing.Imaging.ImageFormat ImageFormat = GetImageFormat(FilePath);

            bool Res = false;

            if (CountSwf)

                Res = (ImageFormat != null || System.IO.Path.GetExtension(FilePath).ToUpper().Trim('.').Equals("SWF"));
            else
                Res = (ImageFormat != null);

            return Res;
        }
        public static bool IsFlashFormat(string FilePath)
        {
            return System.IO.Path.GetExtension(FilePath).ToUpper().Trim('.').Equals("SWF");
        }

        //Thumbnail Handling
        /// <summary>
        /// make a thumbnail of specified file and write it into IO
        /// </summary>
        /// <param name="path">Tha path of file to make thumbnnail from (can be server relative or physical path)</param>
        /// <param name="width">maximum width of thumbnail (enter 0 if there is no limit)</param>
        /// <param name="height">maximum height of thumbnail (enter 0 if there is no limit)</param>
        /// <param name="thumbname">the name that will append to original file name and will be used as 'Thumbnail File Name' , Default Name is 'DefaultThumb'</param>
        /// <returns>Tha path of writed thumbnail file</returns>
        public static string MakeThumbnailOf(string path, int thumbwidth, int thumbheight, string thumbname)
        {
            string physicalPath = "";
            bool isRelativePath = false;
            if (thumbname == "")
                thumbname = "DefaultThumb";

            if (path.StartsWith("~"))
            {
                physicalPath = HttpContext.Current.Server.MapPath(path);
                isRelativePath = true;
            }
            else
                physicalPath = path;

            Image image = Image.FromFile(physicalPath);
            Size newSize = GetNewSize(image.Width, image.Height, thumbwidth, thumbheight);
            Image thumb = image.GetThumbnailImage(newSize.Width, newSize.Height, () => false, IntPtr.Zero);

            string folder = Path.GetDirectoryName(physicalPath);
            string name = Path.GetFileNameWithoutExtension(physicalPath);
            string ext = Path.GetExtension(physicalPath);
            string newFileName = name + "_" + thumbname + ext;
            thumb.Save(folder + "\\" + newFileName);

            image.Dispose();
            thumb.Dispose();

            if (isRelativePath)
                return path.TrimEnd(Path.GetFileName(path).ToCharArray()) + newFileName;
            else
                return folder + "\\" + newFileName;
        }
        private static Size GetNewSize(int SourceWidth, int SourceHeight, int MaxThumbWidth, int MaxThumbHeight)
        {
            //validation
            if (SourceWidth == 0 || SourceHeight == 0)
                throw new Exception("Source image size can not 0 width or height");
            //validation
            if (MaxThumbWidth == 0 && MaxThumbHeight == 0)
                throw new Exception("Thumbnail image size should have at least width or height");

            Size res = new Size();
            Size source = new Size() { Width = SourceWidth, Height = SourceHeight };
            Size maxthumb = new Size() { Width = MaxThumbWidth, Height = MaxThumbHeight };

            //just heigh limit
            if (maxthumb.Width == 0)
                return GetNewSizeBasedOfHeight(source, maxthumb);

            //just width limit
            if (maxthumb.Height == 0)
                return GetNewSizeBasedOfWidth(source, maxthumb);

            switch (GetSizeType(source))
            {
                case RectType.Vertical:
                    return GetNewSizeBasedOnHeightThenWidth(source, maxthumb);
                case RectType.Horizantal:
                    return GetNewSizeBasedOnWidthThenHeight(source, maxthumb);
                default:
                    if (GetSizeType(maxthumb) == RectType.Horizantal)
                        return GetNewSizeBasedOnHeightThenWidth(source, maxthumb);
                    else
                        return GetNewSizeBasedOnWidthThenHeight(source, maxthumb);
            }
        }
        private static Size GetNewSizeBasedOnWidthThenHeight(Size source, Size maxthumb)
        {
            Size res = new Size();
            res = GetNewSizeBasedOfWidth(source, maxthumb);
            if (!Validate(res, maxthumb))
                res = GetNewSizeBasedOfHeight(source, maxthumb);
            if (!Validate(res, maxthumb))
                throw new Exception("can not calculate a new thumbnail size with preserved aspect ratio");
            return res;
        }
        private static Size GetNewSizeBasedOnHeightThenWidth(Size source, Size maxthumb)
        {
            Size res = new Size();
            res = GetNewSizeBasedOfHeight(source, maxthumb);
            if (!Validate(res, maxthumb))
                res = GetNewSizeBasedOfWidth(source, maxthumb);
            if (!Validate(res, maxthumb))
                throw new Exception("can not calculate a new thumbnail size with preserved aspect ratio");
            return res;
        }
        private static Size GetNewSizeBasedOfWidth(Size Source, Size Thumb)
        {
            Size res = new Size();
            res.Width = Thumb.Width;
            float ratio = (float)Source.Width / (float)Thumb.Width;
            res.Height = Convert.ToInt32(((float)Source.Height) / ratio);
            return res;
        }
        private static Size GetNewSizeBasedOfHeight(Size Source, Size Thumb)
        {
            Size res = new Size();
            res.Height = Thumb.Height;
            float ratio = (float)Source.Height / (float)Thumb.Height;
            res.Width = Convert.ToInt32(((float)Source.Width) / ratio);
            return res;
        }
        private static bool Validate(Size NewSize, Size Thumb)
        {
            if (NewSize.Width <= Thumb.Width && NewSize.Height <= Thumb.Height)
                return true;

            return false;
        }
        private static RectType GetSizeType(Size rect)
        {
            if (rect.Width > rect.Height)
                return RectType.Horizantal;
            else if (rect.Width < rect.Height)
                return RectType.Vertical;
            else
                return RectType.Box;
        }
        public static int GetFileSize(string path)
        {
            if (path == "")
                return 0;

            string physicalPath = "";

            if (path.StartsWith("~"))
                physicalPath = HttpContext.Current.Server.MapPath(path);
            else
                physicalPath = path;

            FileStream fs;
            if (File.Exists(physicalPath))
                fs = File.OpenRead(physicalPath);
            else
                return 0;

            int len = (int)fs.Length / 1000;

            fs.Close();
            fs.Dispose();
            return len;
        }

    }

    public enum RectType
    {
        Horizantal,
        Vertical,
        Box
    }
    public class Size
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
