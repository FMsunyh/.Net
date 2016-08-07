using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace Fly.General
{
    public abstract class RoAndMi : Element
    {
        /// <summary>
        /// 角色（子弹）是否是好的
        /// </summary>
        private bool m_Good;


        /// <summary>
        /// 角色（子弹）的宽度
        /// </summary>
        public readonly int m_WIDTH;

        /// <summary>
        /// 角色（子弹）的高度
        /// </summary>
        public readonly int m_HEIGHT;

        /// <summary>
        /// 角色（子弹）在X轴的速度
        /// </summary>
        protected readonly int m_XSPEED;

        /// <summary>
        /// 角色（子弹）在Y轴的速度
        /// </summary>
        protected readonly int m_YSPEED;

        /// <summary>
        /// 角色（敌人和英雄）的构造函数
        /// </summary>
        /// <param name="x">指定的x轴坐标</param>
        /// <param name="y">指定的y轴坐标</param>
        /// <param name="good">好坏</param>
        /// <param name="width">角色的宽度</param>
        /// <param name="height">角色的高度</param>
        /// <param name="xspeed">相对于x轴的速度</param>
        /// <param name="yspeed">相对于y轴的速度</param>
        public RoAndMi(int x, int y, bool good, int width, int height, int xspeed, int yspeed)
            : base(x, y)
        {
            m_Good = good;
            m_WIDTH = width;
            m_HEIGHT = height;
            m_XSPEED = xspeed;
            m_YSPEED = yspeed;
        }

        /// <summary>
        /// 设置的角色的好坏
        /// </summary>
        public bool Good
        {
            get { return m_Good; }
            set { m_Good = value; }
        }

        /// <summary>
        /// 获取角色的矩形区域
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRectangle()
        {
            return new Rectangle(X,Y,m_WIDTH,m_HEIGHT);
        }

        /// <summary>
        /// 角色移动的方法
        /// </summary>
        protected abstract void Move(); 
    }
}
