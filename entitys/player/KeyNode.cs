using Godot;
using System;
using System.Collections.Generic;

public partial class KeyNode : Node
{
	private List<string> keys = new();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void AddKey(string key){
		keys.Add(key);
	}

	public bool HasKey(string key){
		return keys.Contains(key);
	}

	

}
