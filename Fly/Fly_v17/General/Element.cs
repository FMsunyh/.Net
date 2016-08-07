using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraGonQuest.General
{
    public abstract class Element
    {
        /// <summary>
        /// 元素的坐标
        /// </summary>
        protected int x, y;

        public Element(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// 获取元素的横坐标
        /// </summary>
        public int X
        {
            get { return x; }
        }

        /// <summary>
        /// 获取元素的纵坐标
        /// </summary>
        public int Y
        {
            get { return y; }
        }

        private bool Live = true;

        /// <summary>
        /// 获取或设置角色的生命
        /// </summary>
        public bool IsLive
        {
            get { return Live; }
            set { Live = value; }
        }

        /// <summary>
        /// 绘制自己的方法
        /// </summary>
        /// <param name="g"></param>
        public abstract void Draw(Graphics g);
    }
}
