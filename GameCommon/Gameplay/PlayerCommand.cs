using System;

namespace GameCommon.Gameplay
{
    [Flags]
    public enum PlayerCommand : int
    {
        UP = 1 << 1,
        Down = 1 << 2,
        Left = 1 << 3,
        Right = 1 << 4,
        Rotate = 1 << 5,
        Shoot = 1 << 6

    }

    public class PlayerCommandObject
    {
        public ulong seqNo;
        public PlayerCommand Command;
        public float Rotation;    // todo: add fields as required
    }

    public class PlayerState
    {
        public ulong seqNo;
        public uint PlayerId;
        public int X, Y;
        public int Rot;
    }
}
