using System;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SlickOne.WebUtility
{
    public static class ImageHelper
    {
        /// <summary>
        /// 随机生成字符数字组合
        /// http://weblogs.asp.net/karlapudi/archive/2008/05/13/custom-captcha-image-action-result-for-asp-net-mvc.aspx
        /// </summary>
        /// <param name="numberOfChars"></param>
        /// <returns></returns>
        public static string SelectRandom(int numberOfChars)
        {
            if (numberOfChars > 36)
            {
                throw new InvalidOperationException("随机字符长度不能大于 36.");
            }

            char[] columns = new char[36];

            for (int charPos = 65; charPos < 65 + 26; charPos++)
                columns[charPos - 65] = (char)charPos;

            for (int intPos = 48; intPos <= 57; intPos++)
                columns[26 + (intPos - 48)] = (char)intPos;

            StringBuilder randomBuilder = new StringBuilder(1024);
            Random randomSeed = new Random();
            for (int incr = 0; incr < numberOfChars; incr++)
            {
                randomBuilder.Append(columns[randomSeed.Next(36)].ToString());
            }
            return randomBuilder.ToString();
        }

        public static void OverlapEllipse()
        {

        }
    }
}

