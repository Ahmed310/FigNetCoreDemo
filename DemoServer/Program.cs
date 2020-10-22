using System;
using FigNet.Core;
using FigNetServer;
using System.Threading;
using System.Diagnostics;

namespace DemoServer
{
    class Program
    {
        private static int frameMilliseconds;
        private static float deltaTime = 0;
        static void Main(string[] args)
        {
            IServer serverApp = new ServerApplication();
            serverApp.SetUp();
            FN.Logger.Info($"FrameRate {FN.Settings.FrameRate}");
            Run(serverApp);
            serverApp.TearDown();
        }

        private static void Run(IServer server)
        {
            frameMilliseconds = 1000 / FN.Settings.FrameRate;

            Stopwatch stopwatch = new Stopwatch();
            int overTime = 0;

            while (!Console.KeyAvailable)
            {
                stopwatch.Restart();

                deltaTime = (frameMilliseconds + overTime) * 0.001f;

                server.Process(deltaTime);

                stopwatch.Stop();

                int stepTime = (int)stopwatch.ElapsedMilliseconds;

                if (stepTime <= frameMilliseconds)
                {
                    Thread.Sleep(frameMilliseconds - stepTime);
                    overTime = 0;
                }
                else
                {
                    overTime = stepTime - frameMilliseconds;
                }
            }
        }
    }
}