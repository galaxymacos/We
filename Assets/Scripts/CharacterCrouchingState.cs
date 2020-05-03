using UnityEngine;

public class CharacterCrouchingState : CharacterGroundedState
{
    private bool belowCeiling;
    private bool isCrouching;
    
    public CharacterCrouchingState(StateMachine stateMachine, Character character) : base(stateMachine, character)
    {
    }

    public override void Enter()
    {
        base.Enter();
        character.SetAnimationBool(character.CrouchParam,true);
        speed = character.crouchSpeed;
        character.ColliderSize = character.CrouchColliderHeight;
        belowCeiling = false;
        isCrouching = true;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        isCrouching = !RewiredInput.instance.BButtonPressing;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isCrouching || belowCeiling)
        {
            character.SetAnimationBool(character.CrouchParam, false);
            stateMachine.ChangeState(character.standing);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        belowCeiling =
            character.CheckCollisionOverlap(character.transform.position + Vector3.up * character.NormalColliderHeight);
    }
}