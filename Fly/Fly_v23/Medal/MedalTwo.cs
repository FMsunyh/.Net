using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fly.General;
using Fly.Medal;

namespace Fly
{
    public class MedalTwo:Medals  
    {
        private static Image m_MedalImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\Medals\\Medal02.png");

        public MedalTwo(Roles role, int xspeed, int yspeed, int level)
            : base(role, m_MedalImage.Width, m_MedalImage.Height, xspeed, yspeed, level)
        { }

        public override void Draw(Graphics g)
        {
            if (!this.IsLive)
            {
                HitCheck.GetInstance().ReMoveElement(this);
                return;
            }

            base.Move();
            g.DrawImage(m_MedalImage,x,y);
        }
    }
}
