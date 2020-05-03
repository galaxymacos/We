using UnityEngine;

public class CharacterStandingState : CharacterGroundedState
{
    private bool jump;
    private bool crouch;
    private float speed;

    public CharacterStandingState(StateMachine stateMachine, Character character) : base(stateMachine, character)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        character.ResetMoveDirection();
        speed = character.walkSpeed;
        jump = false;
        crouch = false;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        
        jump = RewiredInput.instance.AButtonPressing;
        crouch = RewiredInput.instance.BButtonPressing;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (crouch)
        {
            stateMachine.ChangeState(character.crouching);
        }
        else if (jump)
        {
            stateMachine.ChangeState(character.jumping);
        }
    }

    
}