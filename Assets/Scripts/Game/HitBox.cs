using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class HitBox : ViewController
	{
		public GameObject Owner;
		void Start()
		{
			// Code Here
			if (!Owner)
			{
				Owner = transform.parent.gameObject;
			}
		}
	}
}
