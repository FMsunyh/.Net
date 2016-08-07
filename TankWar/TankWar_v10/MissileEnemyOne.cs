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
    /// 敌人EnemyOne 的子弹类
    /// </summary>
    class MissileEnemyOne : Missiles
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
            {"L",Image.FromFile(m_ImagePath+"EnemyOne.png")},
            {"LU",Image.FromFile(m_ImagePath+"EnemyOne.png")},
            {"U",Image.FromFile(m_ImagePath+"EnemyOne.png")},
            {"RU",Image.FromFile(m_ImagePath+"EnemyOne.png")},
            {"R",Image.FromFile(m_ImagePath+"EnemyOne.png")},
            {"RD",Image.FromFile(m_ImagePath+"EnemyOne.png")},
            {"D",Image.FromFile(m_ImagePath+"EnemyOne.png")},
            {"LD",Image.FromFile(m_ImagePath+"EnemyOne.png")},
        };

        /// <summary>
        /// 坦克的子弹的构造函数
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="good">好坏</param>
        /// <param name="xspeed">x轴上的速度</param>
        /// <param name="yspeed">y轴上的速度</param>
        /// <param name="dir">方向</param>
        /// <param name="power">攻击能力</param>
        public MissileEnemyOne(Roles role, bool good, int xspeed, int yspeed, RolesDirection dir, int power)
            : base(role,good, 20, 20,  xspeed, yspeed, dir, power)
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
        /// 绘制子弹
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(System.Drawing.Graphics g)
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
