using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TankWar.General
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
        /// 创建角色或者子弹的构造函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="good"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="xspeed"></param>
        /// <param name="yspeed"></param>
        public RoAndMi(int x, int y, bool good, int width, int height, int xspeed, int yspeed)
            : base(x, y)
        {
            m_Good = good;
            m_WIDTH = width;
            m_HEIGHT = height;
            m_XSPEED = xspeed;
            m_YSPEED = yspeed;
        }

        public bool Good
        {
            get { return m_Good; }
            set { m_Good = value; }
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(X, Y, m_WIDTH, m_HEIGHT);
        }

        /// <summary>
        /// 角色移动的方法
        /// </summary>
        protected abstract void Move();
    }

}
