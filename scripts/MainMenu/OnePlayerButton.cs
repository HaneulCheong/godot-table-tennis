using Godot;


public class OnePlayerButton : MenuButton
{
    public override void _Pressed()
    {
        GetTree().ChangeScene("res://scenes/Main/Main.tscn");
    }
}
