using FigNet.Core;
using System.Numerics;
using GameCommon.Gameplay;
using DemoServer.Modules.Room;
using System.Collections.Generic;
using GameCommon;
using FigNetProviders;
using DemoServer.Modules.Operations;

namespace DemoServer.Game
{
    public class PlayerEntity : GameObject, ITickable
    {
        public IPeer Peer { get; }
        public FigNetRoom Room { get; }

        public float Hp { get; set; }
        public bool IsCollidingWithWall { get; set; }

        private readonly Queue<PlayerCommandObject> playerCommands = new Queue<PlayerCommandObject>();
        
        // to keep history of player for 1 sec
        private readonly Vector2[] history;
        private int historyFrame = 0;
        private IMessage playerStateCache = null;

        public PlayerEntity(IPeer peer, FigNetRoom room)
        {
            Peer = peer;
            Room = room;

            // keep track of history for 1 second
            history = new Vector2[(int)(1 / Constants.DELTA_TIME)];
        }

        public override void UpdateProximityList()
        {
            for (int i = 0; i < Room.Players.Count; i++)
            {
                this.ProximityList = Room.Players.FindAll(go => go.IsProximity(this));
            }
        }

        public void Send(IMessage operation, IPeer peer, DeliveryMethod mode, byte channelId = 0)
        {
            peer.SendMessage(operation, mode, channelId);
        }

        public void Tick(float deltaTime)
        {
            ExecuteCommand(deltaTime);
            RecordHistory(deltaTime);
        }

        public void AddPlayerCommand(PlayerCommandObject command)
        {
            playerCommands.Enqueue(command);
        }

        public void ExecuteCommand(float deltaTime)
        {
            if (playerCommands.Count > 0)
            {
                PlayerCommandObject command = playerCommands.Dequeue();
                ApplyPlayerCommand(command, deltaTime);
            }
        }

        public bool ApplyDamage()
        {
            Hp -= Constants.BULLET_DAMAGE;
            return Hp > 0;
        }

        private void ApplyPlayerCommand(PlayerCommandObject command, float deltaTime)
        {
            if (command.Command.HasFlag(PlayerCommand.UP))
            {
                Transform.p.Y -= Constants.PLAYER_SPEED * Constants.DELTA_TIME;
                IsCollidingWithWall = Room.IsCollisionWithMap(this);
                if (IsCollidingWithWall)
                {
                    Transform.p.Y += Constants.PLAYER_SPEED * Constants.DELTA_TIME;
                }
            }
            else if (command.Command.HasFlag(PlayerCommand.Down))
            {
                Transform.p.Y += Constants.PLAYER_SPEED * Constants.DELTA_TIME;
                IsCollidingWithWall = Room.IsCollisionWithMap(this);
                if (IsCollidingWithWall)
                {
                    Transform.p.Y -= Constants.PLAYER_SPEED * Constants.DELTA_TIME;
                }
            }

            if (command.Command.HasFlag(PlayerCommand.Left))
            {
                Transform.p.X += Constants.PLAYER_SPEED * Constants.DELTA_TIME;
                IsCollidingWithWall = Room.IsCollisionWithMap(this);
                if (IsCollidingWithWall)
                {
                    Transform.p.X -= Constants.PLAYER_SPEED * Constants.DELTA_TIME;
                }
            }
            else if (command.Command.HasFlag(PlayerCommand.Right))
            {
                Transform.p.X -= Constants.PLAYER_SPEED * Constants.DELTA_TIME;
                IsCollidingWithWall = Room.IsCollisionWithMap(this);
                if (IsCollidingWithWall)
                {
                    Transform.p.X += Constants.PLAYER_SPEED * Constants.DELTA_TIME;
                }
            }

            if (command.Command.HasFlag(PlayerCommand.Rotate))
            {
                //Transform.r = command.Rotation;
                //angle = command.Rotation;
            }

            if (command.Command.HasFlag(PlayerCommand.Shoot))
            {
                //var bullet = game.GetAvaliableBullet();
                //// todo: make Gun offset constant (offset moved inside init)
                //bullet.Init(Peer.ID, Transform.p.X + 0.0f, Transform.p.Y, angle);
            }

            playerStateCache = PlayerStateOperation.GetOperation(command.seqNo,
                                                                    Peer.Id,
                                                                    (int)(base.Transform.p.X * 1000),
                                                                    (int)(base.Transform.p.Y * 1000),
                                                                    (int)(command.Rotation * 1000),
                                                                    (int)(Hp * 1000),
                                                                    command.Command.HasFlag(PlayerCommand.Shoot));

            SendToAll((Operation)playerStateCache);

            // logger.Information($"pId {Peer.ID} Proximity: {ProximityList.Count} - cmd {command.Command} - Pos {Transform.p} - Rot {angle} - seqNo {command.seqNo}");
        }


        private void SendToAll(Operation operation)
        {
            // send new transform to all player in proximity
            foreach (var player in ProximityList)
            {
                var pEntity = player as PlayerEntity;
                pEntity.Send(operation, pEntity.Peer, DeliveryMethod.Reliable, 0 );
            }
        }


        private void RecordHistory(float deltaTime)
        {
            history[historyFrame++] = Transform.p; 

            if (historyFrame >= history.Length)
            {
                historyFrame = 0;
            }
        }

        
    }
}
