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
    /// 爆炸类
    /// </summary>
    public class EnemyBomb : Element
    {
        /// <summary>
        /// 实现爆炸效果，每次画一张
        /// </summary>
        private int step = 0;

        /// <summary>
        /// 获取图片路径
        /// </summary>
        private static string m_ImagePath = Directory.GetCurrentDirectory() + "\\images\\Bomp\\";

        private static Image m_ImagesBomb = Image.FromFile(m_ImagePath + "EnemyBomb.gif");
   

        /// <summary>
        /// 爆炸的构造函数
        /// </summary>
        /// <param name="role"></param>
        public EnemyBomb(Roles role)
            : base(role.X + role.m_WIDTH / 2 - m_ImagesBomb.Width / 12, role.Y + role.m_HEIGHT / 2 - m_ImagesBomb.Height / 2)
        {

        }

        /// <summary>
        /// 绘制爆炸效果
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
            Rectangle rect = new Rectangle(m_ImagesBomb.Width / 6 * step, 0, m_ImagesBomb.Width / 6, m_ImagesBomb.Height);
            if (step < 6)
            {
                g.DrawImage(m_ImagesBomb, x, y,rect, GraphicsUnit.Pixel);
                step++;
            }
            else
            {
                IsLive = false;
            }
        }
    }
}
