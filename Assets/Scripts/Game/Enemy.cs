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


        private void Update()
        {
            if (playerTrans != null)
            {
                var direction = (playerTrans.position - transform.position).normalized;

                transform.Translate(direction * MovementSpeed * Time.deltaTime);
            }

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
