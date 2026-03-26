using Godot;

public partial class Main : Node2D
{
	public override void _Ready()
	{
		Messaging.EventBus.Trigger<StartGame>();
	}
}
