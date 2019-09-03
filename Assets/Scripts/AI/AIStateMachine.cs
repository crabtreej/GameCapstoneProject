using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * StateMachine class for mananging AI. Sets the
 * parentMachine of every state as itself so the
 * states can access game information.
 */
public class AIStateMachine : IStateMachine
{
    public GameObject playerObj { get; private set; }
    public GameObject aiObj { get; private set; }
    private Dictionary<IState, IState> transitionMap;
    private IState currentState;
    
    /**
     * Accepts game world information, and NavMesh 
     * is accessed statically (don't have to pass it in). 
     */
    public AIStateMachine(GameObject player, GameObject ai)
    {
        playerObj = player;
        aiObj = ai;
        transitionMap = new Dictionary<IState, IState>();
    }


     /** Accepts the state to start in. Must provide a transition
     * out of that state (it can be a self-loop) in AddTransition, 
     * or nothing is going to work. 
     */
    public void SetInitialState(IState state)
    {
        currentState = state;
    }

    /**
     * Adds a transition from oldState to newState, so when oldState
     * should transition, it goes to newState. 
     * You NEED to add a transition from initialState in the constructor to
     * a new state, or nothing will ever change.
     * Transitions are one-to-one/non-conditional, or many-to-one, for now.
     * Also sets parent machine as this.
     */
    public void AddTransition(IState oldState, IState newState)
    {
        transitionMap.Add(oldState, newState);
    }

    /**
     * Removes this state and the state it was supposed to 
     * transition to. This only needs the keyed state because
     * the map is currently one-to-one or many-to-one for state transitions.
     * Sets parent machine to null.
     */
    public void RemoveTransition(IState state)
    {
        transitionMap.Remove(state);
    }
    /**
     * Checks if the state thinks it should transition, then goes
     * to the next state that was mapped with AddTransition.
     * Next, it calls update on the state, performing the AIs logic.
     */
    public void Update()
    {
        if(currentState.ShouldTransition())
        {
            // Not checking if it's in the map, because we should throw an error
            // if it's not.
            currentState = transitionMap[currentState];
        }
        currentState.Update();
    }
}
