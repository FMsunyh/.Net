using Fly.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fly
{
    public class MissileHeroFour : MissileHero
    {
        private bool m_IsLeftRight,m_Step=true;

        private int m_yStep = 20;
        /// <summary>
        /// 获取图片
        /// </summary>
        private static Dictionary<string, Image> images = new Dictionary<string, Image>() 
        {
            {"U",Image.FromFile(m_ImagePath+"missileFour.gif")},    
        };

        /// <summary>
        /// 英雄的子弹的构造函数
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="good">好坏</param>
        /// <param name="xspeed">x轴上的速度</param>
        /// <param name="yspeed">y轴上的速度</param>
        /// <param name="dir">方向</param>
        /// <param name="power">攻击能力</param>
        public MissileHeroFour(Roles role, bool good, int xspeed, int yspeed, MissileDirection dir, int power, bool isLeftRight)
            : base(role, good, 7, 28, xspeed, yspeed, dir, power)
        {
            m_IsLeftRight = isLeftRight;
        }

        /// <summary>
        /// 重写移动的方法
        /// </summary>
        protected override void Move()
        {
            if (m_Step)
            {
                if (m_IsLeftRight)
                {
                    x += m_XSPEED;
                }
                else
                {
                    x -=m_XSPEED;
                }
                m_Step = false;
            }
            else
            {
                y -= m_YSPEED + m_yStep * 2;
                m_yStep++;
            }

            //子弹边界处理
            if (y < 0)
            {
                IsLive = false;
            }
        }

        /// <summary>
        /// 重写绘制的方法
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
            if (!this.IsLive)
            {
                HitCheck.GetInstance().ReMoveElement(this);
                return;
            }

            this.Move();
            base.Draw(g, images, x + 8, y);
        }
    }
}
