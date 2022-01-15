using System;
using Godot;


/// <summary>게임 전체를 관리하는 노드</summary>
public class Level : Node
{
    ////////////////////
    // 상수
    ////////////////////

    /// <summary>매치를 이기기 위해 필요한 점수, 즉 매치 포인트</summary>
    public const int MatchPoint = 11;

    ////////////////////
    // 필드
    ////////////////////

    private int _playerScore = 0;
    private int _opponentScore = 0;

    ////////////////////
    // 속성
    ////////////////////

    /// <value>플레이어 점수.</value>
    public int PlayerScore
    {
        get { return _playerScore; }
        private set
        {
            /// 음수가 전달되면 변경하지 않음
            if (value < 0)
            {
                GD.Print(new ArgumentOutOfRangeException(value.ToString()));
                return;
            }
            /// 변경할 때 PlayerScore 노드의 Text도 같이 변경
            _playerScore = value;
            GetNode<Label>("PlayerScore").Text = PlayerScore.ToString();
        }
    }

    /// <value>적 점수</value>
    public int OpponentScore
    {
        get { return _opponentScore; }
        private set
        {
            /// 음수가 전달되면 변경하지 않음
            if (value < 0)
            {
                GD.Print(new ArgumentOutOfRangeException(value.ToString()));
                return;
            }
            /// 변경할 때 OpponentScore 노드의 Text도 같이 변경
            _opponentScore = value;
            GetNode<Label>("OpponentScore").Text = OpponentScore.ToString();
        }
    }

    ////////////////////
    // Godot 메서드
    ////////////////////

    /// <summary>이 노드의 <c>_Ready</c> 메서드입니다.</summary>
    public override void _Ready()
    {
        // 점수 초기화
        PlayerScore = 0;
        OpponentScore = 0;
    }

    /// <summary>이 노드의 <c>_Process</c> 메서드입니다.</summary>
    public override void _Process(float delta)
    {
        // "ui_exit"로 게임 종료
        if (Input.IsActionJustPressed("ui_exit"))
        {
            GetTree().Quit();
        }

        // "ui_restart"로 점수 초기화 뒤 게임 다시 시작
        if (Input.IsActionJustPressed("ui_restart"))
        {
            PlayerScore = 0;
            OpponentScore = 0;
            GetTree().CallGroup("MatchPointGroup", "Reset");
        }

        // "ui_fullscreen"으로 전체화면 모드 켜기/끄기
        if (Input.IsActionJustPressed("ui_fullscreen"))
        {
            OS.WindowFullscreen = !OS.WindowFullscreen;
        }
    }

    ////////////////////
    // Godot 시그널 메서드
    ////////////////////

    /// LeftArea 노드에 다른 오브젝트(여기서는 공)이
    /// 닿았을 경우의 시그널 의해 호출됩니다.
    private void _OnLeftAreaBodyEntered(object body)
    {
        // 적 득점
        OpponentScore++;
        GetNode<AudioStreamPlayer>("Scored").Play();

        // OpponentScore가 매치 포인트가 아닐 경우:
        if (OpponentScore < MatchPoint)
        {
            GetNode<Timer>("NextGameTimer").Start();
        }
        // OpponentScore가 매치 포인트일 경우:
        else
        {
            GetTree().CallGroup("MatchPointGroup", "MatchPoint");
        }
    }

    /// RightArea 노드에 다른 오브젝트(여기서는 공)이
    /// 닿았을 경우의 시그널에 의해 호출됩니다.
    private void _OnRightAreaBodyEntered(object body)
    {
        // 플레이어 득점
        PlayerScore++;
        GetNode<AudioStreamPlayer>("Scored").Play();

        // PlayerScore가 매치 포인트가 아닐 경우:
        if (PlayerScore < MatchPoint)
        {
            GetNode<Timer>("NextGameTimer").Start();
        }
        // PlayerScore가 매치 포인트일 경우:
        else
        {
            GetTree().CallGroup("MatchPointGroup", "MatchPoint");
        }
    }

    /// NextGameTimer 노드의 Timeout 시그널에 의해 호출됩니다.
    private void _OnNextGameTimerTimeout()
    {
        GetNode<Opponent>("Opponent").AdjustSpeed();
        GetNode<Ball>("Ball").Reset();
    }
}
