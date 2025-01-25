using Godot;

public partial class ShieldArea : Area2D {
	[Export] private float _knockbackForce = 32;
	[Export] private float _knockBackRadius = 24;
	[Export] private float _dashKnockbackRadius = 32;
	[Export] private float _stompKnockbackRadius = 64;
	[Export] private float _stunTime = 0.4f;
	[Export] private ShieldComponent _shieldComponent;
	

	public override void _Process(double delta) {
		CollisionShape2D collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
		MeshInstance2D meshInstance2D = GetNode<MeshInstance2D>("MeshInstance2D");
		State currentState = GetParent().GetNode<StateMachine>("StateMachine").CurrentState;

		collisionShape2D.Disabled = !_shieldComponent.IsBlocking;
		meshInstance2D.Visible = _shieldComponent.IsBlocking;
		
		if (currentState is PlayerDashState) {
			((CircleShape2D)collisionShape2D.Shape).Radius = _dashKnockbackRadius;
			((SphereMesh)meshInstance2D.Mesh).Radius = _dashKnockbackRadius;
			((SphereMesh)meshInstance2D.Mesh).Height = _dashKnockbackRadius * 2;

		}
		else if (currentState is PlayerStompState) {
			((CircleShape2D)collisionShape2D.Shape).Radius = _stompKnockbackRadius;
			((SphereMesh)meshInstance2D.Mesh).Radius = _stompKnockbackRadius;
			((SphereMesh)meshInstance2D.Mesh).Height = _stompKnockbackRadius * 2;
		}
		else {
			((CircleShape2D)collisionShape2D.Shape).Radius = _knockBackRadius;
			((SphereMesh)meshInstance2D.Mesh).Radius = _knockBackRadius;
			((SphereMesh)meshInstance2D.Mesh).Height = _knockBackRadius * 2;
		}

		if (!_shieldComponent.IsBlocking) return;
		foreach (Area2D area in GetOverlappingAreas()) {
			if (area is not Hurtbox) continue;
			SpeedComponent speedComponent = area.GetParent().GetNode<SpeedComponent>("SpeedComponent");
			StateMachine stateMachine = area.GetParent().GetNode<StateMachine>("StateMachine");
			StunState stunState = stateMachine.GetNode<StunState>("StunState");

			Vector2 direction = -area.GlobalPosition.DirectionTo(GlobalPosition);
			speedComponent.Velocity = direction * _knockbackForce * (float)delta;
			stunState.StunTime = _stunTime;
			stateMachine.ChangeState("StunState");

		}
	}
}
