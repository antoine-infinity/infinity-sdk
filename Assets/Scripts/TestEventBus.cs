using System;
using Infinity.Runtime.Core.Events;
using UnityEngine;

public class TestEventBus : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InfinityEventBus.Subscribe<TestEvent>(data => { Debug.Log("TestEvent => " + data.Data); });
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