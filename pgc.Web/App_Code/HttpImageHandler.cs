using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Threading;


namespace ImageHandler
{
    public class HttpImageHandler : IHttpHandler
    {


        #region Global Fields
        // set below flags if you want ignore image resize (only in keep ratio mode)
        bool IgnoreSameSize = true;
        bool IgnoreLargerSize = true;
        bool IgnoreSmallerSize = false;
        bool IgnoreRangeSize = true;
        int IgnoreRangeValue = 50;

        int Width = 0;
        int Height = 0;
        #endregion

        public void ProcessRequest(System.Web.HttpContext context)
        {

            //Thread.Sleep(5000);

            string path = context.Request.PhysicalPath;

            if (context.Request.Params["height"] != null)
            {
                try
                {
                    Height = int.Parse(context.Request.Params["height"]);
                }
                catch
                {
                    Height = 0;
                }
            }

            if (context.Request.Params["width"] != null)
            {
                try
                {
                    Width = int.Parse(context.Request.Params["width"]);
                }
                catch
                {
                    Width = 0;
                }
            }

            if (Width <= 0 && Height <= 0)
            {
                //write original image
                Response(context, path);

            }
            else
            {


                if (context.Request.Params["mode"] != null)
                {
                    try
                    {
                        mode = (ImgResizeMode)Enum.Parse(typeof(ImgResizeMode), context.Request.Params["mode"].ToLower());
                    }
                    catch
                    {
                        mode = ImgResizeMode.keepratio;
                    }
                }

                //  if ignore image resize (only in keep ratio mode) write original image
                if (mode == ImgResizeMode.keepratio)
                {
                    System.Drawing.Image origImg = System.Drawing.Image.FromFile(path);

                    int targetWidth = Width;
                    int originalWidth = origImg.Width;

                    bool ignore = false;

                    if (IgnoreSameSize)
                        ignore = (originalWidth - 5) <= targetWidth && targetWidth <= (originalWidth + 5);

                    if (IgnoreRangeSize)
                        ignore = (originalWidth - IgnoreRangeValue) <= targetWidth && targetWidth <= (originalWidth + IgnoreRangeValue);

                    if (IgnoreLargerSize)
                        ignore = ignore || targetWidth >= (originalWidth);

                    if (IgnoreSmallerSize)
                        ignore = ignore || targetWidth <= (originalWidth);

                    if (ignore)
                    {
                        //write original image
                        Response(context, path);
                    }

                }


                //if image already created and exist on server write it
                string alreadyPath = retrieveFileIfExist(path, Width, Height, mode);

                //if its first time and image not exist on server,resize and create image then write it
                if (string.IsNullOrEmpty(alreadyPath))
                {
                    //resize and create image
                    getResizedImage(path, Width, Height, mode);
                    alreadyPath = retrieveFileIfExist(path, Width, Height, mode);
                }

                //write resized image
                Response(context, alreadyPath);

            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region Global Methods

        ImgResizeMode mode = ImgResizeMode.keepratio;

        private void Response(System.Web.HttpContext context, string path)
        {

            DateTime imageLastModifiedTime = File.GetLastWriteTimeUtc(path);
            context.Response.Clear();
            context.Response.ContentType = getContentType(path);
            context.Response.WriteFile(path);

            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetExpires(DateTime.Now.AddDays(7));
            context.Response.Cache.SetLastModified(imageLastModifiedTime);
            context.Response.AddHeader("Last-Modified", imageLastModifiedTime.ToLongDateString());
            context.Response.End();

            ////ImageContainer is an arbitrary type that contains image data
            //DateTime? ifModifiedSinceTime = GetIfModifiedSinceUTCTime(context);
            //DateTime imageLastModifiedTime = File.GetLastWriteTimeUtc(path);
            ////strip milliseconds before comparison
            //bool clientNeedsLatest = ifModifiedSinceTime == null || (imageLastModifiedTime > (((DateTime)ifModifiedSinceTime).AddSeconds(1)));

            //if (clientNeedsLatest)
            //{
            //    //write latest image to response
            //    context.Response.Clear();
            //    context.Response.ContentType = getContentType(path);
            //    context.Response.WriteFile(path);


            //    //tell client to cache
            //    context.Response.Cache.SetCacheability(HttpCacheability.Private);
            //    context.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            //    context.Response.Cache.SetLastModified(imageLastModifiedTime);

            //    //set age/expires so that client doesn't attempt to use cache
            //    context.Response.Cache.SetMaxAge(new TimeSpan(0, 0, 0)); //max-age=0
            //    context.Response.Cache.SetExpires(DateTime.Now.ToUniversalTime());

            //    context.Response.End();
            //}
            //else
            //{
            //    //tell the client the image has not changed!
            //    context.Response.ClearContent();
            //    context.Response.StatusCode = (int)System.Net.HttpStatusCode.NotModified;
            //    context.Response.SuppressContent = true;
            //}

        }

        Image getResizedImage(string path, int width, int height, ImgResizeMode mode)
        {
            Image res = Image.FromFile(path);

            switch (mode)
            {
                case ImgResizeMode.keepratio:
                    res = getResizedImageKeepRatio(res, width, height);
                    break;
                case ImgResizeMode.stretch:
                    res = getResizedImageStretch(res, width, height);
                    break;
                case ImgResizeMode.crop:
                    res = getResizedImageCrop(res, width, height, false);
                    break;
                case ImgResizeMode.cropandscale:
                    res = getResizedImageCrop(res, width, height, true);
                    break;
                default:
                    res = getResizedImageKeepRatio(res, width, height);
                    break;
            }

            saveFileIfNotExist(res, path, width, height, mode);

            return res;
        }

        private void saveFileIfNotExist(Image image, string path, int width, int height, ImgResizeMode mode)
        {
            string folder = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);
            string ext = Path.GetExtension(path);
            string newFileName = name + "_" + width + "_" + height + "_" + mode + ext;
            string filepath = folder + "\\" + newFileName;

            if (!File.Exists(filepath))
            {
                if (ext == ".jpg" || ext == ".jpeg")
                    CompressAndSaveImage(image, filepath, 90);
                else
                    image.Save(filepath);
            }
        }

        private string retrieveFileIfExist(string path, int Width, int Height, ImgResizeMode mode)
        {
            string folder = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);
            string ext = Path.GetExtension(path);
            string newFileName = name + "_" + Width + "_" + Height + "_" + mode + ext;

            if (File.Exists(folder + "\\" + newFileName))
                return folder + "\\" + newFileName;
            else
                return string.Empty;
        }

