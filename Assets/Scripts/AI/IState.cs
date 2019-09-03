using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    void Update();
    void ExitState();
    bool ShouldTransition();
}
