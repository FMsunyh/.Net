using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    class Global
    {
        /// <summary>
        /// 格子大小
        /// </summary>

        public const int m_CELL_SIZE = 25;

        /// <summary>
        /// 方块大小 宽
        /// </summary>
        public const int m_WIDHT = 23;

        /// <summary>
        /// 方块大小 高
        /// </summary>
        public const int m_HEIGHT = 23;

        /// <summary>
        /// 一行 最多能够容下的格子总数
        /// </summary>
        public const int m_NUM_WIDTH_CELL = 18;

        /// <summary>
        /// 一列 最多能够容下的格子总数
        /// </summary>
        public const int m_NUM_HEIGHT_CELL = 25;

        /// <summary>
        /// 游戏窗口大小 宽
        /// </summary>
        public const int m_GAME_WIDTH = m_NUM_WIDTH_CELL * m_CELL_SIZE;

        /// <summary>
        /// 游戏窗口大小 高
        /// </summary>
        public const int m_GAME_HEIGHT = m_NUM_HEIGHT_CELL * m_CELL_SIZE;
    }
}
