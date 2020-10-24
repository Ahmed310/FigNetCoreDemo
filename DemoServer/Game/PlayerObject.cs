using DemoServer.Modules.Room;
using FigNet.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoServer.Game
{
    public class PlayerObject
    {
        public IPeer Peer { get; }
        public FigNetRoom Room { get; }
        public PlayerObject(IPeer peer, FigNetRoom room)
        {
            Peer = peer;
            Room = room;
        }
    }
}
