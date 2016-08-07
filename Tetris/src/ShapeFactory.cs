using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class ShapeFactory
    {
        public Shape m_ShapeA;

        public Shape m_ShapeB;

        private int[][,] m_Shapes = new int[7][,]
        {         
           /* 第一种 */
           new int[,]
           { 
	            {   1, 1, 0, 0, 
                    1, 1, 0, 0, 
                    0, 0, 0, 0, 
                    0, 0, 0, 0 } ,
           },

	        /* 第二种 */
	        new int[,]
            { 
	            {   1, 1, 1, 0, 
                    0, 0, 1, 0, 
                    0, 0, 0, 0, 
                    0, 0, 0, 0 },

	            {   0, 1, 0, 0, 
                    0, 1, 0, 0, 
                    1, 1, 0, 0, 
                    0, 0, 0, 0 },

	            {   1, 0, 0, 0, 
                    1, 1, 1, 0, 
                    0, 0, 0, 0, 
                    0, 0, 0, 0 },

	            {   1, 1, 0, 0,
                    1, 0, 0, 0, 
                    1, 0, 0, 0, 
                    0, 0, 0, 0 } 
            },

	        /* 第三种 */
	        new int[,]
            { 
	            {   1, 1, 1, 0, 
                    1, 0, 0, 0, 
                    0, 0, 0, 0, 
                    0, 0, 0, 0 },

	            {   1, 1, 0, 0, 
                    0, 1, 0, 0, 
                    0, 1, 0, 0, 
                    0, 0, 0, 0 },

	            {   0, 0, 1, 0, 
                    1, 1, 1, 0, 
                    0, 0, 0, 0, 
                    0, 0, 0, 0 },

	            {   1, 0, 0, 0, 
                    1, 0, 0, 0, 
                    1, 1, 0, 0, 
                     0, 0, 0, 0 }
            },

	        /* 第四种 */
	        new int[,]
            {

	            {   1, 1, 0, 0,
                    0, 1, 1, 0, 
                    0, 0, 0, 0, 
                    0, 0, 0, 0 },

	            {   0, 1, 0, 0, 
                    1, 1, 0, 0, 
                    1, 0, 0, 0, 
                    0, 0, 0, 0, } 
            },

	        /* 第五种 */
	        new int[,]
            { 
	            {   0, 1, 1, 0, 
                    1, 1, 0, 0, 
                    0, 0, 0, 0, 
                    0, 0, 0, 0 },

	            {   1, 0, 0, 0, 
                    1, 1, 0, 0, 
                    0, 1, 0, 0, 
                    0, 0, 0, 0 } 
            },

	        /* 第六种 */
	        new int[,]
            {
	            {   0, 1, 0, 0, 
                    1, 1, 1, 0, 
                    0, 0, 0, 0, 
                    0, 0, 0, 0 },

	            {   1, 0, 0, 0, 
                    1, 1, 0, 0, 
                    1, 0, 0, 0, 
                    0, 0, 0, 0 },

	            {   1, 1, 1, 0, 
                    0, 1, 0, 0, 
                    0, 0, 0, 0, 
                    0, 0, 0, 0 },

	            {   0, 1, 0, 0, 
                    1, 1, 0, 0, 
                    0, 1, 0, 0, 
                    0, 0, 0, 0 } 
            },

	        /* 第七种 */
	        new int[,]
            {
	            {   1, 1, 1, 1, 
                    0, 0, 0, 0, 
                    0, 0, 0, 0, 
                    0, 0, 0, 0 },

	            {   0, 1, 0, 0, 
                    0, 1, 0, 0, 
                    0, 1, 0, 0, 
                    0, 1, 0, 0 }
            }
            
        };


        private ShapeFactory()
        {
            m_ShapeA = this.MakeShape();
            m_ShapeB = this.MakeShape();
        }

        /// <summary>
        /// 
        /// </summary>
        private static volatile ShapeFactory instance = null;

        /// <summary>
        /// 创建一个ShapeFactory对象
        /// </summary>
        /// <returns></returns>
        public static ShapeFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new ShapeFactory();
            }

            return instance;
        }

        private Shape MakeShape()
        {
            Shape shape = new Shape();

            int type = new Random().Next(m_Shapes.Length);

            int status = new Random().Next(m_Shapes[type].Length / 16);

            shape.SetStutas(status);

            shape.SetBody(m_Shapes[type]);

            return shape;
        }

        public Shape GetShape()
        {
            m_ShapeA = m_ShapeB;
            m_ShapeB = MakeShape();

            m_ShapeA.SetLeft(8);
            m_ShapeA.SetTop(0);
            return m_ShapeA;
        }

    }
}
