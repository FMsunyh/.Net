using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Tetris
{
    /// <summary>
    /// 司令部（控制中心）
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// 游戏背景颜色
        /// </summary>
        SolidBrush m_GameBackgroundBrush = null;

        /// <summary>
        /// 窗口大小 宽
        /// </summary>
        private const int m_FORMWIDTH = Global.m_GAME_WIDTH+150;

        /// <summary>
        /// 窗口大小 高
        /// </summary>
        private const int m_FORMHEIGHT = Global.m_GAME_HEIGHT;

        /// <summary>
        /// 游戏开始线程
        /// </summary>
        private Thread m_RePaintThread = null;

        /// <summary>
        /// 游戏状态
        /// </summary>
        private bool m_IsLive = true;

        /// <summary>
        /// 图形形状实例
        /// </summary>
        private Shape m_Shape = null;

        /// <summary>
        /// 累积方块实例
        /// </summary>
        private Ground m_Ground = null;

        /// <summary>
        /// 游戏等级速度
        /// </summary>
        private int m_Speed = 500;



        public Form1()
        {
            InitializeComponent();

            this.ClientSize = new System.Drawing.Size(m_FORMWIDTH, m_FORMHEIGHT);

            m_GameBackgroundBrush = new SolidBrush(Color.Black);

            m_Shape = ShapeFactory.GetInstance().GetShape();

            m_Ground = new Ground();

            //启动线程
            m_RePaintThread = new Thread(PaintThread);
            m_RePaintThread.Start();
        }

        /// <summary>
        /// 绘制全部内容
        /// </summary>
        private void PaintAll()
        {
            lock (this)
            {
                Graphics g = this.CreateGraphics();

                Bitmap bmp = new Bitmap(m_FORMWIDTH, m_FORMHEIGHT);
                Graphics buffergs = Graphics.FromImage(bmp);

                buffergs.FillRectangle(m_GameBackgroundBrush, 0, 0, m_FORMWIDTH, m_FORMHEIGHT);

                //绘制所有东西
                Display(buffergs);

                g.DrawImage(bmp, 0, 0);

                bmp.Dispose();
                bmp = null;

                buffergs.Dispose();
                buffergs = null;

                g.Dispose();
                g = null;
            }
        }

        /// <summary>
        /// 绘制当前方块和累积方块
        /// </summary>
        /// <param name="g"></param>
        private void Display(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Gray), Global.m_GAME_WIDTH, 0, 150, Global.m_GAME_HEIGHT);
            g.DrawLine(new Pen(Color.Yellow,3), Global.m_GAME_WIDTH+2, 0, Global.m_GAME_WIDTH+2, Global.m_GAME_HEIGHT);
            g.DrawString("FMsunyh", new Font("Rage", 16), new SolidBrush(Color.Yellow), new PointF(Global.m_GAME_WIDTH + 20, Global.m_GAME_HEIGHT - 130));
            g.DrawString("2013-12-01", new Font("Rage", 16), new SolidBrush(Color.Yellow), new PointF(Global.m_GAME_WIDTH + 20, Global.m_GAME_HEIGHT - 100));

            m_Shape.Draw(g);
            m_Ground.Draw(g);        
            ShapeFactory.GetInstance().m_ShapeB.Draw(g);
        }

        /// <summary>
        /// 游戏线程
        /// </summary>
        private void PaintThread()
        {
            while (this.m_IsLive)
            {
                this.PaintAll();

                if (IsAbleShapeMoveDown(m_Shape))
                {
                    m_Shape.MoveDown();
                }

                Thread.Sleep(m_Speed);
            }

            if (!this.m_IsLive)
            {
               
                DialogResult result = MessageBox.Show("是否继续游戏", "GameOver", MessageBoxButtons.OKCancel);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    this.m_IsLive = true;
                    ReStart();
                }
                else
                {
                    m_RePaintThread.Abort();
                    Application.Exit();
                }
            }
        }

        /// <summary>
        /// 游戏重新开始
        /// </summary>
        private void ReStart()
        {
            m_Ground.Init();
            //启动线程
            m_RePaintThread = new Thread(PaintThread);
            m_RePaintThread.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //窗口关闭时，关闭线程，并退出程序
            m_RePaintThread.Abort();
            Application.Exit();
        }

        /// <summary>
        /// 按键处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    {
                        if (m_Ground.IsMoveAble(m_Shape, Shape.m_RorateL))
                        {
                            m_Shape.Rotate();
                        }
                        break;

                    }
                case Keys.D:
                case Keys.Right:
                    {
                        if (m_Ground.IsMoveAble(m_Shape, Shape.m_RightL))
                        {
                            m_Shape.MoveRight();
                        }
                        break;
                    }
                case Keys.S:
                case Keys.Down:
                    {
                        if (IsAbleShapeMoveDown(m_Shape))
                        {
                            m_Shape.MoveDown();
                        }

                        break;
                    }
                case Keys.A:
                case Keys.Left:
                    {
                        if (m_Ground.IsMoveAble(m_Shape, Shape.m_LeftL))
                        {
                            m_Shape.MoveLeft();
                        }
                        break;
                    }
                default:
                    break;
            }

            this.PaintAll();
        }

        /// <summary>
        /// 判断图形是否能够下移
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public bool IsAbleShapeMoveDown(Shape shape)
        {
            lock (this)
            {
                if (m_Ground.IsMoveAble(m_Shape, Shape.m_DownL))
                {
                    return true;
                }

                m_Ground.AcceptShape(this.m_Shape);

                if (m_Ground.IsFull())
                {
                    this.m_Shape = ShapeFactory.GetInstance().GetShape();
                }
                else 
                {
                    this.m_IsLive = false;
                }
                return false;
            }
        }
    }
}
