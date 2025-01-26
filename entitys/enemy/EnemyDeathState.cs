using Godot;

public partial class EnemyDeathState : State {
	[Export] CharacterBody2D body;
    public override void Enter() {
        body.QueueFree();
    }
}
