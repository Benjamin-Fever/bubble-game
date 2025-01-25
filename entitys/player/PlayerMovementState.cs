using Godot;
using System;

public partial class PlayerMovementState : State {
	[Export] private float _speed = 200f;
	[Export] private InputComponent _inputComponent;
	[Export] private SpeedComponent _speedComponent;
    public override bool CanChangeState() {
        return _stateMachine.CurrentState is IdleState;
    }

    public override void Update(double delta) {
        _speedComponent.Velocity = _inputComponent.GetMovementDirection() * _speed * (float)delta;
    }
}

