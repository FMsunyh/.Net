using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Fly
{
    /// <summary>
    /// 游戏开始设置类
    /// </summary>
    public class GameStartSetting
    {
        /// <summary>
        /// 设置选项在游戏区的位置 x坐标
        /// </summary>
        private int m_x;

        /// <summary>
        /// 设置选项在游戏区的位置 y坐标
        /// </summary>
        private int m_y;

        /// <summary>
        /// 选项索引
        /// </summary>
        private int m_StepImage = 0;

        /// <summary>
        /// 获取图片路径
        /// </summary>
        private static string m_ImagePath = Directory.GetCurrentDirectory() + "\\images\\StartSelect\\";

        private static Image[] m_ImagesStartSetting = new Image[] 
        {
            Image.FromFile(m_ImagePath+"Fly_StartSelect1.png"),
            Image.FromFile(m_ImagePath+"Fly_StartSelect2.png"),
            Image.FromFile(m_ImagePath+"Fly_StartSelect3.png"),
            Image.FromFile(m_ImagePath+"Fly_StartSelect4.png"),
            Image.FromFile(m_ImagePath+"Fly_StartSelect5.png"),
            Image.FromFile(m_ImagePath+"Fly_SatrtBack.png")
        };

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public GameStartSetting(int x,int y)
        {
            this.m_x = x;
            this.m_y = y;
        }

        public void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Right:
                    {
                        this.m_StepImage++;
                        break;
                    }
                case Keys.Down:
                case Keys.Left:
                    {
                        this.m_StepImage--;
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// 绘制设置选项
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            g.DrawImage(m_ImagesStartSetting[m_ImagesStartSetting.Length-1],0,0);
            g.DrawImage(m_ImagesStartSetting[Math.Abs(m_StepImage)%5],m_x,m_y);
        }
    }
}
