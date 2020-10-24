using GameCommon;
using FigNetProviders;
using System.Collections.Generic;

namespace DemoServer.Modules.Operations
{
    public class RoomIsFull
    {
        public static Operation GetOperation()
        {
            Operation operation = new Operation((ushort)OperationCode.RoomIsFull, null, null);
            return operation;
        }
    }
}
