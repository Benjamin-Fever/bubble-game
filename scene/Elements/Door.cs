using Godot;
using System;

public partial class Door : Area2D
{
	private const float DoorCoolDown = 1f;
	[Export] public string DestinationLevel { get; set; }
	[Export] public Vector2 DestinationVector { get; set; }
	[Export] private Node2D spawn;

	private static float currentDoorCoolDown = 0f;

	public override void _Ready()
	{
		spawn = GetNode<Node2D>("Spawn");
		BodyEntered += OnBodyEntered;
		
	}

	public override void _Process(double delta) {
		if (currentDoorCoolDown > 0f) {
			currentDoorCoolDown -= (float)delta;
		}
	}

	public virtual void OnBodyEntered(Node2D body) {
		GD.Print(currentDoorCoolDown);
		if (body is CharacterBody2D player && currentDoorCoolDown <= 0f) {
			if ( player.IsInGroup("player")) {
				player.GlobalPosition = DestinationVector;
			}
			SceneManager.ChangeScene("res://scene/Levels/"+DestinationLevel + ".tscn");
			GD.Print(GetViewport().GetCamera2D());
			GetViewport().GetCamera2D().GlobalPosition = DestinationVector;
			currentDoorCoolDown = DoorCoolDown;
				
		}
	}

}
