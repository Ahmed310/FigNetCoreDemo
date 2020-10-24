using DemoServer.Game;
using DemoServer.Modules.Operations;
using FigNet.Core;
using GameCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoServer.Modules.Room
{
    public enum RoomType
    {
        Open,
        Private,
        Game
    }

    public class FigNetRoom
    {
        public int Id;
        public string Name;
        public int MaxPlayersAllowed = 10;
        public RoomType Type;

        private readonly List<PlayerObject> Players = new List<PlayerObject>();

        public int Maxplayers { get; }

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
                player.Peer.SendMessage(message, method, channelId);
            }
        }

        public void SendMessageToPeer(uint peerId, IMessage message, DeliveryMethod method, byte channelId = 0)
        {
            var peer = Players.Find(p=>p.Peer.Id == peerId).Peer;
            peer.SendMessage(message, method, channelId);
            
        }


        private void AddPlayer(IPeer peer)
        {
            SendMessageToPeer(peer.Id, SpawnLocalPlayer.GetOperation(peer.Peer.Id, this.Id), DeliveryMethod.Reliable);

            for (int i = 0; i < Players.Count; i++)
            {
                // send new player list of existing player
                SendMessageToPeer(peer.Id, SpawnRemotePlayer.GetOperation(Players[i].Peer.Id, this.Id), DeliveryMethod.Reliable);
            }

            // tell existing players about new player
            SendMessageToAll(SpawnRemotePlayer.GetOperation(peer.Id, this.Id), DeliveryMethod.Reliable);
            var player = new PlayerObject(peer, this);
            Players.Add(player);

            FN.Logger.Info($"Players in Game {Players.Count}");

            //OnPlayerJoin?.Invoke(Players.Count);
        }

        public void OnData(IMessage data, IPeer peer) 
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> room status if room is empty shut down the game</returns>
        private bool RemovePlayer(uint id)
        {
            var player = Players.Find(p => p.Peer.Id == id);
            if (player == null) return false;
            Players.Remove(player);
            SendMessageToAll(PlayerLeftRoom.GetOperation(id, this.Id), DeliveryMethod.Reliable);
            // todo: fire player count update
            FN.Logger.Info($"player left the game {id}");
            return Players.Count < 1;
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

        public void OnPlayerLeft(IPeer peer) 
        {
            RemovePlayer(peer.Id);
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
