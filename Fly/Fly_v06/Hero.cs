using Fly.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                case Keys.ControlKey:
                    Fire();
                    break;
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

            int xTop, yLeft =0;

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
            throw new NotImplementedException();
        }

        /// <summary>
        /// 英雄开火
        /// </summary>
        public override void Fire()
        {
            HitCheck.GetInstance().AddElement(new MissileHero(this, this.Good, 20, 20, MissileDirection.LUU, 10));
            HitCheck.GetInstance().AddElement(new MissileHero(this, this.Good, 20, 20, MissileDirection.U, 10));
            HitCheck.GetInstance().AddElement(new MissileHero(this, this.Good, 20, 20, MissileDirection.RUU, 10));
        }

        /// 重写父类 Roles的Draw方法
        /// </summary>
        /// <param name="g">画笔</param>
        public override void Draw(Graphics g)
        {
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

            if (y + this.m_HEIGHT > MainForm.m_GAMEHEIGHT - 90 )
            {
                y = MainForm.m_GAMEHEIGHT - 90 - this.m_HEIGHT;
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
