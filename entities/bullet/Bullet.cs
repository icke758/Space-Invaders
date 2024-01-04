using Godot;

public partial class Bullet : RigidBody2D
{
	public override void _Ready()
	{
		Timer timer = GetNode<Timer>("Timer");
		timer.Timeout += () => QueueFree();
	}
}
