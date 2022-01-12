using System;
using Godot;


/// <summary>게임 전체를 관리하는 노드</summary>
public class Level : Node
{
    ////////////////////
    // 상수
    ////////////////////

    /// <summary>매치를 이기기 위해 필요한 점수, 즉 매치포인트</summary>
    public const int MatchPoint = 11;

    ////////////////////
    // 속성
    ////////////////////

    /// <value>플레이어 점수</value>
    public int PlayerScore { get; private set; } = 10;
    /// <value>적 점수</value>
    public int OpponentScore { get; private set; } = 10;

    ////////////////////
    // Godot 메서드
    ////////////////////

    /// <summary>Godot 내장 <c>_Process</c> 메소드입니다.</summary>
    public override void _Process(float delta)
    {
        // ESC 키를 누를 경우 게임을 종료합니다.
        if (Input.IsActionPressed("ui_cancel"))
        {
            GetTree().Quit();
        }

        // 스페이스 바를 누를 경우 점수를 초기화하고 게임을 다시 시작합니다.
        if (Input.IsActionPressed("ui_select"))
        {
            PlayerScore = 0;
            OpponentScore = 0;
            GetTree().CallGroup("MatchPointGroup", "Reset");
        }

        // 현재 양 쪽의 점수를 각자의 Label 노드로 전달합니다.
        GetNode<Label>("PlayerScore").Text = PlayerScore.ToString();
        GetNode<Label>("OpponentScore").Text = OpponentScore.ToString();
    }

    ////////////////////
    // Godot 시그널 메서드
    ////////////////////

    /// <summary>
    /// <c>LeftArea</c> 노드에 다른 오브젝트(여기서는 공)이
    /// 닿았을 경우의 시그널 의해 호출됩니다.
    /// </summary>
    private void _OnLeftAreaBodyEntered(object body)
    {
        OpponentScore++;
        // OpponentScore가 매치 포인트가 아닐 경우
        if (OpponentScore < MatchPoint)
        {
            GetNode<Timer>("NextGameTimer").Start();
        }
        // OpponentScore가 매치 포인트일 경우
        else
        {
            GetTree().CallGroup("MatchPointGroup", "MatchPoint");
        }
    }

    /// <summary>
    /// <c>RightArea</c> 노드에 다른 오브젝트(여기서는 공)이
    /// 닿았을 경우의 시그널 의해 호출됩니다.
    /// </summary>
    private void _OnRightAreaBodyEntered(object body)
    {
        PlayerScore++;
        // PlayerScore가 매치 포인트가 아닐 경우
        if (PlayerScore < MatchPoint)
        {
            GetNode<Timer>("NextGameTimer").Start();
        }
        // PlayerScore가 매치 포인트일 경우
        else
        {
            GetTree().CallGroup("MatchPointGroup", "MatchPoint");
        }
    }

    /// <summary>다음 게임을 준비합니다.</summary>
    private void _OnNextGameTimerTimeout()
    {
        GetNode<Opponent>("Opponent").AdjustSpeed();
        GetNode<Ball>("Ball").Reset();
    }
}
