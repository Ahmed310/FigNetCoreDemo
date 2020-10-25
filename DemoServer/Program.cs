using System;
using FigNet.Core;
using FigNetServer;
using System.Threading;
using System.Diagnostics;
using DemoServer.Modules.Room;

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
            AddDefaultRooms();
            FN.Logger.Info($"FrameRate {FN.Settings.FrameRate}");
            Run(serverApp);
            serverApp.TearDown();
        }

        private static void AddDefaultRooms() 
        {
            var roomManager = ServiceLocator.GetService<RoomManager>();

            roomManager.AddRoom(0, "Lobby", 100, RoomType.Open);
            roomManager.AddRoom(1, "Zone1", 10, RoomType.Open);
            roomManager.AddRoom(2, "Zone2", 10, RoomType.Open);
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