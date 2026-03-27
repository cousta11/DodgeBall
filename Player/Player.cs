using Godot;
using System;
using Messaging;

public partial class Player : CharacterBody2D
{
	public int Speed = 1000;
	private AnimatedSprite2D _sprite;
	private bool isStoped = true;
	public static bool IsPaused = false;

	[EventHandler(typeof(StartGame))]
	public void Start()
	{
		isStoped = false;
		Position = new Vector2(970, 810);
		Show();
	}

	[EventHandler(typeof(StopGame))]
	public void Stop()
	{
		isStoped = true;
		Hide();
	}

	[EventHandler]
	public void PauseGame(PauseGame pauseGame)
	{
		if(pauseGame.IsPaused)
		{
		}
		else
		{
		}
	}

	public override void _Ready()
	{
		Hide();
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		this.WireEvents();
	}

	public override void _Process(double delta)
	{
		if(Input.IsActionPressed("Quit"))
		{
			GetTree().Quit();
		}

		if(Input.IsActionPressed("Space"))
		{
			Player.IsPaused = !Player.IsPaused;
			EventBus.Trigger<PauseGame>(new PauseGame(IsPaused: Player.IsPaused));
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if(isStoped) return;
		var direction = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
		
		Velocity = direction.Normalized()*Speed;
		MoveAndSlide();
		
		if(Velocity.Length() > 0)
			_sprite.Play();
		else
			_sprite.Stop();
		
		if(Velocity.X != 0)
			_sprite.FlipH = Velocity.X < 0;
	}
}
