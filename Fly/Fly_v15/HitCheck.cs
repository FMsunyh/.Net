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
        /// 创建英雄的子弹装载器
        /// </summary>
        private List<MissileHero> m_MissileHero = new List<MissileHero>();

        /// <summary>
        /// 创建英雄的弹道装载器
        /// </summary>
        private List<MissileBomb> m_MissieHeroBomb = new List<MissileBomb>();

        /// <summary>
        /// 创建敌人的子弹装载器
        /// </summary>
        private List<Missiles> m_MissileEnemy = new List<Missiles>();

        /// <summary>
        /// 创建敌人装载器
        /// </summary>
        private List<Roles> m_Enemys = new List<Roles>();

        /// <summary>
        /// 创建爆炸的装载器
        /// </summary>
        private List<Element> m_Bomb = new List<Element>();

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

            if (e is MissileBomb)
            {
                m_MissieHeroBomb.Add(e as MissileBomb);
                return;
            }

            if (e is Roles)
            {
                m_Enemys.Add(e as Roles);
                return;
            }

            if (e is Missiles)
            {
                m_MissileEnemy.Add(e as Missiles);
                return;
            }

            if (e is Element)
            {
                m_Bomb.Add(e);
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

            //绘制英雄的导弹
            for (int i = 0; i < m_MissieHeroBomb.Count; i++)
            {
                m_MissieHeroBomb[i].Draw(g);
            }

            //绘制敌人
            for (int i = 0; i < m_Enemys.Count; i++)
            {
                m_Enemys[i].Draw(g);
            }

            //绘制敌人的子弹
            for (int i = 0; i < m_MissileEnemy.Count; i++)
            {
                m_MissileEnemy[i].Draw(g);
            }

            //绘制爆炸效果
            for (int i = 0; i < m_Bomb.Count; i++)
            {
                m_Bomb[i].Draw(g);
            }
        }

        public void DoHitCheck()
        {
            // 做元素的碰撞检测
            if (m_MyHero.IsLive)
            {
                //英雄与敌人做碰撞检测
                for (int i = 0; i < m_Enemys.Count; i++)
                {
                    if (m_MyHero.GetRectangle().IntersectsWith(m_Enemys[i].GetRectangle()))
                    {
                        m_MyHero.Death();
                    }
                }

                //英雄与敌人的子弹做碰撞检测
                for (int i = 0; i < m_MissileEnemy.Count; i++)
                {
                    if (m_MyHero.GetRectangle().IntersectsWith(m_MissileEnemy[i].GetRectangle()))
                    {
                        m_MyHero.Bleeding(m_MissileEnemy[i].Power);
                        m_MissileEnemy[i].IsLive = false;
                    }
                }
            }

            //英雄与敌人的子弹做碰撞检测
            for (int i = 0; i < m_MissileHero.Count; i++)
            {
                for (int j = 0; j < m_Enemys.Count; j++)
                {
                    if (m_MissileHero[i].GetRectangle().IntersectsWith(m_Enemys[j].GetRectangle()))
                    {
                        m_Enemys[j].Bleeding(m_MissileHero[i].Power);
                        m_MissileHero[i].IsLive = false;
                    }
                }
            }
        }

        public void ReMoveElement(Element e)
        {
            // 移除元素
            if (e is MissileHero)
            {
                m_MissileHero.Remove(e as MissileHero);
                return;
            }

            if (e is MissileBomb)
            {
                m_MissieHeroBomb.Remove(e as MissileBomb);
                return;
            }

            if (e is Roles)
            {
                m_Enemys.Remove(e as Roles);
                return;
            }

            if (e is Missiles)
            {
                m_MissileEnemy.Remove(e as Missiles);
                return;
            }

        }

        /// <summary>
        /// 英雄发炸弹
        /// </summary>
        /// <param name="power"></param>
        public void Bombing(int power)
        {
            //让所有敌人的子弹死亡
            for (int i = 0; i < m_MissileEnemy.Count; i++)
            {
                if (!(m_MissileEnemy[i] is EatBlood||m_MissileEnemy[i] is EatMissile))
                {
                    m_MissileEnemy[i].IsLive = false;
                }               
            }

            for (int i = 0; i < m_Enemys.Count; i++)
            {
                m_Enemys[i].Bleeding(power);
            }
        }

    }
}
