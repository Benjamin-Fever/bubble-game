using Godot;
using System;

public partial class CameraController : Camera2D {
	[Export] private Node2D target;
    [Export] private float cameraLerp = 0.01f;

    public override void _Ready() {
        if (target == null) {
            GD.PrintErr("CameraController: target is null");
            return;
        }
        //GlobalPosition = target.GlobalPosition;
    }

    public override void _Process(double delta) {
        GlobalPosition = GlobalPosition.Lerp(target.GlobalPosition, cameraLerp);
        LimitLeft = 0;
        LimitTop = 0;
        LimitRight = SceneManager.currentScene.GetMapBounds().End.X;
        LimitBottom = SceneManager.currentScene.GetMapBounds().End.Y;
    }

}
