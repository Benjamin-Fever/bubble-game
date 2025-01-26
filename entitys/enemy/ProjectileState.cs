using Godot;

public partial class ProjectileState : State {
	[Export] public float ProjectileSpeed = 100;
	[Export] private float TimeBetweenShots = 1.0f;
	[Export] private PackedScene ProjectileScene;
	[Export] private CharacterBody2D body;
	private float timer;

	public override void Enter() {
		timer = TimeBetweenShots;
		CallDeferred(MethodName.ShootProjectile);
	}

	public override void Update(double delta) {
		timer -= (float)delta;
		if (timer <= 0) {
			CallDeferred(MethodName.ShootProjectile);
			timer = TimeBetweenShots;
		}
	}

	private void ShootProjectile() {
		Projectile projectile = ProjectileScene.Instantiate<Projectile>();
		body.AddChild(projectile);
		projectile._direction = body.GlobalPosition.DirectionTo(GetTree().Root.GetNode<CharacterBody2D>("Main/Player").GlobalPosition);
		projectile.Speed = ProjectileSpeed;
		projectile.GlobalPosition = body.GlobalPosition + (projectile._direction * 32);
		projectile.shooter = body;
	}
}
