using Godot;


namespace Game.MainScene.PaddleScene
{
    /// <summary>라켓 노드</summary>
    public class Paddle : KinematicBody2D, IMatchPointGroup
    {
        ////////////////////
        // 속성
        ////////////////////

        [Export]
        /// <summary>속력</summary>
        protected float Speed { get; set; } = 600;

        [Export]
        protected string UpAction { get; set; } = "ui_up";

        [Export]
        protected string DownAction { get; set; } = "ui_down";

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
        public virtual Vector2 Velocity
        {
            get => new Vector2(0, Input.GetAxis(UpAction, DownAction));
        }

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
            MoveAndCollide(Velocity * Speed * delta);
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
