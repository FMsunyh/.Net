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
        /// 碰撞实例
        /// </summary>
        private static volatile HitCheck instance = null;

        /// <summary>
        /// 定义MyTank
        /// </summary>
        private Tank m_MyTank = null;

        /// <summary>
        /// 创建自己的子弹装载器
        /// </summary>
        private List<MissilesMyTank> m_MissileMyTank = new List<MissilesMyTank>();

        /// <summary>
        /// 创建敌人坦克的装载器
        /// </summary>
        private List<Roles> m_EnemysTank = new List<Roles>();

        /// <summary>
        /// 创建敌人的子弹装载器
        /// </summary>
        private List<Missiles> m_MissileEnemy = new List<Missiles>();


        /// <summary>
        /// 创建爆炸的装载器
        /// </summary>
        private List<Element> m_Bomb = new List<Element>();

        private List<Wall> m_Wall = new List<Wall>();

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
        /// 返回当前MyTank
        /// </summary>
        public Tank MyTank
        {
            get { return m_MyTank; }
        }

        /// <summary>
        /// 产生一个角色
        /// </summary>
        /// <param name="e"></param>
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
                m_EnemysTank.Add(e as EnemyOne);
                return;
            }

            if (e is Missiles)
            {
                m_MissileEnemy.Add(e as Missiles);
                return;
            }

            if (e is Wall)
            {
                m_Wall.Add(e as Wall);
                return;
            }

            if (e is Element)
            {
                m_Bomb.Add(e);
                return;
            }

        }

        /// <summary>
        /// 绘制游戏所有角色
        /// </summary>
        /// <param name="g"></param>
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
            for (int i = 0; i < m_EnemysTank.Count; i++)
            {
                m_EnemysTank[i].Draw(g);
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

            //绘制墙
            for (int i = 0; i < m_Wall.Count; i++)
            {
                m_Wall[i].Draw(g);
            }

            //绘制游戏信息
            this.DrawMessage(g);
        }

        /// <summary>
        /// 碰撞检测
        /// </summary>
        public void DoHitCheck()
        {
            //MyTank子弹打到敌人
            for (int i = 0; i < m_MissileMyTank.Count; i++)
            {
                for (int j = 0; j < m_EnemysTank.Count; j++)
                {
                    if (m_MissileMyTank[i].GetRectangle().IntersectsWith(m_EnemysTank[j].GetRectangle()))
                    {
                        m_EnemysTank[j].Bleeding(m_MissileMyTank[i].Power);
                        m_MissileMyTank[i].IsLive = false;
                    }
                }
            }

            // 做元素的碰撞检测
            if (m_MyTank.IsLive)
            {
                for (int i = 0; i < m_EnemysTank.Count; i++)
                {
                    if (m_MyTank.GetRectangle().IntersectsWith(m_EnemysTank[i].GetRectangle()))
                    {
                        m_MyTank.Death();
                    }
                }

                for (int i = 0; i < m_MissileEnemy.Count; i++)
                {
                    if (m_MyTank.GetRectangle().IntersectsWith(m_MissileEnemy[i].GetRectangle()))
                    {
                        m_MyTank.Bleeding(m_MissileEnemy[i].Power);
                        m_MissileEnemy[i].IsLive = false;
                    }
                }
            }

            //撞到墙
            for (int i = 0; i < m_Wall.Count; i++)
            {
                switch (m_Wall[i].TypeWall1)
                {
                    case Wall.TypeWall.GRASS:
                        {
                            break;
                        }

                    case Wall.TypeWall.WATER:
                        {
                            //MyTank撞到石头墙
                            if (m_Wall[i].GetRectangle().IntersectsWith(m_MyTank.GetRectangle()))
                            {
                                m_MyTank.StopMove();
                            }

                            //敌人撞到石头墙
                            for (int k = 0; k < m_EnemysTank.Count; k++)
                            {
                                if (m_Wall[i].GetRectangle().IntersectsWith(m_EnemysTank[k].GetRectangle()))
                                {
                                    m_EnemysTank[k].StopMove();
                                }
                            }
                            break;
                        }

                    case Wall.TypeWall.STONE:
                        {
                            //MyTank撞到石头墙
                            if (m_Wall[i].GetRectangle().IntersectsWith(m_MyTank.GetRectangle()))
                            {
                                m_MyTank.StopMove();
                            }

                            //敌人撞到石头墙
                            for (int k = 0; k < m_EnemysTank.Count; k++)
                            {
                                if (m_Wall[i].GetRectangle().IntersectsWith(m_EnemysTank[k].GetRectangle()))
                                {
                                    m_EnemysTank[k].StopMove();
                                }
                            }

                            //MyTank子弹
                            for (int m = 0; m < m_MissileMyTank.Count; m++)
                            {
                                if (m_Wall[i].GetRectangle().IntersectsWith(m_MissileMyTank[m].GetRectangle()))
                                {
                                    m_MissileMyTank[m].IsLive = false;
                                }
                            }

                            //敌人的子弹
                            for (int n = 0; n < m_MissileEnemy.Count; n++)
                            {
                                if (m_Wall[i].GetRectangle().IntersectsWith(m_MissileEnemy[n].GetRectangle()))
                                {
                                    m_MissileEnemy[n].IsLive = false;
                                }
                            }
                            break;
                        }
                    default:
                        break;                  
                }
            }
        }

        /// <summary>
        /// 在装载器中移除死亡的角色
        /// </summary>
        /// <param name="e"></param>
        public void ReMoveElement(Element e)
        {
            // 移除元素
            if (e is MissilesMyTank)
            {
                m_MissileMyTank.Remove(e as MissilesMyTank);
                return;
            }

            if (e is Roles)
            {
                m_EnemysTank.Remove(e as EnemyOne);
                return;
            }

            if (e is Missiles)
            {
                m_MissileEnemy.Remove(e as Missiles);
                return;
            }

            if (e is Element)
            {
                m_Bomb.Remove(e as TankDeathBomp);
                return;
            }
        }

        /// <summary>
        /// 绘制游戏信息
        /// </summary>
        /// <param name="g"></param>
        private void DrawMessage(Graphics g)
        {
            String drawString;
            PointF drawPoint;

            // 字体和填充颜色.
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Yellow);

            drawString = "MyTank 子弹： " + m_MissileMyTank.Count;
            drawPoint = new PointF(5.0F, 60.0F);  // 位置.
            g.DrawString(drawString, drawFont, drawBrush, drawPoint);// 绘制.

            // 敌人坦克的数量.
            drawString = "敌人的坦克数量 ： " + m_EnemysTank.Count;
            drawPoint = new PointF(550.0F, 5.0F);  // 位置.
            g.DrawString(drawString, drawFont, drawBrush, drawPoint);// 绘制.

            //敌人的子弹数
            drawString = "敌人的子弹数量 ： " + m_MissileEnemy.Count;
            drawPoint = new PointF(550.0F, 30.0F);  // 位置.
            g.DrawString(drawString, drawFont, drawBrush, drawPoint);// 绘制.


            //敌人的子弹数
            drawString = "爆炸数量 ： " + m_Bomb.Count;
            drawPoint = new PointF(550.0F, 60.0F);  // 位置.
            g.DrawString(drawString, drawFont, drawBrush, drawPoint);// 绘制.

        }
    }
}
