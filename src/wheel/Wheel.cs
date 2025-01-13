using Godot;
using System;
using System.Linq;

public partial class Wheel : Sprite2D
{
	[Export] public float rotSpeed;
	private Sprite2D cursor;
	public int score;
	public int finalScore;
	private RichTextLabel scoreText;

	public int perfectHit = 3;
	public int goodHit = 2;
	public int okayHit = 1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rotSpeed *= 100;
		cursor = GetNode<Sprite2D>("Cursor");
		scoreText = GetNode<RichTextLabel>("CanvasLayer/Score");
		// GetNode<Area2D>("Cursor/Perfect").AreaEntered += (Area2D area) => {	score += perfectHit; };
		// GetNode<Area2D>("Cursor/Perfect").AreaExited += (Area2D area) => {	score = 0; };
		// GetNode<Area2D>("Cursor/Good").AreaEntered += (Area2D area) => {	score += goodHit; };
		// GetNode<Area2D>("Cursor/Good").AreaExited += (Area2D area) => {	score = 0; };
		// GetNode<Area2D>("Cursor/Okay").AreaEntered += (Area2D area) => {	score += okayHit; };
		// GetNode<Area2D>("Cursor/Okay").AreaExited += (Area2D area) => {	score = 0; };
		GetNode<Timer>("CanvasLayer/ScoreDecayTimer").Timeout += () => { if (scoreText.Text != null){	scoreText.Text = null;} };
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		cursor.RotationDegrees += rotSpeed * (float)delta;
		if (Input.IsActionJustPressed("Click")){
			rotSpeed *= -1;
			// if (score == perfectHit) { scoreText.Text = "perfect";}
			// else if (score == goodHit) { scoreText.Text = "good";}
			// else if (score == okayHit) { scoreText.Text = "okay";}
			// else { scoreText.Text = "ass </3";}
		}
		//scoreText.Text = score.ToString();
	}
}
