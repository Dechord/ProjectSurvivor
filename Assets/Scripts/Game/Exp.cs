using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Exp : ViewController
	{
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<CollectableArea>())
            {
                Global.Exp.Value++;
                this.DestroyGameObjGracefully();
            }
        }
    }
}
