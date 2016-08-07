using Fly.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fly
{
    /// <summary>
    /// 碰撞检测类
    /// </summary>
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

        /// <summary>
        /// 创建英雄对象
        /// </summary>
        private Hero m_MyHero = null;

        /// <summary>
        /// 创建英雄的子弹
        /// </summary>
        private List<MissileHero> m_MissileHero = new List<MissileHero>();

        /// <summary>
        /// 创建敌人泛型
        /// </summary>
        private List<Roles> m_enemys = new List<Roles>();

        public Hero MyHero
        {
            get { return m_MyHero; }
        }

        /// <summary>
        /// 根据对象的类型
        /// 添加对象的方法
        /// </summary>
        /// <param name="e">实例对象</param>
        public void AddElement(Element e)
        {
            if (e is Hero)
            {
                m_MyHero = e as Hero;
                return;
            }

            if (e is MissileHero)
            {
                m_MissileHero.Add(e as MissileHero);
                return;
            }

            if (e is Roles)
            {
                m_enemys.Add(e as EnemyOne);
                return;
            }
        }

        /// <summary>
        /// 绘制角色的方法
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            //绘制英雄
            m_MyHero.Draw(g);

            //绘制英雄的子弹
            for (int i = 0; i < m_MissileHero.Count; i++)
            {
                m_MissileHero[i].Draw(g);
            }

            //绘制敌人
            for (int i = 0; i < m_enemys.Count; i++)
            {
                m_enemys[i].Draw(g);
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
