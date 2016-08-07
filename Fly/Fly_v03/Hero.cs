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
    public class Hero : Roles
    {
        private static Image m_MyImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\myPlane.png");

        Rectangle m_Rect;

        private static int m_HeroWidth, m_HeroHeight;

        //用户是否按下"上\下\左\右"
        private bool PU = false, PD = false, PL = false, PR = false;



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
            MainForm.m_Missiles.Add(new MissileHero(this,this.Good, 20, 20, MissileDirection.LUU, 10));
            MainForm.m_Missiles.Add(new MissileHero(this, this.Good, 20, 20, MissileDirection.U, 10));
            MainForm.m_Missiles.Add(new MissileHero(this, this.Good, 20, 20, MissileDirection.RUU, 10));
        }
        /// 重写父类 Roles的Draw方法
        /// </summary>
        /// <param name="g">画笔</param>
        public override void Draw(Graphics g)
        {
            this.ConfirmRolesImage();
            this.Move();
            base.Draw(g, m_MyImage, m_Rect, x, y);
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

            if (x + this.m_WIDTH > MainForm.m_GAMEWIDTH - 25)
            {
                x = MainForm.m_GAMEWIDTH - 25 - this.m_WIDTH;
            }

            if (y + this.m_HEIGHT > MainForm.m_GAMEHEIGHT - 90)
            {
                y = MainForm.m_GAMEHEIGHT - 90 - this.m_HEIGHT;
            }
        }
    }
}
