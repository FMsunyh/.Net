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
    /// <summary>
    /// Boss爆炸类
    /// </summary>
    public class BombTwo : Element
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
            Image.FromFile(m_ImagePath+"blast2_0.gif"),
            Image.FromFile(m_ImagePath+"blast2_1.gif"),
            Image.FromFile(m_ImagePath+"blast2_2.gif"),
            Image.FromFile(m_ImagePath+"blast2_3.gif"),
            Image.FromFile(m_ImagePath+"blast2_4.gif"),
            Image.FromFile(m_ImagePath+"blast2_5.gif"),
            Image.FromFile(m_ImagePath+"blast2_6.gif"),
            Image.FromFile(m_ImagePath+"blast2_7.gif"),
            Image.FromFile(m_ImagePath+"blast2_8.gif")
        };

        /// <summary>
        /// Boss爆炸的构造函数
        /// </summary>
        /// <param name="role"></param>
        public BombTwo(Roles role)
            : base(role.X + role.m_WIDTH / 2 - m_ImagesBomb[0].Width / 2, role.Y + role.m_HEIGHT / 2 - m_ImagesBomb[0].Height/2)
        {
 
        }

        /// <summary>
        /// 绘制爆炸效果
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
            if (step < m_ImagesBomb.Length*4)
            {
                g.DrawImage(m_ImagesBomb[step/4], x, y);
                step++;
            }
            else
            {
                IsLive = false;
            }
        }
    }
}
