using Godot;
using System;

[GlobalClass]
public partial class BreakableWall : Area2D {
	private Node2D currentBody;
	[Export] private bool _isBroken = false;
    public override void _Ready() {
        BodyEntered += (Node2D body) => {
			currentBody = body;
		};
		BodyExited += (Node2D body) => {
			currentBody = null;
		};
    }

    public override void _Process(double delta) {
		if (_isBroken) {
			foreach (Node2D child in GetChildren()) {
				child.QueueFree();
			}
		}
		if (currentBody == null) return;
		GD.Print(currentBody.Name);
        if (currentBody.GetNode<StateMachine>("StateMachine").CurrentState is PlayerDashState && currentBody.GetNode<ShieldComponent>("ShieldComponent").IsBlocking) {
			_isBroken = true;
		}
    }
}
