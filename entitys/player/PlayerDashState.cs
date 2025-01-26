using Godot;
using System;

public partial class PlayerDashState : State {
    [Export] private float _dashDistance = 200f;
    [Export] private float _dashTime = 0.5f;
    [Export] private SpeedComponent _speedComponent;
    [Export] private ShieldComponent shieldComponent;

    private Vector2 _startingDashPosition;

	public override bool CanChangeState() {
        return _stateMachine.CurrentState is IdleState || _stateMachine.CurrentState is PlayerMovementState;
    }

    public override void Enter() {
        _startingDashPosition = _speedComponent.Body.GlobalPosition;
        _prevFramePosition = _speedComponent.Body.GlobalPosition;
        _speedComponent.Velocity = _speedComponent.Direction * _dashDistance / _dashTime;
    }

    private Vector2 _prevFramePosition;
    public override void Update(double delta) {
        
        if (_startingDashPosition.DistanceTo(_speedComponent.Body.GlobalPosition) >= _dashDistance || _prevFramePosition.DistanceTo(_speedComponent.Body.GlobalPosition) < 1) {
            _speedComponent.Velocity = Vector2.Zero;
            GD.Print("Dash ended");
            ChangeState("IdleState");
        }
        _prevFramePosition = _speedComponent.Body.GlobalPosition;
        GD.Print(_prevFramePosition);
    }

    public override void Exit()
    {
        if(shieldComponent.IsBlocking){
            shieldComponent._currentShieldHealth = 0;
        }
    }
}

