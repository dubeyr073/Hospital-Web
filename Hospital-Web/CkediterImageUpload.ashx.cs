//using NEWSLIVE24X07.Controllers;
using NEWSLIVE24X07.Models.Log;
//using OnlineServiceProvider.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace NEWSLIVE24X07
{
    /// <summary>
    /// Summary description for Upload
    /// </summary>
    public class CkediterImageUpload : IHttpHandler
    {
        //SessiionManagementController Obj = new SessiionManagementController();
        public void ProcessRequest(HttpContext context)
        {
            //if (Obj.SessionManagment())
            //{
                string CKEditorFuncNum = context.Request["CKEditorFuncNum"];
                try
                {

                    HttpPostedFile uploads = context.Request.Files["upload"];
                    if ((uploads.ContentType == "image/jpeg") || (uploads.ContentType == "image/png") || (uploads.ContentType == "image/jpg"))
                    {
                        string FileExtention = "jpg";
                        switch (uploads.ContentType)
                        {
                            case "image/jpeg":
                                FileExtention = "jpeg";
                                break;
                            case "image/png":
                                FileExtention = "png";
                                break;
                            case "image/jpg":                              
                            default:
                                FileExtention = "jpg";
                                break;
                        }
                    string file = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "." + FileExtention;//System.IO.Path.GetFileName(uploads.FileName).Split('.')[1];//System.IO.Path.GetFileName(uploads.FileName);
                        string DirName = DateTime.Now.ToString("yyyyMMdd");
                        string path = CreateDiretery(context.Server.MapPath(".") + "\\NewsImages\\" + DirName);
                        string UplodePathFile = ResizeImage(context.Request.Files["upload"], file, path, (uploads.ContentType.Split('/')[1]));
                        //uploads.SaveAs(path + "\\" + file);
                        string LinkURL = context.Request.Url.GetLeftPart(UriPartial.Authority); //+ Url.Content("~");
                        string url = LinkURL + "/NewsImages/" + DirName + "/" + file; // Location Where we Save the image
                        context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction( " + CKEditorFuncNum + ", \"" + url + "\");</script>");

                    }
                    else
                    {
                        context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction( " + CKEditorFuncNum + ", \"" + "Not a valid Image only uplode jpeg/jpg/png image" + "\");</script>");
                    }
                }
                catch (Exception Ex)
                {
                    context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction( " + CKEditorFuncNum + ", \"" + "Please check image size, It must be less than 3MB" + "\");</script>");
                    Logging.Log(LogLevel.Error, "CkediterImageUpload ErrorDeatils " + " : " + Ex.Message.ToString());
                }
           // }
            context.Response.End();
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        private string CreateDiretery(String Path)
        {
            try
            {
                if (!Directory.Exists(Path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(Path);
                }
            }
            catch (Exception Ex)
            {
                Logging.Log(LogLevel.Error, "CkediterImageUpload ErrorDeatils " + " : " + Ex.Message.ToString());
            }
            return Path;
        }
        

        public string ResizeImage(HttpPostedFile fileToUpload,string Filename,string URL,string fileFormate)
        {
            
            string myfile = Filename;

            try
            {
                using (Image image = Image.FromStream(fileToUpload.InputStream, true, false))
                {
                    var path = Path.Combine(URL, myfile);
                    try
                    {
                        //Size can be change according to your requirement 
                        float thumbWidth = 370F;
                        float thumbHeight = 280F;
                        //calculate  image  size
                        if (image.Width > image.Height)
                        {
                            thumbHeight = ((float)image.Height / image.Width) * thumbWidth;
                        }
                        else
                        {
                            thumbWidth = ((float)image.Width / image.Height) * thumbHeight;
                        }

                        int actualthumbWidth = Convert.ToInt32(Math.Floor(thumbWidth));
                        int actualthumbHeight = Convert.ToInt32(Math.Floor(thumbHeight));
                        var thumbnailBitmap = new Bitmap(actualthumbWidth, actualthumbHeight);
                        var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
                        thumbnailGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        thumbnailGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        thumbnailGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        var imageRectangle = new Rectangle(0, 0, actualthumbWidth, actualthumbHeight);
                        thumbnailGraph.DrawImage(image, imageRectangle);
                        var ms = new MemoryStream();
                        //thumbnailBitmap.Save(path, ImageFormat.Jpeg);
                        ImageFormat ImgFormate = ImageFormat.Jpeg;
                        //switch (fileFormate.ToUpper())
                        //{
                        //    case "JPEG":
                        //        ImgFormate = ImageFormat.Jpeg;
                        //        break;
                        //    case "JPG":
                        //        ImgFormate = ImageFormat.Jpeg;
                        //        break;
                        //    case "PNG":
                        //        ImgFormate = ImageFormat.Png;
                        //        break;
                        //    default:
                        //        ImgFormate = ImageFormat.Jpeg;
                        //        break;
                        //}
                        thumbnailBitmap.Save(path, ImgFormate);
                        ms.Position = 0;
                        GC.Collect();
                        thumbnailGraph.Dispose();
                        thumbnailBitmap.Dispose();
                        image.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Logging.Log(LogLevel.Error, "CkediterImageUpload ErrorDeatils " + " : " + ex.Message.ToString());
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log(LogLevel.Error, "CkediterImageUpload ErrorDeatils " + " : " + ex.Message.ToString());
                throw ex;
            }
            return myfile;
        }
    }
}