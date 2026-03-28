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

		this.WireEvents();

		_bestScoreLabel.Text = $"Best score: {_bestScore}";
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
			GetTree().Quit();
		Direction = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
	}

	[EventHandler(typeof(StartGame))]
	public void Start()
	{
		//Input.MouseMode = Input.MouseModeEnum.Captured;

		_score = 0;
		_scoreLabel.Text = $"Score: {_score}";

		_buttonStart.Hide();
		_buttonPause.Hide();
		_buttonExit.Hide();
		_bestScoreLabel.Hide();
		_isStoped = false;

		_scoreTimer.Start();
	}

	[EventHandler(typeof(StopGame))]
	public void Stop()
	{
		_isStoped = true;
		_scoreTimer.Stop();
		//Input.MouseMode = Input.MouseModeEnum.Visible;

		if(_score > _bestScore)
			_bestScore = _score;

		_buttonStart.Show();
		_buttonExit.Show();
		_bestScoreLabel.Show();
		_buttonPause.Hide();

		_bestScoreLabel.Text = $"Best score: {_bestScore}";
	}

	public void OnTimeOut()
	{
		_score++;
		_scoreLabel.Text = $"Score: {_score}";
	}

	public void OnStartPressed()
	{
		EventBus.Trigger<StartGame>();
	}

	public void OnExitPressed()
	{
		GetTree().Quit();
	}

	public void OnPaused()
	{
		if(_isStoped) return;

		_isPaused = !_isPaused;
		GetTree().Paused = _isPaused;
		if(_isPaused)
		{
			_scoreTimer.Stop();
			_buttonExit.Show();
			_bestScoreLabel.Show();
			_buttonPause.Show();
			//Input.MouseMode = Input.MouseModeEnum.Visible;
		}
		else
		{
			_buttonExit.Hide();
			_bestScoreLabel.Hide();
			_buttonPause.Hide();
			//Input.MouseMode = Input.MouseModeEnum.Captured;
			_scoreTimer.Start();
		}
	}
}
