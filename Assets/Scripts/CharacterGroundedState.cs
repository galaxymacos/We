using UnityEngine;

public class CharacterGroundedState : State
{
    public Vector2 moveVector;
    protected float speed;
    
    public Character character;
    public CharacterGroundedState(StateMachine stateMachine, Character character) : base(stateMachine)
    {
        this.character = character;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        moveVector = RewiredInput.instance.LeftJoystickInput;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        character.Move(moveVector);
    }

    public override void Exit()
    {
        base.Exit();
        character.ResetMoveParams();
    }
}