using Fly.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Fly
{
    /// <summary>
    /// 敌人类
    /// </summary>
    public class EnemyFifteen : Roles
    {
        /// <summary>
        /// 获取图片路径
        /// </summary>
        private static string m_ImagePath = Directory.GetCurrentDirectory() + "\\images\\Enemy\\EnemyFifteen\\";

        /// <summary>
        /// 载入敌人的图片
        /// </summary>
        private static Image[] m_EnemyImage = new Image[] 
        {
            Image.FromFile(m_ImagePath+"Fly_Enemy01.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy02.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy03.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy04.png")         
        };

        /// <summary>
        /// 随机选取敌人图片
        /// </summary>
        private int rand = MainForm.m_EnemyRandom.Next(0, m_EnemyImage.Length);

        private int step = 0;
        private Rectangle rect;

        private int m_Speed = 0;

        /// <summary>
        ///敌人的构造函数
        /// </summary>
        /// <param name="x">初始x轴坐标</param>
        /// <param name="y">初始y轴坐标</param>
        /// <param name="good">好坏</param>
        /// <param name="xspeed">相对于x轴的速度</param>
        /// <param name="yspeed">相对于y轴的速度</param>
        /// <param name="life">生命值</param>
        public EnemyFifteen(int x, int y, bool good, int xspeed, int yspeed, int life)
            : base(x, y, good, m_EnemyImage[0].Width / 3, m_EnemyImage[0].Height, xspeed, yspeed, life)
        {
        }

        /// <summary>
        /// 死亡后，产生爆炸效果
        /// </summary>
        protected override void ThisBomb()
        {
            HitCheck.GetInstance().AddElement(new EnemyBomb(this));

            //爆炸的时候放出 直射子弹
            if (MainForm.m_EnemyRandom.Next(0, 100) < 10)
            {
                HitCheck.GetInstance().AddElement(new EatGoodsThree(this, !this.Good, 5, 5, MissileDirection.STOP, 3));
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

            rect = new Rectangle((m_EnemyImage[0].Width / 3) * step, 0, m_EnemyImage[0].Width / 3, m_EnemyImage[0].Height);
            base.Draw(g, m_EnemyImage[rand], rect, x, y);
        }

        /// <summary>
        /// 敌人开火
        /// </summary>
        public override void Fire()
        {
            HitCheck.GetInstance().AddElement(new MissileFifteen(this, this.Good, 10, 10, Missiles.MissileDirections[MainForm.m_EnemyRandom.Next(7, 12)], 30));
        }

        /// <summary>
        ///敌人的移动，移动过程中，可以随机的开火
        /// </summary>
        protected override void Move()
        {
            if (y > 500 && m_Speed == 0)
            {
                y -= m_YSPEED;
            }
            else
            {
                y -= m_YSPEED - m_Speed;
                m_Speed++;
            }

            //界面外的100像素是用来刷敌人的,所以判断是否超出-100以外
            if (y > MainForm.m_GAMEHEIGHT + 1000)
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
