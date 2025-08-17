using Unity.Netcode;
using UnityEngine.SceneManagement;

namespace Infinity.Runtime.Managers
{
    public static class InfinitySceneManager
    {
        public static NetworkSceneManager SceneManager => NetworkManager.Singleton.SceneManager;
        
        public static void LoadScene(string scenePath)
        {
            var progress = SceneManager.LoadScene(scenePath, LoadSceneMode.Single);
        }
    }
}