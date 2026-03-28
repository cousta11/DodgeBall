using Godot;
using System;
using Messaging;

public partial class Player : CharacterBody2D
{
	public int Speed = 1000;
	private AnimatedSprite2D _sprite;

	public override void _Ready()
	{
		Hide();
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		this.WireEvents();
	}

	[EventHandler(typeof(StartGame))]
	public void Start()
	{
		Position = new Vector2(970, 810);
		Show();
	}

	[EventHandler(typeof(StopGame))]
	public void Stop()
	{
		Hide();
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = UI.Direction.Normalized()*Speed;
		MoveAndSlide();
		
		if(Velocity.Length() > 0)
			_sprite.Play();
		else
			_sprite.Stop();
		
		if(Velocity.X != 0)
			_sprite.FlipH = Velocity.X < 0;
	}
}
