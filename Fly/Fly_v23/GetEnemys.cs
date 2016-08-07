using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fly
{
    public class GetEnemys
    {
        public static void GetEnemyOne()
        {
            HitCheck.GetInstance().AddElement(new EnemyOne(MainForm.m_EnemyRandom.Next(-90, 500), -50, false, 10, 10, 10, MainForm.m_EnemyRandom.Next(0, 2) == 0 ? true : false));
        }

        public static void GetEnemyTwo()
        {
            HitCheck.GetInstance().AddElement(new EnemyTwo(MainForm.m_GAMEWIDTH + 30, MainForm.m_EnemyRandom.Next(100, 550), false, 10, 10, 10));
        }

        public static void GetEnemyThree()
        {
            HitCheck.GetInstance().AddElement(new EnemyThree(-50, MainForm.m_EnemyRandom.Next(80, 500), false, 15, 15, 10));
        }

        public static void GetEnemyFour()   
        {
            int rand = MainForm.m_EnemyRandom.Next(0, 2);
            HitCheck.GetInstance().AddElement(new EnemyFour(0, -50, false, 20, 20, 20, rand == 0 ? true : false));
        }

        public static void GetEnemyFive()
        {
            int rand = MainForm.m_EnemyRandom.Next(0, 2);
            HitCheck.GetInstance().AddElement(new EnemyFive(rand * 300 + 100, MainForm.m_GAMEHEIGHT, false, 15, 15, 40));
        }

        public static void GetEnemySix()
        {
            for (int i = 0; i < 10; i++)
            {
                HitCheck.GetInstance().AddElement(new EnemySix(-80 * i, -50 * i, false, 10, 6, 30, true));
                HitCheck.GetInstance().AddElement(new EnemySix(550 + 80 * i, -50 * i, false, 10, 6, 30, false));
            }
        }

        public static void GetEnemySeven()
        {
            for (int i = 0; i < 10; i++)
            {
                HitCheck.GetInstance().AddElement(new EnemySeven(300 - 80 * i, -50 * i, false, 10, 6, 30, true));
                HitCheck.GetInstance().AddElement(new EnemySeven(300 + 80 * i, -50 * i, false, 10, 6, 30, false));
            }
        }

        public static void GetEnemyEight()
        {
            int rand = MainForm.m_EnemyRandom.Next(0, 2);
            HitCheck.GetInstance().AddElement(new EnemyEight(rand * MainForm.m_GAMEWIDTH - 30, 50, false, 5, 5, 40, rand == 0 ? true : false));
        }

        public static void GetEnemyNine()
        {
            for (int i = 0; i < 10; i++)
            {
                HitCheck.GetInstance().AddElement(new EnemyNine(30 + 50 * i, -50 * i, false, 20, 20, 10));
                HitCheck.GetInstance().AddElement(new EnemyNine(30 + 50 * i, -50 * (20 - i), false, 20, 20, 10));
            }
        }

        public static void GetEnemyTen()
        {
            int rand = MainForm.m_EnemyRandom.Next(0, 2);
            HitCheck.GetInstance().AddElement(new EnemyTen(rand * 300 + 100, -50, false, 5, 5, 40));
        }

        public static void GetEnemyEleven()
        {
            int rand = MainForm.m_EnemyRandom.Next(0, 2);
            for (int i = 0; i < 6; i++)
            {
                HitCheck.GetInstance().AddElement(new EnemyEleven(rand * 250 + 10 + 50 * i, -200 * i, false, 30, 30, 40));
            }
        }

        public static void GetEnemyTwelve()
        {
            int rand = MainForm.m_EnemyRandom.Next(0, 2);
            HitCheck.GetInstance().AddElement(new EnemyTwelve(rand * 300 + 100, -50, false, 40, 40, 10));
        }

        public static void GetEnemyThirteen()
        {
            int rand = MainForm.m_EnemyRandom.Next(0, 2);

            if (rand == 1)
            {
                for (int i = 0; i < 6; i++)
                {
                    HitCheck.GetInstance().AddElement(new EnemyThirteen(550 + 90 * i, 100 + 40 * i, false, 30, 30, 50, true));
                }
            }

            else
            {
                for (int i = 0; i < 6; i++)
                {
                    HitCheck.GetInstance().AddElement(new EnemyThirteen(-90 * i, 100 + 40 * i, false, 30, 30, 50, false));
                }
            }
        }

        public static void GetEnemyFourteen()
        {
            for (int i = 0; i < 6; i++)
            {
                HitCheck.GetInstance().AddElement(new EnemyFourteen(530 + 90 * (6 - i), 100 + 30 * i, false, 30, 30, 50, true));
                HitCheck.GetInstance().AddElement(new EnemyFourteen(-90 * (6 - i), 100 + 30 * i, false, 30, 30, 50, false));
            }
        }

        public static void GetEnemyFifteen()
        {
            int rand = MainForm.m_EnemyRandom.Next(0, 6);
            for (int i = 0; i < 3; i++)
            {
                HitCheck.GetInstance().AddElement(new EnemyFifteen(50 * rand + 30 * i, MainForm.m_GAMEHEIGHT + 90 * i, false, 20, 20, 20));
            }
        }

        public static void GetEnemySixteen()
        {
            for (int i = 0; i < 6; i++)
            {
                HitCheck.GetInstance().AddElement(new EnemySixteen(20 + 90 * i, -90 * i, false, 30, 30, 40));
            }
        }

        public static void GetEnemySeventeen()
        {
            for (int i = 0; i < 8; i++)
            {
                HitCheck.GetInstance().AddElement(new EnemySeventeen(50, -50 * i, false, 30, 30, 60));
                HitCheck.GetInstance().AddElement(new EnemySeventeen(500, -50 * i, false, 30, 30, 60));
            }
        }

        public static void GetEnemyEighteen()
        {
            int rand = MainForm.m_EnemyRandom.Next(0, 2);
            HitCheck.GetInstance().AddElement(new EnemyEighteen(rand * MainForm.m_GAMEWIDTH - 30, 50, false, 5, 5, 100, rand == 0 ? true : false));
        }

        public static void GetEnemyNineteen()
        {
            int rand = MainForm.m_EnemyRandom.Next(0, 2);
            if (rand == 1)
            {
                HitCheck.GetInstance().AddElement(new EnemyNineteen(100, -50, false, 5, 5, 80));
            }
            else
            {
                HitCheck.GetInstance().AddElement(new EnemyNineteen(400, -50, false, 5, 5, 80));
            }
        }

        public static void GetEnemyTwenty()
        {
            int rand = MainForm.m_EnemyRandom.Next(0, 2);
            if (rand == 1)
            {
                HitCheck.GetInstance().AddElement(new EnemyTwenty(100, MainForm.m_GAMEHEIGHT, false, 4, 4, 80));
            }
            else
            {
                HitCheck.GetInstance().AddElement(new EnemyTwenty(400, MainForm.m_GAMEHEIGHT, false, 4, 4, 80));
            }
        }

        public static void GetEnemyTwentyOne()
        {
            HitCheck.GetInstance().AddElement(new EnemyTwentyOne(MainForm.m_EnemyRandom.Next(100, 500), -50, false, 20, 20, 10));
        }

        public static void GetEnemyTwentyTwo()
        {
            HitCheck.GetInstance().AddElement(new EnemyTwentyTwo(MainForm.m_EnemyRandom.Next(100, 500), -50, false, 20, 20, 10));
        }

        public static void GetEnemyStone()
        {
            HitCheck.GetInstance().AddElement(new EnemyStone(MainForm.m_EnemyRandom.Next(10, 500), -50, false, 2, 2, 100));
        }

        public static void GetEnemyBoss()
        {
            HitCheck.GetInstance().AddElement(new EnemyBoss(-90, 200, false, 6, 6, 500, true, true));
        }
    }
}
