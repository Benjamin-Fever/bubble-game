using Godot;
using System;

public partial class LockedDoor : Door
{
	[Export]
	public bool IsLocked { get; set; } = true;
	
	[Export] 
	public string RequiredKey { get; set; }
	private StaticBody2D staticBody;
	[Export] private Sprite2D closedSprite;
	 public override void _Ready()
	{   
		staticBody = GetNode<StaticBody2D>("StaticBody2D");
		BodyEntered += OnBodyEntered;
	}

	public override void _Process(double delta)
	{
		
			ToggleCollision(IsLocked);
		
	}

	public override void OnBodyEntered(Node2D body)
	{
		if (body is CharacterBody2D player && player.IsInGroup("player")) 
		{
		KeyNode kn = player.GetNode<KeyNode>("KeyNode");
		//kn.printKeys();
		
		if (IsLocked && !kn.HasKey(RequiredKey))
		{
			return;
		}
		
		//make sprite invisible

		
		ToggleCollision(false);
		base.OnBodyEntered(body);
		}
	}
	  private void ToggleCollision(bool locked)
	{
		staticBody.GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", !locked);
		IsLocked = locked;
		closedSprite.Visible = locked;
	}
}
