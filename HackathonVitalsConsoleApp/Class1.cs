using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonVitalsConsoleApp
{
    class Vitals
    {
      public  int Temperature { get; set;}
      public  int Heartbeat { get; set; }

        //Set Vitals
        public void setTemperature(int temp)
        {
            Temperature = temp;
        }

        public void setHeartBeat(int heart)
        {
            Heartbeat = heart;
        }

        //Get Vitals
        public int getTemperature()
        {
            return Temperature;
        }

        public int getHeartBeat()
        {
            return Heartbeat;
        }
    }


}
