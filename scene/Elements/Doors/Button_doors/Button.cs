using Godot;
using System;

	public partial class Button : Area2D
{
    [Export]
    public NodePath ConnectedDoorPath { get; set; }
    
    private ButtonDoor connectedDoor;
    //private Sprite2D buttonSprite;
    private bool isPressed = false;

    public override void _Ready()
    {
        //buttonSprite = GetNode<Sprite2D>("Sprite2D");
        connectedDoor = GetNode<ButtonDoor>(ConnectedDoorPath);
        BodyEntered += OnBodyEntered;
        
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is CharacterBody2D player && player.IsInGroup("player"))
        {
            isPressed = true;
            //buttonSprite.Modulate = Colors.Gray; 
            connectedDoor.SetDoorState(false); 
        }
    }

   
}

