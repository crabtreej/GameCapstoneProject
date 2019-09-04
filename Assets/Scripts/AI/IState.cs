public interface IState 
{
    // Performs state work
    void Update();
    // Cleans up any state-specific information (if necessary)
    void ExitState();
    // Tells the state machine if it should go to whatever the next state is
    bool ShouldTransition();
}
