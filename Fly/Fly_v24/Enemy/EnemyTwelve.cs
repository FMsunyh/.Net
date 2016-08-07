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
    /// ������
    /// </summary>
    public class EnemyTwelve : Roles
    {
        /// <summary>
        /// ��ȡͼƬ·��
        /// </summary>
        private static string m_ImagePath = Directory.GetCurrentDirectory() + "\\images\\Enemy\\EnemyTwelve\\";

        /// <summary>
        /// ������˵�ͼƬ
        /// </summary>
        private static Image[] m_EnemyImage = new Image[] 
        {
            Image.FromFile(m_ImagePath+"Fly_Enemy01.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy02.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy03.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy04.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy05.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy06.png"),  
            Image.FromFile(m_ImagePath+"Fly_Enemy07.png")
        };

        /// <summary>
        /// ���ѡȡ����ͼƬ
        /// </summary>
        private int rand = MainForm.m_EnemyRandom.Next(0, m_EnemyImage.Length);

        private int m_Speed=2;

        private int step = 0;
        private Rectangle rect;

        /// <summary>
        ///���˵Ĺ��캯��
        /// </summary>
        /// <param name="x">��ʼx������</param>
        /// <param name="y">��ʼy������</param>
        /// <param name="good">�û�</param>
        /// <param name="xspeed">�����x����ٶ�</param>
        /// <param name="yspeed">�����y����ٶ�</param>
        /// <param name="life">����ֵ</param>
        public EnemyTwelve(int x, int y, bool good, int xspeed, int yspeed, int life)
            : base(x, y, good, m_EnemyImage[0].Width / 3, m_EnemyImage[0].Height, xspeed, yspeed, life)
        {
        }

        /// <summary>
        /// �����󣬲�����ըЧ��
        /// </summary>
        protected override void ThisBomb()
        {
            HitCheck.GetInstance().AddElement(new EnemyBomb(this));

            //��ը��ʱ��ų�����һ��
            if (MainForm.m_EnemyRandom.Next(0, 100) < 10)
            {
                HitCheck.GetInstance().AddElement(new EatGoodsTwo(this, !this.Good, 5, 5, MissileDirection.STOP, 2));
                HitCheck.GetInstance().AddElement(new MedalNine(this, 0, MainForm.m_RollSpeed, 9));
            }
        }

        /// <summary>
        /// ���Ƶ���
        /// </summary>
        /// <param name="g">����</param>
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
        /// ���˿���
        /// </summary>
        public override void Fire()
        {
            HitCheck.GetInstance().AddElement(new MissileTwelve(this, this.Good, 10, 10, Missiles.MissileDirections[MainForm.m_EnemyRandom.Next(7, 12)], 30));
        }

        /// <summary>
        ///���˵��ƶ����ƶ������У���������Ŀ���
        /// </summary>
        protected override void Move()
        {
            if (y < MainForm.m_GAMEHEIGHT)
            {
                y += (m_YSPEED-m_Speed);

                m_Speed++;
            }

            //�������100����������ˢ���˵�,�����ж��Ƿ񳬳�-100����
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
