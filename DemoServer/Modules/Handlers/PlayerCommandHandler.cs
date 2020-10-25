using GameCommon;
using FigNet.Core;
using DemoServer.Modules.Room;
using System;
using GameCommon.Gameplay;

namespace DemoServer.Modules.Handlers
{
    public class PlayerCommandHandler : IHandler
    {
        private RoomManager rooms;
        public ushort OpCode => (ushort)OperationCode.PlayerState;

        public void HandleMessage(IMessage message, uint PeerId)
        {
            rooms ??= ServiceLocator.GetService<RoomManager>();

            int roomId = Convert.ToInt32(message.Parameters[0]);
            PlayerCommand command = (PlayerCommand)Convert.ToInt32(message.Parameters[1]);
            ulong seqId = Convert.ToUInt64(message.Parameters[2]);
            int rotation = Convert.ToInt32(message.Parameters[3]);
            var room = rooms.Rooms[roomId];
            if (room == null) return;
            var player = room.GetPlayerByID(PeerId);
            if (player == null) return ;    // player is null when he leaves the game & there are some commands in queue
            player.AddPlayerCommand(new PlayerCommandObject() { Command = command, seqNo = seqId, Rotation = (rotation / 1000f) });  // todo: assign other fields as well    
        }
    }
}
