using Fly.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Fly
{
    public abstract class MissileHero : Missiles
    {
        /// <summary>
        /// 获取当前工作目录
        /// </summary>
        protected static string m_ImagePath = Directory.GetCurrentDirectory() + "\\images\\Missiles\\Hero\\";       

        /// <summary>
        /// 英雄的子弹的构造函数
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="good">好坏</param>
        /// <param name="xspeed">x轴上的速度</param>
        /// <param name="yspeed">y轴上的速度</param>
        /// <param name="dir">方向</param>
        /// <param name="power">攻击能力</param>
        public MissileHero(Roles role, bool good,int width,int height, int xspeed, int yspeed, MissileDirection dir, int power)
            : base(role, good, width, height, xspeed, yspeed, dir, power)
        {
        }        
    }
}
