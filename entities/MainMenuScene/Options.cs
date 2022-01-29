using System;
using System.Collections.Generic;
using Godot;


namespace Game.MainMenuScene
{
    public class Options : VBoxContainer
    {
        public override void _Ready()
        {
            var buttonList = new List<Button>();
            foreach (Button button in GetChildren())
            {
                if (button.Visible) { buttonList.Add(button); }
            }

            SetButtonFocusChain(buttonList);
            buttonList[0].GrabFocus();
        }

        private void SetButtonFocusChain(List<Button> buttonList)
        {
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
