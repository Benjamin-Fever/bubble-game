using Godot;
using System;

public partial class PlayerStompState : State {
    [Export] private float _stompTimer = 1;
    [Export] private ShieldComponent shieldComponent; 
    private float currentTimer;

	public override bool CanChangeState() {
        return _stateMachine.CurrentState is IdleState || _stateMachine.CurrentState is PlayerMovementState;
    }

    public override void Enter() {

        currentTimer = _stompTimer;
    }

    public override void Update(double delta) {
        currentTimer -= (float)delta;
        if (currentTimer <= 0) {
            ChangeState("IdleState");
        }
    }

    public override void Exit()
    {
        shieldComponent._currentShieldHealth = 0;
    }
}

