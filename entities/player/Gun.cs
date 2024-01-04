using Godot;

public partial class Gun : Node2D
{
	[Export] PackedScene bullet_tscn;
	[Export] float BULLET_SPEED = 600f;
	[Export] float BULLET_PER_SECOND = 5f;
	[Export] float BULLET_DAMAGE = 30f;

	private float fireRate;

	private float TIME_UNTIL_FIRE = 0f;

	public override void _Ready()
	{
		fireRate = 1 / BULLET_PER_SECOND;
	}

	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("click") && TIME_UNTIL_FIRE > fireRate) {
			RigidBody2D bullet = bullet_tscn.Instantiate<RigidBody2D>();
			bullet.Rotation = GlobalRotation;
			bullet.GlobalPosition = GlobalPosition;
			bullet.LinearVelocity = bullet.Transform.X * BULLET_SPEED;

			GetTree().Root.AddChild(bullet);

			TIME_UNTIL_FIRE = 0f;
		} else {
			TIME_UNTIL_FIRE += (float)delta;
		}
	}
}
