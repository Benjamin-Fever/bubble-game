using Godot;

public partial class StunState : State {
	[Export] public float StunTime = 1.0f;
	private float _currentStunTime;

	public override void Enter() {
		_currentStunTime = StunTime;
	}

    public override void Update(double delta) {
		_currentStunTime -= (float)delta;
		if (_currentStunTime <= 0) {
			ChangeState("IdleState");
			ChangeState("ChasePlayerState");
		}
    }
}
