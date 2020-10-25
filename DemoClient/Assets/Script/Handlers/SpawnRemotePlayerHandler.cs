using GameCommon;
using FigNet.Core;

public class SpawnRemotePlayerHandler : IHandler
{
    public ushort OpCode => (ushort)OperationCode.SpawnRemotePlayer;

    public void HandleMessage(IMessage message, uint PeerId)
    {
        FN.Logger.Info($"@SpawnRemotePlayerHandler");
    }
}