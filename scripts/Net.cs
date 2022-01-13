using Godot;


public class Net : Sprite, IMatchPointGroup
{
    public override void _Ready()
    {
        Reset();
    }

    public void MatchPoint()
    {
        Visible = false;
    }

    public void Reset()
    {
        Visible = true;
    }
}
