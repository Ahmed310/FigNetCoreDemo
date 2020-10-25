using GameCommon;
using FigNetProviders;
using System.Collections.Generic;

public class JoinRoomOperation
{
    public static Operation GetOperation(uint peerId, int roomId)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { 0, (uint)peerId },  // peerId
                { 1, (int)roomId }   // roomId
            };
        Operation operation = new Operation((ushort)OperationCode.JoinRoom, parameters, null);
        return operation;
    }
}