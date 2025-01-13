using Godot;
using System;

public partial class Conductor : AudioStreamPlayer
{
	[Export] private float bpm;
	[Signal] public delegate void BeatEventHandler(int position);
	[Export] private int measures;
	[Signal] public delegate void MeasureEventHandler(int position);

	public double songPos;
	public int songPosInBeats;
	public float secondsPerBeat;
	public int lastBeat;
	public int beatsBeforeStart;
	public int measure;
	public float fps;
	public bool playing = false;

	public override void _Ready(){
		secondsPerBeat = fps / bpm; //might have to just set to 60 and use physics process, otherwise could give unfair advantage to higher fps
	}

	public override void _Process(double delta){
		if (playing){
			songPos = GetPlaybackPosition() + AudioServer.GetTimeSinceLastMix();
			songPos -= AudioServer.GetOutputLatency();
			songPosInBeats = (int)Math.Floor(songPos / secondsPerBeat) + beatsBeforeStart;
			reportBeat();
		}
	}

	private void reportBeat(){
		if (lastBeat < songPosInBeats){
			if (measure > measures){	measure = 1;}
			EmitSignal(SignalName.Beat, songPosInBeats);
			EmitSignal(SignalName.Measure, measure);
			lastBeat = songPosInBeats;
			measure++;
		}
	}

	public void playFromBeat(int beat, int offset){
		Play();
		Seek(beat * secondsPerBeat);
		beatsBeforeStart = offset;
		for (int i = 0; i < beatsBeforeStart; i++){
			measure++;
			if (measure > measures){	measure = 1;}
		}
	}
}
