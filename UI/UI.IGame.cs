using Godot;
using Messaging;

public partial class UI : IGame
{
	[EventHandler(typeof(StartGame))]
	public void Start()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;

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
		Input.MouseMode = Input.MouseModeEnum.Visible;

		if(_score > _bestScore)
			_bestScore = _score;

		_buttonStart.Show();
		_buttonExit.Show();
		_bestScoreLabel.Show();
		_buttonPause.Hide();

		_bestScoreLabel.Text = $"Best score: {_bestScore}";
		Save();
	}
}
