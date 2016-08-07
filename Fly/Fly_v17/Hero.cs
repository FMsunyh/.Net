using DraGonQuest.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DraGonQuest
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
        /// 导弹个数
        /// </summary>
        private int m_BombCount = 3;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        public override void Bleeding(int i)
        {
            base.Bleeding(i);
            if (i == 0)
            {
                //升级子弹
                if (m_MissileLevel < 3)
                {
                    m_MissileLevel++;
                }

            }
        }

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
                default: break;
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

        /// <summary>
        /// 爆炸效果
        /// </summary>
        protected override void ThisBomb()
        {
            HitCheck.GetInstance().AddElement(new BombOne(this));

        }

        /// <summary>
        /// 英雄开火
        /// </summary>
        public override void Fire()
        {

            if (!this.IsLive)
            {
                return;
            }

            //Sound sound = new Sound(Directory.GetCurrentDirectory() + "\\music\\HeroFire.wav", MainForm.GetInstance());
            //sound.Play();

            switch (m_MissileLevel)
            {
                case 1:
                    {
                        HitCheck.GetInstance().AddElement(new MissileHero(this, this.Good, 20, 20, MissileDirection.U, 10));
                        break;
                    }
                case 2:
                    {
                        HitCheck.GetInstance().AddElement(new MissileHero(this, this.Good, 20, 20, MissileDirection.LUU, 10));
                        HitCheck.GetInstance().AddElement(new MissileHero(this, this.Good, 20, 20, MissileDirection.U, 10));
                        HitCheck.GetInstance().AddElement(new MissileHero(this, this.Good, 20, 20, MissileDirection.RUU, 10));
                        break;
                    }
                case 3:
                    {
                        HitCheck.GetInstance().AddElement(new MissileHero(this, this.Good, 20, 20, MissileDirection.LUU, 10));
                        HitCheck.GetInstance().AddElement(new MissileHero(this, this.Good, 20, 20, MissileDirection.U, 10));
                        HitCheck.GetInstance().AddElement(new MissileHero(this, this.Good, 20, 20, MissileDirection.RUU, 10));
                        HitCheck.GetInstance().AddElement(new MissileHero(this, this.Good, 20, 20, MissileDirection.LU, 10));
                        HitCheck.GetInstance().AddElement(new MissileHero(this, this.Good, 20, 20, MissileDirection.RU, 10));
                        break;
                    }
                default:
                    break;
            }

        }

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
            HitCheck.GetInstance().AddElement(new MissileBomb(this, this.Good, 30, 30, MissileDirection.U, power));
            m_BombCount--;
        }

        /// 重写父类 Roles的Draw方法
        /// </summary>
        /// <param name="g">画笔</param>
        public override void Draw(Graphics g)
        {
            m_BloodBar.NowLife = m_life;

            //绘制血条
            m_BloodBar.Draw(g);

            //绘制炸弹
            for (int i = 0; i < m_BombCount; i++)
            {
                g.DrawImage(m_MyBomb, 10 + (i * 20), 100);
            }

            if (!this.IsLive)
            {
                //HitCheck.GetInstance().ReMoveElement(this);
                return;
            }
            this.ConfirmRolesImage();
            this.Move();
            base.Draw(g, m_MyImage, m_Rect, x, y);
            DrawStringPointF(g);
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

            // Hero的位置坐标.
            String drawString = "x坐标：" + this.X + "\ny坐标：" + this.Y;

            // 字体和填充颜色.
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Yellow);

            // 位置.
            PointF drawPoint = new PointF(5.0F, 5.0F);

            // 绘制.
            g.DrawString(drawString, drawFont, drawBrush, drawPoint);
        }


    }
}
