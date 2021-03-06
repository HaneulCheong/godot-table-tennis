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
        private float Speed { get; set; } = Global.Settings["Paddle_Speed"];

        /// <value>눌렀을 때 위로 올라갈 Input키</value>
        [Export]
        private string UpAction { get; set; } = "ui_up";

        /// <value>눌렀을 때 아래로 내려갈 Input키</value>
        [Export]
        private string DownAction { get; set; } = "ui_down";

        /// <value>게임 내 픽셀 높이</value>
        public float Height
        {
            get
            {
                ColorRect sprite = GetNode<ColorRect>("ColorRect");
                return Scale.y * sprite.RectSize.y;
            }
            set
            {
                ColorRect sprite = GetNode<ColorRect>("ColorRect");
                Scale = new Vector2(
                    Scale.x, value / sprite.RectSize.y
                );
            }
        }

        /// <value>상대 속도</value>
        public virtual Vector2 Velocity
        {
            get => new Vector2(0, Input.GetAxis(UpAction, DownAction));
        }

        /// <value>초기 위치</value>
        private Vector2 InitialPosition { get; set; } = Vector2.Zero;

        ////////////////////
        // Godot 메서드
        ////////////////////

        public override void _Ready()
        {
            Height = Global.Settings["Paddle_Size"] * 10;
            AddToGroup("MatchPointGroup");
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
