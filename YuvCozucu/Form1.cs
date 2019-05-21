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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int frame2 = 0;
        int ysayisi = 0;
        byte[] bytes3;
        byte[] bytes;
        int en = 0;
        int boy = 0;
        string pathSource;
        int x;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(en, boy);
            Image image;
            int pos = 0;
      

            pos = 0;
            for (int t = 0; t < frame2; t++)
            {
                for (int y = 0; y < boy; y++)
                {
                    for (int x = 0; x < en; x++)
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(bytes3[pos], bytes3[pos], bytes3[pos]));
                        pos++;

                    }

                }

                pictureBox1.Image = bmp;
                pictureBox1.Refresh();
                System.Threading.Thread.Sleep(1000/144);
              
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(en, boy);
            Image image;
            int pos = 0;
            pos = 0;
            for (int t = 0; t < frame2; t++)
            {
                for (int y = 0; y < boy; y++)
                {
                    for (int x = 0; x < en; x++)
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(bytes3[pos], bytes3[pos], bytes3[pos]));
                        pos++;

                    }
                }
                bmp.Save(Path.Combine("C:\\Users\\ARSLAN\\Desktop\\Frames", "Frame" + Convert.ToString(t + 1) + ".bmp"));
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(pathSource))
            {
                label5.Text = "Dosya seçimi yaptığınızdan emin olun.";
            }

            else
            {
                FileStream fsSource = new FileStream(pathSource, FileMode.Open, FileAccess.Read);
                bytes = new byte[fsSource.Length];
                int numBytesToRead = (int)fsSource.Length;
                int numBytesRead = 0;
                int x = comboBox1.SelectedIndex;
                int kontrol = 0;
                foreach (char chr in textBox1.Text)
                {
                    if (!Char.IsNumber(chr))
                    {
                        kontrol = 1;
                    }
                }

                foreach (char chr in textBox2.Text)
                {
                    if (!Char.IsNumber(chr))
                    {
                        kontrol = 1;
                    }
                }

                if (kontrol == 0)
                {
                    en = Int32.Parse(textBox1.Text);
                    boy = Int32.Parse(textBox2.Text);
                    double piksel = 0;
                    double frame = 0;


                    int boyut = (int)(fsSource.Length / 3);

                    while (numBytesToRead > 0)
                    {
                       
                        int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                        if (n == 0)
                            break;

                        numBytesRead += n;
                        numBytesToRead -= n;
                    }
                    numBytesRead = bytes.Length;


                    if (x == 0)
                    {
                        piksel = en * boy * 3;
                    }
                    if (x == 1)
                    {
                        piksel = en * boy * 2;
                    }
                    if (x == 2)
                    {
                        piksel = en * boy * 1.5;
                    }

                    frame = numBytesRead / piksel;
                    frame2 = (int)frame;
                    int bytesayisi = 0;

                    if (x == 0)
                    {
                        bytesayisi = (numBytesRead / 3) / frame2;
                        ysayisi = (numBytesRead / 3);
                    }

                    if (x == 1)
                    {
                        bytesayisi = (numBytesRead / 2) / frame2;
                        ysayisi = (numBytesRead / 2);
                    }

                    else if (x == 2)
                    {
                        bytesayisi = ((numBytesRead / 3) * 2) / frame2;
                        ysayisi = (numBytesRead / 3) * 2;
                    }
                    bytes3 = new byte[ysayisi];
                    

                    int a = bytesayisi;
                    int b = 0;
                    int c = 0;

                    if (x == 0)
                    {
                        for (int i = 0; i < (fsSource.Length); i++)
                        {
                            if (b < a)
                            {
                                bytes3[c] = bytes[i];
                                c++;
                                b++;
                            }
                            else if (b == (a * 3)-1)
                            {
                                b = 0;
                            }
                            else
                            {
                                b++;
                            }

                        }
                    }

                    else if (x == 1)
                    {
                        b = 0;
                        c = 0;
                        for (int i = 0; i < (fsSource.Length); i++)
                        {
                            if (b < a)
                            {
                                bytes3[c] = bytes[i];
                                c++;
                                b++;
                            }
                            else if (b == (a * 2) -1 )
                            {
                                b = 0;
                            }
                            else
                            {
                                b++;
                            }

                        }
                    }
                    else if (x == 2)
                    {
                        a = bytesayisi;
                        b = 0;
                        c = 0;
                        for (int i = 0; i < (fsSource.Length); i++)
                        {
                            if (b < a)
                            {
                                bytes3[c] = bytes[i];
                                c++;
                                b++;
                            }
                            else if (b == a + (a / 2) - 1)
                            {
                                b = 0;
                            }
                            else
                            {
                                b++;
                            }

                        }
                    }

                }

                else
                {
                    label1.Text = "En boy oranını tekrar giriniz.";
                }
            }
           
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string strfilename;

        if (openFileDialog1.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                strfilename = openFileDialog1.FileName;
                pathSource = strfilename;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
