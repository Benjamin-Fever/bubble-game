using Godot;
using System;

[GlobalClass]
public partial class HealthComponent : Node {
	[Signal] public delegate void HealthChangedEventHandler(int currentHealth);
	[Signal] public delegate void HealthDepletedEventHandler();


	[Export] public int MaxHealth { get; set; } = 100;
	public int CurrentHealth {get; private set;}

	public override void _Ready() {
		CurrentHealth = MaxHealth;
	}

	public void AddHealth(int amount) {
		CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
		EmitSignal(SignalName.HealthChanged, CurrentHealth);
	}

	public void RemoveHealth(int amount) {
		CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
		EmitSignal(SignalName.HealthChanged, CurrentHealth);
		if (CurrentHealth <= 0) {
			EmitSignal(SignalName.HealthDepleted);
		}
	}
}
