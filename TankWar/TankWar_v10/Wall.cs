using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using TankWar.General;

namespace TankWar
{
    /// <summary>
    /// 围墙类（产生各种各样的墙。石头 水 草）
    /// </summary>
    public class Wall:Element
    {

        /// <summary>
        /// 初始值图片
        /// </summary>
        private static Image m_Stone = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\Wall\\stone.gif");
        private static Image m_Water = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\Wall\\water.png");
        private static Image m_Grass = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\Wall\\grass.gif");
        
        private int m_Width;

        public int Width
        {
            get { return m_Width; }
            set { m_Width = value; }
        }
        private int m_Height;

        public int Height
        {
            get { return m_Height; }
            set { m_Height = value; }
        }

        public enum TypeWall { GRASS,WATER,STONE };

        private TypeWall m_TypeWall;

        public TypeWall TypeWall1
        {
            get { return m_TypeWall; }
            set { m_TypeWall = value; }
        }

        public Wall(int x, int y, int width, int height, TypeWall type)
            : base(x, y)
        {
            Width = width;
            Height = height;
            m_TypeWall = type;
        }

        public override void Draw(Graphics g)
        {
            Image wall = null;
        
            switch (this.m_TypeWall)
            {
                case TypeWall.WATER:
                    {
                        wall = m_Water;
                        break;
                    }
                case TypeWall.STONE:
                    {
                        wall = m_Stone;
                        break;
                    }
                case TypeWall.GRASS:
                    {
                        wall = m_Grass;
                        break;
                    }
                default:
                    break;
            }
            g.DrawImage(wall,x,y);
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(this.x, this.y, this.Width, this.Height);
        }
    }
}
