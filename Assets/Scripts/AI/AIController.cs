using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject player;
    AIStateMachine machine;

    // Start is called before the first frame update
    void Start()
    {
        //Random.InitState((int)(Time.realtimeSinceStartup * 1000));
        machine = new AIStateMachine(player, gameObject);
        IState wanderState = new AIWanderState(machine);
        machine.SetInitialState(wanderState);
        machine.AddTransition(wanderState, wanderState);
    }

    // Update is called once per frame
    void Update()
    {
        machine.Update();
    }
}
