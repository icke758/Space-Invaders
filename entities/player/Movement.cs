using Godot;
using System;

public partial class Movement : CharacterBody2D
{
	private readonly float MOVE_SPEED = 150.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		velocity.X = 0;
		velocity.Y = 0;
		if(Input.IsKeyPressed(Key.A))
			velocity.X = - MOVE_SPEED;

		else if(Input.IsKeyPressed(Key.D))
			velocity.X = MOVE_SPEED;

		else if(Input.IsKeyPressed(Key.W))
			velocity.Y = - MOVE_SPEED;
			
		else if(Input.IsKeyPressed(Key.S))
			velocity.Y = MOVE_SPEED;
		
		Velocity = velocity;
		MoveAndSlide(); 
	}
}
