using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraGonQuest
{
    public class GameOver
    {
        /// <summary>
        /// 载入英雄图片
        /// </summary>
        private static Image m_GameOverImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\replace\\Fly_StrGameOver.png");


        public GameOver()
        {

        }

        public static void Draw(Graphics g)
        {
            g.DrawImage(m_GameOverImage, MainForm.m_GAMEWIDTH / 3 - m_GameOverImage.Width/2, MainForm.m_GAMEHEIGHT / 3);
        }
    }
}
