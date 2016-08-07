using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraGonQuest.General;

namespace DraGonQuest
{
    /// <summary>
    /// 血块类
    /// </summary>
    public class BloodBar : Element
    {
        /// <summary>
        /// 血条块的高度
        /// </summary>
        private const int m_WIDTH = 1;

        /// <summary>
        /// 血条快的长度
        /// </summary>
        private const int m_HEIGHT = 10;

        /// <summary>
        /// 血条块的总量
        /// </summary>
        private int m_allLife;

        /// <summary>
        ///当前血块的大小 
        /// </summary>
        private int m_nowLife;

        /// <summary>
        /// 设置当前生命值（血块值）
        /// </summary>
        public int NowLife
        {
            set   
            {
                if (m_nowLife > m_allLife)
                {
                    m_allLife = m_nowLife;
                }
                m_nowLife = value;
            }
        }

        /// <summary>
        /// 血块的构造函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="allLife"></param>
        public BloodBar(int x, int y, int allLife)
            : base(x, y)
        {
            this.m_allLife = allLife;
            this.m_nowLife = allLife;
        }

        /// <summary>
        /// 绘制血块
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
            // 绘制.
            g.DrawString("Hero life:", new Font("Arial", 15), new SolidBrush(Color.Yellow), x, y - 22);

            g.DrawRectangle(new Pen(Color.Red), x, y, m_WIDTH * m_allLife, m_HEIGHT);
            g.FillRectangle(new SolidBrush(Color.Red), x, y, m_WIDTH * m_nowLife, m_HEIGHT);
        }
    }
}
