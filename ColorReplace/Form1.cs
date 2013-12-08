using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ColorReplace
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string img_file = string.Empty;

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// 将图像中某些颜色转换为透明
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="colors">要转换为透明的颜色</param>
        private Bitmap MakeImageTransparent(Image img, List<Color> colors)
        {
            Bitmap bmp = (Bitmap)img;
            Bitmap newBmp = new Bitmap(bmp.Width, bmp.Height);
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color pix = bmp.GetPixel(x, y);
                    if (colors.Contains(pix))
                    {
                        newBmp.SetPixel(x, y, Color.Transparent);
                    }
                    else
                    {
                        newBmp.SetPixel(x, y, pix);
                    }
                }
            }
            return newBmp;
        }

        /// <summary>
        /// 如果不是透明色，用其他颜色代替
        /// </summary>
        /// <param name="img"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private Bitmap MakeOtherColorIfNotTransparent(Image img, Color color)
        {
            Bitmap bmp = (Bitmap)img;

            Bitmap newBmp = new Bitmap(bmp.Width, bmp.Height);
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color pix = bmp.GetPixel(x, y);
                    if (pix != Color.FromArgb(0,255,255,255))
                    {
                        newBmp.SetPixel(x, y, color);
                    }
                    else
                    {
                        newBmp.SetPixel(x, y, Color.Transparent);
                    }
                }
            }
            return newBmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.img_file == string.Empty)
            {
                MessageBox.Show("请选择文件！");
            }
            else
            {
                Image img = Image.FromFile(img_file);
                List<Color> colors = new List<Color>();
                if (listBox1.Items.Count > 0)
                {
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        string line = listBox1.Items[i].ToString();
                        string[] arr = line.Split(new char[] { ',' });
                        int c1 = int.Parse(arr[0]);
                        int c2 = int.Parse(arr[1]);
                        int c3 = int.Parse(arr[2]);
                        int c4 = int.Parse(arr[3]);
                        Color cr = Color.FromArgb(c1, c2, c3, c4);
                        colors.Add(cr);
                    }
                    Bitmap bmpnew = MakeImageTransparent(img, colors);
                    string filename = DateTime.Now.Ticks.ToString() + ".bmp";
                    bmpnew.Save(filename);
                    MessageBox.Show("ok!");
                }
                else
                {
                    MessageBox.Show("没有指定颜色");
                }


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "图片|*.jpg";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.img_file = ofd.FileName;
                textBox1.Text = this.img_file;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] s = textBox3.Text.Split(new char[] { ',' });
            bool ts = true;
            if (s.Length == 4)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    try
                    {
                        int.Parse(s[i]);
                    }
                    catch
                    {
                        ts = false;
                    }
                }
            }
            else
            {
                ts = false;
            }
            if (!ts)
            {
                MessageBox.Show("无效的ARGB格式");
            }
            else
            {
                listBox1.Items.Add(textBox3.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.img_file == string.Empty)
            {
                MessageBox.Show("请选择文件！");
            }
            else
            {
                Image img = Image.FromFile(img_file);
                List<Color> colors = new List<Color>();
                if (listBox1.Items.Count > 0)
                {

                    string line = listBox1.Items[0].ToString();
                    string[] arr = line.Split(new char[] { ',' });
                    int c1 = int.Parse(arr[0]);
                    int c2 = int.Parse(arr[1]);
                    int c3 = int.Parse(arr[2]);
                    int c4 = int.Parse(arr[3]);
                    Color cr = Color.FromArgb(c1, c2, c3, c4);


                    Bitmap bmpnew = MakeOtherColorIfNotTransparent(img, cr);
                    string filename = DateTime.Now.Ticks.ToString() + ".bmp";
                    bmpnew.Save(filename);
                    MessageBox.Show("ok!");
                }
                else
                {
                    MessageBox.Show("没有指定颜色");
                }


            }
        }
    }
}
