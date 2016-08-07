using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TankWar
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 获取或设置游戏窗口的宽度
        /// </summary>
        public const int m_GAMEWIDTH = 800;

        /// <summary>
        /// 获取或设置游戏窗口的高度
        /// </summary>
        public const int m_GAMEHEIGHT = 600;

        /// <summary>
        /// 设置游戏背景
        /// </summary>
        public static Bitmap _Background = new Bitmap(Application.StartupPath + "\\images\\background.png");

        /// <summary>
        /// 设置游戏是否开始
        /// </summary>
        private bool _IsStart = false;

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
        /// 产生随机数发生器
        /// </summary>
        public static Random m_EnemyRandom = new Random();

        public Wall wall;

        public MainForm()
        {
            InitializeComponent();
            LauchForm();
        }

        private void LauchForm()
        {
            //注意边框所占大小
            this.Width = m_GAMEWIDTH + 20;        //设置窗口的宽度
            this.Height = m_GAMEHEIGHT + 40;      //设置窗口的高度

            //设置显示图元控件的几个属性：解决闪烁问题
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);

            _IsStart = true;
        }

        /// <summary>
        /// 绘制游戏背景
        /// </summary>
        /// <param name="g">画笔</param>
        private void DrawBackground(Graphics g)
        {
            g.DrawImage(_Background, 0, 0, m_GAMEWIDTH, m_GAMEHEIGHT);
        }

        /// <summary>
        /// 绘制线程
        /// </summary>
        private void PaintThread()
        {
            //游戏开始，刷新屏幕
            while (_IsStart)
            {
                //绘制背景图片
                this.AllPaint();

                this.GetEnemys();

                //碰撞检测
                HitCheck.GetInstance().DoHitCheck();

                Thread.Sleep(50);
            }
        }

        private void AllPaint()
        {
            Graphics g = this.CreateGraphics();

            //在内存中创建（虚拟）一张位图，将来绘图时，在这张位图上作画，达到双缓冲的效果，解除闪烁问题
            _BufferImg = new Bitmap(m_GAMEWIDTH, m_GAMEHEIGHT);

            //指定Image 返回新的Graphics
            _G = Graphics.FromImage(_BufferImg);

            //绘制背景图片
            DrawBackground(_G);

            HitCheck.GetInstance().Draw(_G);

            g.DrawImage(_BufferImg, 0, 0, m_GAMEWIDTH, m_GAMEHEIGHT);

            _BufferImg.Dispose();
            _BufferImg = null;

            _G.Dispose();
            _G = null;

            g.Dispose();
            g = null;

        }


        /// <summary>
        /// 清理资源
        /// </summary>
        private void DisResource()
        {
            _IsStart = false;
            _PaintTread.Join();

            if (_BufferImg != null)
            {
                _BufferImg.Dispose();
            }
            if (_G != null)
            {
                _G.Dispose();
            }
            if (_Background != null)
            {
                _Background.Dispose();
            }

        }

        /// <summary>
        /// 产生围墙地图
        /// </summary>
        private void GetEnemys()
        {
            if (m_EnemyRandom.Next(0, 200) < 5)
            {
                HitCheck.GetInstance().AddElement(new EnemyOne(30, 100, false, 10, 10, 10));
            }
        }

        private void GetMap()
        {
            for (int i = 0; i < 5; i++)
            {
                HitCheck.GetInstance().AddElement(new Wall(50*i, 150, 50, 50, TankWar.Wall.TypeWall.STONE));
            }

            for (int i = 0; i < 5; i++)
            {
                HitCheck.GetInstance().AddElement(new Wall(500+50 * i, 450, 50, 50, TankWar.Wall.TypeWall.STONE));
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    HitCheck.GetInstance().AddElement(new Wall(50 * i+450, 100+50*j, 50, 50, TankWar.Wall.TypeWall.GRASS));
                }
            }

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    HitCheck.GetInstance().AddElement(new Wall(50 * i + 100, 300 + 50 * j, 50, 50, TankWar.Wall.TypeWall.WATER));
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //HitCheck.GetInstance().AddElement(new Wall(200, 150, 50, 200,TankWar.Wall.TypeWall.WATER));
            //HitCheck.GetInstance().AddElement(new Wall(400, 150, 50, 200, TankWar.Wall.TypeWall.STONE));
            //HitCheck.GetInstance().AddElement(new Wall(600, 150, 50, 200, TankWar.Wall.TypeWall.GRASS));

            HitCheck.GetInstance().AddElement(new Tank(300, 100, 10, 10, 200, true));
            GetMap();

            _PaintTread = new Thread(PaintThread);
            _PaintTread.Start();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            HitCheck.GetInstance().MyTank.KeyUp(e);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            HitCheck.GetInstance().MyTank.KeyDown(e);
        }

    }
}
