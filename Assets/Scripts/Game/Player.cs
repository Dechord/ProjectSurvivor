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
                        //
                        Abilities.gameObject.SetActive(false);
                        this.DestroyGameObjGracefully();
                    }                  
                }               
                UIKit.OpenPanel<UIGameOverPanel>();
			}).UnRegisterWhenGameObjectDestroyed(this);
        }


        private void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            Vector2 direction = new Vector2(horizontal, vertical).normalized;
            transform.Translate(direction * MovementSpeed * Time.deltaTime);
        }
    }
}
