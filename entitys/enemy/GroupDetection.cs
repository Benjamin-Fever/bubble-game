using Godot;
using System;

[GlobalClass]
public partial class GroupDetection : Area2D {
	[Signal] public delegate void GroupMemberEnteredEventHandler();
	[Signal] public delegate void GroupMemberExitedEventHandler();

	[Export] private string _groupName;

    public override void _Ready() {
        BodyEntered += (Node2D body) => {
			if (body.IsInGroup(_groupName)) {
				EmitSignal(SignalName.GroupMemberEntered);
			}
		};

		BodyExited += (Node2D body) => {
			if (body.IsInGroup(_groupName)) {
				EmitSignal(SignalName.GroupMemberExited);
			}
		};
    }

}
