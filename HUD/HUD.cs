using Godot;
using Messaging;

public partial class HUD: CanvasLayer
{
	private static int _bestScore = 0;
	private static int _score;

	private static Timer _scoreTimer;
	private static Label _scoreLabel;
	private static Label _bestScoreLabel;

	private static Button _buttonStart;
	private static Button _buttonExit;
	private static Button _buttonPause;

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

	[EventHandler(typeof(StartGame))]
	public void Start()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;

		_score = 0;
		_scoreTimer.Start();
		_scoreLabel.Text = $"Score: {_score}";

		_buttonStart.Hide();
		_buttonPause.Hide();
		_buttonExit.Hide();
		_bestScoreLabel.Hide();
	}

	[EventHandler(typeof(StopGame))]
	public void Stop()
	{
		Input.MouseMode = Input.MouseModeEnum.Visible;

		_scoreTimer.Stop();

		if(_score > _bestScore)
			_bestScore = _score;

		_buttonStart.Show();
		_buttonExit.Show();
		_bestScoreLabel.Show();
		_buttonPause.Hide();

		_bestScoreLabel.Text = $"Best score: {_bestScore}";
	}

	[EventHandler]
	public void Pause(PauseGame e)
	{
		OnPauseToggled(e.ToggleedOn);
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

	public void OnPauseToggled(bool toggleedOn)
	{
		GetTree().Paused = toggleedOn;
		if(toggleedOn)
		{
			_scoreTimer.Stop();
			_buttonExit.Show();
			_bestScoreLabel.Show();
			_buttonPause.Show();
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
		else
		{
			_buttonExit.Hide();
			_bestScoreLabel.Hide();
			_buttonPause.Hide();
			Input.MouseMode = Input.MouseModeEnum.Captured;
			_scoreTimer.Start();
		}
	}
}
