using Godot;


/// <summary>플레이어 라켓 노드</summary>
public class Player : Paddle
{
    /// <summary>플레이어의 위아래 키 입력에 따라 <c>Velocity.y</c>가 결정됩니다.</summary>
    public override Vector2 Velocity
    {
        get
        {
            Vector2 value = new Vector2();

            if (Input.IsActionPressed("ui_up"))
            {
                value.y -= 1;
            }
            if (Input.IsActionPressed("ui_down"))
            {
                value.y += 1;
            }

            return value;
        }
    }
}
