using Godot;


namespace Game
{
    public class Global : Node
    {
        /// <value>경기를 이기기 위해 필요한 점수, 즉 매치 포인트</value>
        [Export]
        public int MatchPoint { get; private set; } = 11;
    }
}
