using GameCommon;
using FigNetProviders;
using System.Collections.Generic;

namespace DemoServer.Modules.Operations
{
    public class SpawnLocalPlayer
    {
        public static Operation GetOperation(uint peerId, int roomId) 
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { 0, (uint)peerId },  // peerId
                { 1, (int)roomId }   // roomId
            };
            Operation operation = new Operation((ushort)OperationCode.SpawnLocalPlayer, parameters, null);
            return operation;
        }
    }
}
