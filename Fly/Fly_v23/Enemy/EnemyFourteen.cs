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
    /// ������
    /// </summary>
    public class EnemyFourteen : Roles
    {
        /// <summary>
        /// ��ȡͼƬ·��
        /// </summary>
        private static string m_ImagePath = Directory.GetCurrentDirectory() + "\\images\\Enemy\\EnemyFourteen\\";

        /// <summary>
        /// ���˵ĳ�ʼ����
        /// </summary>
        public bool m_StartDir;

        /// <summary>
        /// ������˵�ͼƬ
        /// </summary>
        private static Image[] m_EnemyImage = new Image[] 
        {
            Image.FromFile(m_ImagePath+"Fly_Enemy01.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy02.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy03.png"),
            Image.FromFile(m_ImagePath+"Fly_Enemy04.png")        
        };

        /// <summary>
        /// ���ѡȡ����ͼƬ
        /// </summary>
        private int rand = MainForm.m_EnemyRandom.Next(0, m_EnemyImage.Length);

        private int step = 0;
        private Rectangle rect;

        private int m_Speed = 0;

        /// <summary>
        ///���˵Ĺ��캯��
        /// </summary>
        /// <param name="x">��ʼx������</param>
        /// <param name="y">��ʼy������</param>
        /// <param name="good">�û�</param>
        /// <param name="xspeed">�����x����ٶ�</param>
        /// <param name="yspeed">�����y����ٶ�</param>
        /// <param name="life">����ֵ</param>
        public EnemyFourteen(int x, int y, bool good, int xspeed, int yspeed, int life, bool startDir)
            : base(x, y, good, m_EnemyImage[0].Width / 3, m_EnemyImage[0].Height, xspeed, yspeed, life)
        {
            this.m_StartDir = startDir;
        }

        /// <summary>
        /// �����󣬲�����ըЧ��
        /// </summary>
        protected override void ThisBomb()
        {
            HitCheck.GetInstance().AddElement(new EnemyBomb(this));

            //��ը��ʱ��ų� ��Ѫ��+ѫ��
            if (MainForm.m_EnemyRandom.Next(0, 100) < 10)
            {
                HitCheck.GetInstance().AddElement(new EatBlood(this, !this.Good, 5, 5, MissileDirection.STOP, -30));
                HitCheck.GetInstance().AddElement(new MedalThree(this, 0, MainForm.m_RollSpeed, 3));
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
            HitCheck.GetInstance().AddElement(new MissileFourteen(this, this.Good, 10, 10, Missiles.MissileDirections[MainForm.m_EnemyRandom.Next(7, 12)], 30));
        }

        /// <summary>
        ///���˵��ƶ����ƶ������У���������Ŀ���
        /// </summary>
        protected override void Move()
        {
            if (m_StartDir)
            {
                if (x > 100 && (m_XSPEED - m_Speed != 0))
                {
                    x -= m_XSPEED - m_Speed;
                    m_Speed++;
                }
            }
            else
            {
                if (x < 500 && (m_XSPEED - m_Speed != 0))
                {
                    x += m_XSPEED - m_Speed;
                    m_Speed++;
                }
            }

            if (MainForm.m_EnemyRandom.Next(0, 100) < 3)
            {
                this.Fire();
            }
        }
    }
}
