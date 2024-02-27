using UnityEngine;
using QFramework;
using System.Collections.Generic;

namespace ProjectSurvivor
{
	public partial class SimpleAbility : ViewController
	{
		private float mCurrentSeconds = 0;
        private float mAbAttackDis = 5.0f;

        private void Start()
        {
            abilityCircleCollider2D.radius = mAbAttackDis;
            abilityCircleCollider2D.OnTriggerEnter2DEvent((collider)=> 
            {               
                var enemy = collider.GetComponentInParent<Enemy>();
                if (enemy != null)
                {
                    enemy.Hurt(Global.SimpleAbilityDamage.Value);
                }
            }).UnRegisterWhenGameObjectDestroyed(this);
        }

        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;

            if (mCurrentSeconds >= Global.SimpleAbilityDuration.Value)            
            {
                mCurrentSeconds = 0;
                abilityCircleCollider2D.enabled = true;
                ActionKit.Delay(0.2f, () =>
                {
                    abilityCircleCollider2D.enabled = false;
                }).Start(this);

                //var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

                //foreach (var enemy in enemies)
                //{
                //    var distance = (transform.position - enemy.transform.position).magnitude;

                //    if (distance <= mAbAttackDis)
                //    {
                //        enemy.Sprite.color = Color.red;
                //        var enemyRefCache = enemy;
                //        ActionKit.Delay(mAbDurationTime, () =>
                //        {
                //            enemyRefCache.Sprite.color = Color.white;
                //        }).StartGlobal();
                //    }
                //}               
            }
        }

        private void OnDestroy()
        {
        }
    }
}
