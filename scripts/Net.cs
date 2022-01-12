using Godot;
using System;

public class Net : Sprite, IMatchPointGroup
{
    public override void _Ready()
    {
        Visible = true;
    }

    public void MatchPoint() { Visible = false; }

    public void Reset() { Visible = true; }
}
