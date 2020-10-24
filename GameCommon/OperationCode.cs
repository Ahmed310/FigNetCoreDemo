using System;

namespace GameCommon
{
    public enum OperationCode : ushort
    {
        SpawnLocalPlayer,
        SpawnRemotePlayer,
        PlayerLeftRoom,
        RoomIsFull,


        JoinRoom,
        PlayerState,

    }
}
