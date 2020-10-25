using FigNet.Core;
using DemoServer.Game;
using GameCommon.Gameplay;
using System.Collections.Generic;
using DemoServer.Modules.Operations;
using Object = GameCommon.Gameplay.Object;

namespace DemoServer.Modules.Room
{
    public enum RoomType
    {
        Open,
        Private,
        Game
    }

    public class FigNetRoom : ITickable
    {
        public int Id;
        public string Name;
        public int MaxPlayersAllowed = 10;
        public RoomType Type;

        public readonly List<GameObject> Players = new List<GameObject>();

        public int Maxplayers => MaxPlayersAllowed;
        public bool IsCollisionWithMap(Object @object) => false; // Map.IsCollisionWithCircle(@object) // todo: implement collisionMap
        // Net messages
        // broadcast | except sender
        // send by id

        // OnData : IMessage, Sender Id

        // OnPlayerLEft
        // OnPlayerJoin [spawn players with correct position]

        public void SendMessageToAll(IMessage message, DeliveryMethod method, byte channelId = 0) 
        {
            foreach (var player in Players)
            {
                (player as PlayerEntity).Peer.SendMessage(message, method, channelId);
            }
        }

        public void SendMessageToPeer(uint peerId, IMessage message, DeliveryMethod method, byte channelId = 0)
        {
            var pEntity = (Players.Find(p=>(p as PlayerEntity).Peer.Id == peerId) as PlayerEntity);
            if (pEntity != null)
            {
                var peer = pEntity.Peer;
                peer.SendMessage(message, method, channelId);
            }
            
        }

        public void Tick(float deltaTime)
        {
            for (int i = 0; i < Players.Count; i++)
            {
                (Players[i] as PlayerEntity).Tick(deltaTime);
            }
        }

        private void AddPlayer(IPeer peer)
        {
            SendMessageToPeer(peer.Id, SpawnLocalPlayer.GetOperation(peer.Peer.Id, this.Id), DeliveryMethod.Reliable);

            for (int i = 0; i < Players.Count; i++)
            {
                // send list of existing players to new player 
                SendMessageToPeer(peer.Id, SpawnRemotePlayer.GetOperation((Players[i] as PlayerEntity).Peer.Id, this.Id), DeliveryMethod.Reliable);
            }

            // tell existing players about new player
            SendMessageToAll(SpawnRemotePlayer.GetOperation(peer.Id, this.Id), DeliveryMethod.Reliable);
            var player = new PlayerEntity(peer, this);
            Players.Add(player);

            FN.Logger.Info($"Players in room {Players.Count}");

            //OnPlayerJoin?.Invoke(Players.Count);
        }

        public void OnData(IMessage data, IPeer peer) 
        {
            // redirect onplayerstate here
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> room status if room is empty shut down the game</returns>
        private bool RemovePlayer(uint id)
        {
            var player = Players.Find(p => (p as PlayerEntity).Peer.Id == id);
            if (player == null) return false;
            Players.Remove(player);
            SendMessageToAll(PlayerLeftRoom.GetOperation(id, this.Id), DeliveryMethod.Reliable);
            // todo: fire player count update
            FN.Logger.Info($"player left the room {id}");
            return true;
        }


        public bool OnPlayerEnter(IPeer peer) 
        {
            bool sucess = true;

            if (Players.Count == Maxplayers)
            {
                sucess = false;
            }
            else
            {
                AddPlayer(peer);
            }

            return sucess;
        }

        public bool OnPlayerLeft(IPeer peer) 
        {
            return RemovePlayer(peer.Id);
        }


        public PlayerEntity GetPlayerByID(uint id) 
        {
           return (Players.Find(p => (p as PlayerEntity).Peer.Id == id) as PlayerEntity);
        }

        public FigNetRoom(int id, string name, int maxPlayers, RoomType type)
        {
            this.Id = id;
            this.Name = name;
            this.MaxPlayersAllowed = maxPlayers;
            this.Type = type;
        }

        public static FigNetRoom CreateRoom(int id, string name, int maxPlayers, RoomType type)
        {
            return new FigNetRoom(id, name, maxPlayers, type);
        }

        
    }
}
