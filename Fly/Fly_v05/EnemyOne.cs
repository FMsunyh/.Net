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
    /// 敌人类
    /// </summary>
    public class EnemyOne : Roles
    {
        /// <summary>
        /// 载入敌人的图片
        /// </summary>
        private static Image m_EnemyImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\enemy1.gif");

        /// <summary>
        /// 敌人的初始方向
        /// </summary>
        public bool m_StartDir;

        /// <summary>
        ///敌人的构造函数
        /// </summary>
        /// <param name="x">初始x轴坐标</param>
        /// <param name="y">初始y轴坐标</param>
        /// <param name="good">好坏</param>
        /// <param name="xspeed">相对于x轴的速度</param>
        /// <param name="yspeed">相对于y轴的速度</param>
        /// <param name="life">生命值</param>
        /// <param name="startDir">初始方向</param>
        public EnemyOne(int x, int y, bool good, int xspeed, int yspeed, int life, bool startDir)
            : base(x, y, good, m_EnemyImage.Width, m_EnemyImage.Height, xspeed, yspeed, life)
        {
            this.m_StartDir = startDir;
        }

        /// <summary>
        /// 死亡后，产生爆炸效果
        /// </summary>
        protected override void ThisBomb()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 绘制敌人
        /// </summary>
        /// <param name="g">画笔</param>
        public override void Draw(Graphics g)
        {
            this.Move();
            base.Draw(g, m_EnemyImage, x, y);
        }

        /// <summary>
        /// 敌人开火
        /// </summary>
        public override void Fire()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        ///敌人的移动，移动过程中，可以随机的开火
        /// </summary>
        protected override void Move()
        {
            if (m_StartDir)
            {
                if (x + this.m_WIDTH < MainForm.m_GAMEWIDTH - 20)
                {
                    x += m_XSPEED;
                }
                else
                {
                    //碰到右边界，改变方向
                    m_StartDir = !m_StartDir;
                }
            }
            else
            {
                if (x > 0)
                {
                    x -= m_XSPEED;
                }
                else
                {
                    //碰到左边界，改变方向
                    m_StartDir = !m_StartDir;
                }
            }
            y += (int)(0.5 * m_YSPEED);

            //界面外的100像素是用来刷敌人的,所以判断是否超出-100以外
            if (x < -100 || y < -100 || x > MainForm.m_GAMEWIDTH + 100 || y > MainForm.m_GAMEHEIGHT + 100)
            {
                IsLive = false;
            }
            else
            {
                if (MainForm.m_EnemyRandom.Next(0, 100) < 5)
                {
                    this.Fire();
                }
            }
        }
    }
}
