using UnityEngine;
using QFramework;
using System.Linq;

namespace ProjectSurvivor
{
	public partial class SimpleKnife : ViewController
	{
		private Transform playerTrans;
		public float mDuration = 1.0f;
		private float mCurrentDuration;
		private void Awake()
        {
			playerTrans = GameObject.Find("Player").transform;

		}

        private void Update()
        {
			mCurrentDuration += Time.deltaTime;

			if (mCurrentDuration >= mDuration)
			{

				var enemy = FindObjectsOfType<Enemy>(false).OrderBy(enemy => (enemy.Position() - playerTrans.position).magnitude).FirstOrDefault();
				if (enemy)
				{
					Knife.Instantiate()
					.Show()
					.Position(this.Position())
					.Self((self) =>
					{
						if (playerTrans)
						{

							var direction = (enemy.Position() - playerTrans.position).normalized;
							var velocity = direction * 10;
							self.GetComponent<Rigidbody2D>().velocity = velocity;

							self.OnTriggerEnter2DEvent((collider) =>
							{
								var hurtBox = collider.GetComponent<HurtBox>();
								if (hurtBox)
								{
									if (hurtBox.Owner.CompareTag("Enemy"))
									{
										hurtBox.Owner.GetComponent<IEnemy>().Hurt(5);
										self.DestroyGameObjGracefully();
									}
								}
							}).UnRegisterWhenGameObjectDestroyed(self);

							//
							ActionKit.OnUpdate.Register(() =>
							{
								if ((self.Position() - playerTrans.position).magnitude > 20)
								{
									self.DestroyGameObjGracefully();
								}
							}).UnRegisterWhenGameObjectDestroyed(self);
						}
					});

					mCurrentDuration = 0;
				}
			}
		}
    }
}
