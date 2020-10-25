using GameCommon;
using FigNet.Core;

public class RoomLeftHandler : IHandler
{
    public ushort OpCode => (ushort)OperationCode.PlayerLeftRoom;

    public void HandleMessage(IMessage message, uint PeerId)
    {
        FN.Logger.Info($"@RoomLeftHandler");
    }
}