using Fly.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Fly
{
    /// <summary>
    /// 英雄类
    /// </summary>
    public class Hero : Roles
    {
        /// <summary>
        /// 载入英雄图片
        /// </summary>
        private static Image m_MyImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\myPlane.png");

        private static Image m_ProtectImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\protect.gif");

        /// <summary>
        /// 载入炸弹图片
        /// </summary>
        private static Image m_MyBomb = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\myBomb.gif");

        /// <summary>
        /// 在英雄的图片上选择英雄的位置
        /// </summary>
        Rectangle m_Rect;

        /// <summary>
        /// 英雄的大小，宽度和高度
        /// </summary>
        private static int m_HeroWidth, m_HeroHeight;

        /// <summary>
        /// 用户的按键，按下为true.
        /// </summary>
        private bool PU = false, PD = false, PL = false, PR = false;

        /// <summary>
        /// 英雄的血块
        /// </summary>
        BloodBar m_BloodBar = null;

        /// <summary>
        /// 子弹的等级
        /// </summary>
        private int m_MissileLevel = 1;

        /// <summary>
        /// 子弹类型
        /// </summary>
        private int m_MissileType = 1;

        private int m_BombType = 1;

        /// <summary>
        /// 导弹个数
        /// </summary>
        private int m_BombCount = 3;

        private bool m_IsDirectMissile = false;

        /// <summary>
        /// 统计不同等级的勋章个数
        /// </summary>
        private int[] m_Levelcount = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        /// <summary>
        /// 绘制保护层
        /// </summary>
        public bool m_IsProtectLayer = false;

        /// <summary>
        /// 保护层有效时间
        /// </summary>
        public int m_ProtectTime = 0;

        /// <summary>
        /// 积分
        /// </summary>
        private int m_Scores = 0;

        public int XX
        {
            set { x = value; }
        }

        public int YY
        {
            set { y = value; }
        }

        public RolesDirection Dir
        {
            set { this.dir = value; }
        }

        /// <summary>
        /// 英雄的构造函数
        /// </summary>
        /// <param name="x">初始化x轴的坐标</param>
        /// <param name="y">初始化y轴的坐标</param>
        /// <param name="xspeed">相对于x轴的速度</param>
        /// <param name="yspeed">相对于y轴的速度</param>
        /// <param name="life">生命值</param>
        /// <param name="good">好坏</param>
        public Hero(int x, int y, int xspeed, int yspeed, int life, bool good)
            : base(x, y, good, m_MyImage.Width / 3, m_MyImage.Height / 4, xspeed, yspeed, life)
        {
            m_HeroWidth = 40;
            m_HeroHeight = 40;
            m_BloodBar = new BloodBar(5, 80, 200);

        }

        public void AddScores(int scores)
        {
            m_Scores += scores;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        public override void Bleeding(int i)
        {
            if (i < 10)
            {

                switch (i)
                {
                    case 0:
                        {
                            //西瓜
                            //子弹变形
                            m_MissileType++;
                            m_MissileType = m_MissileType % 3 + 1;
                            break;
                        }
                    case 1:
                        {
                            //保护层
                            m_IsProtectLayer = true;
                            m_ProtectTime = 0;
                            break;
                        }
                    case 2:
                        {
                            //原子导弹
                            m_BombCount++;

                            //导弹变形   
                            m_BombType++;
                            //m_BombType = 2;

                            break;
                        }
                    case 3:
                        {
                            //直射子弹
                            m_IsDirectMissile = true;
                            break;
                        }
                    case 4:
                        {
                            m_MissileType++;
                            m_MissileType = m_MissileType % 3 + 1;
                            break;
                        }
                    case 5:
                        {
                            //500分
                            this.AddScores(500);
                            break;
                        }
                    case 6:
                        {
                            //1000分
                            this.AddScores(1000);
                            break;
                        }
                    case 7:
                        {
                            //加子弹
                            if (m_MissileLevel < 4)
                            {
                                m_MissileLevel++;
                            }
                            break;
                        }
                    case 8:
                        {
                            //加满子弹
                            m_MissileLevel = 4;
                            break;
                        }
                    default:
                        {
                            base.Bleeding(i);
                            break;
                        }
                }
            }

            else
            {
                if (this.m_IsProtectLayer)
                {
                    this.m_ProtectTime++;
                    if (this.m_ProtectTime > 100)
                    {
                        this.m_IsProtectLayer = false;
                        this.m_ProtectTime = 0;
                    }
                }

                else
                {
                    base.Bleeding(i);
                }
            }

        }

        #region 控制方向

        public void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    PU = true;
                    break;
                case Keys.Down:
                    PD = true;
                    break;
                case Keys.Left:
                    PL = true;
                    break;
                case Keys.Right:
                    PR = true;
                    break;
                default:
                    break;
            }
            ConfirmRolesDirection();
        }

        public void KeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    PU = false;
                    break;
                case Keys.Down:
                    PD = false;
                    break;
                case Keys.Left:
                    PL = false;
                    break;
                case Keys.Right:
                    PR = false;
                    break;
                case Keys.J:
                    Fire();
                    break;

                case Keys.Space:
                    {
                        if (m_BombCount > 0)
                        {
                            FireBomb(200);
                        }
                        break;
                    }
                default: break;
            }
            ConfirmRolesDirection();
        }

        /// <summary>
        /// 确定英雄的移动方向
        /// </summary>
        private void ConfirmRolesDirection()
        {
            if (PL && !PU && !PR && !PD)
                dir = RolesDirection.L;
            else if (PL && PU && !PR && !PD)
                dir = RolesDirection.LU;
            else if (!PL && PU && !PR && !PD)
                dir = RolesDirection.U;
            else if (!PL && PU && PR && !PD)
                dir = RolesDirection.RU;
            else if (!PL && !PU && PR && !PD)
                dir = RolesDirection.R;
            else if (!PL && !PU && PR && PD)
                dir = RolesDirection.RD;
            else if (!PL && !PU && !PR && PD)
                dir = RolesDirection.D;
            else if (PL && !PU && !PR && PD)
                dir = RolesDirection.LD;
            else if (!PL && !PU && !PR && !PD)
                dir = RolesDirection.STOP;
        }

        /// <summary>
        /// 选择Hero的图像
        /// </summary>
        private void ConfirmRolesImage()
        {

            int xTop, yLeft = 0;

            switch (dir)
            {
                case RolesDirection.L:
                case RolesDirection.LD:
                case RolesDirection.LU:
                    {
                        xTop = 0;
                        break;
                    }
                case RolesDirection.R:
                case RolesDirection.RU:
                case RolesDirection.RD:
                    {
                        xTop = 2;
                        break;
                    }
                default:
                    {
                        xTop = 1;
                    }
                    break;
            }

            m_Rect = new Rectangle(m_HeroWidth * xTop, m_HeroHeight * yLeft, m_HeroWidth, m_HeroHeight);
        }

        #endregion 
        /// <summary>
        /// 爆炸效果
        /// </summary>
        protected override void ThisBomb()
        {
            HitCheck.GetInstance().AddElement(new BombOne(this));

            //修改游戏状态
            MainForm.m_GameStatus = GameStatus.GameOver;
        }

        #region 英雄开火

        /// <summary>
        /// 英雄开火
        /// </summary>
        public override void Fire()
        {
            int x, y;

            x = 40;
            y = 60;

            if (!this.IsLive)
            {
                return;
            }

            switch (m_MissileLevel)
            {
                case 1:
                    {
                        switch (m_MissileType)
                        {
                            case 1:
                                {
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.U, 10));
                                    break;
                                }
                            case 2:
                                {
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.U, 30));
                                    break;
                                }

                            case 3:
                                {
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.U, 20));
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case 2:
                    {
                        switch (m_MissileType)
                        {
                            case 1:
                                {
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.U, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.LUU, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.RUU, 10));
                                    break;
                                }
                            case 2:
                                {
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.U, 30));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.LUU, 30));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.RUU, 30));
                                    break;
                                }

                            case 3:
                                {
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.U, 20));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.LUU, 20));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.RUU, 20));
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }

                case 3:
                    {
                        switch (m_MissileType)
                        {
                            case 1:
                                {
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.U, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.LUU, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.RUU, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.LU, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.RU, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.L, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.R, 10));
                                    break;
                                }
                            case 2:
                                {
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.U, 30));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.LUU, 30));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.RUU, 30));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.LU, 30));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.RU, 30));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.L, 30));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.R, 30));
                                    break;
                                }

                            case 3:
                                {
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.U, 20));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.LUU, 20));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.RUU, 20));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.LU, 20));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.RU, 20));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.L, 20));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.R, 20));
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }

                case 4:
                    {
                        switch (m_MissileType)
                        {
                            case 1:
                                {
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.U, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.LUU, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.RUU, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.LU, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.RU, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.L, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.R, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.LDD, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.RDD, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.LD, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.RD, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroOne(this, this.Good, x, y, MissileDirection.D, 10));
                                    break;
                                }
                            case 2:
                                {
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.U, 300));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.LUU, 30));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.RUU, 30));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.LU, 30));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.RU, 30));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.L, 30));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.R, 30));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.LDD, 30));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.RDD, 30));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.LD, 30));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.RD, 30));
                                    HitCheck.GetInstance().AddElement(new MissileHeroTwo(this, this.Good, x, y, MissileDirection.D, 30));
                                    break;
                                }

                            case 3:
                                {
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.U, 20));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.LUU, 20));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.RUU, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.LU, 20));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.RU, 10));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.L, 20));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.R, 20));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.LDD, 20));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.RDD, 20));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.LD, 20));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.RD, 20));
                                    HitCheck.GetInstance().AddElement(new MissileHeroThree(this, this.Good, x, y, MissileDirection.D, 20));
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                default:
                    break;
            }

            if (m_IsDirectMissile)
            {
                HitCheck.GetInstance().AddElement(new MissileHeroFour(this, this.Good, 20, 40, MissileDirection.U, 10, true));
                HitCheck.GetInstance().AddElement(new MissileHeroFour(this, this.Good, 20, 40, MissileDirection.U, 10, false));
            }
        }

        #endregion

        /// <summary>
        /// 发出导弹
        /// </summary>
        /// <param name="power"></param>
        private void FireBomb(int power)
        {
            if (!this.IsLive)
            {
                return;
            }

            switch (m_BombType)
            {
                case 1:
                    {
                        HitCheck.GetInstance().AddElement(new MissileBombOne(this, this.Good, 30, 30, MissileDirection.U, power));
                        break;
                    }
                case 2:
                    {
                        HitCheck.GetInstance().AddElement(new MissileBombTwo(this, this.Good, 30, 30, MissileDirection.U, power));
                        break;
                    }
                default:
                    break;
            }
       
            m_BombCount--;
        }

        /// 重写父类 Roles的Draw方法
        /// </summary>
        /// <param name="g">画笔</param>
        public override void Draw(Graphics g)
        {
            if (m_life >= m_BloodBar.AllLife)
            {
                m_life = m_BloodBar.AllLife;
            }           
            m_BloodBar.NowLife = m_life;  

            //绘制血条
            m_BloodBar.Draw(g);

            //绘制炸弹
            g.DrawImage(m_MyBomb, 10, 100);

            if (!this.IsLive)
            {
                HitCheck.GetInstance().ReMoveElement(this);
                return;
            }
            this.ConfirmRolesImage();
            this.Move();
            base.Draw(g, m_MyImage, m_Rect, x, y);

            if (m_IsProtectLayer)
            {
                this.ProtectLayer(g);
            }

            this.DrawStringPointF(g);
        }

        private void ProtectLayer(Graphics g)
        {
            Rectangle rect = new Rectangle(m_ProtectImage.Width / 2 * (MainForm.m_EnemyRandom.Next(0, 100) < 50 ? 1 : 0), 0, m_ProtectImage.Width / 2, m_ProtectImage.Height);
            base.Draw(g, m_ProtectImage, rect, x - 12, y - 12);
        }

        /// <summary>
        /// 重写父类Roles的Move方法
        /// </summary>
        protected override void Move()
        {
            base.Move();

            //超出边界检测
            if (x < 0) x = 0;
            if (y < 0) y = 0;

            if (x + this.m_WIDTH > MainForm.m_GAMEWIDTH - 30)
            {
                x = MainForm.m_GAMEWIDTH - 30 - this.m_WIDTH;
            }

            if (y + this.m_HEIGHT > MainForm.m_GAMEHEIGHT - 50)
            {
                y = MainForm.m_GAMEHEIGHT - 50 - this.m_HEIGHT;
            }
        }

        /// <summary>
        /// 绘制英雄的坐标
        /// </summary>
        /// <param name="g">画笔</param>
        public void DrawStringPointF(Graphics g)
        {

            // 字体和填充颜色.
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Yellow);

            // Hero的位置坐标.
            String drawString = "x坐标：" + this.X + "\ny坐标：" + this.Y;
            // 位置.
            PointF drawPoint = new PointF(5.0F, 5.0F);

            //积分
            String StrScores = m_Scores.ToString();
            PointF pointScores = new PointF(500.0F, 5.0F);

            //导弹
            String bombCount = "X" + m_BombCount.ToString();
            PointF pointBomb = new PointF(30.0F, 110.0F);

            // 绘制.
            g.DrawString(drawString, drawFont, drawBrush, drawPoint);

            //积分
            g.DrawString(StrScores, drawFont, drawBrush, pointScores);

            //导弹
            g.DrawString(bombCount, drawFont, drawBrush, pointBomb);
        }

        public void EatMedals(int medal)
        {
            switch (medal)
            {
                case 1:
                    {
                        m_Levelcount[0]++;
                        break;
                    }
                case 2:
                    {
                        m_Levelcount[1]++;
                        break;
                    }
                case 3:
                    {
                        m_Levelcount[2]++;
                        break;
                    }
                case 4:
                    {
                        m_Levelcount[3]++;
                        break;
                    }
                case 5:
                    {
                        m_Levelcount[4]++;
                        break;

                    }
                case 6:
                    {
                        m_Levelcount[5]++;
                        break;
                    }
                case 7:
                    {
                        m_Levelcount[6]++;
                        break;
                    }
                case 8:
                    {
                        m_Levelcount[7]++;
                        break;
                    }
                case 9:
                    {
                        m_Levelcount[8]++;
                        break;
                    }
                case 10:
                    {
                        m_Levelcount[9]++;
                        break;
                    }
                default:
                    break;
            }
        }

    }
}
