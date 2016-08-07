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
        /// 获取图片路径
        /// </summary>
        private static string m_ImagePath = Directory.GetCurrentDirectory() + "\\images\\Enemy\\EnemyOne\\";

        /// <summary>
        /// 载入敌人的图片
        /// </summary>
        private static Image[] m_EnemyImage = new Image[] 
        {
            Image.FromFile(m_ImagePath+"Fly_Enemy01.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy02.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy03.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy04.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy05.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy06.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy07.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy08.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy09.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy10.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy11.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy12.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy13.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy14.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy15.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy16.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy17.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy18.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy19.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy20.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy21.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy22.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy23.png")
        };

        /// <summary>
        /// 随机选取敌人图片
        /// </summary>
        private int rand = MainForm.m_EnemyRandom.Next(0, 23);

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
        public EnemyOne(int x, int y, bool good, int xspeed, int yspeed, int life, bool startDir)
            : base(x, y, good, m_EnemyImage[0].Width / 3, m_EnemyImage[0].Height, xspeed, yspeed, life)
        {
            this.m_StartDir = startDir;
        }

        /// <summary>
        /// 死亡后，产生爆炸效果
        /// </summary>
        protected override void ThisBomb()
        {
            //throw new NotImplementedException();
            HitCheck.GetInstance().AddElement(new BombOne(this));

            //爆炸的时候放出 吃的东西,放出 500积分
            if (MainForm.m_EnemyRandom.Next(0, 100) < 5)
            {
                HitCheck.GetInstance().AddElement(new EatGoodsFive(this, this.Good, 5, 5, MissileDirection.STOP, 5));
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
            HitCheck.GetInstance().AddElement(new MissileOne(this, this.Good, 10, 10, Missiles.MissileDirections[MainForm.m_EnemyRandom.Next(7, 12)], 10));
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
                if (MainForm.m_EnemyRandom.Next(0, 100) < 3)
                {
                    this.Fire();
                }
            }
        }
    }
}
