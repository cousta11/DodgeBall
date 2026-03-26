using Godot;

public partial class Area2d : Area2D
{
	public void OnBodyEntered(Node2D body)
	{
		Messaging.EventBus.Trigger<StopGame>();
	}
}
