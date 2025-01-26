using System;
using Godot;
using Godot.Collections;

public partial class ShieldArea : Area2D {
	[Signal] public delegate void ShieldAttackedEventHandler();
	[Export] private float _knockbackDistance = 32;
	[Export] private float _knockbackRadius = 24;
	[Export] private float _dashKnockbackDistance = 32;
	[Export] private float _dashKnockbackRadius = 32;
	[Export] private float _stompKnockbackDistance = 32;
	[Export] private float _stompKnockbackRadius = 64;
	[Export] private float _knockbackTime = 0.2f;
	[Export] private ShieldComponent _shieldComponent;

    public override void _Ready() {
        AreaEntered += (Area2D area2D) => {
			if (area2D.GetParent() is Projectile){
				EmitSignal(SignalName.ShieldAttacked);
			}
		};
    }


    public override void _Process(double delta) {
		CollisionShape2D collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
		MeshInstance2D meshInstance2D = GetNode<MeshInstance2D>("MeshInstance2D");
		State currentState = GetParent().GetNode<StateMachine>("StateMachine").CurrentState;

		collisionShape2D.Disabled = !_shieldComponent.CanBlock();
		meshInstance2D.Visible = _shieldComponent.CanBlock();

		float knockbackDistance = _knockbackDistance;
		
		if (currentState is PlayerDashState) {
			((CircleShape2D)collisionShape2D.Shape).Radius = _dashKnockbackRadius;
			((SphereMesh)meshInstance2D.Mesh).Radius = _dashKnockbackRadius;
			((SphereMesh)meshInstance2D.Mesh).Height = _dashKnockbackRadius * 2;
			knockbackDistance = _dashKnockbackDistance;

		}
		else if (currentState is PlayerStompState) {
			((CircleShape2D)collisionShape2D.Shape).Radius = _stompKnockbackRadius;
			((SphereMesh)meshInstance2D.Mesh).Radius = _stompKnockbackRadius;
			((SphereMesh)meshInstance2D.Mesh).Height = _stompKnockbackRadius * 2;
			knockbackDistance = _stompKnockbackDistance;
		}
		else {
			((CircleShape2D)collisionShape2D.Shape).Radius = _knockbackRadius;
			((SphereMesh)meshInstance2D.Mesh).Radius = _knockbackRadius;
			((SphereMesh)meshInstance2D.Mesh).Height = _knockbackRadius * 2;
		}

		if (!_shieldComponent.CanBlock()) return;
		foreach (Area2D area in GetOverlappingAreas()) {
			if (area is not Hurtbox) continue;
			StateMachine stateMachine = area.GetParent().GetNode<StateMachine>("StateMachine");
			KnockbackState knockbackState = stateMachine.GetNode<KnockbackState>("KnockbackState");
			Vector2 direction = -area.GlobalPosition.DirectionTo(GlobalPosition);

			knockbackState.knockbackDirection = direction;
			knockbackState.knockbackDistance = knockbackDistance;
			knockbackState.knockbackTime = _knockbackTime;

			stateMachine.ChangeState("KnockbackState");
		}
	}
}
