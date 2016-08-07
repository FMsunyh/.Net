using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TankWar.General
{
    public abstract class Roles : RoAndMi
    {
        /// <summary>
        /// 角色的生命值
        /// </summary>
        protected int m_life;

        protected RolesDirection dir = RolesDirection.STOP;


        /// <summary>
        /// 记录移动前一次坐标
        /// </summary>
        private Point m_OldXY;


        public Roles(int x, int y, bool good, int width, int height, int xspeed, int yspeed, int life)
            : base(x, y, good, width, height, xspeed, yspeed)
        {
            this.m_life = life;
            m_OldXY = new Point(x,y);
        }


        public void Death()
        {
            this.m_life = 0;
            this.IsLive = false;

            //引发爆炸
            ThisBomb();
        }

        /// <summary>
        /// 角色爆炸的方法
        /// </summary>
        protected abstract void ThisBomb();

        /// <summary>
        /// 定义角色流血的方法
        /// </summary>
        /// <param name="i">流血量</param>
        public virtual void Bleeding(int i)
        {
            if (this.IsLive)
            {
                m_life -= i;
            }

            if (m_life <= 0)
            {
                this.Death();
            }
        }

        //角色开火的抽象方法
        public abstract void Fire();

        /// <summary>
        /// 角色移动
        /// </summary>
        protected override void Move()
        {
            m_OldXY = new Point(x, y);

            switch (dir)
            {
                case RolesDirection.L:
                    {
                        x -= m_XSPEED;
                        break;
                    }

                case RolesDirection.LU:
                    {
                        x -= m_XSPEED;
                        y -= m_YSPEED;
                        break;
                    }
                case RolesDirection.U:
                    {
                        y -= m_YSPEED;
                        break;
                    }
                case RolesDirection.RU:
                    {
                        x += m_XSPEED;
                        y -= m_YSPEED;
                        break;
                    }
                case RolesDirection.R:
                    {
                        x += m_XSPEED;
                        break;
                    }
                case RolesDirection.RD:
                    {
                        x += m_XSPEED;
                        y += m_YSPEED;
                        break;
                    }
                case RolesDirection.D:
                    {
                        y += m_YSPEED;
                        break;
                    }
                case RolesDirection.LD:
                    {
                        x -= m_XSPEED;
                        y += m_YSPEED;
                        break;
                    }
                case RolesDirection.STOP:
                    break;
                default: break;
            }

            //超出边界检测
            if (x < 0) x = 0;
            if (y < 0) y = 0;

            if (x + this.m_WIDTH > MainForm.m_GAMEWIDTH - 20)
            {
                x = MainForm.m_GAMEWIDTH - this.m_WIDTH - 20;
            }

            if (y + this.m_HEIGHT > MainForm.m_GAMEHEIGHT - 20)
            {
                y = MainForm.m_GAMEHEIGHT - this.m_HEIGHT - 20;
            }

        }


        public void StopMove()
        {
            x = m_OldXY.X;
            y = m_OldXY.Y;
        }



        /// <summary>
        /// 绘制角色
        /// </summary>
        /// <param name="g">画笔</param>
        /// <param name="image">图片</param>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        protected void Draw(Graphics g, Image image, int x, int y)
        {
            g.DrawImage(image, x, y);
        }

    }
}
