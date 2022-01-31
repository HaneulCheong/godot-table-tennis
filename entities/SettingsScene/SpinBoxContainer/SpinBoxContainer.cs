using Godot;


namespace Game.SettingsScene.SpinBoxContainerScene
{
    /// <summary>게임의 설정 하나를 표시하는 컨테이너입니다.</summary>
    [Tool]
    public class SpinBoxContainer : HBoxContainer
    {
        ////////////////////
        // 필드
        ////////////////////

        /// <summary><c>Key</c>의 내부 필드</summary>
        private string _key = "Key";
        /// <summary><c>MinValue</c>의 내부 필드</summary>
        private int _minValue = 0;
        /// <summary><c>MaxValue</c>의 내부 필드</summary>
        private int _maxValue = 100;
        /// <summary><c>Step</c>의 내부 필드</summary>
        private int _step = 10;
        /// <summary><c>Value</c>의 내부 필드</summary>
        private int _value = 50;

        ////////////////////
        // 속성
        ////////////////////

        /// <value>이 설정의 이름</value>
        [Export]
        public string Key
        {
            get => _key;
            set
            {
                _key = value;
                GetNode<Label>("Key").Text = value.Replace("_", " ");
            }
        }

        /// <value>이 설정의 최소값</value>
        [Export]
        private int MinValue
        {
            get => _minValue;
            set
            {
                _minValue = value;
                // spinBoxLabel.Value = Value;
            }
        }

        /// <value>이 설정의 최대값</value>
        [Export]
        private int MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                // spinBoxLabel.Value = Value;
            }
        }

        /// <value>이 설정의 변경 단위</value>
        [Export]
        private int Step
        {
            get => _step;
            set
            {
                _step = value;
                // spinBoxLabel.Value = Value;
            }
        }

        /// <value>이 설정의 현재 값</value>
        [Export]
        public int Value
        {
            get => _value;
            set
            {
                _value = RoundToStep(value);
                spinBoxLabel.Value = Value;
            }
        }

        /// <value>이 설정의 값을 표시할 스핀박스 레이블</value>
        private SpinBoxLabel spinBoxLabel
        {
            get => GetNode<SpinBoxLabel>("SpinBoxLabel");
        }

        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Ready()
        {
            Value = Value;
            Connect("focus_entered", this, nameof(OnFocusChange));
            Connect("focus_exited", this, nameof(OnFocusChange));
        }

        /// <summary>Focus돼 있을 때 "ui_right" 또는 "ui_left"에
        /// 따라 <c>Value</c>를 변경합니다.</summary>
        public override void _GuiInput(InputEvent @event)
        {
            if (!HasFocus()) { return; }

            if (@event.IsActionPressed("ui_right")) { Value += Step; }
            if (@event.IsActionPressed("ui_left")) { Value -= Step; }
        }

        ////////////////////
        // Godot 신호 메서드
        ////////////////////

        /// <summary>Focus가 바뀔 때 스핀박스 레이블의
        /// 꾸밈 여부도 같이 바꿔줍니다.</summary>
        private void OnFocusChange() => spinBoxLabel.Formatted = HasFocus();

        ////////////////////
        // 메서드
        ////////////////////

        /// <summary>주어진 정수를 최소값, 최대값,
        /// 그리고 변경 단위에 맞춰 반환합니다.</summary>
        /// <param name="value">변경할 정수</param>
        private int RoundToStep(int value)
        {
            value = Mathf.Clamp(value, MinValue, MaxValue);
            int offset = (value - MinValue) % Step;

            if (offset == 0)
            {
                return value;
            }
            else
            {
                if (offset <= Step / 2)
                {
                    return value - offset;
                }
                else
                {
                    return value + (Step - offset);
                }
            }
        }
    }
}
