using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using ThoughtWorks.QRCode.Codec;

namespace CreateQR
{
    /// <summary>
    /// index 的摘要说明
    /// </summary>
    public class index : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string url = context.Request.QueryString["url"];
            if (!string.IsNullOrEmpty(url)) {
                Bitmap bmp = GetDimensionalCode(url);
                MemoryStream ms = new MemoryStream();
                try
                {
                    bmp.Save(ms, ImageFormat.Png);
                    context.Response.ClearContent();
                    context.Response.ContentType = "image/Png";
                    context.Response.BinaryWrite(ms.ToArray());
                }
                finally
                {
                    //显式释放资源 
                    bmp.Dispose();
                }
            }            
        }

        /// <summary>
        /// 根据链接获取二维码
        /// </summary>
        /// <param name="link">链接</param>
        /// <returns>返回二维码图片</returns>
        private Bitmap GetDimensionalCode(string link)
        {
            Bitmap bmp = null;
            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = 4;
                //int version = Convert.ToInt16(cboVersion.Text);
                qrCodeEncoder.QRCodeVersion = 7;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                bmp = qrCodeEncoder.Encode(link);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Invalid version !");
            }
            return bmp;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}