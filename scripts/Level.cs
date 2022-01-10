using Godot;


/// <summary>게임 전체를 관리하는 노드</summary>
public class Level : Node
{
    /// <summary>플레이어 점수</summary>
    private int PlayerScore { get; set; }
    /// <summary>적 점수</summary>
    private int OpponentScore { get; set; }

    public override void _Process(float delta)
    {
        // ESC 키를 누를 경우 게임을 종료합니다.
        if (Input.IsActionPressed("ui_cancel"))
        {
            this.GetTree().Quit();
        }

        // 스페이스 바를 누를 경우 점수를 초기화하고 게임을 다시 시작합니다.
        if (Input.IsActionPressed("ui_select"))
        {
            PlayerScore = 0;
            OpponentScore = 0;
            this.Reset();
        }

        this.GetNode<Label>("PlayerScore").Text = this.PlayerScore.ToString();
        this.GetNode<Label>("OpponentScore").Text = this.OpponentScore.ToString();
    }

    /// <summary><c>LeftArea</c> 노드에 다른 오브젝트(여기서는 공)이
    /// 닿았을 경우 Signal에 의해 호출됩니다.</summary>
    private void _OnLeftAreaBodyEntered(object body)
    {
        this.OpponentScore++;
        this.Reset();
    }

    /// <summary><c>RightArea</c> 노드에 다른 오브젝트(여기서는 공)이
    /// 닿았을 경우 Signal에 의해 호출됩니다.</summary>
    private void _OnRightAreaBodyEntered(object body)
    {
        this.PlayerScore++;
        this.Reset();
    }

    /// <summary>게임을 다시 시작합니다.</summary>
    private void Reset()
    {
        this.GetNode<Ball>("Ball").Reset();
        this.GetNode<Paddle>("Player").Reset();
        this.GetNode<Paddle>("Opponent").Reset();
    }
}
