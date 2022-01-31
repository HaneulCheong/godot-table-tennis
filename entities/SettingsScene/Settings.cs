using System.Collections.Generic;
using Godot;

namespace Game.SettingsScene
{
    using Shared;
    using Internal;
    using SpinBoxContainerScene;

    /// <summary>게임 설정을 바꾸는 창입니다.</summary>
    public class Settings : VBoxContainer
    {
        ////////////////////
        // 속성
        ////////////////////

        /// <value>현재 창 내부에 임시로 저장해둔 게임 설정</value>
        private SettingDict SettingsBuffer { get; set; } = new SettingDict();

        /// <value>게임 설정 목록을 나열하는 컨테이너</value>
        private VBoxContainer SettingList
        {
            get => GetNode<VBoxContainer>("SettingList");
        }

        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Ready()
        {
            GetNode<SceneChangeButton>("ButtonContainer/ConfirmButton").Connect(
                "pressed", this, nameof(OnConfirm)
            );
            GetNode<TextButton>("ButtonContainer/DefaultButton").Connect(
                "pressed", this, nameof(OnDefault)
            );

            Load();
            GetNode<SceneChangeButton>(
                "ButtonContainer/CancelButton"
            ).GrabFocus();
        }

        ////////////////////
        // Godot 신호 메서드
        ////////////////////

        /// <summary>이 창의 게임 설정을 저장합니다.</summary>
        private void OnConfirm()
        {
            SettingsBuffer.Clear();
            foreach (SpinBoxContainer setting in SettingList.GetChildren())
            {
                SettingsBuffer.Add(
                    setting.Key, setting.Value
                );
            }
            Global.Settings = SettingsBuffer.Clone();
        }

        /// <summary>현재 게임 설정을 기본 설정을 되돌립니다.</summary>
        private void OnDefault()
        {
            Global.ResetSettings();
            Load();
        }

        ////////////////////
        // Godot 신호 메서드
        ////////////////////

        /// <summary>현재 게임 설정을 불러옵니다.</summary>
        private void Load()
        {
            SettingsBuffer = Global.Settings.Clone();
            foreach (KeyValuePair<string, int> kvp in SettingsBuffer)
            {
                foreach (SpinBoxContainer setting in SettingList.GetChildren())
                {
                    if (setting.Key != kvp.Key) { continue; }
                    setting.Value = kvp.Value;
                }
            }
        }
    }
}
