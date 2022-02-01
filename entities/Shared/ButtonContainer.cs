using System;
using System.Collections.Generic;
using Godot;


namespace Game.Shared
{
    /// <summary>선택지 버튼을 나열할 컨테이너 노드</summary>
    public class ButtonContainer : Container
    {
        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Ready()
        {
            // 드러나있는 모든 버튼의 목록 생성
            var visibleButtons = new List<Button>();
            foreach (Button button in GetChildren())
            {
                if (button.Visible) { visibleButtons.Add(button); }
            }
            SetButtonFocusChain(visibleButtons);

            visibleButtons[0].GrabFocus();
        }

        ////////////////////
        // 메서드
        ////////////////////

        /// <summary>이 노드가 <c>VBoxContainer</c>나 <c>HBoxContainer</c>면
        /// 소속 버튼 중 첫 버튼과 마지막 버튼을 이어줍니다.</summary>
        /// <param name="buttons">사슬을 설정할 버튼의 목록</param>
        private void SetButtonFocusChain(List<Button> buttons)
        {
            Button firstButton = buttons[0];
            Button lastButton = buttons[buttons.Count - 1];
            NodePath firstButtonPath = lastButton.GetPathTo(firstButton);
            NodePath lastButtonPath = firstButton.GetPathTo(lastButton);

            if (this.GetClass() == nameof(VBoxContainer))
            {
                firstButton.FocusNeighbourTop = lastButtonPath;
                lastButton.FocusNeighbourBottom = firstButtonPath;
            }
            else if (this.GetClass() == nameof(HBoxContainer))
            {
                firstButton.FocusNeighbourLeft = lastButtonPath;
                lastButton.FocusNeighbourRight = firstButtonPath;
            }
        }
    }
}
