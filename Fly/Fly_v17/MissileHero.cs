using DraGonQuest.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraGonQuest
{
    public class MissileHero : Missiles
    {
        /// <summary>
        /// 获取当前工作目录
        /// </summary>
        private static string m_ImagePath = Directory.GetCurrentDirectory() + "\\images\\";

        /// <summary>
        /// 获取图片
        /// </summary>
        private static Dictionary<string, Image> images = new Dictionary<string, Image>() 
        {
            {"L",Image.FromFile(m_ImagePath+"missileH_L.gif")},
            {"LU",Image.FromFile(m_ImagePath+"missileH_LU.gif")},
            {"LUU",Image.FromFile(m_ImagePath+"missileH_LUU.gif")},
            {"U",Image.FromFile(m_ImagePath+"missileH_U.gif")},
            {"RUU",Image.FromFile(m_ImagePath+"missileH_RUU.gif")},
            {"RU",Image.FromFile(m_ImagePath+"missileH_RU.gif")},
            {"R",Image.FromFile(m_ImagePath+"missileH_R.gif")},
            {"RD",Image.FromFile(m_ImagePath+"missileH_RD.gif")},
            {"RDD",Image.FromFile(m_ImagePath+"missileH_RDD.gif")},
            {"D",Image.FromFile(m_ImagePath+"missileH_D.gif")},
            {"LD",Image.FromFile(m_ImagePath+"missileH_LD.gif")},
            {"LDD",Image.FromFile(m_ImagePath+"missileH_LDD.gif")}
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
        public MissileHero(Roles role, bool good, int xspeed, int yspeed, MissileDirection dir, int power)
            : base(role, good, images["L"].Width, images["L"].Height, xspeed, yspeed, dir, power)
        {
        }

        /// <summary>
        /// 重写移动的方法
        /// </summary>
        protected override void Move()
        {
            base.Move();

            //子弹边界处理
            if (x < 0 || y < 0 || x > MainForm.m_GAMEWIDTH || y > MainForm.m_GAMEHEIGHT)
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
            base.Draw(g, images, x, y);
        }
    }
}
