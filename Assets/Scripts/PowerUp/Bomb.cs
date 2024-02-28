using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Bomb : ViewController
	{
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<CollectableArea>())
            {
                foreach (var enemy in GameObject.FindObjectsOfType<Enemy>(false))
                {
                    enemy.Hurt(enemy.HP);
                }
                //
                CameraController.Shake();
                //
                AudioKit.PlaySound("Bomb");
                this.DestroyGameObjGracefully();
            }
        }
    }
}
