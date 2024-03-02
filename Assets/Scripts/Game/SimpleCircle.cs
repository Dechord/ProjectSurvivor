using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class SimpleCircle : ViewController
	{
		[SerializeField]
		private float radius = 3.0f;
		void Start()
		{
			Circle.OnTriggerEnter2DEvent((collider)=> 
			{
				var hurtBox = collider.GetComponent<HurtBox>();
				if (hurtBox)
				{
					if (hurtBox.Owner.CompareTag("Enemy"))
					{
						hurtBox.Owner.GetComponent<Enemy>().Hurt(2);
					}					
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

        private void Update()
        {
			var degree = Time.frameCount;
			var locationPos = new Vector2(-Mathf.Cos(degree * Mathf.Deg2Rad) * radius, Mathf.Sin(degree * Mathf.Deg2Rad) * radius);
			Circle.LocalPosition(locationPos.x, locationPos.y);
		}
    }
}
