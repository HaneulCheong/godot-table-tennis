using Godot;
using Godot.Collections;


[Tool]
public class MenuButton : Button
{
    private bool _selected = false;

    [Export]
    private string RawText { get; set; } = "Button";
    [Export]
    private bool Selected
    {
        get { return _selected; }
        set
        {
            _selected = value;
            Text = Selected ? $"> {RawText} <" : RawText;
        }
    }

    public override void _Ready()
    {
        Connect(
            "focus_entered", this, nameof(_OnFocusChanged), new Array { true }
        );
        Connect(
            "focus_exited", this, nameof(_OnFocusChanged), new Array { false }
        );
    }

    public override void _Process(float delta)
    {
        if (Engine.EditorHint) { Selected = Selected; }
    }

    public void _OnFocusChanged(bool value)
    {
        Selected = value;
    }
}
