using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;

namespace SlickOne.WebUtility
{
    public class FileHelper
    {
        /// <summary>
        /// 根据日期设置文件存放路径
        /// </summary>
        /// <param name="fileExtensionName"></param>
        /// <returns></returns>
        public static string GetFilePathFormattedByDate()
        {
            var filePath = System.DateTime.Now.Year.ToString()
                   + "\\" + System.DateTime.Now.Month.ToString()
                   + "\\" + System.DateTime.Now.Day.ToString();

            return filePath;
        }

        public static byte[] GetFileContent(HttpPostedFileBase file)
        {
            byte[] content;
            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                content = memoryStream.ToArray();
            }
            return content;
        }

        public static byte[] ResizeImage(HttpPostedFileBase file)
        {
             //得到处理后的图片
            byte[] fileContent = GetFileContent(file);

            Image toImage;
            using (MemoryStream mStream = new MemoryStream(fileContent))
            {
                toImage = GetThumbNail(100, 100, mStream);
            }

            //写文件到内存
            byte[] content;
            using (MemoryStream newStream = new MemoryStream())
            {
                ImageFormat imgFormat = GetImgFileFormatType(Path.GetExtension(file.FileName).Substring(1));
                toImage.Save(newStream, imgFormat);
                content = newStream.ToArray();
            }

            return content;
        }

         /// <summary>
        /// 取得图片缩略图
        /// </summary>
        /// <param name="toW">变化的宽</param>
        /// <param name="toH">变化的高</param>
        /// <param name="ImgStream">图片文件流</param>
        /// <param name="waterStr">如果为空,表示不加水印</param>
        /// <returns></returns>
        public static System.Drawing.Image GetThumbNail(
            int toW, int toH,
            System.IO.Stream ImgStream,
            string waterStr = null)
        {
            //原图
            System.Drawing.Image oImg = System.Drawing.Image.FromStream(ImgStream);
            //原图尺寸
            System.Drawing.Size orgSize = new System.Drawing.Size(oImg.Width, oImg.Height);
            //目标尺寸
            System.Drawing.Size toSize = new System.Drawing.Size(toW, toH);
            //真实尺寸
            System.Drawing.Size realSize = Resize(toSize, orgSize);
            //如果真实的尺寸 >大于原图尺寸 图片不做拉伸
            if (realSize.Width > orgSize.Width && realSize.Height > orgSize.Height)
            {
                realSize.Width = orgSize.Width;
                realSize.Height = orgSize.Height;
            }

            System.Drawing.Image bitmap = new System.Drawing.Bitmap(realSize.Width, realSize.Height);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.Clear(System.Drawing.Color.White);
                g.DrawImage(oImg, new System.Drawing.Rectangle(0, 0, realSize.Width, realSize.Height),
                    new System.Drawing.Rectangle(0, 0, orgSize.Width, orgSize.Height),
                    System.Drawing.GraphicsUnit.Pixel);
            }

