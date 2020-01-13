using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BeehiveSimulator
{
    [Serializable]
    public class Hive
    {
        private World world;
        private const int InitialBees = 6;    //初始蜜蜂数量
        private const double InitialHoney = 3.2;   //蜂巢初始蜂蜜量
        private const double MaximumHoney = 15.0;   //最大蜂蜜量
        private const double NectarHoneyRatio = .25;  //花蜜转蜂蜜的转化率
        private const double MinimumHoneyForCreatingBees = 4.0;   //产生蜜蜂最少需要的蜂蜜量
        private const int MaximumBees = 8;  //最大蜜蜂数量

        private Dictionary<string, Point> locations;     //设置几个固定的位置
        private int beeCount = 0;

        public double Honey { get; private set; }    //蜂巢总的蜂蜜量

        [NonSerialized]
        public BeeMessage MessageSender;

        private void InitializeLocations()
        {
            locations = new Dictionary<string, Point>();
            locations.Add("Entrance", new Point(600, 100));
            locations.Add("Nursery", new Point(95, 154));
            locations.Add("HoneyFactory", new Point(157, 58));
            locations.Add("Exit", new Point(167, 174));
        }

        public Point GetLocation(string location)
        {
            if (locations.Keys.Contains(location))
                return locations[location];
            else
                throw new ArgumentException("Unknown location: " + location);
        }

        public Hive(World world, BeeMessage MessageSender)
        {
            this.MessageSender = MessageSender;
            this.world = world;
            Honey = InitialHoney;
            InitializeLocations();
            Random random = new Random();
            for (int i = 0; i < InitialBees; i++)
                AddBee(random);
        }

        public bool AddHoney(double nectar)
        {
            double honeyToAdd = nectar * NectarHoneyRatio;
            if (honeyToAdd + Honey > MaximumHoney)
                return false;
            Honey += honeyToAdd;
            return true;
        }

        public bool ConsumeHoney(double amount)
        {
            if (amount > Honey)
                return false;
            else
            {
                Honey -= amount;
                return true;
            }
        }

        private void AddBee(Random random)
        {
            beeCount++;
            int r1 = random.Next(100) - 50;
            int r2 = random.Next(100) - 50;
            Point startPoint = new Point(locations["Nursery"].X + r1,
                                         locations["Nursery"].Y + r2);
            Bee newBee = new Bee(beeCount, startPoint, world, this);
            newBee.MessageSender += this.MessageSender;
            world.Bees.Add(newBee);
            // Once we have a system, we need to add this bee to the system
        }

        public void Go(Random random)
        {
            if (world.Bees.Count < MaximumBees
                 && Honey > MinimumHoneyForCreatingBees
                 && random.Next(10) == 1)
            {
                AddBee(random);
            }
        }
    }
}