using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class Scene2D : Node2D {
	[Signal] public delegate void SceneLoadedEventHandler();
	[Export] public SceneData sceneData;
	[Export] private GameMap mapLayer;

	public Array<Vector2> GetPathToPoint(Vector2 start, Vector2 end) {
		return mapLayer.GetPathToPoint(start, end);
	}

	public void Save() {
		foreach (NodePath path in sceneData.data.Keys) {
			Node2D node = GetNodeOrNull<Node2D>(path);
			if (node == null) {
				GD.PrintErr("Node not found: " + path);
				continue;
			}

			Dictionary<string, Variant> nodeData = sceneData.data[path];
			foreach (string key in nodeData.Keys) {
				nodeData[key] = node.Get(key);
			}   
		}
	}

	public void Load() {
		foreach (NodePath path in sceneData.data.Keys) {
			Node2D node = GetNodeOrNull<Node2D>(path);
			if (node == null) {
				GD.PrintErr("Node not found: " + path);
				continue;
			}

			Dictionary<string, Variant> nodeData = sceneData.data[path];
			foreach (string key in nodeData.Keys) {
				node.Set(key, nodeData[key]);
			}   
		}
	}

	public Rect2I GetMapBounds() {
		return new Rect2I(Vector2I.Zero, mapLayer.GetUsedRect().Size * 32);
	}
}
