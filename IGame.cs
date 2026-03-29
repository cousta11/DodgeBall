using Messaging;

public interface IGame
{
	[EventHandler(typeof(StartGame))]
	void Start();

	[EventHandler(typeof(StopGame))]
	void Stop();
}
