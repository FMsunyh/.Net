using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Fly.General;

namespace Fly
{
    /// <summary>
    /// 加血类
    /// </summary>
    public class EatBlood:Missiles
    {
        /// <summary>
        /// 敌人的子弹图片目录
        /// </summary>
        private static Image m_ImageEatBlood = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\Goods\\EatBlood.gif");

        private bool m_DirRight;
        private bool m_DirLeft;

        private int m_Disappear = 0;

        /// <summary>
        /// 加血块的构造函数
        /// </summary>
        /// <param name="role"></param>
        /// <param name="good"></param>
        /// <param name="xspeed"></param>
        /// <param name="yspeed"></param>
        /// <param name="dir"></param>
        /// <param name="power"></param>
        public EatBlood(Roles role, bool good, int xspeed, int yspeed, MissileDirection dir, int power)
            : base(role, good, m_ImageEatBlood.Width, m_ImageEatBlood.Height, xspeed, yspeed, dir, power)
        {
            this.m_DirRight = false;
            this.m_DirLeft = false;
        }

        /// <summary>
        /// 绘制加血心块
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(System.Drawing.Graphics g)
        {
            if(!this.IsLive)
            {
                HitCheck.GetInstance().ReMoveElement(this);
                return;
            }
            this.Move();
            g.DrawImage(m_ImageEatBlood, x, y);
        }

        /// <summary>
        /// 加血心块移动的方法
        /// </summary>
        protected override void Move()
        {
            if (m_DirLeft)
            {
                if (x < 550)
                {
                    x += m_XSPEED;
                }
                else
                {
                    m_DirLeft = !m_DirLeft;
                    m_Disappear++;
                }
            }
            else
            {
                if (x + this.m_WIDTH > 50)
                {
                    x -= m_XSPEED;
                }
                else
                {
                    m_DirLeft = !m_DirLeft;
                }
            }

            if (m_DirRight)
            {
                if (y > 0)
                {
                    y -= m_YSPEED;
                }
                else
                {
                    m_DirRight = !m_DirRight;
                }
            }
            else
            {
                if (y < 700)
                {
                    y += m_YSPEED;
                }
                else
                {
                    m_DirRight = !m_DirRight;
                }
            }

            if (m_Disappear > 1)
            {
                this.IsLive = false;
            }
        }
    }
}
