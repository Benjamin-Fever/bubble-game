using Godot;

public partial class ProjectileHitbox : Hitbox {
    public override void _Ready() {
        AreaEntered += OnAreaEntered;
    }

	private void OnAreaEntered(Area2D area) {
		if (area is Hurtbox hurtbox) {
			hurtbox.OnHit(this);
			EmitSignal(SignalName.HitboxCollision);
			return;
		}
		else if (area is ShieldArea) {
			EmitSignal(SignalName.HitboxCollision);
			return;

		}

	}
}
