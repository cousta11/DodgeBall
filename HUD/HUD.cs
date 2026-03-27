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

	public override void _Ready()
	{
		_scoreTimer = GetNode<Timer>("ScoreTimer");

		_scoreLabel = GetNode<Label>("ScoreLabel");
		_bestScoreLabel = GetNode<Label>("BestScoreLabel");

		_buttonStart = GetNode<Button>("StartButton");
		_buttonExit = GetNode<Button>("ExitButton");

		_scoreTimer.Stop();

		this.WireEvents();
	}

	[EventHandler(typeof(StartGame))]
	public void Start()
	{
		Input.MouseMode = Input.MouseModeEnum.Hidden;

		_score = 0;
		_scoreTimer.Start();
		_scoreLabel.Text = $"Score: {_score}";

		_buttonStart.Hide();
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

		_bestScoreLabel.Text = $"Best score: {_bestScore}";
	}

	[EventHandler]
	public void PauseGame(PauseGame pauseGame)
	{
		if(pauseGame.IsPaused)
		{
			_buttonExit.Show();
			_bestScoreLabel.Show();
		}
		else
		{
			_buttonExit.Hide();
			_bestScoreLabel.Hide();
		}
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
}
