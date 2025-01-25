using Godot;
using System;

public partial class CameraController : Camera2D {
	[Export] Node2D target;

    public override void _Ready() {
        if (target == null) {
            GD.PrintErr("CameraController: target is null");
            return;
        }
        //GlobalPosition = target.GlobalPosition;
    }

    public override void _Process(double delta) {
        GlobalPosition = GlobalPosition.Lerp(target.GlobalPosition, 0.15f);
        LimitLeft = 0;
        LimitTop = 0;
        LimitRight = SceneManager.currentScene.GetMapBounds().End.X;
        LimitBottom = SceneManager.currentScene.GetMapBounds().End.Y;
    }

}
