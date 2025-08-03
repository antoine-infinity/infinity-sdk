using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

namespace Infinity.Runtime.Networking
{
    public class InfinityNetworkManager : MonoBehaviour
    {
        public NetworkManager networkManager;
        public UnityTransport transport;
    }
}