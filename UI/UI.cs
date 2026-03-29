using Godot;
using Messaging;

public partial class UI : CanvasLayer
{
	public static Vector2 Direction;

	private bool _isPaused = false;
	private bool _isStoped = true;

	private int _bestScore = 0;
	private int _score;

	private Timer _scoreTimer;
	private Label _scoreLabel;
	private Label _bestScoreLabel;

	private Button _buttonStart;
	private Button _buttonExit;
	private Button _buttonPause;

	private void quit()
	{
		Save();
		GetTree().Quit();
	}

	public override void _Ready()
	{
		_scoreTimer = GetNode<Timer>("ScoreTimer");

		_scoreLabel = GetNode<Label>("ScoreLabel");
		_bestScoreLabel = GetNode<Label>("BestScoreLabel");

		_buttonStart = GetNode<Button>("StartButton");
		_buttonExit = GetNode<Button>("ExitButton");
		_buttonPause = GetNode<Button>("PauseButton");

		_scoreTimer.Stop();
		_buttonPause.Hide();

		Load();
		_bestScoreLabel.Text = $"Best score: {_bestScore}";

		this.WireEvents();
	}

	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("Space"))
		{
			if(_isStoped)
				EventBus.Trigger<StartGame>();
			else
				OnPaused();
		}
		if(Input.IsActionJustPressed("Quit"))
			quit();
		Direction = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
	}

}
