using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TankWar.General;

namespace TankWar
{
    public class HitCheck
    {
        /// <summary>
        /// 构造函数私有化
        /// </summary>
        private HitCheck()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private static volatile HitCheck instance = null;


        private Tank m_MyTank = null;

        /// <summary>
        /// 创建自己的子弹装载器
        /// </summary>
        private List<MissilesMyTank> m_MissileMyTank= new List<MissilesMyTank>();

        /// <summary>
        /// 创建敌人泛型
        /// </summary>
        private List<Roles> m_enemys = new List<Roles>();

        /// <summary>
        /// 创建一个HitCheck对象
        /// </summary>
        /// <returns></returns>
        public static HitCheck GetInstance()
        {
            if (instance == null)
            {
                instance = new HitCheck();
            }

            return instance;
        }

        

        public Tank MyTank
        {
            get { return m_MyTank; }
        }

        public void AddElement(Element e)
        {
            if (e is Tank)
            {
                m_MyTank = e as Tank;
                return;
            }

            if (e is MissilesMyTank)
            {
                m_MissileMyTank.Add(e as MissilesMyTank);
                return;
            }

            if (e is Roles)
            {
                m_enemys.Add(e as EnemyOne);
                return;
            }
        }

        public void Draw(Graphics g)
        {
            //绘制英雄
            m_MyTank.Draw(g);

            //绘制子弹
            for (int i = 0; i < m_MissileMyTank.Count; i++)
            {
                m_MissileMyTank[i].Draw(g);
            }

            //绘制敌人
            for (int i = 0; i < m_enemys.Count; i++)
            {
                m_enemys[i].Draw(g);
            }

            //for (int i = 0; i < m_Missiles.Count; i++)
            //{
            //    m_Missiles[i].Draw(_G);
            //}
        }

        public void DoHitCheck()
        {
            //TODO 做元素的碰撞检测
        }

        public void ReMoveElement(Element e)
        {
            //TODO 移除元素
        }
    }
}
