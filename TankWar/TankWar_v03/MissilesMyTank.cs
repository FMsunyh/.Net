using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using TankWar.General;

namespace TankWar
{
    class MissilesMyTank : Missiles
    {

         /// <summary>
        /// 获取当前工作目录
        /// </summary>
        private static string m_ImagePath = Directory.GetCurrentDirectory() + "\\images\\missile\\";

        /// <summary>
        /// 获取图片
        /// </summary>
        private static Dictionary<string, Image> images = new Dictionary<string, Image>() 
        {
            {"L",Image.FromFile(m_ImagePath+"missileM_L.png")},
            {"LU",Image.FromFile(m_ImagePath+"missileM_LU.png")},
            {"U",Image.FromFile(m_ImagePath+"missileM_U.png")},
            {"RU",Image.FromFile(m_ImagePath+"missileM_RU.png")},
            {"R",Image.FromFile(m_ImagePath+"missileM_R.png")},
            {"RD",Image.FromFile(m_ImagePath+"missileM_RD.png")},
            {"D",Image.FromFile(m_ImagePath+"missileM_D.png")},
            {"LD",Image.FromFile(m_ImagePath+"missileM_LD.png")},
        };

        /// <summary>
        /// MyTank的子弹的构造函数
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="good">好坏</param>
        /// <param name="xspeed">x轴上的速度</param>
        /// <param name="yspeed">y轴上的速度</param>
        /// <param name="dir">方向</param>
        /// <param name="power">攻击能力</param>
        public MissilesMyTank(Roles role, bool good, int xspeed, int yspeed, RolesDirection dir, int power)
            : base(role,good, images["LU"].Width, images["LU"].Height,  xspeed, yspeed, dir, power)
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
                //IsLive = false;
            }
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            this.Move();
            base.Draw(g, images, x, y);
        }
    }
}
