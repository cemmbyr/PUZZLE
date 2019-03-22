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

namespace sonyazLab
{
    public partial class Form1 : Form
    {
        Image[] imgarray = new Image[16];
        Image[] imgdogru = new Image[16];
        Bitmap image1, image2;
        int a = 0;
        int sayaç = 0, puan = 0, skor, buyuk = 0, temp, karsılastır = 0,puan3=0;
        String buyuk2;
        public static int puan2 = 0;
        int[] dizi = new int[2];
        String isim;
        List<String> kullanicilar = new List<String>();
        List<int> skorlar = new List<int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            openFileDialog1.FileName = "Dosya Seçiniz";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.Title = "Hızlı Resim";
            openFileDialog1.InitialDirectory = "C:\\";


            using (StreamReader sr = new StreamReader("D:\\Skor.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    string str;
                    string[] strArray;
                    str = sr.ReadLine();

                    strArray = str.Split(' ');
                    isim = strArray[0];
                    skor = int.Parse(strArray[1]);
                    kullanicilar.Add(isim);
                    skorlar.Add(skor);
                }

            }
            
            if (kullanicilar.Count != 0 && skorlar.Count != 0)
            {
                for (int i = 0; i < skorlar.Count-1; i++)
                {
                    for (int j = 0; j < skorlar.Count-i-1 ; j++)
                    {
                        if (skorlar[j] > skorlar[j+1])
                        {
                            buyuk = skorlar[j];
                            skorlar[j] = skorlar[j + 1];
                            skorlar[j + 1] = buyuk;
                            buyuk2 = kullanicilar[j];
                            kullanicilar[j] = kullanicilar[j + 1];
                            kullanicilar[j + 1] = buyuk2;

                        }
                    }
                }
            }
            if (kullanicilar.Count != 0 && skorlar.Count != 0)
            {
                for (int i = skorlar.Count-1 ; i >= 0; i--)
                {
                    //richTextBox1.Text = kullanicilar[i] + " " + skorlar[i];
                    richTextBox1.AppendText(kullanicilar[i] + " " + skorlar[i]+"\n");
                }
            }
                
            
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox9.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox10.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox11.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox12.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox13.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox14.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox15.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox16.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a = 0;
            puan = 0;
            sayaç++;
            Directory.CreateDirectory(@"D:\" + sayaç);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                // DOSYAYI AÇTIK VE RESMİMİZİ 16 EŞ PARÇAYA BÖLDÜK
                Bitmap bmp = new Bitmap(Image.FromFile(openFileDialog1.FileName));
                int yukseklik = bmp.Height / 4;
                int genislik = bmp.Width / 4;
                var img = Image.FromFile(openFileDialog1.FileName);
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        var index = i * 4 + j;
                        imgarray[index] = new Bitmap(genislik, yukseklik);
                        var graphics = Graphics.FromImage(imgarray[index]);
                        graphics.DrawImage(img, new Rectangle(0, 0, genislik, yukseklik), new Rectangle(j * genislik, i * yukseklik, genislik, yukseklik), GraphicsUnit.Pixel);
                        graphics.Dispose();
                    }
                }
                //RESMİMİZİ (RESİMLER) ADLI DOSYAMIZA KAYIT ETTİK
                for (int d = 0; d < 16; d++)
                {
                    Image parcaResim = imgarray[d];
                    parcaResim.Save(@"D:\" + sayaç + "\\" + d + "x" + d + ".jpg", ImageFormat.Jpeg);
                }
                // RESİMLERİN DOĞRU HALİNİ TUTAN DİZİMİZİ OLUŞTURDUK
                for (int i = 0; i < 16; i++)
                {
                    imgdogru[i] = Image.FromFile(@"D:\" + sayaç + "\\" + i + "x" + i + ".jpg");
                }
                // RESMİ KARIŞTIRMAK İÇİN RASTGELE SAYILARIMIZI OLUŞTURUYORUZ
                Random rnd = new Random();
                int RastgeleSayi = rnd.Next(0, 16);
                int[] sayilar = new int[16];
                sayilar[0] = RastgeleSayi;
                for (int i = 1; i < sayilar.Length; i++)
                {
                    RastgeleSayi = rnd.Next(0, 16);
                    for (int j = 0; j < i; j++)
                    {
                        if (sayilar[j] == RastgeleSayi)
                        {
                            i--;
                            break;
                        }
                        else
                        {
                            sayilar[i] = RastgeleSayi;
                        }
                    }
                }
                //PİCTUREBOXLARA RASTGELE PARÇALARI ATIYORUZ
                pictureBox1.Image = imgdogru[sayilar[0]];
                pictureBox2.Image = imgdogru[sayilar[1]];
                pictureBox3.Image = imgdogru[sayilar[2]];
                pictureBox4.Image = imgdogru[sayilar[3]];
                pictureBox5.Image = imgdogru[sayilar[4]];
                pictureBox6.Image = imgdogru[sayilar[5]];
                pictureBox7.Image = imgdogru[sayilar[6]];
                pictureBox8.Image = imgdogru[sayilar[7]];
                pictureBox9.Image = imgdogru[sayilar[8]];
                pictureBox10.Image = imgdogru[sayilar[9]];
                pictureBox11.Image = imgdogru[sayilar[10]];
                pictureBox12.Image = imgdogru[sayilar[11]];
                pictureBox13.Image = imgdogru[sayilar[12]];
                pictureBox14.Image = imgdogru[sayilar[13]];
                pictureBox15.Image = imgdogru[sayilar[14]];
                pictureBox16.Image = imgdogru[sayilar[15]];

                //BU DİZİMİZDE KARŞIK ELEMANLARI TUTUYORUZ
                imgarray[0] = pictureBox1.Image;
                imgarray[1] = pictureBox2.Image;
                imgarray[2] = pictureBox3.Image;
                imgarray[3] = pictureBox4.Image;
                imgarray[4] = pictureBox5.Image;
                imgarray[5] = pictureBox6.Image;
                imgarray[6] = pictureBox7.Image;
                imgarray[7] = pictureBox8.Image;
                imgarray[8] = pictureBox9.Image;
                imgarray[9] = pictureBox10.Image;
                imgarray[10] = pictureBox11.Image;
                imgarray[11] = pictureBox12.Image;
                imgarray[12] = pictureBox13.Image;
                imgarray[13] = pictureBox14.Image;
                imgarray[14] = pictureBox15.Image;
                imgarray[15] = pictureBox16.Image;

                for (int i = 0; i < 16; i++)
                {
                    image1 = new Bitmap(imgarray[i]);
                    image2 = new Bitmap(imgdogru[i]);
                    if (compare(image1, image2))
                    {
                        karsılastır++;
                        puan = puan + 5;
                        puan2 = puan;
                    }
                }
                if (karsılastır != 0)
                {
                    label2.Text = "Oyuna Başlayabilirsiniz";
                }
                else
                {

                    label2.Text = "Eşleşen En Az bir Parça Yok Lütfen Karıştırın";
                }
            }
            if (karsılastır == 15)
            {
                puan2 = 100;
                Form2 ac = new Form2();
                ac.Show();
            }
            karsılastır = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form2 ac = new Form2();
            ac.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int RastgeleSayi = rnd.Next(0, 16);
            int[] sayilar = new int[16];
            sayilar[0] = RastgeleSayi;
            for (int i = 1; i < sayilar.Length; i++)
            {
                RastgeleSayi = rnd.Next(0, 16);
                for (int j = 0; j < i; j++)
                {
                    if (sayilar[j] == RastgeleSayi)
                    {
                        i--;
                        break;
                    }
                    else
                    {
                        sayilar[i] = RastgeleSayi;
                    }
                }
            }
            pictureBox1.Image = imgdogru[sayilar[0]];
            pictureBox2.Image = imgdogru[sayilar[1]];
            pictureBox3.Image = imgdogru[sayilar[2]];
            pictureBox4.Image = imgdogru[sayilar[3]];
            pictureBox5.Image = imgdogru[sayilar[4]];
            pictureBox6.Image = imgdogru[sayilar[5]];
            pictureBox7.Image = imgdogru[sayilar[6]];
            pictureBox8.Image = imgdogru[sayilar[7]];
            pictureBox9.Image = imgdogru[sayilar[8]];
            pictureBox10.Image = imgdogru[sayilar[9]];
            pictureBox11.Image = imgdogru[sayilar[10]];
            pictureBox12.Image = imgdogru[sayilar[11]];
            pictureBox13.Image = imgdogru[sayilar[12]];
            pictureBox14.Image = imgdogru[sayilar[13]];
            pictureBox15.Image = imgdogru[sayilar[14]];
            pictureBox16.Image = imgdogru[sayilar[15]];

            imgarray[0] = pictureBox1.Image;
            imgarray[1] = pictureBox2.Image;
            imgarray[2] = pictureBox3.Image;
            imgarray[3] = pictureBox4.Image;
            imgarray[4] = pictureBox5.Image;
            imgarray[5] = pictureBox6.Image;
            imgarray[6] = pictureBox7.Image;
            imgarray[7] = pictureBox8.Image;
            imgarray[8] = pictureBox9.Image;
            imgarray[9] = pictureBox10.Image;
            imgarray[10] = pictureBox11.Image;
            imgarray[11] = pictureBox12.Image;
            imgarray[12] = pictureBox13.Image;
            imgarray[13] = pictureBox14.Image;
            imgarray[14] = pictureBox15.Image;
            imgarray[15] = pictureBox16.Image;


            for (int i = 0; i < 16; i++)
            {
                image1 = new Bitmap(imgarray[i]);
                image2 = new Bitmap(imgdogru[i]);
                if (compare(image1, image2))
                {
                    karsılastır++;
                    puan = puan + 5;
                    puan2 = puan;
                }
            }
            puan = 0;
            if (karsılastır != 0)
            {
                label2.Text = "Oyuna Başlayabilirsiniz";
            }
            else
            {
                label2.Text = "Eşleşen En Az bir Parça Yok Lütfen Karıştırın";
            }
            if (karsılastır == 15)
            {
                puan2 = 100;
                Form2 ac = new Form2();
                ac.Show();
            }
            karsılastır = 0;

        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (dizi[0] != 0)
            {
                dizi[1] = 1;
            }
            if (dizi[1] == 0)
            {
                dizi[0] = 1;
            }
            if (dizi[0] != 0 && dizi[1] == 1)
            {

                degisim();

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (dizi[0] != 0)
            {
                dizi[1] = 2;
            }
            if (dizi[1] == 0)
            {
                dizi[0] = 2;
            }
            if (dizi[0] != 0 && dizi[1] == 2)
            {

                degisim();

            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (dizi[0] != 0)
            {
                dizi[1] = 3;
            }
            if (dizi[1] == 0)
            {
                dizi[0] = 3;
            }
            if (dizi[0] != 0 && dizi[1] == 3)
            {
                degisim();

            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (dizi[0] != 0)
            {
                dizi[1] = 4;
            }
            if (dizi[1] == 0)
            {
                dizi[0] = 4;
            }
            if (dizi[0] != 0 && dizi[1] == 4)
            {

                degisim();

            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (dizi[0] != 0)
            {
                dizi[1] = 5;
            }
            if (dizi[1] == 0)
            {
                dizi[0] = 5;
            }
            if (dizi[0] != 0 && dizi[1] == 5)
            {

                degisim();

            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (dizi[0] != 0)
            {
                dizi[1] = 6;
            }
            if (dizi[1] == 0)
            {
                dizi[0] = 6;
            }
            if (dizi[0] != 0 && dizi[1] == 6)
            {

                degisim();

            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (dizi[0] != 0)
            {
                dizi[1] = 7;
            }
            if (dizi[1] == 0)
            {
                dizi[0] = 7;
            }
            if (dizi[0] != 0 && dizi[1] == 7)
            {
                degisim();

            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (dizi[0] != 0)
            {
                dizi[1] = 8;
            }
            if (dizi[1] == 0)
            {
                dizi[0] = 8;
            }
            if (dizi[0] != 0 && dizi[1] == 8)
            {

                degisim();

            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (dizi[0] != 0)
            {
                dizi[1] = 9;
            }
            if (dizi[1] == 0)
            {
                dizi[0] = 9;
            }
            if (dizi[0] != 0 && dizi[1] == 9)
            {

                degisim();

            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            if (dizi[0] != 0)
            {
                dizi[1] = 10;
            }
            if (dizi[1] == 0)
            {
                dizi[0] = 10;
            }
            if (dizi[0] != 0 && dizi[1] == 10)
            {

                degisim();

            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            if (dizi[0] != 0)
            {
                dizi[1] = 11;
            }
            if (dizi[1] == 0)
            {
                dizi[0] = 11;
            }
            if (dizi[0] != 0 && dizi[1] == 11)
            {

                degisim();

            }
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            if (dizi[0] != 0)
            {
                dizi[1] = 12;
            }
            if (dizi[1] == 0)
            {
                dizi[0] = 12;
            }
            if (dizi[0] != 0 && dizi[1] == 12)
            {

                degisim();

            }
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            if (dizi[0] != 0)
            {
                dizi[1] = 13;
            }
            if (dizi[1] == 0)
            {
                dizi[0] = 13;
            }
            if (dizi[0] != 0 && dizi[1] == 13)
            {
                degisim();

            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            if (dizi[0] != 0)
            {
                dizi[1] = 14;
            }
            if (dizi[1] == 0)
            {
                dizi[0] = 14;
            }
            if (dizi[0] != 0 && dizi[1] == 14)
            {

                degisim();

            }
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            if (dizi[0] != 0)
            {
                dizi[1] = 15;
            }
            if (dizi[1] == 0)
            {
                dizi[0] = 15;
            }
            if (dizi[0] != 0 && dizi[1] == 15)
            {

                degisim();

            }
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            if (dizi[0] != 0)
            {
                dizi[1] = 16;
            }
            if (dizi[1] == 0)
            {
                dizi[0] = 16;
            }
            if (dizi[0] != 0 && dizi[1] == 16)
            {

                degisim();

            }
        }

        private bool compare(Bitmap bmp1, Bitmap bmp2)
        {
            bool equals = true;
            bool flag = true;

            if (bmp1.Size == bmp2.Size)
            {
                for (int x = 0; x < bmp1.Width; ++x)
                {
                    for (int y = 0; y < bmp1.Height; ++y)
                    {
                        if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                        {
                            equals = false;
                            flag = false;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        break;
                    }
                }
            }
            else
            {
                equals = false;
            }
            return equals;
        }
        public void degisim()
        {
            a++;
            int x, y;
            x = dizi[0];
            y = dizi[1];
            pictureBox17.Image = imgarray[x - 1];
            imgarray[x - 1] = imgarray[y - 1];
            imgarray[y - 1] = pictureBox17.Image;
            pictureBox17.Image = null;

            pictureBox1.Image = imgarray[0];
            pictureBox2.Image = imgarray[1];
            pictureBox3.Image = imgarray[2];
            pictureBox4.Image = imgarray[3];
            pictureBox5.Image = imgarray[4];
            pictureBox6.Image = imgarray[5];
            pictureBox7.Image = imgarray[6];
            pictureBox8.Image = imgarray[7];
            pictureBox9.Image = imgarray[8];
            pictureBox10.Image = imgarray[9];
            pictureBox11.Image = imgarray[10];
            pictureBox12.Image = imgarray[11];
            pictureBox13.Image = imgarray[12];
            pictureBox14.Image = imgarray[13];
            pictureBox15.Image = imgarray[14];
            pictureBox16.Image = imgarray[15];

            dizi[0] = 0;
            dizi[1] = 0;

            for (int i = 0; i < 16; i++)
            {
                image1 = new Bitmap(imgarray[i]);
                image2 = new Bitmap(imgdogru[i]);
                if (compare(image1, image2))
                {
                    puan = puan + 5;
                    puan2 = puan;

                }
            }
            if (a%15==0)
            {
                puan3 = puan3 - 10;
                Console.WriteLine("Uğradık");
            }
            puan2 = puan2 + puan3;
            if (puan == 80 && a<15)
            {
                puan2 = 100;
                Form2 ac = new Form2();
                ac.Show();
            }
            else if(puan==80 && a > 15)
            {
                Form2 ac = new Form2();
                ac.Show();
            }
            puan = 0;
            
        }
    }
}

