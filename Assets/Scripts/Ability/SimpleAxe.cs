using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class SimpleAxe : ViewController
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
                Axe.Instantiate()
                    .Show()
                    .Position(this.Position())
                    .Self((self) =>
                    {
                        var rigidbody2d = self.GetComponent<Rigidbody2D>();
                        var randomX = RandomUtility.Choose(-8,-5,-3,3,5,8);
                        var randomY = RandomUtility.Choose(3, 5, 8);
                        rigidbody2d.velocity = new Vector2(randomX, randomY);

                        self.OnTriggerEnter2DEvent((collider)=> 
                        {
                            var hurtBox = collider.GetComponent<HurtBox>();
                            if (hurtBox)
                            {
                                if (hurtBox.Owner.CompareTag("Enemy"))
                                {
                                    hurtBox.Owner.GetComponent<IEnemy>().Hurt(2);
                                }
                            }
                        }).UnRegisterWhenGameObjectDestroyed(self);
                        //
                        ActionKit.OnUpdate.Register(() =>
                        {
                            if (playerTrans)
                            {
                                if (playerTrans.position.y - self.position.y > 15)
                                {
                                    self.DestroyGameObjGracefully();
                                }
                            }
                        }).UnRegisterWhenGameObjectDestroyed(self);
                    });
                
                mCurrentDuration = 0;
            }
        }
    }
}
