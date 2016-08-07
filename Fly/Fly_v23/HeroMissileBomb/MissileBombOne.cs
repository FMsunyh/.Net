using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fly.General;

namespace Fly
{
    public class MissileBombOne : MissileBomb
    {
        /// <summary>
        /// 炸弹的装载器
        /// </summary>
        private static Dictionary<string, Image> m_MissilesImage = new Dictionary<string, Image>() 
        {
            {"U",Image.FromFile(m_ImagePath+"MissileBomb.gif")}
        };

        public MissileBombOne(Roles role, bool good, int xspeed, int yspeed, MissileDirection dir, int power)
            : base(role, good, m_MissilesImage["U"].Width, m_MissilesImage["U"].Height, xspeed, yspeed, MissileDirection.U, power)
        {
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            if (this.IsLive == false)
            {
                //发生爆炸效果
                HitCheck.GetInstance().AddElement(new BombFour(this));

                //调用Bombing方法让敌人减血
                HitCheck.GetInstance().Bombing(m_Power);
                HitCheck.GetInstance().ReMoveElement(this);
                return;
            }
            this.Move();
            base.Draw(g, m_MissilesImage, x, y);
        }

        protected override void Move()
        {
            base.Move();
            if (m_RoleY - y > 150)
            {
                this.IsLive = false;
            }
        }
    }
}
