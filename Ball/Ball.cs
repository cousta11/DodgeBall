using Godot;
using System;
using Messaging;

public partial class Ball : RigidBody2D
{
	private Vector2 _flightDirection;
	private float _speed = 1500;

	public override void _Ready()
	{
		Hide();
		this.WireEvents();
	}

	[EventHandler(typeof(StartGame))]
	public void Start()
	{
		Position = new Vector2(970, 270);
		float angle = (float)Random.Shared.NextDouble()*Mathf.Tau;
		_flightDirection =
			new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).Normalized();
		Show();

		LinearVelocity = _flightDirection * _speed;
	}

	[EventHandler(typeof(StopGame))]
	public void Stop()
	{
		Hide();
		LinearVelocity = Vector2.Zero;
	}
}
