using Godot;
using System;

[GlobalClass]
public partial class ShieldComponent : Node {
	[Export] private float ShieldHealth = 5;
	[Export] private float ParryTimer = 0.5f;
	private float _currentShieldHealth;
	private float _currentParryTimer;

	public bool IsBlocking { get; private set; }
	public bool IsParrying { get; private set; }

	public void StartBlock() {
		if (IsParrying || ShieldHealth <= 0) return;
		IsBlocking = true;
	}

	public void StopBlock() {
		IsBlocking = false;
	}

	public void StartParry() {
		if (IsBlocking) return;
		IsParrying = true;
		_currentParryTimer = ParryTimer;
	}

	public void ShieldAttacked() {
		if (IsBlocking) ShieldHealth -= 1;
	}

	public bool CanBlock(){
		return ShieldHealth > 0 && IsBlocking;
	}

    public override void _Process(double delta) {
		if (!IsParrying) return;

		_currentParryTimer -= (float)delta;
		if (_currentParryTimer <= 0) {
			IsParrying = false;
		}
    }
}
