using Godot;

[GlobalClass]
public partial class EnemyDeathState : State {
	public override void Enter() {
		QueueFree();
	}
}
