﻿using GameCommon;
using FigNetProviders;
using System.Collections.Generic;

namespace DemoServer.Modules.Operations
{
    public class SpawnRemotePlayer
    {
        public static Operation GetOperation(uint peerId, int roomId)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { 0, (uint)peerId },  // peerId
                { 1, (int)roomId }   // roomId
            };
            Operation operation = new Operation((ushort)OperationCode.SpawnRemotePlayer, parameters, null);
            return operation;
        }
    }
}
