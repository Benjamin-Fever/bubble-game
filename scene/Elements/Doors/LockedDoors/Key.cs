using Godot;
using System;

public partial class Key : Area2D
{
	[Export]
	public string keyName { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnBodyEntered(Node body)
	{
		if (body is CharacterBody2D player && player.IsInGroup("player"))
		{
			GD.Print("Key Collected");
			player.GetNode<KeyNode>("KeyNode").AddKey(keyName);
			QueueFree();
		}
	}
}
