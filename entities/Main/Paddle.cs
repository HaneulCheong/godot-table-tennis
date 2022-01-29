using Godot;


namespace Game.MainScene
{
    /// <summary>모든 라켓의 기본이 되는
    /// 추상 <c>KinematicBody2D</c> 클래스</summary>
    public abstract class Paddle : KinematicBody2D, IMatchPointGroup
    {
        ////////////////////
        // 상수
        ////////////////////

        /// <summary>초기 속력</summary>
        protected const float initialSpeed = 600;

        ////////////////////
        // 속성
        ////////////////////

        /// <value>게임 내 스프라이트의 높이</value>
        public float Height
        {
            get
            {
                Sprite sprite = GetNode<Sprite>("Sprite");
                return ((float)(sprite.Texture.GetHeight()) * sprite.Scale.y);
            }
        }

        /// <value>이동 방향</value>
        public abstract Vector2 Direction { get; }

        /// <value>기준 속력</value>
        protected float Speed { get; set; } = initialSpeed;

        /// <value>초기 위치</value>
        private Vector2 InitialPosition { get; set; }

        ////////////////////
        // Godot 메서드
        ////////////////////

        /// <summary>이 노드의 <c>_Ready</c> 메서드입니다.</summary>
        public override void _Ready()
        {
            AddToGroup("MatchPointGroup");
            // 초기 위치 저장
            InitialPosition = Position;
            Reset();
        }

        /// <summary>이 노드의 <c>_PhysicsProcess</c> 메서드입니다.</summary>
        public override void _PhysicsProcess(float delta)
        {
            MoveAndCollide(Direction * Speed * delta);
            // 밀렸을 경우 원위치 복귀
            if (Position.x != InitialPosition.x)
            {
                Position = new Vector2(InitialPosition.x, Position.y);
            }
        }

        ////////////////////
        // 메서드
        ////////////////////

        /// <summary>이 노드를 숨깁니다.</summary>
        public void MatchPoint()
        {
            Visible = false;
        }

        /// <summary>이 노드를 드러낸 뒤 초기 위치로 돌아갑니다.</summary>
        virtual public void Reset()
        {
            Visible = true;
            Position = InitialPosition;
        }
    }
}
