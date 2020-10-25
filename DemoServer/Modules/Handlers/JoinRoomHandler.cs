using System;
using GameCommon;
using FigNet.Core;
using DemoServer.Modules.Room;

namespace DemoServer.Modules.Handlers
{
    public class JoinRoomHandler : IHandler
    {
        public ushort OpCode => (ushort)OperationCode.JoinRoom;
        private RoomManager rooms;
        public void HandleMessage(IMessage message, uint PeerId)
        {
            rooms ??= ServiceLocator.GetService<RoomManager>();

            int roomId = Convert.ToInt32(message.Parameters[0]);
            var peer = FN.PeerCollection.GetPeerByID(PeerId);
            var sucess = rooms.PlayerEnter(roomId, peer);

            // todo: 

            // here response with roomjoin operation with sucess status

            if (sucess)
            {

            }
            else
            {
                
            }
        }
    }
}
