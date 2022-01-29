namespace Game.MainScene
{
    /// <summary>MatchPointGroup에 속한 노드들의 인터페이스</summary>
    public interface IMatchPointGroup
    {
        ////////////////////
        // Godot 메서드
        ////////////////////

        /// <summary>이 노드의 <c>_Ready</c> 메서드입니다.
        /// AddToGroup()을 포함해야 합니다.</summary>
        void _Ready();

        ////////////////////
        // 메서드
        ////////////////////

        /// <summary>어느 한 쪽이 매치 포인트에 도달했을 경우
        /// <c>Main</c> 노드에 의해 호출됩니다.</summary>
        void MatchPoint();

        /// <summary>게임을 다시 시작할 때
        /// <c>Main</c> 노드에 의해 호출됩니다.</summary>
        void Reset();
    }
}
