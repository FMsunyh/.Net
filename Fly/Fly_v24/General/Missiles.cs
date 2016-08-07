﻿using Fly.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace Fly
{
    /// <summary>
    /// 子弹类
    /// </summary>
    public abstract class Missiles : RoAndMi
    {
        /// <summary>
        /// 子弹的方向
        /// </summary>
        public static MissileDirection[] MissileDirections = new MissileDirection[]
        {
            MissileDirection.L,
            MissileDirection.LU,
            MissileDirection.LUU,
            MissileDirection.U,
            MissileDirection.RUU,
            MissileDirection.RU,
            MissileDirection.R,
            MissileDirection.RD,
            MissileDirection.RDD,
            MissileDirection.D,
            MissileDirection.LDD,
            MissileDirection.LD,
        };

        public static double COT30 = 1.732;
        public static double COT60 = 0.577;

        /// <summary>
        /// 角色（子弹）的方向
        /// </summary>
        protected MissileDirection m_Dir; //默认是停止的

        /// <summary>
        /// 子弹的威力
        /// </summary>
        protected int m_Power;

        public int Power
        {
            get { return m_Power; }
        }

        /// <summary>
        /// 子弹的构造方法
        /// </summary>
        /// <param name="role">角色（敌人或者英雄）</param>
        /// <param name="good">好坏</param>
        /// <param name="width">角色的宽度</param>
        /// <param name="height">角色的高度</param>
        /// <param name="xspeed">相对于x轴的速度</param>
        /// <param name="yspeed">相对于y轴的速度</param>
        /// <param name="dir">角色的方向</param>
        /// <param name="power">角色的攻击能力</param>
        public Missiles(Roles role, bool good, int width, int height, int xspeed, int yspeed, MissileDirection dir, int power)
            : base(
            (int)(role.X + role.m_WIDTH / 2 - width / 2),
            (int)(role.Y + role.m_HEIGHT / 2 - height / 2),
            good, width, height, xspeed, xspeed)
        {
            this.m_Dir = dir;
            this.m_Power = power;
        }

        /// <summary>
        /// 重写父类 RoAndMi的Move方法
        /// 子弹的移动方法
        /// </summary>
        protected override void Move()
        {
            switch (m_Dir)
            {
                case MissileDirection.L:
                    {
                        x -= m_XSPEED;
                        break;
                    }
                case MissileDirection.LU:
                    {
                        x -= (int)(COT30 * m_XSPEED);
                        y -= m_YSPEED;
                        break;
                    }
                case MissileDirection.LUU:
                    {
                        x -= (int)(COT60 * m_XSPEED);
                        y -= m_YSPEED;
                        break;
                    }
                case MissileDirection.U:
                    {
                        y -= m_YSPEED;
                        break;
                    }
                case MissileDirection.RU:
                    {
                        x += (int)(COT30 * m_XSPEED);
                        y -= m_YSPEED;
                        break;
                    }
                case MissileDirection.RUU:
                    {
                        x += (int)(COT60 * m_XSPEED);
                        y -= m_YSPEED;
                        break;
                    }
                case MissileDirection.R:
                    {
                        x += m_XSPEED;
                        break;
                    }
                case MissileDirection.RD:
                    {
                        x += (int)(COT30 * m_XSPEED);
                        y += m_YSPEED;
                        break;
                    }
                case MissileDirection.RDD:
                    {
                        x += (int)(COT60 * m_XSPEED);
                        y += m_YSPEED;
                        break;
                    }
                case MissileDirection.D:
                    {
                        y += m_YSPEED;
                        break;
                    }
                case MissileDirection.LD:
                    {
                        x -= (int)(COT30 * m_XSPEED);
                        y += m_YSPEED;
                        break;
                    }
                case MissileDirection.LDD:
                    {
                        x -= (int)(COT60 * m_XSPEED);
                        y += m_YSPEED;
                        break;
                    }
                default: break;
            }
        }

        /// <summary>
        /// 重写父类RoAndMi的 Darw方法
        /// </summary>
        /// <param name="g">画笔</param>
        /// <param name="images">绘制的图片</param>
        /// <param name="x">指定位置的x轴坐标</param>
        /// <param name="y">指定位置的y轴坐标</param>
        protected void Draw(Graphics g, Dictionary<string, Image> images, int x, int y)
        {
            switch (m_Dir)
            {
                case MissileDirection.L:
                    {
                        g.DrawImage(images["L"], x, y);
                        break;
                    }
                case MissileDirection.LU:
                    {
                        g.DrawImage(images["LU"], x, y);
                        break;
                    }
                case MissileDirection.LUU:
                    {
                        g.DrawImage(images["LUU"], x, y);
                        break;
                    }
                case MissileDirection.U:
                    {
                        g.DrawImage(images["U"], x, y);
                        break;
                    }
                case MissileDirection.RU:
                    {
                        g.DrawImage(images["RU"], x, y);
                        break;
                    }
                case MissileDirection.RUU:
                    {
                        g.DrawImage(images["RUU"], x, y);
                        break;
                    }
                case MissileDirection.R:
                    {
                        g.DrawImage(images["R"], x, y);
                        break;
                    }
                case MissileDirection.RD:
                    {
                        g.DrawImage(images["RD"], x, y);
                        break;
                    }
                case MissileDirection.RDD:
                    {
                        g.DrawImage(images["RDD"], x, y);
                        break;
                    }
                case MissileDirection.D:
                    {
                        g.DrawImage(images["D"], x, y);
                        break;
                    }
                case MissileDirection.LD:
                    {
                        g.DrawImage(images["LD"], x, y);
                        break;
                    }
                case MissileDirection.LDD:
                    {
                        g.DrawImage(images["LDD"], x, y);
                        break;
                    }
                default: break;
            }
        }
    }
}
