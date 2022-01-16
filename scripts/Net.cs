using Godot;


/// <summary>경기장 중앙 네트를 구현하는 <c>Sprite</c> 노드</summary>
public class Net : Sprite, IMatchPointGroup
{
    ////////////////////
    // Godot 메서드
    ////////////////////

    /// <summary>이 노드의 <c>_Ready</c> 메서드입니다.</summary>
    public override void _Ready()
    {
        AddToGroup("MatchPointGroup");
    }

    ////////////////////
    // 메서드
    ////////////////////

    /// <summary>이 노드를 숨깁니다.</summary>
    public void MatchPoint()
    {
        Visible = false;
    }

    /// <summary>이 노드를 드러냅니다.</summary>
    public void Reset()
    {
        Visible = true;
    }
}
