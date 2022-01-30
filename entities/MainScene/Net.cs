using Godot;


namespace Game.MainScene
{
    /// <summary>경기장 중앙 네트를 구현하는 <c>Sprite</c> 노드</summary>
    public class Net : Sprite, IMatchPointGroup
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

        public void MatchPoint() => Hide();

        public void Reset() => Show();
    }
}
