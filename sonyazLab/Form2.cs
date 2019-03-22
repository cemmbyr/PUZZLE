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

namespace sonyazLab
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        

        private void Form2_Load(object sender, EventArgs e)
        {
            String puann = Convert.ToString(Form1.puan2);
            label3.Text = puann;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            String Kullanici = textBox1.Text;
            StreamWriter sw = File.AppendText("D:\\Skor.txt");
            sw.WriteLine(Kullanici+" "+Form1.puan2);
            sw.Close();

            this.Close();
            Application.Exit();

        }
    }
}
