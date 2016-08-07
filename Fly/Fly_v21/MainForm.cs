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
        public static int m_GameLevel = 2;

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
        public static int m_RollSpeed = 2;

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

                this.Invalidate();
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// 游戏开始前
        /// </summary>
        private void GameStart()
        {
            m_StartSetting = new GameStartSetting(MainForm.m_GAMEWIDTH / 2 - 100, MainForm.m_GAMEHEIGHT / 2 - 150);

            while (m_GameStatus == GameStatus.GameSetting)
            {
                m_StartSetting.Draw(_G);
                this.Invalidate();
                Thread.Sleep(50);
            }

            _PaintTread = new Thread(PaintThread);
            _PaintTread.Start();
        }

        /// <summary>
        /// 创建敌人
        /// </summary>
        private void GetEnemy()
        {
            if (m_EnemyRandom.Next(0, 200) < 200)
            {
                if (m_Boss == 0)
                {
                    GetEnemys.GetEnemyOne();
                    GetEnemys.GetEnemyTwo();
                    GetEnemys.GetEnemyThree();
                    GetEnemys.GetEnemyFour();
                    GetEnemys.GetEnemyFive();
                    GetEnemys.GetEnemySix();
                    GetEnemys.GetEnemySeven();
                    GetEnemys.GetEnemyEight();
                    GetEnemys.GetEnemyNine();
                    GetEnemys.GetEnemyTen();
                    GetEnemys.GetEnemyEleven();
                    GetEnemys.GetEnemyTwelve();
                    GetEnemys.GetEnemyThirteen();
                    GetEnemys.GetEnemyFourteen();
                    GetEnemys.GetEnemyFifteen();
                    GetEnemys.GetEnemySixteen();
                    GetEnemys.GetEnemySeventeen();
                    GetEnemys.GetEnemyEighteen();
                    GetEnemys.GetEnemyNineteen();
                    GetEnemys.GetEnemyTwenty();
                    GetEnemys.GetEnemyTwentyOne();
                    GetEnemys.GetEnemyTwentyTwo();
                    GetEnemys.GetEnemyBoss();
                    m_Boss++;
                }
            }
        }

        /// <summary>
        /// 通关成功
        /// </summary>
        private void GameSuccess()
        {
            m_GameLevel++;
            _Roll = 0;

            m_IsGameSuccess = false;
            m_GamePassStart = false;
            m_GameStatus = GameStatus.GamePlay;

            ChooseBackImage();
            //所有元素全部清除
            HitCheck.GetInstance().ClearAll();
            HitCheck.GetInstance().AddElement(new Hero(300, 600, 10, 10, 200, true));
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

            DrawBackground(_G);
            HitCheck.GetInstance().AddElement(new Hero(300, 600, 10, 10, 200, true));
        }


        private void MianForm_Load(object sender, EventArgs e)
        {
            HitCheck.GetInstance().AddElement(new Hero(300, 600, 20, 20, 200, true));

            _PaintTread = new Thread(GameStart);
            _PaintTread.Start();
        }

        private void MianForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_BufferImg, 0, 0, m_Background.Width, m_Background.Height);
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
