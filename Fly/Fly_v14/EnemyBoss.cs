﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fly.General;

namespace Fly
{
    public class EnemyBoss : Roles
    {
        /// <summary>
        /// 载入敌人Boss的图片
        /// </summary>
        private static Image m_EnemyImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\enemyBoss.gif");
        private bool m_DirRight;
        private bool m_DirLeft;

        private BloodBar m_BloodBar;

        /// <summary>
        /// Boss的构造函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="good"></param>
        /// <param name="xspeed"></param>
        /// <param name="yspeed"></param>
        /// <param name="life"></param>
        /// <param name="bu"></param>
        /// <param name="br"></param>
        public EnemyBoss(int x, int y, bool good, int xspeed, int yspeed, int life, bool bu, bool br)
            : base(x, y, good, m_EnemyImage.Width, m_EnemyImage.Height, xspeed, yspeed, life)
        {
            this.m_DirRight = bu;
            this.m_DirLeft = br;
            m_BloodBar = new BloodBar(50, 30, life);
        }

        /// <summary>
        /// 爆炸
        /// </summary>
        protected override void ThisBomb()
        {
            HitCheck.GetInstance().AddElement(new BombTwo(this));
        }

        /// <summary>
        /// 开火
        /// </summary>
        public override void Fire()
        {
            switch (MainForm.m_EnemyRandom.Next(0, 2))
            {
                case 0:
                    {
                        HitCheck.GetInstance().AddElement(new MissileOne(this, this.Good, 10, 10, Missiles.MissileDirections[MainForm.m_EnemyRandom.Next(7, 12)], 10));
                        break;
                    }
                case 1:
                    {
                        HitCheck.GetInstance().AddElement(new MissileTwo(this, this.Good, 10, 10, Missiles.MissileDirections[MainForm.m_EnemyRandom.Next(7, 12)], 10));
                        break;
                    }
                case 2:
                    {
                        HitCheck.GetInstance().AddElement(new MissileThree(this, this.Good, 10, 10, Missiles.MissileDirections[MainForm.m_EnemyRandom.Next(7, 12)], 30));
                        break;
                    }
                default:
                    break;
            }
        }

        public override void Bleeding(int i)
        {
            if (this.IsLive)
            {
                m_life -= (int)(i / 5 + 1);
            }
            if (m_life <= 0)
            {
                Death();
            }
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            m_BloodBar.NowLife = m_life;
            m_BloodBar.Draw(g);

            if (this.IsLive == false)
            {
                HitCheck.GetInstance().ReMoveElement(this);
                return;
            }

            this.Move();
            base.Draw(g, m_EnemyImage, x, y);
        }

        /// <summary>
        /// Boss移动
        /// </summary>
        protected override void Move()
        {
            if (m_DirLeft)
            {
                if (x < 400)
                {
                    x += m_XSPEED;
                }
                else
                {
                    m_DirLeft = !m_DirLeft;
                }
            }
            else
            {
                if (x + this.m_WIDTH > 100)
                {
                    x -= m_XSPEED;
                }
                else
                {
                    m_DirLeft = !m_DirLeft;
                }
            }

            if (m_DirRight)
            {
                if (y > 0)
                {
                    y -= m_YSPEED;
                }
                else
                {
                    m_DirRight = !m_DirRight;
                }
            }
            else
            {
                if (y < 400)
                {
                    y += m_YSPEED;
                }
                else
                {
                    m_DirRight = !m_DirRight;
                }
            }

            //开火
            if (MainForm.m_EnemyRandom.Next(0, 100) < 20)
            {
                Fire();
            }
        }
    }
}
