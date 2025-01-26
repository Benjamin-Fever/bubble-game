using Godot;
using System;

public partial class ShieldProgress : TextureProgressBar
{
	[Export] ShieldComponent shield;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Value = shield._shieldRechargeTimer;	
		MaxValue = shield.ShieldRechargeTimer;
	}
}