            //add water
            if (!string.IsNullOrEmpty(waterStr))
            {
                return addWater(waterStr, bitmap);
            }
            else
            {
                return bitmap;
            }
        }

        /// <summary>
        /// 添加水印
        /// </summary>
        /// <param name="waterStr"></param>
        /// <param name="ImgStream"></param>
        /// <returns></returns>
        private static System.Drawing.Image addWater(string waterStr, System.Drawing.Image ImgStream)
        {
            //源图片
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(ImgStream);
            //水印图片
            System.Drawing.Image bitMapWater = new System.Drawing.Bitmap(waterStr);
            //加水印
            g.InterpolationMode = InterpolationMode.High;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.DrawImage(bitMapWater, ImgStream.Width / 2, ImgStream.Height / 2);

            return ImgStream;
        }

         /// <summary>
        /// 调整大小
        /// </summary>
        /// <param name="ViewSize">外框大小</param>
        /// <param name="ImageSize">图片的实际大小</param>
        /// <returns></returns>
        public static System.Drawing.Size Resize(
            System.Drawing.Size ViewSize,
            System.Drawing.Size ImageSize)
        {
            System.Drawing.Size MySize = new System.Drawing.Size();
            if (ViewSize.Width >= ImageSize.Width && ViewSize.Height >= ImageSize.Height)
            {
                MySize.Height = ImageSize.Height;
                MySize.Width = ImageSize.Width;
            }
            else if (ViewSize.Width > ViewSize.Height)//宽大于高
            {
                if (ImageSize.Width > ImageSize.Height)//按比例
                {
                    float scale = ImageSize.Height / (float)ImageSize.Width;
                    if (ViewSize.Height / (float)ViewSize.Width < scale)
                    {
                        MySize.Height = ViewSize.Height;
                        MySize.Width = (int)(ViewSize.Height / scale);
                    }
                    else
                    {
                        MySize.Width = ViewSize.Width;
                        MySize.Height = (int)(ViewSize.Width * scale);
                    }
                }
                else if (ImageSize.Height > ImageSize.Width)//非比例
                {
                    float scale = ImageSize.Width / (float)ImageSize.Height;
                    MySize.Height = ViewSize.Height;
                    MySize.Width = (int)(ViewSize.Height * scale);
                }
                else//正方
                {
                    MySize.Height = ViewSize.Height;
                    MySize.Width = ViewSize.Height;
                }
            }
            else if (ViewSize.Width < ViewSize.Height)//高大于宽
            {
                if (ImageSize.Width < ImageSize.Height)//按比例
                {
                    float scale = ImageSize.Width / (float)ImageSize.Height;
                    if (ViewSize.Width / (float)ViewSize.Height < scale)
                    {
                        MySize.Width = ViewSize.Width;
                        MySize.Height = (int)(ViewSize.Width / scale);
                    }
                    else
                    {
                        MySize.Height = ViewSize.Height;
                        MySize.Width = (int)(ViewSize.Height * scale);
                    }
                }
                else if (ImageSize.Height < ImageSize.Width)//非比例
                {
                    float scale = ImageSize.Height / (float)ImageSize.Width;
                    MySize.Width = ViewSize.Width;
                    MySize.Height = (int)(ViewSize.Width * scale);
                }
                else//正方
                {
                    MySize.Height = ViewSize.Width;
                    MySize.Width = ViewSize.Width;
                }
            }
            else//正方
            {
                if (ImageSize.Width > ImageSize.Height)//宽大于高
                {
                    float scale = ImageSize.Height / (float)ImageSize.Width;
                    MySize.Width = ViewSize.Width;
                    MySize.Height = (int)(ViewSize.Width * scale);
                }
                else if (ImageSize.Width < ImageSize.Height)//高大于宽
                {
                    float scale = ImageSize.Width / (float)ImageSize.Height;
                    MySize.Height = ViewSize.Height;
                    MySize.Width = (int)(ViewSize.Height * scale);
                }
                else//正方
                {
                    MySize.Height = ViewSize.Height;
                    MySize.Width = ViewSize.Height;
                }
            }
            return MySize;
        }

        /// <summary>
        /// 根据文件扩展名来获取要对图片进行格式化的方式
        /// </summary>
        /// <param name="ImgType"></param>
        /// <returns></returns>
        private static ImageFormat GetImgFileFormatType(string ImgType)
        {
            ImageFormat FormatType = ImageFormat.Jpeg;
            switch (ImgType)
            {
                case "BMP":     //BMP 
                    FormatType = ImageFormat.Bmp;
                    break;
                case "GIF":     //GIF 
                    FormatType = ImageFormat.Gif;
                    break;
                case "JPG":     //JPG 
                    FormatType = ImageFormat.Jpeg;
                    break;
                case "JPEG":    //JPEG
                    FormatType = ImageFormat.Jpeg;
                    break;
                case "TIF":    //TIF
                    FormatType = ImageFormat.Tiff;
                    break;
                case "JIFF":    //JIFF
                    FormatType = ImageFormat.Jpeg;
                    break;
                case "PNG":    //PNG
                    FormatType = ImageFormat.Png;
                    break;
                default:
                    FormatType = ImageFormat.Jpeg;
                    break;
            }
            return FormatType;
        }

        /// <summary>
        /// 写文件到物理磁盘
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="newFileName">新文件名称</param>
        /// <param name="fileContent">文件内容</param>
        /// <returns></returns>
        public static void SaveFileToPhysicalDisk(string filePath,
            string fileName,
            byte[] fileContent)
        {
            try
            {
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                string fullFileName = filePath + "\\" + fileName;
                using (FileStream fileStream = new FileStream(fullFileName, FileMode.Create))
                {
                    fileStream.Write(fileContent, 0, fileContent.Length);
                    fileStream.Flush();
                    fileStream.Close();
                }
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// 删除物理路径
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="isRecursived">是否递归</param>
        public static void DeletePathAndFiles(string filePath, bool isRecursived = false)
        {
            try
            {
                if (Directory.Exists(filePath))
                {
                    Directory.Delete(filePath, isRecursived);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
