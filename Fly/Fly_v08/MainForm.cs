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
        public const int m_GAMEWIDTH = 620;

        /// <summary>
        /// 获取或设置游戏窗口的高度
        /// </summary>
        public const int m_GAMEHEIGHT = 770;

        /// <summary>
        /// 设置游戏背景
        /// </summary>
        public static Bitmap m_Background = new Bitmap(Application.StartupPath + "\\images\\background.png");

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

        /// <summary>
        /// 随机数，创建敌人实例时会用到
        /// </summary>
        public static Random m_EnemyRandom = new Random();

        public MainForm()
        {
            InitializeComponent();
            LauchForm();
        }

        /// <summary>
        /// 初始化主要的变量
        /// </summary>
        private void LauchForm()
        {
            this.Width = m_GAMEWIDTH;        //设置窗口的宽度
            this.Height = m_GAMEHEIGHT;      //设置窗口的高度

            //设置显示图元控件的几个属性：解决闪烁问题
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);

            //在内存中创建（虚拟）一张位图，将来绘图时，在这张位图上作画，达到双缓冲的效果，解除闪烁问题
            _BufferImg = new Bitmap(m_Background.Width, m_Background.Height);

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
            g.DrawImage(m_Background, 0, _Roll - m_Background.Height, m_Background.Width, m_Background.Height);
            g.DrawImage(m_Background, 0, _Roll, m_Background.Width, m_Background.Height);
            _Roll += 2;
            if (_Roll >= m_Background.Height)
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

                this.GetEnemys();

                //碰撞检测
                HitCheck.GetInstance().DoHitCheck();

                HitCheck.GetInstance().Draw(_G);


                this.Invalidate();
                Thread.Sleep(50);
            }
        }

        private void MianForm_Load(object sender, EventArgs e)
        {
            HitCheck.GetInstance().AddElement(new Hero(300,600,10,10,100,true));
   
            _PaintTread = new Thread(PointThread);
            _PaintTread.Start();
        }

        private void MianForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_BufferImg, 0, 0, m_Background.Width, m_Background.Height);
            //e.Graphics.DrawImage(_BufferImg, 0, 0, 600, 700);
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
            m_Background.Dispose();

        }

        /// <summary>
        /// 创建敌人
        /// </summary>
        private void GetEnemys()
        {
            if (m_EnemyRandom.Next(0, 200) < 15)
            {
                HitCheck.GetInstance().AddElement(new EnemyOne(m_EnemyRandom.Next(-90,500),-50,false,10,10,10,true));
            }
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

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            HitCheck.GetInstance().MyHero.KeyDown(e);
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            HitCheck.GetInstance().MyHero.KeyUp(e);
        }
    }
}
