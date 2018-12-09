using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mc.Winform
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var width = 286;
            var height = 512;
            var titleFormat = new StringFormat(StringFormatFlags.DisplayFormatControl);
            titleFormat.LineAlignment = StringAlignment.Center;
            titleFormat.Alignment = StringAlignment.Center;
            var titleFont = new Font("宋体", 12, FontStyle.Bold);
            var biaoyuFont = new Font("宋体", 10, FontStyle.Bold);

            //titleFormat.LineAlignment
            //背景图片
            System.Drawing.Image image = System.Drawing.Image.FromFile(AppContext.BaseDirectory+ "images/bg-house-property.png");
            Bitmap bitmap = new Bitmap(image, width, height);
            Graphics g = Graphics.FromImage(bitmap);

            var text = "辽大二部 新上房源 南北通透 全明户型 不临街 户口没问题 能落户";
            float fontSize = 12.0f;             //字体大小
            float textWidth = text.Length * fontSize;  //文本的长度
            //下面定义一个矩形区域，以后在这个矩形里画上白底黑字
            float rectX = 16;
            float rectY =260;
            float rectWidth = text.Length * (fontSize + 8);
            float rectHeight = fontSize + 8;
            //声明矩形域
            RectangleF textArea = new RectangleF(rectX, rectY, rectWidth, rectHeight);
            var titleHeight = ((int)g.MeasureString(text, titleFont, width - 32, StringFormat.GenericTypographic).Height);
            textArea.Size = new Size(width-32, ((int)g.MeasureString(text,titleFont , width - 32, StringFormat.GenericTypographic).Height));

            Font font = new Font("宋体", fontSize);   //定义字体
            
            Brush whiteBrush = new SolidBrush(Color.Black);   //白笔刷，画文字用
            Brush blackBrush = new SolidBrush(Color.Transparent);   //黑笔刷，画背景用

            g.FillRectangle(blackBrush, rectX, rectY, rectWidth, rectHeight);

            System.Drawing.Image cover = System.Drawing.Image.FromFile(AppContext.BaseDirectory + "images/houseimage.jpg");
            System.Drawing.Image erweima = System.Drawing.Image.FromFile(AppContext.BaseDirectory + "images/mini.png");
            //画封面图
            g.DrawString("租房子找芒果!", biaoyuFont, new SolidBrush(Color.Black),65, 45);
            g.DrawString("值得信赖!", biaoyuFont,new SolidBrush(Color.Black),65, 65);
            g.DrawImage(cover,20,94,244,129);
            //画Title
            g.DrawString(text, titleFont, whiteBrush, textArea, titleFormat);
            g.DrawString("总价300万", new Font("宋体", 11), new SolidBrush(Color.FromArgb(254, 120, 93)), 16, titleHeight + 300);
            g.DrawString("4室2厅1卫 • 105㎡", new Font("宋体", 10), new SolidBrush(Color.FromArgb(187, 187, 187)), 16, titleHeight + 330);
            StringFormat formatarea = new StringFormat(StringFormatFlags.DisplayFormatControl);
            
            formatarea.Alignment = StringAlignment.Far;
           
            g.DrawString("大东区-陶瓷城", new Font("宋体", 10), new SolidBrush(Color.FromArgb(187, 187, 187)), width-16, titleHeight + 330,formatarea);
            g.DrawImage(erweima,(width-80)/2 , titleHeight+360, 80, 80);
            
            //StringFormat.LineAlignment = StringAlignment.Center;
            StringFormat format = new StringFormat(StringFormatFlags.DisplayFormatControl);            
            format.Alignment = StringAlignment.Center;
            //var memoWidth=g.MeasureString("长按扫码查看详情", new Font("宋体", 10)).Width;
            g.DrawString("长按扫码查看详情",new Font("宋体",10),new SolidBrush(Color.FromArgb(187,187,187)),(width / 2), titleHeight+450, format);
            MemoryStream ms = new MemoryStream();
            //保存为Jpg类型
            bitmap.Save(ms, ImageFormat.Jpeg);
            pictureBox1.Image = bitmap;
        }

        //public void wordsWrap(Graphics ctx,string title,float maxWidth, float startX,float startY,float height) {
        //    float lineWidth = 0;
        //    float lastSubStrIndex = 0;
        //    for (float i = 0; i < title.length; i++)
        //    {
        //        lineWidth += ctx.MeasureString(name[i]).width;
        //        if (lineWidth > maxWidth)
        //        {
        //            ctx.fillText(name.substring(lastSubStrIndex, i), startX, srartY);
        //            srartY += wordsHight;
        //            lineWidth = 0;
        //            lastSubStrIndex = i;
        //        }
        //        if (i == name.length - 1)
        //        {
        //            ctx.fillText(name.substring(lastSubStrIndex, i + 1), startX, srartY);
        //        }
        //    }
        //}
    }
}
