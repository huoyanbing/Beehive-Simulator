using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BeehiveSimulator
{
    [Serializable]
    public class Flower
    {
        private const int LifeSpanMin = 15000;     //最低寿命
        private const int LifeSpanMax = 30000;     //最高寿命
        private const double InitialNectar = 1.5;    //初始花蜜量
        private const double MaxNectar = 5.0;      //最大花蜜量
        private const double NectarAddedPerTurn = 0.01;    //每循环一次，花蜜增加量
        private const double NectarGatheredPerTurn = 0.3;  // 蜜蜂采集一次的花蜜量

        public Point Location { get; private set; }   //位置
         
        public int Age { get; private set; }    //年龄

        public bool Alive { get; private set; }   //是否活的

        public double Nectar { get; private set; }    //花蜜量

        public double NectarHarvested { get; set; }   //被采的花蜜量

        private int lifeSpan;    //花的寿命

        public Flower(Point location, Random random)
        {
            Location = location;
            Age = 0;
            Alive = true;
            Nectar = InitialNectar;
            NectarHarvested = 0;
            lifeSpan = random.Next(LifeSpanMin, LifeSpanMax + 1);
        }

        public double HarvestNectar()
        {
            if (NectarGatheredPerTurn > Nectar)
                return 0;
            else
            {
                Nectar -= NectarGatheredPerTurn;
                NectarHarvested += NectarGatheredPerTurn;
                return NectarGatheredPerTurn;
            }
        }

        public void Go()
        {
            Age++;
            if (Age > lifeSpan)
                Alive = false;
            else
            {
                Nectar += NectarAddedPerTurn;
                if (Nectar > MaxNectar)
                    Nectar = MaxNectar;
            }
        }
    }
}