        private static void CompressAndSaveImage(Image img, string fileName, long quality)
        {
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            img.Save(fileName, GetCodecInfo("image/jpeg"), parameters);
        }

        private static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            foreach (ImageCodecInfo encoder in ImageCodecInfo.GetImageEncoders())
                if (encoder.MimeType == mimeType)
                    return encoder;
            throw new ArgumentOutOfRangeException(
                string.Format("'{0}' not supported", mimeType));
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn, ImageFormat format)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, format);
            return ms.ToArray();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        string getContentType(String path)
        {
            switch (Path.GetExtension(path))
            {
                case ".bmp": return "Image/bmp";
                case ".gif": return "Image/gif";
                case ".jpg": return "Image/jpeg";
                case ".png": return "Image/png";
                default: break;
            }
            return "";
        }

        ImageFormat getImageFormat(String path)
        {
            switch (Path.GetExtension(path))
            {
                case ".bmp": return ImageFormat.Bmp;
                case ".gif": return ImageFormat.Gif;
                case ".jpg": return ImageFormat.Jpeg;
                case ".png": return ImageFormat.Png;
                default: break;
            }
            return ImageFormat.Jpeg;
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

        private DateTime? GetIfModifiedSinceUTCTime(HttpContext context)
        {
            DateTime? ifModifiedSinceTime = null;
            string ifModifiedSinceHeaderText = context.Request.Headers.Get("If-Modified-Since");

            if (!string.IsNullOrEmpty(ifModifiedSinceHeaderText))
            {
                ifModifiedSinceTime = DateTime.Parse(ifModifiedSinceHeaderText);
                //DateTime.Parse will return localized time but we want UTC
                ifModifiedSinceTime = ifModifiedSinceTime.Value.ToUniversalTime();
            }

            return ifModifiedSinceTime;
        }

        public enum ImgResizeMode
        {
            keepratio = 1,
            stretch = 2,
            cropandscale = 3,
            crop = 4
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
        #endregion

        #region Resize Image Keep ratio (default)
        private Image getResizedImageKeepRatio(Image image, int width, int height)
        {
            Size newSize = GetNewSize(image.Width, image.Height, width, height);

            Bitmap newImage = new Bitmap(newSize.Width, newSize.Height);
            using (Graphics gr = Graphics.FromImage(newImage))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(image, new Rectangle(0, 0, newSize.Width, newSize.Height));
            }

            return newImage;

        }

        private static bool Validate(Size NewSize, Size Thumb)
        {
            if (NewSize.Width <= Thumb.Width && NewSize.Height <= Thumb.Height)
                return true;

            return false;
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
        #endregion

        #region Resize image Crop
        private Image getResizedImageCrop(Image image, int width, int height, bool needToScale)
        {

            if (height <= 0 || width <= 0)
                return getResizedImageKeepRatio(image, width, height);

            if (needToScale)
                return scaleAndCrop(image, width, height);
            else
            {
                int x = image.Width / 2 - width / 2;
                int y = image.Height / 2 - height / 2;
                return Crop(image, height, width, x, y);
            }
        }

        private Image Crop(System.Drawing.Image Image, int Height, int Width, int StartAtX, int StartAtY)
        {

            try
            {
                ////check the image height against our desired image height
                //if (Image.Height < Height)
                //{
                //    Height = Image.Height;
                //}

                //if (Image.Width < Width)
                //{
                //    Width = Image.Width;
                //}

                //create a bitmap window for cropping
                Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(72, 72);

                //create a new graphics object from our image and set properties
                using (System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto))
                {
                    grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
                    grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    grPhoto.Clear(Color.White);
                    grPhoto.DrawImage(Image, new Rectangle(0, 0, Width, Height), StartAtX, StartAtY, Width, Height, GraphicsUnit.Pixel);


                    return bmPhoto;
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error cropping image, the error was: " + ex.Message);
            }
        }

        private Image scaleAndCrop(Image image, int width, int height)
        {
            int sourceWidth = image.Width;
            int sourceHeight = image.Height;
            int sourceX = 0;
            int sourceY = 0;
            double destX = 0;
            double destY = 0;

            double nScale = 0;
            double nScaleW = 0;
            double nScaleH = 0;

            nScaleW = ((double)Width / (double)sourceWidth);
            nScaleH = ((double)Height / (double)sourceHeight);


            nScale = Math.Max(nScaleH, nScaleW);
            destY = (Height - sourceHeight * nScale) / 2;
            destX = (Width - sourceWidth * nScale) / 2;


            //if (nScale > 1)
            //    nScale = 1;

            int destWidth = (int)Math.Round(sourceWidth * nScale);
            int destHeight = (int)Math.Round(sourceHeight * nScale);


            System.Drawing.Bitmap bmPhoto = null;
            try
            {
                bmPhoto = new System.Drawing.Bitmap(destWidth + (int)Math.Round(2 * destX), destHeight + (int)Math.Round(2 * destY));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("destWidth:{0}, destX:{1}, destHeight:{2}, desxtY:{3}, Width:{4}, Height:{5}",
                    destWidth, destX, destHeight, destY, Width, Height), ex);
            }
            using (System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto))
            {
                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;

                Rectangle to = new System.Drawing.Rectangle((int)Math.Round(destX), (int)Math.Round(destY), destWidth, destHeight);
                Rectangle from = new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);
                //Console.WriteLine("From: " + from.ToString());
                //Console.WriteLine("To: " + to.ToString());
                grPhoto.DrawImage(image, to, from, System.Drawing.GraphicsUnit.Pixel);

                return bmPhoto;
            }
        }
        #endregion

        #region Resize image stretch
        private Image getResizedImageStretch(Image image, int width, int height)
        {

            if (height <= 0 || width <= 0)
                return getResizedImageKeepRatio(image, width, height);
            //create callback handler
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            //creat BitMap object from image path passed in querystring
            Bitmap myBitmap = new Bitmap(image);
            //create unit object for height and width. This is to convert parameter passed in differen unit like pixel, inch into generic unit.
            System.Web.UI.WebControls.Unit widthUnit = System.Web.UI.WebControls.Unit.Parse(width.ToString());
            System.Web.UI.WebControls.Unit heightUnit = System.Web.UI.WebControls.Unit.Parse(height.ToString());
            //Resize actual image using width and height paramters passed in querystring
            Image myThumbnail = myBitmap.GetThumbnailImage(Convert.ToInt16(widthUnit.Value), Convert.ToInt16(heightUnit.Value), myCallback, IntPtr.Zero);
            return myThumbnail;
        }

        private bool ThumbnailCallback()
        {
            return false;
        }
        #endregion

    }
}
