using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using TankWar.General;

namespace TankWar
{
    public class EnemyOne : Roles
    {
        private static Bitmap[] m_EnemyImages = new Bitmap[8];

        private static Image m_EnemyTank = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\EnemyOne\\enemyU.png");

        /// <summary>
        /// 敌人的前一个方向
        /// </summary>
        private RolesDirection m_FrontDir;

        /// <summary>
        /// 敌人走多少部改变一下方向
        /// </summary>
        private int step = 0;

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
        public EnemyOne(int x, int y, bool good, int xspeed, int yspeed, int life)
            : base(x, y, good, 50, 50, xspeed, yspeed, life)
        {
            m_FrontDir = RolesDirection.D;

            m_EnemyImages[0] = new Bitmap(@"images\EnemyOne\enemyL.png");
            m_EnemyImages[1] = new Bitmap(@"images\EnemyOne\enemyLU.png");
            m_EnemyImages[2] = new Bitmap(@"images\EnemyOne\enemyU.png");
            m_EnemyImages[3] = new Bitmap(@"images\EnemyOne\enemyUR.png");
            m_EnemyImages[4] = new Bitmap(@"images\EnemyOne\enemyR.png");
            m_EnemyImages[5] = new Bitmap(@"images\EnemyOne\enemyRD.png");
            m_EnemyImages[6] = new Bitmap(@"images\EnemyOne\enemyD.png");
            m_EnemyImages[7] = new Bitmap(@"images\EnemyOne\enemyDL.png");
        }

        /// <summary>
        /// 死亡后，产生爆炸效果
        /// </summary>
        protected override void ThisBomb()
        {
            HitCheck.GetInstance().AddElement(new TankDeathBomp(this));
        }

        /// <summary>
        /// 选择Tank的图像
        /// </summary>
        private void ConfirmRolesImage()
        {
            switch (this.dir)
            {
                case RolesDirection.L:
                    {

                        m_EnemyTank = m_EnemyImages[0];
                        break;
                    }
                case RolesDirection.LU:
                    {
                        m_EnemyTank = m_EnemyImages[1];
                        break;
                    }
                case RolesDirection.U:
                    {
                        m_EnemyTank = m_EnemyImages[2];
                        break;
                    }
                case RolesDirection.RU:
                    {
                        m_EnemyTank = m_EnemyImages[3];
                        break;
                    }
                case RolesDirection.R:
                    {
                        m_EnemyTank = m_EnemyImages[4];
                        break;
                    }
                case RolesDirection.RD:
                    {
                        m_EnemyTank = m_EnemyImages[5];
                        break;
                    }
                case RolesDirection.D:
                    {
                        m_EnemyTank = m_EnemyImages[6];
                        break;
                    }
                case RolesDirection.LD:
                    {
                        m_EnemyTank = m_EnemyImages[7];
                        break;
                    }
                default:
                    break;
            }
        }

        private void ChangeDirection()
        {
            if (0 == step)
            {
                step = MainForm.m_EnemyRandom.Next(12) + 3;
                int randomNumber = MainForm.m_EnemyRandom.Next(8);
                dir = (RolesDirection)randomNumber;
            }
            step--;
        }

        
        /// <summary>
        /// 敌人开火
        /// </summary>
        public override void Fire()
        {
            HitCheck.GetInstance().AddElement(new MissileEnemyOne(this, this.Good, 10, 10, this.m_FrontDir, 10));
        }

        /// <summary>
        ///敌人的移动，移动过程中，可以随机的开火
        /// </summary>
        protected override void Move()
        {
            base.Move();

            ChangeDirection();

            if (this.dir != RolesDirection.STOP)
            {
                this.m_FrontDir = this.dir;
            }

            //开火
            if (MainForm.m_EnemyRandom.Next(0, 100) < 5)
            {
                this.Fire();
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
            ConfirmRolesImage();
            base.Draw(g, m_EnemyTank, x, y);
        }

    }
}
