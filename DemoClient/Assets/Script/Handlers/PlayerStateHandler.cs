using System;
using GameCommon;
using FigNet.Core;

public class PlayerStateHandler : IHandler
{
    public ushort OpCode => (ushort)OperationCode.PlayerState;

    public void HandleMessage(IMessage message, uint PeerId)
    {
        ulong seqId = Convert.ToUInt64(message.Parameters[0]);
        uint playerId = Convert.ToUInt32(message.Parameters[1]);
        int x = Convert.ToInt32(message.Parameters[2]);
        int z = Convert.ToInt32(message.Parameters[3]);
        int rot = Convert.ToInt32(message.Parameters[4]);
        int health = Convert.ToInt32(message.Parameters[6]);

        //var game = ServiceLocator.GetService<GameBase>();
        //if (game == null) return;
        //var player = game.GetNetPlayerById(playerId);
        //if (player == null) return;
        //var state = new PlayerState()
        //{
        //    SeqNumber = seqId,
        //    x = x,
        //    z = z,
        //    rotY = rot,
        //    shooting = false
        //};

        //if (message.Parameters.ContainsKey(5))
        //{
        //    state.shooting = true;
        //}

        //player.AddSate(state);
        //player.View.StatUI.UpdateHealth(health / 1000);
        FN.Logger.Info($"@OnPlayerState {playerId} - {x}|{z} -- SeqID {seqId}");
    }
}