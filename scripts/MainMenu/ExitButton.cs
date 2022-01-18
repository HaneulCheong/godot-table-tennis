using Godot;


public class ExitButton : MenuButton
{
    public override void _Pressed()
    {
        GetTree().Quit();
    }
}
