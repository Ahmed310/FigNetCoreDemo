using System;
using GameCommon;
using FigNet.Core;

namespace DemoServer.Modules.Handlers
{
    public class PlayerStateHandler : IHandler
    {
        public ushort OpCode => (ushort)OperationCode.PlayerState;

        public void HandleMessage(IMessage message, uint PeerId)
        {
            throw new NotImplementedException();
        }
    }
}
