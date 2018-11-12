using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Mc.Winform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Image<Rgba32> imageone = null;

        public Image<Rgba32> imagetwo = null;

        IImageFormat format = null;



        /// <summary>
        /// 合并图片 小图片放在大图片上面
        /// </summary>
        /// <param name="TempleBase64Str">模板大图片base64</param>
        /// <param name="OutputBase64Str">模板小图片base64</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">y坐标</param>
        /// <returns></returns>
        public ImageResponse MergeImage(string TempleBase64Str, string OutputBase64Str, int x, int y)
        {
            string strRet = null;
            //if (string.IsNullOrEmpty(TempleBase64Str))
            //{
            //    return new ImageResponse { success = false, errmsg = "请传入模板大图片base64" };
            //}
            //if (string.IsNullOrEmpty(OutputBase64Str))
            //{
            //    return new ImageResponse { success = false, errmsg = "请传入模板小图片base64" };
            //}
            if (x < 0 || y < 0)
            {
                return new ImageResponse { success = false, errmsg = "坐标不能传入负数" };
            }
            try
            {
                //byte[] templebytes = Convert.FromBase64String(TempleBase64Str);
                //byte[] outputbytes = Convert.FromBase64String(OutputBase64Str);
               
                var imagesTemle = imageone;
                //SixLabors.ImageSharp.Image.Load(templebytes, out format);
                var outputImg = imagetwo;
                    //SixLabors.ImageSharp.Image.Load(outputbytes);

                if (imagesTemle.Height - (outputImg.Height + y) <= 0)
                {
                    return new ImageResponse { success = false, errmsg = "Y坐标高度超限" };
                }
                if (imagesTemle.Width - (outputImg.Width + x) <= 0)
                {
                    return new ImageResponse { success = false, errmsg = "X坐标宽度超限" };
                }
                //进行多图片处理
                imagesTemle.Mutate(a =>
                {
                    //还是合并 
                    a.DrawImage(outputImg, 1, new SixLabors.Primitives.Point(x, y));
                });
                strRet = imagesTemle.ToBase64String(format);
                //imagesTemle.SaveAsPng();
                byte[] arr = Convert.FromBase64String(strRet);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                pictureBox3.Image = bmp;
                return new ImageResponse { success = true, base64Str = strRet };
            }
            catch (Exception ex)
            {
                return new ImageResponse { success = false, errmsg = "报错信息" + ex.Message };
            }
        }

        private void Base64StringToImage(string txtFileName)
        {
            try
            {
                FileStream ifs = new FileStream(txtFileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(ifs);

                String inputStr = sr.ReadToEnd();
                byte[] arr = Convert.FromBase64String(inputStr);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                //bmp.Save(txtFileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //bmp.Save(txtFileName + ".bmp", ImageFormat.Bmp);
                //bmp.Save(txtFileName + ".gif", ImageFormat.Gif);
                //bmp.Save(txtFileName + ".png", ImageFormat.Png);
                ms.Close();
                sr.Close();
                ifs.Close();
                this.pictureBox2.Image = bmp;
                if (File.Exists(txtFileName))
                {
                    File.Delete(txtFileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Base64StringToImage 转换失败/nException：" + ex.Message);
            }
        }  


        private void button1_Click(object sender, EventArgs e)
        {
            var result=MergeImage("","",100,100);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:";
            ofd.Title = "打开文件";
            if (ofd.ShowDialog()==DialogResult.OK) {
                imageone= SixLabors.ImageSharp.Image.Load(ofd.FileName,out format);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:";
            ofd.Title = "打开文件";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imagetwo = SixLabors.ImageSharp.Image.Load(ofd.FileName);
            }

        }
    }

    public class ImageResponse {
        public bool success { set; get; }
        public string base64Str { set; get; }
        public string errmsg { set; get; }
    }


}
