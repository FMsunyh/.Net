using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using TankWar.General;

namespace TankWar
{
    class TankDeathBomp:Element
    {
        /// <summary>
        /// 实现爆炸效果，每次画一张
        /// </summary>
        private int step = 0;

        /// <summary>
        /// 获取图片路径
        /// </summary>
        private static string m_ImagePath = Directory.GetCurrentDirectory() + "\\images\\Bomp\\";

        private static Image[] m_ImagesBomb = new Image[] 
        {
            Image.FromFile(m_ImagePath+"bomb_1.png"),
            Image.FromFile(m_ImagePath+"bomb_2.png"),
            Image.FromFile(m_ImagePath+"bomb_3.png"),
            Image.FromFile(m_ImagePath+"bomb_1.png"),
            Image.FromFile(m_ImagePath+"bomb_2.png"),
            Image.FromFile(m_ImagePath+"bomb_1.png"),
            Image.FromFile(m_ImagePath+"bomb_3.png"),
            Image.FromFile(m_ImagePath+"bomb_2.png"),
            Image.FromFile(m_ImagePath+"bomb_1.png")
        };

        /// <summary>
        /// 爆炸的构造函数
        /// </summary>
        /// <param name="role"></param>
        public TankDeathBomp(Roles role)
            : base(role.X + role.m_WIDTH / 2 - m_ImagesBomb[0].Width / 2, role.Y + role.m_HEIGHT / 2 - m_ImagesBomb[0].Height/2)
        {
 
        }

        /// <summary>
        /// 绘制爆炸效果
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {

            if (!this.IsLive)
            {
                HitCheck.GetInstance().ReMoveElement(this);
                return;
            }

            if (step < m_ImagesBomb.Length)
            {
                g.DrawImage(m_ImagesBomb[step], x, y);
                step++;
            }
            else
            {
                IsLive = false;
            }
        }
    }
}
