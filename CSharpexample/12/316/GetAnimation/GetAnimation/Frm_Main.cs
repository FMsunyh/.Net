using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GetAnimation
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
        }

        public int intImage = -1;//�ؼ�ͼƬ����
        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Image = imageList1.Images[//����ͼƬ����
                intImage = intImage++ > 3 ? 0 : intImage];
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;//������ʱ��
        }
    }
}