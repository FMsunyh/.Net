using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using TankWar.General;

namespace TankWar
{
    /// <summary>
    /// 加血类
    /// </summary>
    public class EatBlood : Missiles
    {
        /// <summary>
        /// 敌人的子弹图片目录
        /// </summary>
        private static Image m_ImageEatBlood = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\EatBlood.gif");

        /// <summary>
        /// 加血块的构造函数
        /// </summary>
        /// <param name="role"></param>
        /// <param name="good"></param>
        /// <param name="xspeed"></param>
        /// <param name="yspeed"></param>
        /// <param name="dir"></param>
        /// <param name="power"></param>
        public EatBlood(Roles role, bool good, RolesDirection dir, int power)
            : base(role, good, m_ImageEatBlood.Width, m_ImageEatBlood.Height, 0,0,dir, power)
        {
        }

        /// <summary>
        /// 绘制加血心块
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
            if (!this.IsLive)
            {
                HitCheck.GetInstance().ReMoveElement(this);
                return;
            }
            g.DrawImage(m_ImageEatBlood, x, y);
        }
    }
}
