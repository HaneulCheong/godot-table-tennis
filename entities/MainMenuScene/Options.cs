using System;
using System.Collections.Generic;
using Godot;


namespace Game.MainMenuScene
{
    /// <summary>메인 메뉴 버튼을 나열할 컨테이너 노드</summary>
    public class Options : VBoxContainer
    {
        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Ready()
        {
            // 드러나있는 모든 버튼의 목록 생성
            var buttonList = new List<Button>();
            foreach (Button button in GetChildren())
            {
                if (button.Visible) { buttonList.Add(button); }
            }

            SetButtonFocusChain(buttonList);
            // 목록의 첫 번째 버튼에 Focus
            buttonList[0].GrabFocus();
        }

        ////////////////////
        // 메서드
        ////////////////////

        /// <summary>버튼 사이를 'ui_up'과 'ui_down' 등으로
        /// 이동할 수 있도록 Focus 사슬을 설정합니다.</summary>
        /// <param name="buttonList">사슬을 설정할 버튼의 목록</param>
        private void SetButtonFocusChain(List<Button> buttonList)
        {
            // 각 버튼마다:
            foreach (Button button in buttonList)
            {
                // 현재 버튼의 인덱스 저장
                int index = buttonList.IndexOf(button);

                // 이전 버튼을 현재 버튼의 포커스 체인에 등록
                Button buttonAbove;
                try
                {
                    buttonAbove = buttonList[index - 1];
                }
                catch (Exception e) when (
                    e is IndexOutOfRangeException
                    || e is ArgumentOutOfRangeException
                )
                {
                    buttonAbove = buttonList[buttonList.Count - 1];
                }
                // 등록할 위치는 위와 왼쪽
                button.FocusNeighbourTop = button.GetPathTo(buttonAbove);
                button.FocusNeighbourLeft = button.GetPathTo(buttonAbove);

                // 다음 버튼을 현재 버튼의 포커스 체인에 등록
                Button buttonBelow;
                try
                {
                    buttonBelow = buttonList[index + 1];
                }
                catch (Exception e) when (
                    e is IndexOutOfRangeException
                    || e is ArgumentOutOfRangeException
                )
                {
                    buttonBelow = buttonList[0];
                }
                // 등록할 위치는 아래와 오른쪽
                button.FocusNeighbourBottom = button.GetPathTo(buttonBelow);
                button.FocusNeighbourRight = button.GetPathTo(buttonBelow);
            }
        }
    }
}
