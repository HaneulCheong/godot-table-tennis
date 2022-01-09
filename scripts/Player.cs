using Godot;


public class Player : KinematicBody2D
{
    public const int Speed = 400;
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

            return rawVelocity * Player.Speed;
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        this.MoveAndSlide(this.Velocity);
    }
}
