using Godot;
using Messaging;

public partial class UI : CanvasLayer
{
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
		quit();
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
