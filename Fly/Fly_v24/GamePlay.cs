using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Fly
{
    public class GamePlay
    {
        /// <summary>
        /// 获取图片路径
        /// </summary>
        private static string m_ImagePath = Directory.GetCurrentDirectory() + "\\images\\GamePlay\\";

        /// <summary>
        /// 载入游戏过关图片
        /// </summary>
        private static Image m_GamePassImage = Image.FromFile(m_ImagePath + "Fly_GamePass.png");

        /// <summary>
        /// 载入游戏结束图片
        /// </summary>
        private static Image m_GameOverImage = Image.FromFile(m_ImagePath+"Fly_StrGameOver.png");



        public GamePlay()
        {

        }

        public static void DrawGameOver(Graphics g)
        {
            g.DrawImage(m_GameOverImage, 10, 100);
        }

        public static void DrawGamePass(Graphics g)
        {
            HitCheck.GetInstance().Bombing(500);
            g.DrawImage(m_GamePassImage, 30, 100);          
        }


    }
}
