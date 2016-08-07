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
    public class MissileThree : Missiles
    {
        /// <summary>
        /// 敌人的子弹图片目录
        /// </summary>
        private static string m_ImagePath = Directory.GetCurrentDirectory() + "\\images\\";

        /// <summary>
        /// 敌人子弹的装载器
        /// </summary>
        private static Dictionary<string, Image> m_MissilesImage = new Dictionary<string, Image>() 
        {
            {"L",Image.FromFile(m_ImagePath+"missile3_L.gif")},
            {"LU",Image.FromFile(m_ImagePath+"missile3_LU.gif")},
            {"LUU",Image.FromFile(m_ImagePath+"missile3_LUU.gif")},
            {"U",Image.FromFile(m_ImagePath+"missile3_U.gif")},
            {"RU",Image.FromFile(m_ImagePath+"missile3_RU.gif")},
            {"RUU",Image.FromFile(m_ImagePath+"missile3_RUU.gif")},
            {"R",Image.FromFile(m_ImagePath+"missile3_R.gif")},
            {"RD",Image.FromFile(m_ImagePath+"missile3_RD.gif")},
            {"RDD",Image.FromFile(m_ImagePath+"missile3_RDD.gif")},
            {"D",Image.FromFile(m_ImagePath+"missile3_D.gif")},
            {"LD",Image.FromFile(m_ImagePath+"missile3_LD.gif")},
            {"LDD",Image.FromFile(m_ImagePath+"missile3_LDD.gif")},
        };

        /// <summary>
        /// 敌人子弹的构造函数
        /// </summary>
        /// <param name="role">角色（敌人）</param>
        /// <param name="good">好坏</param>
        /// <param name="width">子弹的宽度</param>
        /// <param name="height">子弹的高度</param>
        /// <param name="xspeed">子弹相对x轴的速度</param>
        /// <param name="yspeed">子弹相对y轴的速度</param>
        /// <param name="dir">子弹的方向</param>
        /// <param name="power">子弹的威力</param>
        public MissileThree(Roles role, bool good, int xspeed, int yspeed, MissileDirection dir, int power)
            : base(role, good, m_MissilesImage["L"].Width, m_MissilesImage["L"].Height, xspeed, yspeed, dir, power)
        {

        }

        /// <summary>
        /// 绘制敌人的子弹
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(System.Drawing.Graphics g)
        {
            if(!this.IsLive)
            {
                HitCheck.GetInstance().ReMoveElement(this);
                return;
            }
            this.Move();
            base.Draw(g,m_MissilesImage,x,y);
        }

        /// <summary>
        /// 敌人子弹移动的方法
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
    }
}
