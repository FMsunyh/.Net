using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Tetris
{
    /// <summary>
    /// 累积方块类
    /// </summary>
    public class Ground
    {
        /// <summary>
        /// 累积方块
        /// </summary>
        private int[,] m_Obstacles = new int[Global.m_NUM_WIDTH_CELL, Global.m_NUM_HEIGHT_CELL];

        /// <summary>
        /// 积分
        /// </summary>
        private int m_Score;


        public Ground()
        {
           m_Score = 0;
           Init();
        }

        public void Init()
        {
            m_Score = 0;

            for (int x = 0; x < Global.m_NUM_WIDTH_CELL; x++)
            {
                for (int y = 0; y < Global.m_NUM_HEIGHT_CELL; y++)
                {
                    m_Obstacles[x, y] = 0;
                }
            }
            
        }

        /// <summary>
        /// 累积方块
        /// </summary>
        /// <param name="shape"></param>
        public void AcceptShape(Shape shape)
        {
            int left = shape.GetLeft();
            int top = shape.GetTop();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (left + x < Global.m_NUM_WIDTH_CELL && top + y < Global.m_NUM_HEIGHT_CELL)
                    {
                        if (shape.IsNember(x, y, false))
                        {
                            m_Obstacles[shape.GetLeft() + x, shape.GetTop() + y] = 1;
                        }
                    }
                }
            }

            //查看是否有满行
            DeleteFullLine();

        }

        /// <summary>
        /// 绘制累积的方块
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            for (int x = 0; x < Global.m_NUM_WIDTH_CELL; x++)
            {
                for (int y = 0; y < Global.m_NUM_HEIGHT_CELL; y++)
                {
                    if (m_Obstacles[x, y] == 1)
                    {
                        g.FillRectangle(new SolidBrush(Color.Blue), x * Global.m_CELL_SIZE, y * Global.m_CELL_SIZE, Global.m_WIDHT, Global.m_HEIGHT);
                    }
                }
            }

            //绘制积分
            g.DrawString("Scores: "+m_Score.ToString(), new Font("Arial", 16), new SolidBrush(Color.Yellow), new PointF(Global.m_GAME_WIDTH+5, 5.0F));
        }

        /// <summary>
        /// 查找填满的行
        /// </summary>
        private void DeleteFullLine()
        {
            for (int y = Global.m_NUM_HEIGHT_CELL - 1; y >= 0; y--)
            {
                bool full = true;
                for (int x = 0; x < Global.m_NUM_WIDTH_CELL; x++)
                {
                    if (m_Obstacles[x, y] == 0)
                    {
                        full = false;
                    }
                }

                if (full)
                {
                    DeleteLine(y);
                    y++;
                    m_Score = m_Score + 100;
                }
            }
        }

        /// <summary>
        /// 删除填满的行
        /// </summary>
        /// <param name="numLine"></param>
        private void DeleteLine(int numLine)
        {
            for (int y = numLine; y > 0; y--)
            {
                for (int x = 0; x < Global.m_NUM_WIDTH_CELL; x++)
                {
                    m_Obstacles[x, y] = m_Obstacles[x, y-1];
                }
            }

            for (int x = 0; x < Global.m_NUM_WIDTH_CELL; x++)
            {
                m_Obstacles[x, 0] = 0;
            }
        }

        /// <summary>
        /// 判断是否接触的累积的方块
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool IsMoveAble(Shape shape, int action)
        {
            int left = shape.GetLeft();
            int top = shape.GetTop();

            switch (action)
            {
                case Shape.m_LeftL:
                    left--;
                    break;
                case Shape.m_RightL:
                    left++;
                    break;
                case Shape.m_DownL:
                    top++;
                    break;
                default:
                    break;
            }

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (shape.IsNember(x, y, action == Shape.m_RorateL))
                    {
                        if (top + y >= Global.m_NUM_HEIGHT_CELL
                            || left + x < 0
                            || left + x >= Global.m_NUM_WIDTH_CELL
                            || m_Obstacles[left + x, top + y] == 1
                            )
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 判断是否到顶了
        /// </summary>
        /// <returns></returns>
        public bool IsFull()
        {
            for (int x = 0; x < Global.m_NUM_WIDTH_CELL; x++)
            {
                if (m_Obstacles[x, 0] == 1)
                {
                    return false;
                }
            }
     
            return true;
        }

    }
}
