using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectSound;

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
        public Image m_Background = Image.FromFile(m_ImagePath + "Fly_BackScr1.gif");

        /// <summary>
        /// 获取图片路径
        /// </summary>
        private static string m_ImagePath = Directory.GetCurrentDirectory() + "\\images\\BackScr\\";

        /// <summary>
        /// 游戏背景
        /// </summary>
        private static Image[] m_GameBack = new Image[] 
        {
            Image.FromFile(m_ImagePath+"Fly_BackScr1.gif"),
            Image.FromFile(m_ImagePath+"Fly_BackScr2.gif"),
            Image.FromFile(m_ImagePath+"Fly_BackScr3.gif"),
            Image.FromFile(m_ImagePath+"Fly_BackScr4.gif"),
            Image.FromFile(m_ImagePath+"Fly_BackScr5.gif")
        };

        /// <summary>
        /// 游戏关卡
        /// </summary>
        public static int m_GameLevel = 1;

        /// <summary>
        /// 游戏是否通关
        /// </summary>
        public static bool m_IsGameSuccess = false;

        /// <summary>
        /// 设置游戏是否开始
        /// </summary>
        private bool _IsStart = false;

        /// <summary>
        /// 设置游戏是否重新开始
        /// </summary>
        private bool m_ReStart = false;

        /// <summary>
        /// 游戏通关，开始
        /// </summary>
        private bool m_GamePassStart = false;

        /// <summary>
        /// 设置游戏背景滚动
        /// </summary>
        private int _Roll = 0;

        /// <summary>
        /// 背景滚动的速度
        /// </summary>
        public static int m_RollSpeed = 3;

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

        /// <summary>
        /// 计数，达到一定数量后，出现Boss
        /// </summary>
        private int m_Boss;

        private bool IsMakeBossA = false, IsMakeBossB = true;

        /// <summary>
        /// 游戏开始设置实例
        /// </summary>
        private GameStartSetting m_StartSetting = null;

        /// <summary>
        /// 游戏状态
        /// </summary>
        public static GameStatus m_GameStatus = GameStatus.GameSetting;

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

            //选择游戏背景
            ChooseBackImage();

            //设置显示图元控件的几个属性：解决闪烁问题
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);

            //在内存中创建（虚拟）一张位图，将来绘图时，在这张位图上作画，达到双缓冲的效果，解除闪烁问题
            _BufferImg = new Bitmap(m_Background.Width, m_Background.Height);

            //指定Image 返回新的Graphics
            _G = Graphics.FromImage(_BufferImg);

            _IsStart = true;
        }

        /// <summary>
        /// 选择游戏背景
        /// </summary>
        private void ChooseBackImage()
        {
            switch (m_GameLevel)
            {
                case 1:
                    {
                        m_Background = m_GameBack[0];
                        break;
                    }
                case 2:
                    {
                        m_Background = m_GameBack[1];
                        break;
                    }
                case 3:
                    {
                        m_Background = m_GameBack[2];
                        break;
                    }
                case 4:
                    {
                        m_Background = m_GameBack[3];
                        break;
                    }
                case 5:
                    {
                        m_Background = m_GameBack[4];
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// 绘制游戏背景
        /// </summary>
        /// <param name="g">画笔</param>
        private void DrawBackground(Graphics g)
        {
            g.DrawImage(m_Background, 0, _Roll - m_Background.Height, m_Background.Width, m_Background.Height);
            g.DrawImage(m_Background, 0, _Roll, m_Background.Width, m_Background.Height);
            _Roll += m_RollSpeed;
            if (_Roll >= m_Background.Height)
            {
                _Roll = 0;
            }
        }

        /// <summary>
        /// 绘制线程，游戏进行中
        /// </summary>
        private void PaintThread()
        {
            //游戏开始，刷新屏幕
            while (_IsStart)
            {
                this.AllPaint();
                Thread.Sleep(80);
            }
        }

        private void AllPaint()
        {
            Graphics g = this.CreateGraphics();

            //在内存中创建（虚拟）一张位图，将来绘图时，在这张位图上作画，达到双缓冲的效果，解除闪烁问题
            _BufferImg = new Bitmap(m_Background.Width, m_Background.Height);

            //指定Image 返回新的Graphics
            _G = Graphics.FromImage(_BufferImg);

            //绘制背景图片
            DrawBackground(_G);

            if (m_GameStatus == GameStatus.GamePlay)
            {
                this.GetEnemy();
            }

            //碰撞检测
            HitCheck.GetInstance().DoHitCheck();

            HitCheck.GetInstance().Draw(_G);

            if (!HitCheck.GetInstance().MyHero.IsLive)
            {
                GamePlay.DrawGameOver(_G);
                if (m_ReStart)
                {
                    RePlay();
                    m_ReStart = false;
                }
            }

            if (m_IsGameSuccess)
            {
                GamePlay.DrawGamePass(_G);
                if (m_GamePassStart)
                {
                    GameSuccess();
                }
            }

            g.DrawImage(_BufferImg, 0, 0, m_Background.Width, m_Background.Height);

            _BufferImg.Dispose();
            _BufferImg = null;

            _G.Dispose();
            _G = null;

            g.Dispose();
            g = null;
        }

        private void GameSetting()
        {

            m_StartSetting = new GameStartSetting(MainForm.m_GAMEWIDTH / 2 - 100, MainForm.m_GAMEHEIGHT / 2 - 150);

            Graphics g = this.CreateGraphics();

            //在内存中创建（虚拟）一张位图，将来绘图时，在这张位图上作画，达到双缓冲的效果，解除闪烁问题
            _BufferImg = new Bitmap(m_Background.Width, m_Background.Height);

            //指定Image 返回新的Graphics
            _G = Graphics.FromImage(_BufferImg);

            m_StartSetting.Draw(_G);

            g.DrawImage(_BufferImg, 0, 0, m_Background.Width, m_Background.Height);

            _BufferImg.Dispose();
            _BufferImg = null;

            _G.Dispose();
            _G = null;

            g.Dispose();
            g = null;
        }

        /// <summary>
        /// 游戏开始前
        /// </summary>
        private void GameStart()
        {
            while (m_GameStatus == GameStatus.GameSetting)
            {
                GameSetting();
                Thread.Sleep(50);
            }

            _PaintTread = new Thread(PaintThread);
            _PaintTread.Start();
        }

        #region 创建敌人
        /// <summary>
        /// 创建敌人
        /// </summary>
        private void GetEnemy()
        {
            if (m_Boss < 10)
            {
                if (m_EnemyRandom.Next(0, 200) < 10)
                {
                    GetEnemys.GetEnemyOne();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 200) < 5)
                {
                    GetEnemys.GetEnemyTwo();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 200) < 5)
                {
                    GetEnemys.GetEnemyThree();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 200) < 5)
                {
                    GetEnemys.GetEnemyFour();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 2000) < 2)
                {
                    GetEnemys.GetEnemyFive();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 2000) < 2)
                {
                    GetEnemys.GetEnemySix();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 2000) < 2)
                {
                    GetEnemys.GetEnemySeven();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 2000) < 2)
                {
                    GetEnemys.GetEnemyEight();
                    m_Boss++;
                }

                if (m_EnemyRandom.Next(0, 1000) < 2)
                {
                    GetEnemys.GetEnemyNine();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 2000) < 2)
                {
                    GetEnemys.GetEnemyTen();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 3000) < 2)
                {
                    GetEnemys.GetEnemyEleven();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 2000) < 2)
                {
                    GetEnemys.GetEnemyTwelve();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 3000) < 2)
                {
                    GetEnemys.GetEnemyThirteen();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 3000) < 2)
                {
                    GetEnemys.GetEnemyFourteen();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 3000) < 2)
                {
                    GetEnemys.GetEnemyFifteen();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 3000) < 2)
                {
                    GetEnemys.GetEnemySixteen();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 3000) < 2)
                {
                    GetEnemys.GetEnemySeventeen();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 2000) < 2)
                {
                    GetEnemys.GetEnemyEighteen();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 2000) < 2)
                {
                    GetEnemys.GetEnemyNineteen();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 3000) < 2)
                {
                    GetEnemys.GetEnemyTwenty();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 500) < 2)
                {
                    GetEnemys.GetEnemyTwentyOne();
                    m_Boss++;
                }
                if (m_EnemyRandom.Next(0, 500) < 2)
                {
                    GetEnemys.GetEnemyTwentyTwo();
                    m_Boss++;
                }
            }

            else
            {
                IsMakeBossA = true;
            }


            if (IsMakeBossB && IsMakeBossA)
            {
                GetEnemys.GetEnemyBoss();
                IsMakeBossB = false;
            }
        }

        #endregion

        /// <summary>
        /// 通关成功
        /// </summary>
        private void GameSuccess()
        {
            m_GameLevel++;
            _Roll = 0;
            m_Boss = 0;

            IsMakeBossB = true;
            IsMakeBossA = false;

            m_IsGameSuccess = false;
            m_GamePassStart = false;
            m_GameStatus = GameStatus.GamePlay;

            ChooseBackImage();
            //所有元素全部清除
            HitCheck.GetInstance().ClearAll();
            //HitCheck.GetInstance().MyHero.XX = 280;
            //HitCheck.GetInstance().MyHero.XX = 600;
            //HitCheck.GetInstance().AddElement(new Hero(300, 600, 20, 20, 200, true));
        }

        /// <summary>
        /// 游戏重新开始
        /// </summary>
        private void RePlay()
        {
            m_Boss = 0;
            _Roll = 0;

            //修改游戏状态
            m_GameStatus = GameStatus.GamePlay;

            //所有元素全部清除
            HitCheck.GetInstance().ClearAll();

            //DrawBackground(_G);
            HitCheck.GetInstance().AddElement(new Hero(300, 600, 20, 20, 200, true));
        }


        private void MianForm_Load(object sender, EventArgs e)
        {
            HitCheck.GetInstance().AddElement(new Hero(300, 600, 20, 20, 200, true));

            _PaintTread = new Thread(GameStart);
            _PaintTread.Start();
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
            if (m_Background != null)
            {
                m_Background.Dispose();
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
            switch (m_GameStatus)
            {

                case GameStatus.GameSetting:
                    {
                        m_StartSetting.KeyDown(e);

                        if (e.KeyCode == Keys.Enter)
                        {
                            m_GameStatus = GameStatus.GamePlay;
                        }
                        break;
                    }

                case GameStatus.GamePass:
                    {
                        HitCheck.GetInstance().MyHero.XX = 280;
                        HitCheck.GetInstance().MyHero.YY = 600;
                        HitCheck.GetInstance().MyHero.Dir = General.RolesDirection.STOP;
                        if (e.KeyCode == Keys.Enter)
                        {
                            m_GamePassStart = true;
                        }
                        break;
                    }

                case GameStatus.GamePlay:
                    {
                        HitCheck.GetInstance().MyHero.KeyDown(e);
                        break;
                    }

                case GameStatus.GameOver:
                    {
                        if (e.KeyCode == Keys.F2)
                        {
                            m_ReStart = true;
                        }
                        break;
                    }
                default:
                    break;

            }
        }


        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (GameStatus.GamePlay == m_GameStatus)
            {
                HitCheck.GetInstance().MyHero.KeyUp(e);
            }
        }
    }
}
