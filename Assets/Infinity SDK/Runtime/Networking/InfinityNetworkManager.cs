using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Infinity.Runtime.Core.Logging;
using Infinity.Runtime.XR;
using Unity.Netcode;

namespace Infinity.Runtime.Networking
{
    public static class InfinityNetworkManager
    {
        public static IEnumerable<NetworkClient> ConnectedClients => NetworkManager.Singleton.ConnectedClients.Values;

        public static NetworkClient LocalClient;
        public static InfinityPlayerRoot LocalPlayerRoot;

        public static void Initialize()
        {
            NetworkManager.Singleton.OnServerStarted += OnServerStarted;
            NetworkManager.Singleton.OnServerStopped += OnServerStopped;

            NetworkManager.Singleton.OnClientStarted += OnClientStarted;
            NetworkManager.Singleton.OnClientStopped += OnClientStopped;

            NetworkManager.Singleton.OnConnectionEvent += OnConnectionEvent;
            NetworkManager.Singleton.OnPreShutdown += OnPreShutdown;

            NetworkManager.Singleton.OnTransportFailure += OnTransportFailure;
        }

        private static void Cleanup()
        {
            NetworkManager.Singleton.OnServerStarted -= OnServerStarted;
            NetworkManager.Singleton.OnServerStopped -= OnServerStopped;

            NetworkManager.Singleton.OnClientStarted -= OnClientStarted;
            NetworkManager.Singleton.OnClientStopped -= OnClientStopped;

            NetworkManager.Singleton.OnConnectionEvent -= OnConnectionEvent;
            NetworkManager.Singleton.OnPreShutdown -= OnPreShutdown;

            NetworkManager.Singleton.OnTransportFailure -= OnTransportFailure;
        }

        #region Callbacks

        private static void OnTransportFailure()
        {
            InfinityLog.Error(typeof(InfinityNetworkManager), $"Transport Failure");
        }

        private static void OnPreShutdown()
        {
            InfinityLog.Info(typeof(InfinityNetworkManager), $"Pre shutdown");
        }

        private static void OnConnectionEvent(NetworkManager arg1, ConnectionEventData arg2)
        {
            InfinityLog.Info(typeof(InfinityNetworkManager), $"Connection Event: {arg2.EventType} - {arg2.ClientId}");
        }

        private static void OnClientStopped(bool obj)
        {
            InfinityLog.Info(typeof(InfinityNetworkManager), $"Client Stopped");
        }

        private static void OnClientStarted()
        {
            InfinityLog.Info(typeof(InfinityNetworkManager), $"Client Started");
            LocalClient = NetworkManager.Singleton.LocalClient;
            LocalPlayerRoot = LocalClient.PlayerObject.GetComponent<InfinityPlayerRoot>();
        }

        private static void OnServerStopped(bool obj)
        {
            Cleanup();
            InfinityLog.Info(typeof(InfinityNetworkManager), $"Server Stopped");
        }

        private static void OnServerStarted()
        {
            InfinityLog.Info(typeof(InfinityNetworkManager), $"Server Started");
        }

        #endregion
    }
}