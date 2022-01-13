using Godot;


/// <summary>플레이어 라켓 노드</summary>
public class Player : Paddle
{
    ////////////////////
    // 속성
    ////////////////////

    /// <value>
    /// 현재 상대 속도.
    /// 플레이어의 위아래 키 입력에 따라
    /// <c>Velocity.y</c>가 결정됩니다.
    /// </value>
    public override Vector2 Direction
    {
        get
        {
            Vector2 value = Vector2.Zero;

            if (Input.IsActionPressed("ui_up")) { value = Vector2.Up; }
            if (Input.IsActionPressed("ui_down")) { value = Vector2.Down; }

            return value;
        }
    }
}
