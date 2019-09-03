using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachine 
{
    void Update();
    void AddTransition(IState oldState, IState newState);
}
