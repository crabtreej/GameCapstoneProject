using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject player;
    AIStateMachine machine;

    /**
     * Start should be used to create any states that you want the StateMachine
     * to run. The states will control the AIs behavior, then ideally all
     * you have to do is update the machine forever.
     */
    void Start()
    {
        //Random.InitState((int)(Time.realtimeSinceStartup * 1000));
        machine = new AIStateMachine(player, gameObject);
        IState wanderState = new AIWanderState(machine);
        IState chaseState = new AIChaseState(machine);
        machine.SetInitialState(wanderState);
        machine.AddTransition(wanderState, chaseState);
		machine.AddTransition(chaseState, wanderState);
    }

    // Update is called once per frame
    void Update()
    {
        machine.Update();
    }
}
