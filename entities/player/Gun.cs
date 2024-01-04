using Godot;

public partial class Gun : Node2D
{
	[Export] PackedScene bullet_tscn;
	[Export] Node2D muzzle;

	[Export] float BULLET_SPEED = 600f;
	[Export] float BULLET_PER_SECOND = 15f;
	[Export] float BULLET_DAMAGE = 30f;

	private float _cooldownTime;
	private float _fireCountdown = 0f;

	// Action names
	private const string FIRE = "pl_fire";

	public override void _Ready()
	{
		muzzle ??= GetNode<Node2D>("Muzzle");
		_cooldownTime = 1 / BULLET_PER_SECOND;
	}

	public override void _Process(double delta)
	{
		if(Input.IsActionPressed(FIRE) && _fireCountdown <= 0)
		{
			_fireCountdown = _cooldownTime;

			Fire();
		} 
		else 
		{
			_fireCountdown -= (float) delta;
		}

		RotateToMouse();
	}

	private void RotateToMouse() {
		Rotate(GetAngleTo(GetGlobalMousePosition()));
	}

	private void Fire() {
		RigidBody2D bullet = bullet_tscn.Instantiate<RigidBody2D>();

		bullet.ZIndex = -1;
		bullet.Rotation = GlobalRotation;
		bullet.GlobalPosition = muzzle.GlobalPosition;
		bullet.LinearVelocity = bullet.Transform.X * BULLET_SPEED;

		GetTree().Root.AddChild(bullet);
	}
}
