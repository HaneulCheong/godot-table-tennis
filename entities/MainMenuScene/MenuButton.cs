using Godot;


namespace Game.MainMenuScene
{
    [Tool]
    public class MenuButton : Button
    {
        private bool _focusTest;

        [Export]
        private string RawText { get; set; }

        [Export]
        private bool FocusTest
        {
            get { return _focusTest; }
            set
            {
                _focusTest = value;
                if (Engine.EditorHint)
                {
                    Text = FocusTest ? $"> {RawText} <" : RawText;
                }
            }
        }

        public override void _Ready()
        {
            Connect("focus_entered", this, nameof(_OnFocusChanged));
            Connect("focus_exited", this, nameof(_OnFocusChanged));
        }

        public override void _Pressed()
        {
            switch (Name)
            {
                case "OnePlayerButton":
                    GetTree().ChangeScene("res://entities/MainScene/Main.tscn");
                    break;
                case "TwoPlayerButton":
                    break;
                case "SettingsButton":
                    break;
                case "ExitButton":
                    GetTree().Quit();
                    break;
                default:
                    throw new System.Exception();
            }
        }

        public void _OnFocusChanged()
        {
            Text = HasFocus() ? $"> {RawText} <" : RawText;
        }
    }
}
