using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class GameStartController : ViewController
	{
        void Start()
		{
			UIKit.OpenPanel<UIGameStartOnlyPanel>();
		}
	}
}
