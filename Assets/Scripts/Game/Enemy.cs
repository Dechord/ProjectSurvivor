using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Enemy : ViewController
	{
		public float MovementSpeed = 2.0f;
        public float HP = 3;

        private Transform playerTrans;
        private void Awake()
        {
            playerTrans = GameObject.Find("Player").transform;

            EnemyGenerator.EnemyCount.Value++;
        }


        private void FixedUpdate()
        {
            if (playerTrans != null)
            {
                var direction = (playerTrans.position - transform.position).normalized;

                SelfRigidbody2D.velocity = direction * MovementSpeed;
            }
            else 
            {
                SelfRigidbody2D.velocity = Vector2.zero;
            }
        }

        private void Update()
        {           
            if (HP <= 0)
            {
                Global.GeneratePowUp(gameObject);
                this.DestroyGameObjGracefully();              
            }
        }

        private bool isIngore = false;
        public void Hurt(float value)
        {
            if (isIngore) return;
            isIngore = true;
            Sprite.color = Color.red;
            //
            AudioKit.PlaySound("Hit");
            //
            FloaingTextController.Play(transform.position + Vector3.up * 0.5f, value.ToString());
            //
            ActionKit.Delay(0.2f, () =>
            {
                HP -= value;
                Sprite.color = Color.white;
                isIngore = false;
            }).Start(this);
        }

        private void OnDestroy()
        {
            EnemyGenerator.EnemyCount.Value--;
        }
    }
}
