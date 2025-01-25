using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class SceneData : Resource {
    [Export] public Dictionary<NodePath, Dictionary<string, Variant>> data = new Dictionary<NodePath, Dictionary<string, Variant>>();
}