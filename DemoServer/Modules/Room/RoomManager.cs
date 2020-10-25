using DemoServer.Game;
using FigNet.Core;
using System.Collections.Generic;

namespace DemoServer.Modules.Room
{
    public class RoomManager : ITickable
    {
        public readonly Dictionary<int, FigNetRoom> Rooms = new Dictionary<int, FigNetRoom>();
        public readonly Dictionary<int, PlayerEntity> Players = new Dictionary<int, PlayerEntity>();
        private bool _lock;
        public void AddRoom(int id, string name, int maxPlayers, RoomType type)
        {
            _lock = true;
            Rooms.Add(id, FigNetRoom.CreateRoom(id, name, maxPlayers, type));
            FN.Logger.Info($"Room: {name} is Active");
            _lock = false;
        }

        public void RemoveRoom(int id)
        {
            _lock = true;
            Rooms.Remove(id);
            _lock = false;
        }

        public void OnData(int roomId, IPeer peer, IMessage data)
        {
            Rooms[roomId].OnData(data, peer);
        }

        public void Tick(float deltaTime)
        {
            if (_lock) return;
            foreach (var room in Rooms)
            {
                room.Value.Tick(deltaTime);
            }
        }

        public bool PlayerLeft(int roomId, IPeer peer)
        {
            return Rooms[roomId].OnPlayerLeft(peer);
        }

        public bool PlayerEnter(int roomId, IPeer peer)
        {
            return Rooms[roomId].OnPlayerEnter(peer);
        }

        
    }
}
