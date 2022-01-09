using Godot;


public class Level : Node
{
    /// <summary>ESC 키를 누를 경우 게임을 종료합니다.</summary>
    public override void _Process(float delta)
    {
        if (Input.IsActionPressed("ui_cancel"))
        {
            this.GetTree().Quit();
        }
    }
}
