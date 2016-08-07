using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris
{
    /// <summary>
    /// 图形类
    /// </summary>
    public class Shape
    {
        /// <summary>
        /// 存放当前方块的标示符数组
        /// </summary>
        private int[,] m_Body;

        /// <summary>
        /// 方块的状态
        /// </summary>
        private int m_Status;

        /// <summary>
        /// 方块距离原点的 X 轴坐标（格子坐标）
        /// </summary>
        private int m_Left;

        /// <summary>
        /// 方块距离原点的 Y 轴坐标（格子坐标）
        /// </summary>
        private int m_Top;

        //用来判断是否执行键盘操作用的
        public const int m_RorateL = 0;
        public const int m_RightL = 1;
        public const int m_LeftL = 2;
        public const int m_DownL = 3;

        private Color[] m_Color = new Color[] 
        {   Color.Beige,        Color.Cyan,         Color.Brown,
            Color.Chocolate,    Color.DarkGreen,    Color.DarkOrange,
            Color.Gold,         Color.Chartreuse,   Color.Plum,
            Color.Yellow,      Color.Khaki,        Color.Red
        };

        private Color color;

        private Random m_Rand= new Random();

        public Shape()
        {
            color = m_Color[m_Rand.Next(0, m_Color.Length)];
            m_Left = 20;
            m_Top = 6;
        }

        /// <summary>
        /// 左移
        /// </summary>
        public void MoveLeft()
        {
            m_Left--;
        }

        /// <summary>
        /// 右移
        /// </summary>
        public void MoveRight()
        {
            m_Left++;
        }

        /// <summary>
        /// 下移
        /// </summary>
        public void MoveDown()
        {
            m_Top++;
        }

        /// <summary>
        /// 旋转
        /// </summary>
        public void Rotate()
        {
            m_Status = (m_Status + 1) % (m_Body.Length / 16);
        }

        /// <summary>
        /// 绘制当前方块
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (GetFlagByPoint(x, y))
                    {
                        g.FillRectangle(new SolidBrush(color), (m_Left + x) * Global.m_CELL_SIZE, (m_Top + y) * Global.m_CELL_SIZE, Global.m_WIDHT, Global.m_HEIGHT);
                    }
                }
            }
        }

        /// <summary>
        /// 设置当前方块
        /// </summary>
        /// <param name="body"></param>
        public void SetBody(int[,] body)
        {
            this.m_Body = body;
        }

        /// <summary>
        /// 设置当前方块的初始状态
        /// </summary>
        /// <param name="status"></param>
        public void SetStutas(int status)
        {
            this.m_Status = status;
        }

        /// <summary>
        /// 返回当前点是否为图形点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool GetFlagByPoint(int x, int y)
        {
            return m_Body[m_Status, y * 4 + x] == 1;
        }

        /// <summary>
        /// 判断当前格子是否为图形的格子（有颜色的）
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="rotate"></param>
        /// <returns></returns>
        public bool IsNember(int x, int y, bool rotate)
        {
            int tempStatus = m_Status;

            if (rotate)
            {
                tempStatus = (m_Status + 1) % (m_Body.Length / 16);
            }

            return m_Body[tempStatus, y * 4 + x] == 1;
        }

        public int GetLeft()
        {
            return m_Left;
        }

        public void SetLeft(int value)
        {
            m_Left = value;
        }

        public void SetTop(int value)
        {
            m_Top = value;
        }

        public int GetTop()
        {
            return m_Top;
        }

    }
}
