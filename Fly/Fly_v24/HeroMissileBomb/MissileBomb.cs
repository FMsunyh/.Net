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
    public abstract class MissileBomb : Missiles
    {
        /// <summary>
        /// 获取图片路径
        /// </summary>
        protected static string m_ImagePath = Directory.GetCurrentDirectory() + "\\images\\HeroMissileBomb\\";

        protected int m_RoleY;

        public MissileBomb(Roles role, bool good,int width,int height, int xspeed, int yspeed, MissileDirection dir, int power)
            : base(role, good, width, height, xspeed, yspeed, MissileDirection.U, power)
        {
            m_RoleY = role.Y;
        }
    }
}
