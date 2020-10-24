using System;
using GameCommon;
using FigNet.Core;


namespace DemoServer.Modules.Handlers
{
    public class JoinRoomHandler : IHandler
    {
        public ushort OpCode => (ushort)OperationCode.JoinRoom;

        public void HandleMessage(IMessage message, uint PeerId)
        {
            
            throw new NotImplementedException();
        }
    }
}
