using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fly.General;

namespace Fly.Medal
{
    public abstract class Medals:Element
    {
        protected readonly int m_XSPEED;
        protected readonly int m_YSPEED;

        protected readonly int m_WIDTH;
        protected readonly int m_HEIGHT;

        //勋章等级
        private int m_Level;

        public int Level
        {
            get { return m_Level; }
            set { m_Level = value; }
        }

        public Medals(Roles role, int width, int height, int xspeed, int yspeed,int level)
            : base(role.X, role.Y)
        {
            this.m_XSPEED = xspeed;
            this.m_YSPEED = yspeed;

            this.m_WIDTH = width;
            this.m_HEIGHT = height;

            this.m_Level = level;
        }

        /// <summary>
        /// 获取角色的矩形区域
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRectangle()
        {
            return new Rectangle(X, Y, m_WIDTH, m_HEIGHT);
        }

        /// <summary>
        /// 角色移动的方法
        /// </summary>
        protected void Move()
        {
            y += m_YSPEED;
            if (y > MainForm.m_GAMEHEIGHT)
            {
                this.IsLive = false;
            }
        } 
    }
}
