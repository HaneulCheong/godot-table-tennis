using System;
using Godot;


namespace Game.MainScene.UserInterfaceScene
{
    /// <summary>경기 결과를 표시하는 팝업</summary>
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

        /// <summary>주어진 점수에 따라 승리 메시지를 반환합니다.</summary>
        /// <param name="playerOneScore">플레이어 1의 점수</param>
        /// <param name="playerTwoScore">플레이어 2의 점수</param>
        /// <exception cref="InvalidOperationException">
        /// 플레이어들의 점수가 같음</exception>
        private string ResultMessage(int playerOneScore, int playerTwoScore)
        {
            string result = null;

            // 플레이어 1이 승리했으면:
            if (playerOneScore > playerTwoScore)
            {
                result = (
                    playerTwoScore > 0 ? "Player 1 Wins!" : "Player 1 Perfect!"
                );
            }
            // 플레이어 2가 승리했으면:
            else if (playerOneScore < playerTwoScore)
            {
                result = (
                    playerOneScore > 0 ? "Player 2 Wins!" : "Player 2 Perfect!"
                );
            }
            // 점수가 같으면 InvalidOperationException
            else
            {
                GD.PushError(new InvalidOperationException().ToString());
                result = "Error!";
            }

            return result;
        }

        /// <summary>승리 메시지를 출력하며 팝업합니다.</summary>
        public void MatchPoint()
        {
            UserInterface scoreBoard = GetParent<UserInterface>();

            GetNode<Label>("VBoxContainer/Result/Label").Text = ResultMessage(
                scoreBoard.PlayerOneScore, scoreBoard.PlayerTwoScore
            );
            PopupCentered();
        }

        /// <summary>이 노드를 숨깁니다.</summary>
        public void Reset() => Hide();
    }
}
