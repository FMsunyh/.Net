using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace UseBackgroundWorker
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
        }
        private int numberToCompute = 0;
        private int highestPercentageReached = 0;

        //����һ���߳��������¼��������
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = ComputeFibonacci((int)e.Argument, this.backgroundWorker1, e);
        }
    
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)//�Ƿ��д�����Ϣ
            {
                MessageBox.Show(//������Ϣ�Ի���
                    e.Error.Message);
            }
            else if (e.Cancelled)//�첽�����Ƿ�ȡ��
            {
                resultLabel.Text = "Canceled";//�����ַ�������
            }
            else
            {
                resultLabel.Text = e.Result.ToString();//��ʾ���
            }
            numericUpDown1.Enabled = true;//����numericUpDown�ؼ�
            startAsyncButton.Enabled = true;//���ÿ�ʼ��ť
            cancelAsyncButton.Enabled = false;//ͣ��ȡ����ť
        }

        private void startAsyncButton_Click(object sender, EventArgs e)
        {
           
            resultLabel.Text = String.Empty;//�õ����ַ�������
            this.numericUpDown1.Enabled = false;//ͣ��numericUpDown�ؼ�
            this.startAsyncButton.Enabled = false;//ͣ�ÿ�ʼ��ť
            this.cancelAsyncButton.Enabled = true;//����ȡ����ť
            numberToCompute = (int)numericUpDown1.Value;//�õ�numericUpDown�ؼ���ֵ
            highestPercentageReached = 0;//����ֵΪ0
            backgroundWorker1.RunWorkerAsync(numberToCompute);//��ʼִ�к�̨����
        }

        long ComputeFibonacci(int n, BackgroundWorker worker, DoWorkEventArgs e)
        {
            if ((n < 0) || (n > 91))
            {
                throw new ArgumentException(//�׳��쳣
                    "value must be >= 0 and <= 91", "n");
            }
            long result = 0;//���������ͱ�������ֵ
            if (worker.CancellationPending)//�ж��Ƿ��Ѿ�ȡ����̨����
            {
                e.Cancel = true;//����ȡ���¼�
            }
            else
            {
                if (n < 2)
                {
                    result = 1;//��������1
                }
                else
                {
                    result = ComputeFibonacci(n - 1, worker, e) +//ʹ�õݹ�õ����
                             ComputeFibonacci(n - 2, worker, e);
                }

                int percentComplete =
                    (int)((float)n / (float)numberToCompute * 100);
                if (percentComplete > highestPercentageReached)
                {
                    highestPercentageReached = percentComplete;
                    worker.ReportProgress(percentComplete);
                }
            }

            return result;//���ؽ��
        }

        private void cancelAsyncButton_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync();//ȡ������ĺ�̨����
            cancelAsyncButton.Enabled = false;//ͣ��ȡ����ť

        }
    }
}