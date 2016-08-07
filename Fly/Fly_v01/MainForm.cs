using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fly
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 获取或设置游戏窗口的宽度
        /// </summary>
        public const int _GAMEWIDTH = 600;

        /// <summary>
        /// 获取或设置游戏窗口的高度
        /// </summary>
        public const int _GAMEHEIGHT = 750;

        /// <summary>
        /// 设置游戏背景
        /// </summary>
        public static Bitmap _Background = new Bitmap(Application.StartupPath + "\\images\\background.png");

        /// <summary>
        /// 设置游戏是否开始
        /// </summary>
        private bool _IsStart = false;

        /// <summary>
        /// 设置游戏背景滚动
        /// </summary>
        private int _Roll = 0;

        /// <summary>
        /// 双缓冲用到的变量
        /// </summary>
        private Bitmap _BufferImg = null;
        private Graphics _G = null;

        /// <summary>
        /// 设置绘制线程
        /// </summary>
        Thread _PaintTread = null;

        public MainForm()
        {
            InitializeComponent();
            LauchForm();
        }

        private void LauchForm()
        {
            this.Width = _GAMEWIDTH;        //设置窗口的宽度
            this.Height = _GAMEHEIGHT;      //设置窗口的高度

            //设置显示图元控件的几个属性：解决闪烁问题
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer|ControlStyles.ResizeRedraw|ControlStyles.AllPaintingInWmPaint,true);

            //在内存中创建（虚拟）一张位图，将来绘图时，在这张位图上作画，达到双缓冲的效果，解除闪烁问题
            _BufferImg = new Bitmap(_GAMEWIDTH,_GAMEHEIGHT);

            //指定Image 返回新的Graphics
            _G = Graphics.FromImage(_BufferImg);

            _IsStart = true;
        }

        /// <summary>
        /// 绘制游戏背景
        /// </summary>
        /// <param name="g">画笔</param>
        private void DrawBackground(Graphics g)
        {
            g.DrawImage(_Background,0,_Roll - _GAMEHEIGHT,600,750);
            g.DrawImage(_Background, 0, _Roll, 600, 750);
            _Roll += 3;
            if (_Roll >= 750)
            {
                _Roll = 0;
            }
        }

        /// <summary>
        /// 绘制线程
        /// </summary>
        private void PointThread()
        {
            //游戏开始，刷新屏幕
            while (_IsStart)
            {
                //绘制背景图片
                DrawBackground(_G);
                this.Invalidate();
                Thread.Sleep(50);
            }
        }

        private void MianForm_Load(object sender, EventArgs e)
        {
            _PaintTread = new Thread(PointThread);
            _PaintTread.Start();
        }

        private void MianForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_BufferImg,0,0,600,750);
        }

        /// <summary>
        /// 清理资源
        /// </summary>
        private void DisResource()
        {
            _IsStart = false;
            _PaintTread.Join();

            _BufferImg.Dispose();
            _G.Dispose();
            _Background.Dispose();

        }

        private void MianForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult msg = MessageBox.Show("真的退出游戏", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msg == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                this.DisResource();
            }
        }
    }
}
