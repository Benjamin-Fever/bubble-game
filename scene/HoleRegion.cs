using Godot;

public partial class HoleRegion : Area2D {
	public override void _Ready() {
		BodyEntered += (Node2D body)=> {
			body.QueueFree();
		};
	}
}
