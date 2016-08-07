using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Fly.General;
using System.IO;


namespace Fly
{
    /// <summary>
    /// 吃子弹类，升级主人公子弹
    /// </summary>
    public class EatGoodsFour:Missiles
    {
        /// <summary>
        /// 西瓜图片目录
        /// </summary>
        private static Image m_ImageEatGoods = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\Goods\\Fly_Goods04.gif");

        private bool m_DirRight;
        private bool m_DirLeft;

        //选取图片位置
        private int m_Step = 0;
        Rectangle rect;

        /// <summary>
        /// 升级子弹的构造函数
        /// </summary>
        /// <param name="role"></param>
        /// <param name="good"></param>
        /// <param name="xspeed"></param>
        /// <param name="yspeed"></param>
        /// <param name="dir"></param>
        /// <param name="power"></param>
        public EatGoodsFour(Roles role, bool good, int xspeed, int yspeed, MissileDirection dir, int power)
            : base(role, good, m_ImageEatGoods.Width, m_ImageEatGoods.Height, xspeed, yspeed, dir, power)
        {
            this.m_DirRight = false;
            this.m_DirLeft = false;
        }

        /// <summary>
        /// 绘制升级子弹
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
            if (m_Step < 1)
            {
                m_Step++;
            }
            else
            {
                m_Step = 0;
            }
            rect = new Rectangle(m_ImageEatGoods.Width / 2 * m_Step, 0, m_ImageEatGoods.Width / 2, m_ImageEatGoods.Height);
            g.DrawImage(m_ImageEatGoods, x, y, rect, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// 升级子弹移动的方法
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
                if (y < 500)
                {
                    y += m_YSPEED;
                }
                else
                {
                    m_DirRight = !m_DirRight;
                }
            }
        }
    }
}
