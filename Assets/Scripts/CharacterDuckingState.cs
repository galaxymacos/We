public class CharacterDuckingState : CharacterGroundedState
{
    private bool belowCeiling;
    private bool crouchHeld;
    
    public CharacterDuckingState(StateMachine stateMachine, PlayerCharacter character) : base(stateMachine, character)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
}