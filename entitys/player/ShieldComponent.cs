using Godot;
using System;

[GlobalClass]
public partial class ShieldComponent : Node {
	[Export] private float ShieldHealth = 5;
	[Export] private float ParryTimer = 0.5f;

	[Export] public float ShieldRechargeTimer = 3.0f;
	private float _currentShieldHealth;
	private float _currentParryTimer;

	public float _shieldRechargeTimer;

	public bool IsBlocking { get; private set; }
	public bool IsParrying { get; private set; }

    public override void _Ready()
    {
        _currentShieldHealth = ShieldHealth;
    }

    public void StartBlock() {
		if (IsParrying || _currentShieldHealth <= 0) return;
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
		if (IsBlocking) _currentShieldHealth -= 1;
	}

	public bool CanBlock(){
		return _currentShieldHealth > 0 && IsBlocking;
	}

    public override void _Process(double delta) {
		if(_currentShieldHealth <= 0 && _shieldRechargeTimer <= 0){
			_shieldRechargeTimer = ShieldRechargeTimer;
		}
		
		if(_shieldRechargeTimer > 0){
			_shieldRechargeTimer -= (float)delta;
			if(_shieldRechargeTimer <= 0){_currentShieldHealth = ShieldHealth;}
		}
		
		if (!IsParrying) return;

		_currentParryTimer -= (float)delta;
		if (_currentParryTimer <= 0) {
			IsParrying = false;
		}
    }
}
