using GameCommon;
using FigNet.Core;

public class SpawnLocalPlayerHandler : IHandler
{
    public ushort OpCode => (ushort)OperationCode.SpawnLocalPlayer;

    public void HandleMessage(IMessage message, uint PeerId)
    {
        FN.Logger.Info($"@SpawnLocalPlayerHandler");
    }
}