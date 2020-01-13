using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeehiveSimulator
{
    public enum BeeState
    {
        Idle,       //空闲
        FlyingToFlower,    //飞向花朵
        GatheringNectar,   //正收集花蜜
        ReturningToHive,   //返回蜂巢
        MakingHoney,      //制作蜂蜜
        Retired       //退休
    }
}
