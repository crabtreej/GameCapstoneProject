using Assets.Scripts.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter
{
    // Singleton Event Center
    private static EventCenter _instance;
    public static EventCenter Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EventCenter();
            }
            return _instance;
        }
    }

    //Declare new references to the events here, which will only run once
    //To ensure that everyone is sending to the same event
    private EventCenter()
    {
        ObjectMadeNoise = new ObjectMadeNoiseEvent<GameObject>();
    }

    public ObjectMadeNoiseEvent<GameObject> ObjectMadeNoise { get; private set; }
}
