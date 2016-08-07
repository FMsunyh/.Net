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
    /// <summary>
    /// 爆炸类
    /// </summary>
    public class BombOne : Element
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
            Image.FromFile(m_ImagePath+"blast1_0.gif"),
            Image.FromFile(m_ImagePath+"blast1_1.gif"),
            Image.FromFile(m_ImagePath+"blast1_2.gif"),
            Image.FromFile(m_ImagePath+"blast1_3.gif"),
            Image.FromFile(m_ImagePath+"blast1_4.gif"),
            Image.FromFile(m_ImagePath+"blast1_5.gif"),
            Image.FromFile(m_ImagePath+"blast1_6.gif"),
            Image.FromFile(m_ImagePath+"blast1_7.gif"),
            Image.FromFile(m_ImagePath+"blast1_8.gif")
        };

        /// <summary>
        /// 爆炸的构造函数
        /// </summary>
        /// <param name="role"></param>
        public BombOne(Roles role)
            : base(role.X + role.m_WIDTH / 2 - m_ImagesBomb[0].Width / 2, role.Y + role.m_HEIGHT / 2 - m_ImagesBomb[0].Height/2)
        {
 
        }

        /// <summary>
        /// 绘制爆炸效果
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
            //throw new NotImplementedException();
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
