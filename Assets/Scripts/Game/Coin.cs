using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Coin : ViewController
	{
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<CollectableArea>())
            {
                AudioKit.PlaySound("Coin");
                Global.Coin.Value++;
                this.DestroyGameObjGracefully();
            }
        }
    }
}
