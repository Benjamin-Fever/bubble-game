using Godot;
using System;

public partial class Door : Area2D
{

	[Export]
	public string DestinationLevel { get; set; }
	
	[Export]
	public Vector2 DestinationVector { get; set; }
	[Export]
	
	private Node2D spawn;

	

	public override void _Ready()
	{
		spawn = GetNode<Node2D>("Spawn");
		BodyEntered += OnBodyEntered;
		
	}

	public virtual void OnBodyEntered(Node2D body)
	{
		GD.Print("Door");
		if (body is CharacterBody2D player)
		{
			if ( player.IsInGroup("player"))
			
				{
				player.GlobalPosition = DestinationVector;
				}
				SceneManager.ChangeScene("res://scene/Levels/"+DestinationLevel + ".tscn");
				
		}
	}

}
