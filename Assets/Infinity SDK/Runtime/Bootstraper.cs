using System.Collections;
using Infinity.Runtime.Core;
using Infinity.Runtime.Core.Logging;
using Infinity.Runtime.Core.Session;
using Infinity.Runtime.Core.Settings;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infinity.Runtime
{
    public class Bootstraper : MonoBehaviour
    {
        private IEnumerator Start()
        {
            // Initialize settings and SDK
            InfinitySDK.Initialize();
            yield return new WaitUntil(() => InfinitySDK.SessionState is InfinityEnums.Core.SessionState.Initialized);

            // Start network session
            InfinitySDK.StartNetworkSession();
            yield return new WaitUntil(() =>
                InfinitySDK.SessionState is InfinityEnums.Core.SessionState.NetworkStarted);

            // Load first scene
            if (NetworkManager.Singleton.IsServer)
            {
                NetworkManager.Singleton.SceneManager.LoadScene(InfinitySettings.Base.FirstScene.Value,
                    LoadSceneMode.Single);
            }
        }
    }
}