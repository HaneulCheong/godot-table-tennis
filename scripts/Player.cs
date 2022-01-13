using Godot;


/// <summary>플레이어 라켓을 구현하는 <c>KinematicBody2D</c> 노드</summary>
public class Player : Paddle
{
    ////////////////////
    // 속성
    ////////////////////

    /// <value>이동 방향. 플레이어의 위아래
    /// 키 입력에 따라 결정됩니다.</value>
    public override Vector2 Direction
    {
        get
        {
            bool up_pressed = Input.IsActionPressed("ui_up");
            bool down_pressed = Input.IsActionPressed("ui_down");

            if (up_pressed && down_pressed) { return Vector2.Zero; }
            else if (up_pressed) { return Vector2.Up; }
            else if (down_pressed) { return Vector2.Down; }
            else { return Vector2.Zero; }
        }
    }
}
