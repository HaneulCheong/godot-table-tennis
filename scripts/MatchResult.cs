using Godot;


public class MatchResult : Label, IMatchPointGroup
{
    public override void _Ready()
    {
        Reset();
    }

    public void MatchPoint()
    {
        Visible = true;
        if (GetNode<Level>("..").PlayerScore == Level.MatchPoint)
        {
            Text = "You Win!";
        }
        else if (GetNode<Level>("..").OpponentScore == Level.MatchPoint)
        {
            Text = "You Lose!";
        }
        else
        {
            Text = "Error!";
        }
    }

    public void Reset()
    {
        Visible = false;
    }
}
