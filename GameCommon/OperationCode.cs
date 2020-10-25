using System;

namespace GameCommon
{
    public enum OperationCode : ushort
    {
        SpawnLocalPlayer = 10,
        SpawnRemotePlayer = 11,
        PlayerLeftRoom = 12,
        RoomIsFull = 13,


        JoinRoom = 20,
        PlayerState = 21,

    }
}
