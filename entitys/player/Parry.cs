using Godot;

public partial class Parry : Area2D {
	[Export] public ShieldComponent ShieldComponent { get; set; }

	public override void _Ready() {
		AreaEntered += OnAreaEntered;
	}

	public override void _Process(double delta) {
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = !ShieldComponent.IsParrying;
		GetNode<MeshInstance2D>("MeshInstance2D").Visible = ShieldComponent.IsParrying;
		if (!ShieldComponent.IsParrying) return;
		foreach (Area2D area in GetOverlappingAreas()) {
			if (area.GetParent() is not Projectile projectile) continue;
			projectile._direction = GlobalPosition.DirectionTo(projectile.shooter.GlobalPosition);
		}
	}

	private void OnAreaEntered(Area2D area) {
		if (!ShieldComponent.IsParrying) return;
		if (area.GetParent() is not Projectile projectile) return;
		projectile._direction = GlobalPosition.DirectionTo(projectile.shooter.GlobalPosition);
	}
}
