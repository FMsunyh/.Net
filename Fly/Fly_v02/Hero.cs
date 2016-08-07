using Fly.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fly
{
    public class Hero : Roles
    {
        private static Image myImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\my.gif");

        //用户是否按下"上\下\左\右"
        private bool PU = false, PD = false, PL = false, PR = false;

        public Hero(int x, int y, int xspeed, int yspeed, int life, bool good)
            : base(x, y, good, myImage.Width, myImage.Height, xspeed, yspeed, life)
        {

        }

        public void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    PU = true;
                    break;
                case Keys.Down:
                    PD = true;
                    break;
                case Keys.Left:
                    PL = true;
                    break;
                case Keys.Right:
                    PR = true;
                    break;
                default: break;
            }
            ConfirmRolesDirection();
        }

        public void KeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    PU = false;
                    break;
                case Keys.Down:
                    PD = false;
                    break;
                case Keys.Left:
                    PL = false;
                    break;
                case Keys.Right:
                    PR = false;
                    break;
                case Keys.ControlKey:
                    Fire();
                    break;
                default: break;
            }
            ConfirmRolesDirection();
        }

        private void ConfirmRolesDirection()
        {
            if (PL && !PU && !PR && !PD)
                dir = RolesDirection.L;
            else if (PL && PU && !PR && !PD)
                dir = RolesDirection.LU;
            else if (!PL && PU && !PR && !PD)
                dir = RolesDirection.U;
            else if (!PL && PU && PR && !PD)
                dir = RolesDirection.RU;
            else if (!PL && !PU && PR && !PD)
                dir = RolesDirection.R;
            else if (!PL && !PU && PR && PD)
                dir = RolesDirection.RD;
            else if (!PL && !PU && !PR && PD)
                dir = RolesDirection.D;
            else if (PL && !PU && !PR && PD)
                dir = RolesDirection.LD;
            else if (!PL && !PU && !PR && !PD)
                dir = RolesDirection.STOP;
        }


        protected override void ThisBomb()
        {
            throw new NotImplementedException();
        }

        public override void Fire()
        {
            //FrmMain.m.Add(new MissileHero(this, 20, 20, this.good, MissileDirection.LUU, 10));
            //FrmMain.m.Add(new MissileHero(this, 20, 20, this.good, MissileDirection.U, 10));
            //FrmMain.m.Add(new MissileHero(this, 20, 20, this.good, MissileDirection.RUU, 10));
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            this.Move();
            base.Draw(g, myImage, x, y);
        }

        protected override void Move()
        {
            base.Move();

            //超出边界检测
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x + this.m_WIDTH > MainForm._GAMEWIDTH - 10)
            {
                x = MainForm._GAMEWIDTH - 10 - this.m_WIDTH;
            }

            if (y + this.m_HEIGHT > MainForm._GAMEHEIGHT - 85)
            {
                y = MainForm._GAMEHEIGHT - 85 - this.m_HEIGHT;
            }
        }
    }
}
