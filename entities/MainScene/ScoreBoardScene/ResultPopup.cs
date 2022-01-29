using System;
using Godot;


namespace Game.MainScene.ScoreBoardScene
{
    /// <summary>경기 결과를 표시하는 <c>Label</c> 노드</summary>
    public class ResultPopup : Popup, IMatchPointGroup
    {
        ////////////////////
        // Godot 메서드
        ////////////////////

        /// <summary>이 노드의 <c>_Ready</c> 메서드입니다.</summary>
        public override void _Ready()
        {
            AddToGroup("MatchPointGroup");
            Reset();
        }

        ////////////////////
        // 메서드
        ////////////////////

        /// <summary>이 노드를 드러낸 뒤, 게임 결과에 따라
        /// 승리 또는 패배 메시지를 출력합니다.</summary>
        public void MatchPoint()
        {
            ScoreBoard scoreBoard = GetParent<ScoreBoard>();
            string result;

            Visible = true;
            if (scoreBoard.PlayerOneScore == GetNode<Global>("Global").MatchPoint)
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
            else if (scoreBoard.PlayerTwoScore == GetNode<Global>("Global").MatchPoint)
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

            GetNode<Label>("Result").Text = result;
        }

        /// <summary>이 노드를 숨깁니다.</summary>
        public void Reset()
        {
            Visible = false;
        }
    }
}
