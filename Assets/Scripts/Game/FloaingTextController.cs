using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace ProjectSurvivor
{
	public partial class FloaingTextController : ViewController
	{
		private static FloaingTextController Instance;

        private void Awake()
        {
			Instance = this;
		}

        private void OnDestroy()
        {
			Instance = null;
        }

        void Start()
		{
			FloatingText.Hide();
		}

		public static void Play(Vector2 pos,string content)
		{
			Instance.FloatingText.InstantiateWithParent(Instance.transform)
				.Position(pos.x,pos.y)
				.Self((f)=> 
				{
					var positionY = pos.y;
					var text = f.Find("Text").GetComponent<Text>();
					text.text = content;
					//
					ActionKit.Sequence().Lerp(0, 0.5f, 0.5f, (p) =>
					{
						f.PositionY(positionY + p * 0.25f);
						text.LocalScaleX(Mathf.Clamp01(p * 8));
						text.LocalScaleY(Mathf.Clamp01(p * 8));
					})
					.Delay(0.5f)
					.Lerp(1.0f, 0, 0.3f, (a)=> 
					{
						text.ColorAlpha(a);
					},()=> 
					{
						f.DestroyGameObjGracefully();
					}).Start(text);

				}).Show();
		}
	}
}
