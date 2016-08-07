using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TankWar.General;

namespace TankWar
{
    /// <summary>
    /// 坦克类
    /// </summary>
    public class Tank : Roles
    {

        //用户是否按下"上\下\左\右"
        private bool PU = false, PD = false, PL = false, PR = false;

        /// <summary>
        /// 我的坦克图片
        /// </summary>
        private static Bitmap[] m_MyTankImages = new Bitmap[8];

        /// <summary>
        /// 初始值 图片
        /// </summary>
        private static Image m_MyTank = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\TANK2\\enemyU.png");

        /// <summary>
        /// 记录上一个方向
        /// </summary>
        private RolesDirection BarrelDirection;

        /// <summary>
        /// 血块
        /// </summary>
        public BloodBar m_BloodBar { get; set; }        

        /// <summary>
        /// Tank的构造函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xspeed"></param>
        /// <param name="yspeed"></param>
        /// <param name="life"></param>
        /// <param name="good"></param>
        public Tank(int x, int y, int xspeed, int yspeed, int life, bool good)
            : base(x, y, good, 50, 50, xspeed, yspeed, life)
        {
            this.dir = RolesDirection.STOP;
            this.BarrelDirection = RolesDirection.U;

            m_BloodBar = new BloodBar(10, 110, 200);

            m_MyTankImages[0] = new Bitmap(@"images\TANK2\enemyL.png");
            m_MyTankImages[1] = new Bitmap(@"images\TANK2\enemyLU.png");
            m_MyTankImages[2] = new Bitmap(@"images\TANK2\enemyU.png");
            m_MyTankImages[3] = new Bitmap(@"images\TANK2\enemyUR.png");
            m_MyTankImages[4] = new Bitmap(@"images\TANK2\enemyR.png");
            m_MyTankImages[5] = new Bitmap(@"images\TANK2\enemyRD.png");
            m_MyTankImages[6] = new Bitmap(@"images\TANK2\enemyD.png");
            m_MyTankImages[7] = new Bitmap(@"images\TANK2\enemyDL.png");
        }

        /// <summary>
        /// 键盘按键按下
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// 键盘按键弹起
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// 确定Tank的移动方向
        /// </summary>
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

        /// <summary>
        /// 选择Tank的图像
        /// </summary>
        private void ConfirmRolesImage()
        {
            switch (this.dir)
            {
                case RolesDirection.L:
                    {

                        m_MyTank = m_MyTankImages[0];
                        break;
                    }
                case RolesDirection.LU:
                    {
                        m_MyTank = m_MyTankImages[1];
                        break;
                    }
                case RolesDirection.U:
                    {
                        m_MyTank = m_MyTankImages[2];
                        break;
                    }
                case RolesDirection.RU:
                    {
                        m_MyTank = m_MyTankImages[3];
                        break;
                    }
                case RolesDirection.R:
                    {
                        m_MyTank = m_MyTankImages[4];
                        break;
                    }
                case RolesDirection.RD:
                    {
                        m_MyTank = m_MyTankImages[5];
                        break;
                    }
                case RolesDirection.D:
                    {
                        m_MyTank = m_MyTankImages[6];
                        break;
                    }
                case RolesDirection.LD:
                    {
                        m_MyTank = m_MyTankImages[7];
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// 爆炸效果
        /// </summary>
        protected override void ThisBomb()
        {
            HitCheck.GetInstance().AddElement(new TankDeathBomp(this));
        }

        /// <summary>
        /// Tank开火
        /// </summary>
        public override void Fire()
        {
            //解决Tank死亡后还可以发子弹的问题
            if (!this.IsLive)
            {
                return;
            }

            HitCheck.GetInstance().AddElement(new MissilesMyTank(this, this.Good, 20, 20, this.BarrelDirection, 20));
        }

        /// 重写父类 Roles的Draw方法
        /// </summary>
        /// <param name="g">画笔</param>
        public override void Draw(Graphics g)
        {

            if (!this.IsLive)
            {
                HitCheck.GetInstance().ReMoveElement(this);
                return;
            }

            if (m_life >= m_BloodBar.AllLife)
            {
                m_life = m_BloodBar.AllLife;
            }
            m_BloodBar.NowLife = m_life;

            //绘制血条
            m_BloodBar.Draw(g);

            this.ConfirmRolesImage();
            this.Move();
            base.Draw(g, m_MyTank, x, y);
            this.DrawStringPointF(g);
        }


        /// <summary>
        /// 绘制Tank的坐标
        /// </summary>
        /// <param name="g">画笔</param>
        public void DrawStringPointF(Graphics g)
        {

            // 字体和填充颜色.
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Yellow);

            // Hero的位置坐标.
            String drawString = "x坐标：" + this.X + "\ny坐标：" + this.Y;
            // 位置.
            PointF drawPoint = new PointF(5.0F, 5.0F);

            // 绘制.
            g.DrawString(drawString, drawFont, drawBrush, drawPoint);

        }

        /// <summary>
        /// 重写父类Roles的Move方法
        /// </summary>
        protected override void Move()
        {
            base.Move();

            if (this.dir != RolesDirection.STOP)
            {
                this.BarrelDirection = this.dir;
            }
        }

    }
}
