using Godot;
using System;

[GlobalClass]
public partial class StateMachine : Node {
	[Export] private State initialState;
	public State CurrentState {get; private set;}

	public void ChangeState(string stateName) {
		State newState = GetNodeOrNull<State>(stateName);
		if (newState == null) {
			GD.PrintErr("State not found: " + stateName);
			return;
		}
		ChangeState(newState);
	}

	public void ChangeState(State newState) {
		if (CurrentState == newState) return;
		if (!newState.CanChangeState()) return;
		
		CurrentState?.Exit();
		CurrentState = newState;
		CurrentState.Enter();
	}

	public override void _Ready() {
		CurrentState = initialState;
		CurrentState.Enter();
	}

	public override void _Process(double delta) {
		CurrentState.Update(delta);
		GD.Print(CurrentState.Name);
	}
}
