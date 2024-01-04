using Godot;

public partial class Bullet : RigidBody2D
{
	public override void _Ready()
	{
		this.BodyEntered += (body) => HandleCollision(body);

		Timer timer = GetNode<Timer>("Timer");
		timer.Timeout += () => QueueFree();
	}

	private void HandleCollision(Node body) {
		//TODO: Actually handle collision
	}
}
