using Godot;
using System;

[GlobalClass]
public partial class HealthComponent : Node {
	[Signal] public delegate void HealthChangedEventHandler(int currentHealth);
	[Signal] public delegate void HealthDepletedEventHandler();


	[Export] public int MaxHealth { get; set; } = 100;
	private int _currentHealth;

	public override void _Ready() {
		_currentHealth = MaxHealth;
	}

	public void AddHealth(int amount) {
		_currentHealth = Mathf.Min(_currentHealth + amount, MaxHealth);
		EmitSignal(SignalName.HealthChanged, _currentHealth);
	}

	public void RemoveHealth(int amount) {
		GD.Print("Removing health");
		_currentHealth = Mathf.Max(_currentHealth - amount, 0);
		EmitSignal(SignalName.HealthChanged, _currentHealth);
		if (_currentHealth <= 0) {
			GD.Print("Health depleted");
			EmitSignal(SignalName.HealthDepleted);
		}
	}
}
