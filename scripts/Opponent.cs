using Godot;
using System;


public class Opponent : KinematicBody2D
{
    private const float MaxSpeed = 400;
    private KinematicBody2D Ball;

    public Vector2 Velocity
    {
        get
        {
            Vector2 rawVelocity = new Vector2();

            if (Input.IsActionPressed("ui_up"))
            {
                rawVelocity.y -= 1;
            }
            if (Input.IsActionPressed("ui_down"))
            {
                rawVelocity.y += 1;
            }

            return rawVelocity;
        }
    }

    private int MoveDirection
    {
        get
        {
            if (Ball.Position.x > this.Position.x)
            {
                return 0;  // 패배, 정지
            }

            if (Math.Abs(Ball.Position.y - this.Position.y) > 25)
            {
                if (Ball.Position.y > this.Position.y)
                {
                    return 1;  // 위로
                }
                else
                {
                    return -1;  // 아래로
                }
            }
            else
            {
                return 0;  // 정지
            }
        }
    }

    public override void _Ready()
    {
        this.Ball = this.GetNode<KinematicBody2D>("../Ball");
    }

    public override void _PhysicsProcess(float delta)
    {
        this.MoveAndSlide(
            new Vector2(0, this.MoveDirection) * Opponent.MaxSpeed
        );
    }
}
