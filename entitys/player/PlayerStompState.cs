using Godot;
using System;

public partial class PlayerStompState : State {
	public override bool CanChangeState() {
        return _stateMachine.CurrentState is IdleState || _stateMachine.CurrentState is PlayerMovementState;
    }

    public override void Enter() {
        ChangeState("IdleState");
    }
}

