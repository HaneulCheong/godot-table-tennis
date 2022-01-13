using Godot;


/// <summary>경기 결과를 표시하는 <c>Label</c> 노드</summary>
public class MatchResult : Label, IMatchPointGroup
{
    ////////////////////
    // 메서드
    ////////////////////

    /// <summary>이 노드의 <c>_Ready</c> 메소드입니다.</summary>
    public override void _Ready()
    {
        Reset();
    }

    /// <summary>이 노드를 드러낸 뒤, 게임 결과에 따라
    /// 승리 또는 패배 메시지를 출력합니다.</summary>
    public void MatchPoint()
    {
        Visible = true;
        if (GetNode<Level>("..").PlayerScore == Level.MatchPoint)
        {
            Text = "You Win!";
        }
        else if (GetNode<Level>("..").OpponentScore == Level.MatchPoint)
        {
            Text = "You Lose!";
        }
        else
        {
            Text = "Error!";
        }
    }

    /// <summary>이 노드를 숨깁니다.</summary>
    public void Reset()
    {
        Visible = false;
    }
}
