using Fly.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fly
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

        private Hero m_MyHero = null;
        private List<MissileHero> m_MissileHero = new List<MissileHero>();

        public Hero MyHero
        {
            get { return m_MyHero; }
        }

        public void AddElement(Element e)
        {
            if (e is Hero)
            {
                m_MyHero = e as Hero;
            }

            if (e is MissileHero)
            {
                m_MissileHero.Add(e as MissileHero);
            }
        }

        public void Draw(Graphics g)
        {
            //绘制英雄
            m_MyHero.Draw(g);

            //绘制子弹
            for (int i = 0; i < m_MissileHero.Count; i++)
            {
                m_MissileHero[i].Draw(g);
            }
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
