/*
using Godot;


namespace Game.SettingsScene
{
    using GodotDict = Godot.Collections.Dictionary;

    public class ConfigFileManager : Node
    {
        [Export]
        private string FileName { get; set; } = "settings.cfg";

        private ConfigFile Config { get; set; } = new ConfigFile();

        private GodotDict DefaultSettings { get; } = new GodotDict {
            { "ResolutionHeight", 720 }
        };

        private GodotDict CurrentSettings { get; set; } = new GodotDict();

        public override void _Ready()
        {
            Load();
            Save();
            GetTree().ChangeScene("res://entities/MainMenuScene/MainMenu.tscn");
        }

        public void Save()
        {
            GD.Print($"Saving settings to {OS.GetUserDataDir()}/{FileName}");
            Config.SetValue("Main", "ResolutionHeight", OS.WindowSize.y);
            Config.Save("user://" + FileName);
        }

        public void Load()
        {
            GD.Print($"Loading settings from {OS.GetUserDataDir()}/{FileName}");
            Error loadResult = Config.Load("user://" + Filename);
        }
    }
}
*/
