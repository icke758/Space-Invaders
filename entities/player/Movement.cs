using Godot;

public partial class Movement : CharacterBody2D
{
	[Export] float speed = 150.0f;
	[Export] private float ACCELERATION = 200.0f;
	[Export] private float DEACCELERATION = 200.0f;
	[Export] private float ROTATION_VELOCITY = 15.0f;

	[Export] private Node2D _ship;

	private Vector2 _inputVelocity;

	// Action names
	private const string LEFT 	= "pl_left";
	private const string RIGHT 	= "pl_right";
	private const string UP 	= "pl_up";
	private const string DOWN 	= "pl_down";

    public override void _Ready()
    {
		_ship ??= GetNode<Node2D>("Ship");
    }

    public override void _Process(double delta)
    {
		_inputVelocity = new Vector2(
			Input.GetAxis(LEFT, RIGHT),
			Input.GetAxis(UP, DOWN)
		).Normalized();

		if (_inputVelocity != Vector2.Zero)
		{
			float speeder = 1.0f;

			// This makes the ship turn almost instantly if we try
			// turning it a full 180 degrees.
			// The dot product of two vectors tells us how similar they are
			// If they are pointing to the same direction, 1.0 is returned
			// If they are perpendicular, 0.0 is returned
			// -1.0 is returned if they are facing opposite directions
			// This function interpolates its result, so any negative value
			// mean opposing vectors
			// We that and if true, we increase the acceleration momentarily 
			if(_inputVelocity.Dot(Velocity.Normalized()) < 0) {
				speeder = 20.0f;
			}

			Velocity = Velocity.MoveToward(_inputVelocity * speed, ACCELERATION * speeder * (float) delta);
			UpdateShipRotation(delta);
		} 
		else 
		{
			Velocity = Velocity.MoveToward(Vector2.Zero, DEACCELERATION * (float) delta);
		}

    }

	private void UpdateShipRotation(double delta)
	{
		float oldRotation = _ship.Rotation;
		float newRotation = Velocity.Angle();

		_ship.Rotation = (float) Mathf.LerpAngle(oldRotation, newRotation, delta * ROTATION_VELOCITY);
	}

    public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide(); 
	}
}
