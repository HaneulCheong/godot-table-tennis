using System.Collections.Generic;
using Godot;


namespace Game
{
    using Internal;

    /// <summary>게임 광역 노드</summary>
    sealed class Global : Node
    {
        ////////////////////
        // 상수
        ////////////////////

        /// <summary>기본 게임 설정</summary>
        private static readonly SettingDict DEFAULT_SETTINGS = new SettingDict {
            { "Volume", 50 },
            { "Match_Point", 11 },
            { "Ball_Speed", 400 },
            { "Paddle_Speed", 600 }
        };

        ////////////////////
        // 필드
        ////////////////////

        /// <summary><c>Settings</c>의 내부 필드</summary>
        private static SettingDict _settings = DEFAULT_SETTINGS.Clone();

        ////////////////////
        // 속성
        ////////////////////

        /// <value>현재 게임 설정</value>
        public static SettingDict Settings
        {
            get
            {
                PrintSettings();
                return _settings;
            }
            set
            {
                _settings = value;
                PrintSettings();
            }
        }

        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Process(float delta)
        {
            // "ui_fullscreen"으로 전체화면 모드 켜기/끄기
            if (Input.IsActionJustPressed("ui_fullscreen"))
            {
                OS.WindowFullscreen = !OS.WindowFullscreen;
            }
        }

        ////////////////////
        // 메서드
        ////////////////////

        /// <summary>게임 설정을 초기화합니다.</summary>
        public static void ResetSettings()
        {
            Settings = DEFAULT_SETTINGS;
        }

        /// <summary>현재 게임 설정을 Godot 콘솔에 출력합니다. </summary>
        public static void PrintSettings()
        {
            GD.Print();
            foreach (KeyValuePair<string, int> kvp in _settings)
            {
                GD.Print($"{kvp.Key}: {kvp.Value}");
            }
        }
    }
}
