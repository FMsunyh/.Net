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
    public class EnemySeven : Roles
    {
        /// <summary>
        /// ��ȡͼƬ·��
        /// </summary>
        private static string m_ImagePath = Directory.GetCurrentDirectory() + "\\images\\Enemy\\EnemySeven\\";

        /// <summary>
        /// ���˵ĳ�ʼ����
        /// </summary>
        public bool m_StartDirY, m_StartDirX;

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

        /// <summary>
        ///���˵Ĺ��캯��
        /// </summary>
        /// <param name="x">��ʼx������</param>
        /// <param name="y">��ʼy������</param>
        /// <param name="good">�û�</param>
        /// <param name="xspeed">�����x����ٶ�</param>
        /// <param name="yspeed">�����y����ٶ�</param>
        /// <param name="life">����ֵ</param>
        public EnemySeven(int x, int y, bool good, int xspeed, int yspeed, int life, bool startDir)
            : base(x, y, good, m_EnemyImage[0].Width / 3, m_EnemyImage[0].Height, xspeed, yspeed, life)
        {
            this.m_StartDirX = startDir;
            this.m_StartDirY = startDir;
        }

        /// <summary>
        /// �����󣬲�����ըЧ��
        /// </summary>
        protected override void ThisBomb()
        {
            HitCheck.GetInstance().AddElement(new EnemyBomb(this));

            //��ը��ʱ��ų�������
            if (MainForm.m_EnemyRandom.Next(0, 100) < 10)
            {
                HitCheck.GetInstance().AddElement(new EatGoodsOne(this, !this.Good, 5, 5, MissileDirection.STOP, 1));
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
            HitCheck.GetInstance().AddElement(new MissileSeven(this, this.Good, 10, 10, Missiles.MissileDirections[MainForm.m_EnemyRandom.Next(7, 12)], 30));
        }

        /// <summary>
        ///���˵��ƶ����ƶ������У���������Ŀ���
        /// </summary>
        protected override void Move()
        {

            if (m_StartDirY)
            {
                if (y < 500)
                {
                    y += m_YSPEED;
                }
                else
                {
                    m_StartDirY = !m_StartDirY;
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
                    m_StartDirY = !m_StartDirY;
                }
            }

            if (m_StartDirX)
            {
                if (x < 550)
                {
                    x += m_XSPEED;
                }
                else
                {
                    m_StartDirX = !m_StartDirX;
                }
            }
            else
            {
                if (x > 10)
                {
                    x -= m_XSPEED;
                }
                else
                {
                    m_StartDirX = !m_StartDirX;
                }
            }

            if (MainForm.m_EnemyRandom.Next(0, 100) < 3)
            {
                this.Fire();
            }

        }
    }
}
