using DemoServer.Modules.Handlers;
using FigNet.Core;

namespace DemoServer.Modules.Room
{
    public class RoomModule : IModule
    {
        private RoomManager roomManager;
        public void Load(IServer server)
        {
            roomManager = new RoomManager();
            ServiceLocator.Bind(roomManager.GetType(), roomManager);
            FN.Logger.Info($"RoomModule loaded");

            FN.HandlerCollection.RegisterHandler(new JoinRoomHandler());
            FN.HandlerCollection.RegisterHandler(new PlayerCommandHandler());

            FN.Server.OnConnected += OnPeerConnected;
            FN.Server.OnDisconnected += OnPeerDisConnected;
        }

        public void Process(float deltaTime)
        {
            roomManager?.Tick(deltaTime);
        }

        public void UnLoad()
        {
            ServiceLocator.UnBind(roomManager.GetType());

            FN.Server.OnConnected -= OnPeerConnected;
            FN.Server.OnDisconnected -= OnPeerDisConnected;
        }

        private void OnPeerConnected(IPeer peer) 
        {
            roomManager.PlayerEnter(0, peer);       // add to lobby by default
        }

        private void OnPeerDisConnected(IPeer peer)
        { }

    }
}
