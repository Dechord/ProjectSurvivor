using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Player : ViewController
	{
		public float MovementSpeed = 5.0f;
        private void Awake()
        {
			HurtBox.OnTriggerEnter2DEvent((collider)=> 
			{
                var hitBox = collider.GetComponent<HitBox>();
                if (hitBox)
                {
                    if (hitBox.Owner.CompareTag("Enemy"))
                    {
                        Global.HP.Value--;
                        
                        if (Global.HP.Value <= 0)
                        {
                            AudioKit.PlaySound("Die");
                            //
                            Abilities.gameObject.SetActive(false);
                            this.DestroyGameObjGracefully();
                            //
                            UIKit.OpenPanel<UIGameOverPanel>();
                        }
                        else 
                        {
                            AudioKit.PlaySound("Hurt");
                        }
                    }                  
                }                               
			}).UnRegisterWhenGameObjectDestroyed(this);
        }


        private void FixedUpdate()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            var targetVelocity = new Vector2(horizontal, vertical).normalized * MovementSpeed;


            Rigidbody2D.velocity = Vector2.Lerp(Rigidbody2D.velocity, targetVelocity, 1.0f - Mathf.Exp(-Time.fixedDeltaTime * 5));
        }
    }
}
