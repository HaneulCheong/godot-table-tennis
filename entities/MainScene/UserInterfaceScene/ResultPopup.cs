using System;
using Godot;


namespace Game.MainScene.UserInterfaceScene
{
    /// <summary>경기 결과를 표시하는 <c>Label</c> 노드</summary>
    public class ResultPopup : Popup, IMatchPointGroup
    {
        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Ready()
        {
            AddToGroup("MatchPointGroup");
            Reset();
        }

        ////////////////////
        // 메서드
        ////////////////////

        /// <summary>이 노드를 드러낸 뒤, 게임 결과에 따라
        /// 승리 메시지를 출력합니다.</summary>
        /// <exception cref="InvalidOperationException">매치 포인트에 도달한
        /// 플레이어가 없음에도 이 메소드가 호출됨</exception>
        public void MatchPoint()
        {
            UserInterface scoreBoard = GetParent<UserInterface>();
            string result;

            Show();
            // 플레이어 1이 승리했을 경우:
            if (
                scoreBoard.PlayerOneScore
                == GetNode<Global>("/root/Global").MatchPoint
            )
            {
                if (scoreBoard.PlayerTwoScore == 0)
                {
                    result = "Player 1 Perfect!";
                }
                else
                {
                    result = "Player 1 Wins!";
                }
            }
            // 플레이어 2가 승리했을 경우:
            else if (
                scoreBoard.PlayerTwoScore
                == GetNode<Global>("/root/Global").MatchPoint
            )
            {
                if (scoreBoard.PlayerOneScore == 0)
                {
                    result = "Player 2 Perfect!";
                }
                else
                {
                    result = "Player 2 Wins!";
                }
            }
            else
            {
                GD.PushError(new InvalidOperationException().ToString());
                result = "Error!";
            }

            GetNode<Label>("VBoxContainer/Result/Label").Text = result;
        }

        public void Reset() => Hide();
    }
}
