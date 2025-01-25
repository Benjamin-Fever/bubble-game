using Godot;
using System;

[GlobalClass]
public partial class Hurtbox : Area2D {
	[Signal] public delegate void HitReceivedEventHandler();

	public void OnHit(Area2D area) {
		EmitSignal(SignalName.HitReceived);
	}
}
