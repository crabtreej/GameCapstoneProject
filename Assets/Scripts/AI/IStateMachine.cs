public interface IStateMachine 
{
    // Checks if it should transition to the next state, then updates whatever
    // the current state is (i.e. actually performs behavior)
    void Update();

    // Map oldState to newState, so when oldState.shouldTransition() == true, the
    // currentState will become newState
    void AddTransition(IState oldState, IState newState);
}
