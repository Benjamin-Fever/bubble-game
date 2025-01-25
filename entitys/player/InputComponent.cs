using Godot;
using System;

public partial class InputComponent : Node {
	[Signal] public delegate void ShieldUpEventHandler();
	[Signal] public delegate void ShieldDownEventHandler();

	[Signal] public delegate void ParryEventHandler();
	[Signal] public delegate void DashEventHandler();
	[Signal] public delegate void StompEventHandler();
	
	[Signal] public delegate void MovementEventHandler();

	public Vector2 GetMovementDirection() {
		return Input.GetVector("move_left", "move_right", "move_up", "move_down").Normalized();
	}

    public override void _Input(InputEvent @event) {
        if (Input.IsActionJustPressed("block")) EmitSignal(SignalName.ShieldUp);
		if (Input.IsActionJustReleased("block")) EmitSignal(SignalName.ShieldDown);

		if (Input.IsActionJustPressed("parry")) EmitSignal(SignalName.Parry);
		if (Input.IsActionJustPressed("dash")) EmitSignal(SignalName.Dash);
		if (Input.IsActionJustPressed("stomp")) EmitSignal(SignalName.Stomp);
	

    }

    public override void _Process(double delta) {
		if (GetMovementDirection() != Vector2.Zero) EmitSignal(SignalName.Movement);
    }
}
