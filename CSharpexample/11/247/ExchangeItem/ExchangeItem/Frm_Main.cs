using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace ExchangeItem
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
        }

        public void AddList()//�������
        {
            string P_Connection = string.Format(//�������ݿ������ַ���
                "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=test.mdb;User Id=Admin");
            OleDbConnection P_OLEDBConnection = //�������Ӷ���
                new OleDbConnection(P_Connection);
            P_OLEDBConnection.Open();//���ӵ����ݿ�
            OleDbCommand P_OLEDBCommand = new OleDbCommand(//�����������
                "select * from [message]",
                P_OLEDBConnection);
            OleDbDataReader P_Reader = //�õ����ݶ�ȡ��
                P_OLEDBCommand.ExecuteReader();
            while (P_Reader.Read())//��ȡ����
            {
                lb_Source.Items.Add(P_Reader[0]);//�����ݷ��뼯��
            }
        }

        private void button2_Click(object sender, EventArgs e)//ȫ����ӵ�ѡ�������
        {
            for (int i = 0; i < lb_Source.Items.Count; i++)
            {
                lb_Source.SelectedIndex = i;//������ѡ����
                lb_Choose.Items.Add(//�������
                    lb_Source.SelectedItem.ToString());
            }
            lb_Source.Items.Clear();//�����
        }

        private void button3_Click(object sender, EventArgs e)//ȫ����ӵ�����Դ��
        {
            for (int i = 0; i < lb_Choose.Items.Count; i++)
            {
                lb_Choose.SelectedIndex = i;//������ѡ����
                lb_Source.Items.Add(//�����
                    lb_Choose.SelectedItem.ToString());
            }
            lb_Choose.Items.Clear();//�����
        }
        private void frmListBox_Load(object sender, EventArgs e)
        {
            AddList();//��ؼ����������
        }

        private void button1_Click(object sender, EventArgs e)//������ӵ�ѡ�������
        {
            if (lb_Source.SelectedIndex != -1)
            {
                this.lb_Choose.Items.Add(//�����
                    this.lb_Source.SelectedItem.ToString());
                this.lb_Source.Items.Remove(//�Ƴ���
                    this.lb_Source.SelectedItem);
            }
        }

        private void button4_Click(object sender, EventArgs e)//������ӵ�����Դ��
        {
            if (lb_Choose.SelectedIndex != -1)
            {
                this.lb_Source.Items.Add(//�����
                    this.lb_Choose.SelectedItem.ToString());
                this.lb_Choose.Items.Remove(//�Ƴ���
                    this.lb_Choose.SelectedItem);
            }
        }
    }
}