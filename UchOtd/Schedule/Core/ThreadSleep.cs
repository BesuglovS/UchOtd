using System.Threading;

namespace UchOtd.Schedule.Core
{
    public static class ThreadSleep
    {
        public static int OK = -1;
        public static int Up = -2;
        public static int Reset = -3;

        public static int time = 1000;

        public static void Run(int t = -1)
        {
            switch (t)
            {
                case -1:
                    Thread.Sleep(time);
                    break;
                case -2:
                    time += 1000;
                    Thread.Sleep(time);
                    break;
                case -3:
                    time = 1000;
                    Thread.Sleep(time);
                    break;
            }
            
        }
        
    }
}
