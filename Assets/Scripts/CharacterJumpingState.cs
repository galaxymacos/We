using UnityEngine;

public class CharacterJumpingState : State
{
    private bool grounded;
    private int jumpParam = Animator.StringToHash("Jump");
    private int landParam = Animator.StringToHash("Land");

    private Character character;
    public CharacterJumpingState(StateMachine stateMachine, Character character) : base(stateMachine)
    {
        this.character = character;
    }

    private void Jump()
    {
        character.transform.Translate(Vector3.up * (character.CollisionOverlapRadius+0.1f));
        character.ApplyImpulse(Vector3.up * character.jumpForce);
        character.TriggerAnimation(jumpParam);
    }

    public override void Enter()
    {
        base.Enter();
        grounded = false;
        Jump();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (grounded)
        {
            character.TriggerAnimation(landParam);
            stateMachine.ChangeState(character.standing);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        grounded = character.CheckCollisionOverlap(character.groundDetector.position);
    }
}