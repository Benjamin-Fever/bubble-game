using Godot;
using System;

public partial class KnockbackState : State {
	[Export] SpeedComponent speedComponent;
	[Export] CharacterBody2D body;
	public float knockbackTime = 0.1f;
	public float knockbackDistance = 32;
	public Vector2 knockbackDirection = Vector2.Zero;

	private Vector2 startPos = Vector2.Zero;

    public override void Enter() {
        startPos = body.GlobalPosition;
		speedComponent.Velocity = knockbackDirection * knockbackDistance / knockbackTime;
    }

    public override void Update(double delta) {
        if (body.GlobalPosition.DistanceTo(startPos) >= knockbackDistance) {
			speedComponent.Velocity = Vector2.Zero;
			ChangeState("StunState");
		}
    }

}
