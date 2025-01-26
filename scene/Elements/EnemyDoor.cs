using Godot;
using System;
using System.Collections.Generic;

public partial class EnemyDoor : Door
{
	[Export] public bool IsLocked { get; set; } = true;

	private StaticBody2D staticBody;

	 public override void _Ready()
	{   
		staticBody = GetNode<StaticBody2D>("StaticBody2D");
		BodyEntered += OnBodyEntered;
	}

	public override void OnBodyEntered(Node2D body)
	{
		GD.Print("HERE");
		if (body is CharacterBody2D player && player.IsInGroup("player")) 
		{
			bool enemiesExist = false;
			foreach (Node child in SceneManager.currentScene.GetChildren()){
				if (child.IsInGroup("enemy")){
					enemiesExist = true;
				}
			}
			if (IsLocked && enemiesExist){
				GD.Print("Locked Door - Player have not killed all enemies");
				return;
			}
		
			ToggleCollision(false);
			GD.Print("Unlocked Door");
			base.OnBodyEntered(body);
		}
	}

	private void ToggleCollision(bool locked){
		staticBody.GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", !locked);
		IsLocked = locked;
	}
}
