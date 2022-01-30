using Godot;


namespace Game.MainScene.PaddleScene
{
    /// <summary>라켓 노드</summary>
    public class Paddle : KinematicBody2D, IMatchPointGroup
    {
        ////////////////////
        // 속성
        ////////////////////

        /// <value>속력</value>
        [Export]
        protected float Speed { get; set; } = 600;

        /// <value>눌렀을 때 위로 올라갈 Input키</value>
        [Export]
        protected string UpAction { get; set; } = "ui_up";

        /// <value>눌렀을 때 아래로 내려갈 Input키</value>
        [Export]
        protected string DownAction { get; set; } = "ui_down";

        /// <value>게임 내 스프라이트의 높이</value>
        public float Height
        {
            get
            {
                Sprite sprite = GetNode<Sprite>("Sprite");
                return (sprite.Texture.GetHeight() * sprite.Scale.y);
            }
        }

        /// <value>상대 속도</value>
        public virtual Vector2 Velocity
        {
            get => new Vector2(0, Input.GetAxis(UpAction, DownAction));
        }

        /// <value>초기 위치</value>
        private Vector2 InitialPosition { get; set; }

        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Ready()
        {
            AddToGroup("MatchPointGroup");
            // 초기 위치 저장
            InitialPosition = Position;
            Reset();
        }

        public override void _PhysicsProcess(float delta)
        {
            // 현재 속도에 맞춰 이동
            MoveAndCollide(Velocity * Speed * delta);
            // 가로로 밀렸을 경우 원위치 복귀
            if (Position.x != InitialPosition.x)
            {
                Position = new Vector2(InitialPosition.x, Position.y);
            }
        }

        ////////////////////
        // 메서드
        ////////////////////

        /// <summary>이 노드를 숨깁니다.</summary>
        public void MatchPoint() => Hide();

        /// <summary>이 노드를 드러낸 뒤 초기 위치로 돌아갑니다.</summary>
        virtual public void Reset()
        {
            Position = InitialPosition;
            Show();
        }
    }
}
