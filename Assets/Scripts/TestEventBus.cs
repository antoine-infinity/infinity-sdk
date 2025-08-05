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
        InfinityEventBus.Subscribe<TestEvent>(data => { Debug.Log("TestEvent => " + data.Data); });
        InfinityEventBus.Subscribe<InfinitySessionStateChangeEvent>(data =>
        {
            InfinityLog.Info(this, $"{data.OldState} -> {data.NewState}");
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InfinityEventBus.Publish(new TestEvent("Test Data"));
        }
    }
}

public class TestEvent
{
    public string Data;

    public TestEvent(string data)
    {
        Data = data;
    }
}