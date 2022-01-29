using Godot;
using Godot.Collections;


namespace Game.MainMenuScene
{
    public class Options : VBoxContainer
    {
        public override void _Ready()
        {
            Array<Button> buttonArray = new Array<Button>(GetChildren());

            SetButtonFocusChain(buttonArray);
            buttonArray[0].GrabFocus();
        }

        private void SetButtonFocusChain(Array<Button> buttonArray)
        {
            foreach (Button button in buttonArray)
            {
                // 현재 버튼의 인덱스 저장
                int index = buttonArray.IndexOf(button);

                // 이전 버튼을 현재 버튼의 포커스 체인에 등록
                Button buttonAbove;
                try
                {
                    buttonAbove = buttonArray[index - 1];
                }
                catch (System.IndexOutOfRangeException)
                {
                    buttonAbove = buttonArray[buttonArray.Count - 1];
                }
                // 등록할 위치는 위와 왼쪽
                button.FocusNeighbourTop = button.GetPathTo(buttonAbove);
                button.FocusNeighbourLeft = button.GetPathTo(buttonAbove);

                // 다음 버튼을 현재 버튼의 포커스 체인에 등록
                Button buttonBelow;
                try
                {
                    buttonBelow = buttonArray[index + 1];
                }
                catch (System.IndexOutOfRangeException)
                {
                    buttonBelow = buttonArray[0];
                }
                // 등록할 위치는 아래와 오른쪽
                button.FocusNeighbourBottom = button.GetPathTo(buttonBelow);
                button.FocusNeighbourRight = button.GetPathTo(buttonBelow);
            }
        }
    }
}
