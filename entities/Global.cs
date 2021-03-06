using System;
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
            { "Paddle_Speed", 600 },
            { "Paddle_Size", 10 },
        };

        ////////////////////
        // 필드
        ////////////////////

        /// <summary><c>Settings</c>의 내부 필드</summary>
        private static SettingDict _settings = DEFAULT_SETTINGS.Clone();

        ////////////////////
        // 속성
        ////////////////////

        /// <summary>현재 게임 상태</summary>
        public static GameStateDict GameState = new GameStateDict();

        /// <value>현재 게임 설정</value>
        public static SettingDict Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                ChangeVolume(Settings["Volume"]);
                foreach (KeyValuePair<string, int> kvp in _settings)
                {
                    GD.Print($"{kvp.Key}: {kvp.Value}");
                }
                GD.Print();
            }
        }

        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Ready()
        {
            ChangeVolume(Settings["Volume"]);
        }

        public override void _Input(InputEvent @event)
        {
            // "ui_fullscreen"으로 전체화면 모드 켜기/끄기
            if (@event.IsActionPressed("ui_fullscreen"))
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

        /// <summary>전달된 정수로 볼륨을 설정합니다.</summary>
        /// <param name="percentage">볼륨의 비율이 될 1~100의 정수</param>
        private static void ChangeVolume(int percentage)
        {
            double scale = Mathf.Clamp(percentage, 1, 100) / 100.0f;
            float decibels = (float)(20 * Math.Log10(scale));
            AudioServer.SetBusVolumeDb(
                AudioServer.GetBusIndex("Master"), decibels
            );
        }
    }


    /// <summary>게임 상태의 종류</summary>
    public enum GameStateType { GameMode }


    /// <summary>게임 모드</summary>
    public enum GameMode { OnePlayer, TwoPlayer }
}
