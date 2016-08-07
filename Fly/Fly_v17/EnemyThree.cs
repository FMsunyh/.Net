using DraGonQuest.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraGonQuest
{
    /// <summary>
    /// 敌人类
    /// </summary>
    public class EnemyThree : Roles
    {
        /// <summary>
        /// 载入敌人的图片
        /// </summary>
        private static Image m_EnemyImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\Enemy\\Fly_Enemy231.png");

        private int step = 0;
        private Rectangle rect;

        /// <summary>
        ///敌人的构造函数
        /// </summary>
        /// <param name="x">初始x轴坐标</param>
        /// <param name="y">初始y轴坐标</param>
        /// <param name="good">好坏</param>
        /// <param name="xspeed">相对于x轴的速度</param>
        /// <param name="yspeed">相对于y轴的速度</param>
        /// <param name="life">生命值</param>
        public EnemyThree(int x, int y, bool good, int xspeed, int yspeed, int life)
            : base(x, y, good, m_EnemyImage.Width / 3, m_EnemyImage.Height, xspeed, yspeed, life)
        {
        }

        /// <summary>
        /// 死亡后，产生爆炸效果
        /// </summary>
        protected override void ThisBomb()
        {
            HitCheck.GetInstance().AddElement(new BombOne(this));
        }

        /// <summary>
        /// 绘制敌人
        /// </summary>
        /// <param name="g">画笔</param>
        public override void Draw(Graphics g)
        {
            if (!this.IsLive)
            {
                HitCheck.GetInstance().ReMoveElement(this);
                return;
            }
            this.Move();

            if (m_IsMissileHit)
            {
                step = 2;
                m_IsMissileHit = false;
            }
            else
            {
                step = 0;
            }

            rect = new Rectangle((m_EnemyImage.Width / 3) * step, 0, m_EnemyImage.Width / 3, m_EnemyImage.Height);
            base.Draw(g, m_EnemyImage, rect, x, y);
        }

        /// <summary>
        /// 敌人开火
        /// </summary>
        public override void Fire()
        {
            HitCheck.GetInstance().AddElement(new MissileThree(this, this.Good, 10, 10, Missiles.MissileDirections[MainForm.m_EnemyRandom.Next(7, 12)], 30));
        }

        /// <summary>
        ///敌人的移动，移动过程中，可以随机的开火
        /// </summary>
        protected override void Move()
        {
            y += m_YSPEED;

            //界面外的100像素是用来刷敌人的,所以判断是否超出-100以外
            if (x < -100 || y < -100 || x > MainForm.m_GAMEWIDTH + 100 || y > MainForm.m_GAMEHEIGHT + 100)
            {
                IsLive = false;
            }
            else
            {
                if (MainForm.m_EnemyRandom.Next(0, 100) < 3)
                {
                    this.Fire();
                }
            }
        }
    }
}
