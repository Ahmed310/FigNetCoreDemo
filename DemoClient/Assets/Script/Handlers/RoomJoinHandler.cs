using GameCommon;
using FigNet.Core;

public class RoomJoinHandler : IHandler
{
    public ushort OpCode => (ushort)OperationCode.JoinRoom;

    public void HandleMessage(IMessage message, uint PeerId)
    {
        FN.Logger.Info($"@RoomJoinHandler");
    }
}