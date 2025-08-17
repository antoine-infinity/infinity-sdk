using Infinity.Runtime.Core;
using Infinity.Runtime.Core.Events;
using Infinity.Runtime.Core.Logging;
using Infinity.Runtime.Core.Session;
using Infinity.Runtime.Core.Settings;
using UnityEngine;

public class TestEventBus : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        InfinityEventBus.Subscribe<InfinitySessionStateChangeEvent>(data =>
        {
            InfinityLog.Info(this, $"{data.OldState} -> {data.NewState}");
        });
    }
}