using GameCommon;
using FigNetProviders;
using System.Collections.Generic;

namespace DemoServer.Modules.Operations
{
    public class PlayerStateOperation
    {
        public static Operation GetOperation(ulong seqNo, uint playerId, int x, int y, int rot, int hp, bool shooting = false)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { 0, seqNo },       // 0: seqNo
                { 1, playerId },    // 1: playerId
                { 2, x },           // 2: x pos
                { 3, y },           // 3: y pos
                { 4, rot },         // 4: player Rotation
                { 6, hp}
            };

            if (shooting)
            {
                parameters.Add(5, true);
            }

            Operation operation = new Operation((byte)OperationCode.PlayerState, parameters, null);
            return operation;
        }
    }
}
