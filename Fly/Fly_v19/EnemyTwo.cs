using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fly.General;

namespace Fly
{
    public class EnemyTwo : Roles
    {
        /// <summary>
        /// 载入敌人的图片
        /// </summary>
        private static Image m_EnemyImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\Enemy\\Fly_Enemy352.png");

        /// <summary>
        /// 敌人的初始方向
        /// </summary>
        public bool m_StartDir;

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
        /// <param name="startDir">初始方向</param>
        public EnemyTwo(int x, int y, bool good, int xspeed, int yspeed, int life, bool startDir)
            : base(x, y, good, m_EnemyImage.Width / 3, m_EnemyImage.Height, xspeed, yspeed, life)
        {
            this.m_StartDir = startDir;
        }

        /// <summary>
        /// 死亡后，产生爆炸效果
        /// </summary>
        protected override void ThisBomb()
        {
            HitCheck.GetInstance().AddElement(new BombOne(this));

            //爆炸时，放出吃的东西，升级子弹
            if (MainForm.m_EnemyRandom.Next(0, 50) < 10)
            {
                HitCheck.GetInstance().AddElement(new EatMissile(this, this.Good, 5, 5, MissileDirection.STOP, 0));
            }
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
            HitCheck.GetInstance().AddElement(new MissileTwo(this, this.Good, 10, 10, Missiles.MissileDirections[MainForm.m_EnemyRandom.Next(7, 12)], 10));
        }

        /// <summary>
        ///敌人的移动，移动过程中，可以随机的开火
        /// </summary>
        protected override void Move()
        {
            x += (int)(0.5 * m_XSPEED);
            if (m_StartDir)
            {
                if (y < 450)
                {
                    y += m_YSPEED;
                }
                else
                {
                    m_StartDir = !m_StartDir;
                }
            }
            else
            {
                if (y > 100)
                {
                    y -= m_YSPEED;
                }
                else
                {
                    m_StartDir = !m_StartDir;
                }
            }

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
