using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fly.General
{
    /// <summary>
    /// 角色类（英雄、敌人、子弹）
    /// </summary>
    public abstract class Roles : RoAndMi
    {
        /// <summary>
        /// 角色的生命值
        /// </summary>
        protected int m_life;

        /// <summary>
        /// 角色方向
        /// </summary>
        protected RolesDirection dir = RolesDirection.STOP;

        public bool m_IsMissileHit = false;

        /// <summary>
        /// 角色的构造函数
        /// </summary>
        /// <param name="x">指定位置的x坐标</param>
        /// <param name="y">指定位置的y坐标</param>
        /// <param name="good">好坏</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="xspeed">相对于x轴的速度</param>
        /// <param name="yspeed">相对于y轴的速度</param>
        /// <param name="life">生命值</param>
        public Roles(int x, int y, bool good, int width, int height, int xspeed, int yspeed, int life)
            : base(x, y, good, width, height, xspeed, yspeed)
        {
            this.m_life = life;
        }

        /// <summary>
        /// 死亡的方法
        /// </summary>
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


        /// <summary>
        /// 抽象方法，角色开火
        /// </summary>
        public abstract void Fire();

        /// <summary>
        /// 角色移动
        /// </summary>
        protected override void Move()
        {
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
        }

        /// <summary>
        /// 绘制角色的方法
        /// </summary>
        /// <param name="g">画笔</param>
        /// <param name="image">绘制的图片</param>
        /// <param name="x">指定位置的x坐标</param>
        /// <param name="y">指定位置的y坐标</param>
        protected void Draw(Graphics g, Image image, int x, int y)
        {
            g.DrawImage(image, x, y);
        }

        /// <summary>
        /// 绘制角色的方法
        /// </summary>
        /// <param name="g">画笔</param>
        /// <param name="image">绘制的原图片</param>
        /// <param name="rect">在原图片上选取部分区域大小</param>
        /// <param name="x">指定位置的x坐标</param>
        /// <param name="y">指定位置的y坐标</param>
        protected void Draw(Graphics g, Image image, Rectangle rect, int x, int y)
        {
            g.DrawImage(image, x, y, rect, GraphicsUnit.Pixel);
        }
    }
}
