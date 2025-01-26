using Godot;
using System;

public partial class HealthUi : MarginContainer {
	[Export] private HealthComponent healthComponent;

    public override void _Process(double delta) {
		foreach (TextureRect node in GetChild(0).GetChildren()){
			((AtlasTexture)node.Texture).Region = new Rect2(0,0,32,32);
		}
        int lostHealth = healthComponent.MaxHealth - healthComponent.CurrentHealth;
		for (int i =0; i < lostHealth; i++){
			TextureRect node = GetChild(0).GetChild<TextureRect>(i);
			((AtlasTexture)node.Texture).Region = new Rect2(32,0,32,32);
		}
    }
}
