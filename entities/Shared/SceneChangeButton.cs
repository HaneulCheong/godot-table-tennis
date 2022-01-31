using System;
using Godot;


namespace Game.Shared
{
    /// <summary>메인 메뉴 버튼 노드</summary>
    public class SceneChangeButton : TextButton
    {
        ////////////////////
        // 속성
        ////////////////////

        /// <value>눌렀을 때 진입할 씬</value>
        [Export(PropertyHint.File, "*.tscn")]
        private string Scene { get; set; } = null;

        ////////////////////
        // Godot 메서드
        ////////////////////

        /// <summary>지정된 <c>Scene</c>에 진입합니다.</summary>
        /// <exception cref="NullReferenceException">
        /// <c>Scene</c>에 지정된 씬이 없음</exception>
        public override void _Pressed()
        {
            if (Scene == null)
            {
                var e = new NullReferenceException(nameof(Scene));
                GD.PushError(e.ToString());
                return;
            }
            GetTree().ChangeScene(Scene);
        }
    }
}
