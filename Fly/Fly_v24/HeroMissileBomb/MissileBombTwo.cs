using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Fly.General;

namespace Fly
{
    public class MissileBombTwo : MissileBomb
    {
        /// <summary>
        /// 炸弹的装载器
        /// </summary>
        private static Dictionary<string, Image> m_MissilesImage = new Dictionary<string, Image>() 
        {
            {"U",Image.FromFile(m_ImagePath+"YuShi.gif")}
        };

        public MissileBombTwo(Roles role, bool good, int xspeed, int yspeed, MissileDirection dir, int power)
            : base(role, good, m_MissilesImage["U"].Width, m_MissilesImage["U"].Height, xspeed, yspeed, MissileDirection.U, power)
        {
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            int xx = x;
            int yy = y;
            if (this.IsLive == false)
            {
                //发生爆炸效果
                for (int i = 0; i < 200; i++)
                {
                    x = MainForm.m_EnemyRandom.Next(10,550);
                    y = -100 * MainForm.m_EnemyRandom.Next(5,100);
                    HitCheck.GetInstance().AddElement(new HeroMissileBombTwo(this));
                }

                HitCheck.GetInstance().ReMoveElement(this);
                return;
            }
            this.Move();
            base.Draw(g, m_MissilesImage, x, y);
        }

        protected override void Move()
        {
            base.Move();
            if (y < 200)
            {
                this.IsLive = false;
            }
        }
    }
}
