using FigNet.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoServer.Modules.Room
{
    public class RoomManager
    {
        public Dictionary<int, FigNetRoom> Rooms = new Dictionary<int, FigNetRoom>();

        public void AddRoom(int id, string name, int maxPlayers, RoomType type)
        {
            Rooms.Add(id, FigNetRoom.CreateRoom(id, name, maxPlayers, type));
        }

        public void RemoveRoom(int id)
        {
            // todo: cleanup room
            Rooms.Remove(id);
        }

        public void OnData(int roomId, IPeer peer, IMessage data)
        {
            Rooms[roomId].OnData(data, peer);
        }

        public void PlayerLeft(int roomId, IPeer peer)
        {
            Rooms[roomId].OnPlayerLeft(peer);
        }

        public bool PlayerEnter(int roomId, IPeer peer)
        {
            return Rooms[roomId].OnPlayerEnter(peer);
        }
    }
}
