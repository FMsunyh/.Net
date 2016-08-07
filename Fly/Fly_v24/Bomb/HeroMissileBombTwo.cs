using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Fly.General;

namespace Fly
{
    /// <summary>
    /// 炸弹爆炸类
    /// </summary>
    public class HeroMissileBombTwo : Element
    {
        /// <summary>
        /// 实现爆炸效果，每次画一张
        /// </summary>
        private int m_Speed = 30;

        /// <summary>
        /// 获取图片路径
        /// </summary>
        private static string m_ImagePath = Directory.GetCurrentDirectory() + "\\images\\HeroMissileBomb\\";

        private static Image m_ImagesBomb = Image.FromFile(m_ImagePath + "YuShi.gif");

        /// <summary>
        /// 炸弹爆炸的构造函数
        /// </summary>
        /// <param name="role"></param>
        public HeroMissileBombTwo(Missiles missile)
            : base(missile.X + missile.m_WIDTH / 2 - m_ImagesBomb.Width / 2, missile.Y + missile.m_HEIGHT / 2 - m_ImagesBomb.Height / 2)
        {

        }

        /// <summary>
        /// 绘制爆炸效果
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {

            g.DrawImage(m_ImagesBomb, x, y);
            Move();
        }

        private void Move()
        {
            if (y < MainForm.m_GAMEHEIGHT)
            {
                y += m_Speed;
                m_Speed++;
            }
            else
            {
                //调用Bombing方法让敌人减血
                HitCheck.GetInstance().Bombing(5);
                IsLive = false;
            }
        }

    }
}
