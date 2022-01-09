using System;
using Godot;


public class Ball : KinematicBody2D
{
    private static Random RandNumGen = new Random();
    private static float Speed = 200;
    public Vector2 Velocity = new Vector2();

    public override void _Ready()
    {
        this.Velocity.x = Ball.RandNumGen.Next(2) == 1 ? 1.0f : -1.0f;
        this.Velocity.y = Ball.RandNumGen.Next(2) == 1 ? 0.8f : -0.8f;
    }

    public override void _PhysicsProcess(float delta)
    {
        KinematicCollision2D collisionObject = this.MoveAndCollide(
            this.Velocity * Ball.Speed * delta
        );

        if (collisionObject != null)
        {
            this.Velocity = this.Velocity.Bounce(collisionObject.Normal);
            this.Velocity.x *= 1.01f;
            this.Velocity.y *= 1.02f;
            GD.Print(Math.Abs(this.Velocity.x));
        }
    }
}
