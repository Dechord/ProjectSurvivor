using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class GetAllExp : ViewController
	{
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<CollectableArea>())
            {
                var playerTrans = GameObject.Find("Player").transform;
                foreach (var exp in GameObject.FindObjectsOfType<Exp>(false))
                {
                    ActionKit.OnUpdate.Register(() =>
                    {
                        if (playerTrans)
                        {
                            var direction = (playerTrans.position - exp.transform.position).normalized;
                            exp.transform.Translate(direction * 5f * Time.deltaTime);
                        }
                    }).UnRegisterWhenGameObjectDestroyed(exp);
                }
                AudioKit.PlaySound("GetAllExp");
                this.DestroyGameObjGracefully();
            }
        }
    }
}
