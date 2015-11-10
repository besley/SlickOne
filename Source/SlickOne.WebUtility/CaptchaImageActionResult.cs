using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace SlickOne.WebUtility
{
    /// <summary>
    /// 生成字符和数字的图片
    /// </summary>
    public class CaptchaImageActionResult : ActionResult
    {
        public Color BackGroundColor { get; set; }
        public Color RandomTextColor { get; set; }
        public string RandomWord { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            Bitmap bmp = new Bitmap(150, 60);
            Graphics graph = Graphics.FromImage(bmp);

            graph.Clear(BackGroundColor);

            SolidBrush brush = new SolidBrush(RandomTextColor);

            Font font = null;
            string myFont, str;

            string[] crypticFonts = new string[11];
            crypticFonts[0] = "Arial";
            crypticFonts[1] = "Verdana";
            crypticFonts[2] = "Comic Sans MS";
            crypticFonts[3] = "Impact";
            crypticFonts[4] = "Haettenschweiler";
            crypticFonts[5] = "Lucida Sans Unicode";
            crypticFonts[6] = "Garamond";
            crypticFonts[7] = "Courier New";
            crypticFonts[8] = "Book Antiqua";
            crypticFonts[9] = "Arial Narrow";
            crypticFonts[10] = "Estrangelo Edessa";

            for (var i = 0; i <= RandomWord.Length - 1; i++)
            {
                myFont = crypticFonts[new Random().Next(i)];
                font = new Font(myFont, 18, FontStyle.Bold | FontStyle.Italic | FontStyle.Strikeout);
                str = RandomWord.Substring(i, 1);
                graph.DrawString(str, font, brush, i * 20, 20);
                graph.Flush();
            }

            context.HttpContext.Response.ContentType = "image/GF";
            bmp.Save(context.HttpContext.Response.OutputStream, ImageFormat.Gif);
            font.Dispose();
            graph.Dispose();
            bmp.Dispose();
        }
    }
}
