using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DragControl
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
        }
        //�����ƶ�����
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            button1.Tag = e.Location;//�õ���ť���������
            DoDragDrop(button1, DragDropEffects.Move);//��ʼ�зŲ���
        }
        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            button2.Tag = e.Location;//�õ���ť���������
            DoDragDrop(button2, DragDropEffects.Move);//��ʼ�зŲ���
        }
        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            button3.Tag = e.Location;//�õ���ť���������
            DoDragDrop(button3, DragDropEffects.Move);//��ʼ�зŲ���
        }
        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            button4.Tag = e.Location;//�õ���ť���������
            DoDragDrop(button4, DragDropEffects.Move);//��ʼ�зŲ���
        }
        //��
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            //�жϽӶ��ĸ���ť����//�ƶ��������
            object data = e.Data.GetData(typeof(Button));
            if (data == button1)
            {
                button1.Top = this.PointToClient(//���㰴ť��X����
                    new Point(e.X, e.Y)).Y - ((Point)button1.Tag).Y;
                button1.Left = this.PointToClient(//���㰴ť��Y����
                    new Point(e.X, e.Y)).X - ((Point)button1.Tag).X;
            }
            if (data == button2)
            {
                button2.Top = this.PointToClient(//���㰴ť��X����
                    new Point(e.X, e.Y)).Y - ((Point)button2.Tag).Y;
                button2.Left = this.PointToClient(//���㰴ť��Y����
                    new Point(e.X, e.Y)).X - ((Point)button2.Tag).X;
            }
            if (data == button3)
            {
                button3.Top = this.PointToClient(//���㰴ť��X����
                    new Point(e.X, e.Y)).Y - ((Point)button3.Tag).Y;
                button3.Left = this.PointToClient(//���㰴ť��Y����
                    new Point(e.X, e.Y)).X - ((Point)button3.Tag).X;
            }
            if (data == button4)
            {
                button4.Top = this.PointToClient(//���㰴ť��X����
                    new Point(e.X, e.Y)).Y - ((Point)button4.Tag).Y;
                button4.Left = this.PointToClient(//���㰴ť��Y����
                    new Point(e.X, e.Y)).X - ((Point)button4.Tag).X;
            }
        }
        //�����Ժ��ַ�ʽ�ƶ�
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;//�����з�Ч��
        }
    }
}