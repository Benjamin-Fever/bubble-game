using Godot;
using Godot.Collections;
using System;

public partial class ChasePlayerState : State {
    [Export] private float ChaseSpeed = 100;
	[Export] private SpeedComponent speedComponent;

    public override void Update(double delta) {
        Vector2 targetPos = GetTree().Root.GetNode<CharacterBody2D>("Main/Player").GlobalPosition;
        Array<Vector2> path = SceneManager.GetPathToPoint(speedComponent.Body.GlobalPosition, targetPos);
		if (path.Count == 0) {
			ChangeState("IdleState");
			return;
		}

		Vector2 direction = speedComponent.Body.GlobalPosition.DirectionTo(path[0]);
		if (speedComponent.Body.GlobalPosition.DistanceTo(path[0]) < 5) {
			speedComponent.Body.GlobalPosition = path[0];
			speedComponent.Velocity = Vector2.Zero;
			path.RemoveAt(0);
			return;
		}
		speedComponent.Velocity = direction.Normalized() * ChaseSpeed * (float)delta;
    }

    public override bool CanChangeState() {
        return _stateMachine.CurrentState is not StunState;
    }
}
