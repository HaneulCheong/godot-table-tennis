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
        private PackedScene Scene { get; set; } = null;

        [Export]
        private bool FocusTest
        {
            get => _focusTest;
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
            Connect("focus_entered", this, nameof(OnFocusChange));
            Connect("focus_exited", this, nameof(OnFocusChange));
        }

        public override void _Pressed()
        {
            if (Scene == null) { GetTree().Quit(); }
            GetTree().ChangeSceneTo(Scene);
        }

        private void OnFocusChange()
        {
            Text = HasFocus() ? $"> {RawText} <" : RawText;
        }
    }
}
