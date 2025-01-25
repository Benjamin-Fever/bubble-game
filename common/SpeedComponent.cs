using Godot;
using System;

[GlobalClass]
public partial class SpeedComponent : Node {
	[Export] private float _drag = 40f;
	[Export] public CharacterBody2D Body {get; private set;}
	public Vector2 Velocity { get; set; }
	public Vector2 Direction { get; private set; }
	public override void _Process(double delta) {
		Velocity = Velocity.MoveToward(Vector2.Zero, _drag * (float)delta);
		Body.Velocity = Velocity;
		if (Velocity.Length() > 0.1f)  Direction = Velocity.Normalized();
		Body.MoveAndSlide();
	}	
}
