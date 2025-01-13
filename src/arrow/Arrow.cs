using Godot;
using System;
using System.Diagnostics;

public partial class Arrow : AnimatedSprite2D
{
	//const float SPAWN_X = 325;
	//const float SPAWN_Y = 1300;

	//remeber to eventually move this all to the cursor script, having this many checks in a single arrow instance
	//is crazy unomptimized, but icba to do allat rn
	[Export] public float noteSpeed = 1.8f;
	[Export] private float noteCleanUpRad;
	public bool deleteNote = false;
	private Area2D noteDeathHitBox;
	private Sprite2D wheel;
	private Timer hitTimer;

	public override void _Ready(){
		hitTimer = GetNode<Timer>("HitTimer");
		GetNode<Area2D>("NoteHitbox").AreaExited += (Area2D) => deleteNote = true;
		wheel = GetTree().CurrentScene.GetNode<Sprite2D>("Wheel");
		noteCleanUpRad += wheel.GlobalPosition.X;
		noteSpeed *= 100;
	}

    public override void _Process(double delta)
    {
		Vector2 wheelPos = wheel.GlobalPosition;
		if (Position.DistanceSquaredTo(wheelPos) > noteCleanUpRad){
			GlobalPosition = GlobalPosition.MoveToward(wheelPos, noteSpeed * (float)delta);
		}
		else{	QueueFree();}

		Node2D hitWindow = wheel.GetNode<Marker2D>("Cursor/%HitWindow");
		if (GlobalPosition < hitWindow.GlobalPosition && !hitTimer.IsStopped()){
			GD.Print("ough " + hitTimer.TimeLeft);
			hitTimer.Stop();
		}
		// if (Position.DistanceTo(wheelPos) > noteCleanUpRad){	GlobalPosition.MoveToward(GlobalPosition.DirectionTo(wheelPos), noteSpeed * (float)delta);}//GlobalPosition += GlobalPosition.DirectionTo(wheelPos) * (noteSpeed);}
		// else{	QueueFree();}
    }
}
