using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Infinity.Runtime.Core.Events;
using Infinity.Runtime.Core.Logging;
using Infinity.Runtime.Core.Session;
using Infinity.Runtime.Core.Settings;
using Infinity.Runtime.Utils;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEditor;
using UnityEngine;

namespace Infinity.Runtime.Core
{
    public static class InfinitySDK
    {
        private static InfinityEnums.Core.SessionState sessionState;

        public static InfinityEnums.Core.SessionState SessionState
        {
            get => sessionState;
            private set
            {
                InfinityEventBus.Publish(new InfinitySessionStateChangeEvent(sessionState, value));
                sessionState = value;
            }
        }

        public static InfinitySDKProperties Properties { get; private set; }

        public static void SetSessionState(InfinityEnums.Core.SessionState state)
        {
            switch (sessionState)
            {
                case InfinityEnums.Core.SessionState.Launching:
                case InfinityEnums.Core.SessionState.Initialized:
                case InfinityEnums.Core.SessionState.NetworkStarted:
                case InfinityEnums.Core.SessionState.Exiting:
                    InfinityLog.Warning(typeof(InfinitySDK),
                        $"Cannot set session state to {state}. This state is internally managed");
                    return;
                case InfinityEnums.Core.SessionState.Lobby:
                case InfinityEnums.Core.SessionState.InGame:
                case InfinityEnums.Core.SessionState.Ended:
                case InfinityEnums.Core.SessionState.Ending:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            InfinityLog.Info(typeof(InfinitySDK), $"Session state is set to {state}.");
            SessionState = sessionState;
        }

        public static void Initialize()
        {
            SessionState = InfinityEnums.Core.SessionState.Launching;

            InfinityLog.Info(typeof(InfinitySDK), "Initializing Infinity SDK");

            InitializePropertiesScriptable();

            InfinitySettings.Initialize();
            
            if (Properties.logSettingAfterInit) InfinitySettings.DisplaySettings();

            SessionState = InfinityEnums.Core.SessionState.Initialized;
        }

        public static void StartNetworkSession()
        {
            // Determine network type
            switch (InfinitySettings.Base.InstanceType.Value)
            {
                case InfinityEnums.Core.InstanceType.Client:
                    StartClientSession();
                    break;
                case InfinityEnums.Core.InstanceType.Server:
                    StartServerSession();
                    break;
                case InfinityEnums.Core.InstanceType.Host:
                    StartHostSession();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            SessionState = InfinityEnums.Core.SessionState.NetworkStarted;
        }

        private static void StartServerSession()
        {
            var localIp = GetLocalIPAddress();
            var serverPort = InfinitySettings.Base.ServerPort.Value;

            InfinityLog.Info(typeof(InfinitySDK), $"Starting server session on {localIp}:{serverPort}");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(localIp, serverPort);
            NetworkManager.Singleton.StartServer();
        }

        private static void StartClientSession()
        {
            var serverIp = InfinitySettings.Base.ServerIp.Value;
            var serverPort = InfinitySettings.Base.ServerPort.Value;

            InfinityLog.Info(typeof(InfinitySDK), $"Starting client session on {serverIp}:{serverPort}");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(serverIp, serverPort);
            NetworkManager.Singleton.StartClient();
        }

        private static void StartHostSession()
        {
            var localIp = GetLocalIPAddress();
            var serverPort = InfinitySettings.Base.ServerPort.Value;

            InfinityLog.Info(typeof(InfinitySDK), $"Starting host session on {localIp}:{serverPort}");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(localIp, serverPort);
            NetworkManager.Singleton.StartHost();
        }

        public static void StopSession()
        {
            SessionState = InfinityEnums.Core.SessionState.Exiting;

            InfinityLog.Info(typeof(InfinitySDK), $"Stopping session");

            StaticCoroutine.Start(StopProcess());
            return;

            IEnumerator StopProcess()
            {
                NetworkManager.Singleton.Shutdown(true);
                yield return new WaitUntil(() => !NetworkManager.Singleton.ShutdownInProgress);

                InfinityLog.Info(typeof(InfinitySDK), $"Network manager is shut down, the application will now exit.");

#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }

        private static string GetLocalIPAddress()
        {
            var entry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in entry.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        private static void InitializePropertiesScriptable()
        {
            var props = Resources.Load<InfinitySDKProperties>("InfinitySDK");
            if (props == null)
            {
                InfinityLog.Warning(typeof(InfinitySDK),
                    $"Cannot load Infinity SDK properties, resource not found, creating it..");
                props = ScriptableObject.CreateInstance<InfinitySDKProperties>();
#if UNITY_EDITOR
                if (!Directory.Exists("Assets/Resources"))
                    Directory.CreateDirectory("Assets/Resources");

                AssetDatabase.CreateAsset(props, "Assets/Resources/InfinitySDK.asset");
                AssetDatabase.SaveAssets();
#endif
            }

            InfinityLog.Info(typeof(InfinitySDK), $"Infinity SDK Properties loaded");
            Properties = props;
        }
    }
}