using Godot;
using Messaging;

public partial class Hud : CanvasLayer
{
	private static int _score;
	private static Timer _scoreTimer;
	private static Label _scoreLabel;

	public override void _Ready()
	{
		_scoreTimer = GetNode<Timer>("ScoreTimer");
		_scoreLabel = GetNode<Label>("ScoreLabel");
		_scoreTimer.Stop();
		this.WireEvents();
	}

	[EventHandler(typeof(StartGame))]
	public void Start()
	{
		_score = 0;
		_scoreTimer.Start();
		_scoreLabel.Text = $"Score: {_score}";
	}

	[EventHandler(typeof(StopGame))]
	public void Stop()
	{
		_scoreTimer.Stop();
	}

	public void OnTimeOut()
	{
		_score++;
		_scoreLabel.Text = $"Score: {_score}";
	}
}
