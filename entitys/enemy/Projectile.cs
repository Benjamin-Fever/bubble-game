using Godot;
using System;

public partial class Projectile : Node2D {
	public float Speed = 200.0f;
	public Vector2 _direction = Vector2.Right;
    public Node2D shooter;
    public bool hasParryed = false;

    public override void _Process(double delta) {
        GlobalPosition += _direction * Speed * (float)delta;
    }

    public void Destroy() {
        QueueFree();
    }
}
