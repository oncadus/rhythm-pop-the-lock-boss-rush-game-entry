using Godot;
using System;

public partial class GameManager : Node
{
	[Export] private float spawnRad;
	private PackedScene arrow = GD.Load<PackedScene>("res://src/arrow/arrow.tscn");
	private Arrow currentArrow;
	private Wheel wheel;
	float cooldown;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentArrow = null;
		wheel = GetTree().CurrentScene.GetNode<Wheel>("Wheel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		cooldown += (float)delta;
		//GD.Print(cooldown);
		if (cooldown >= 2f){
			SpawnArrow();
			//GD.Print(currentArrow + " instanced at " +  currentArrow.GlobalPosition);
			cooldown = 0;
		}
	}

	public void SpawnArrow(){
		float theta = Mathf.DegToRad(GD.RandRange(0, 360));
		float spawnX = spawnRad * MathF.Cos(theta);
		float spawnY = spawnRad * MathF.Sin(theta);
		Vector2 spawnLoc = new Vector2(spawnX, spawnY);
		currentArrow = arrow.Instantiate() as Arrow;
		currentArrow.GlobalPosition = wheel.GlobalPosition + spawnLoc;
		//currentArrow.GlobalPosition = new Vector2(wheel.GlobalPosition.X + spawnRad, wheel.GlobalPosition.Y);
		AddChild(currentArrow);
	}
}
