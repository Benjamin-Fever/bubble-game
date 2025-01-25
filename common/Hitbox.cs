using Godot;
using System;

[GlobalClass]
public partial class Hitbox : Area2D {
	[Signal] public delegate void HitboxCollisionEventHandler();
    public override void _Ready() {
        AreaEntered += OnAreaEntered;
    }

	private void OnAreaEntered(Area2D area) {
		if (area is Hurtbox hurtbox) {
			hurtbox.OnHit(this);
			EmitSignal(SignalName.HitboxCollision);
		}
	}
}
