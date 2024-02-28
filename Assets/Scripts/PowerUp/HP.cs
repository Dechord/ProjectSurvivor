using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class HP : ViewController
	{
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<CollectableArea>())
            {
                if (Global.HP.Value != Global.MaxHP.Value)
                {
                    AudioKit.PlaySound("HP");
                    Global.HP.Value++;
                    this.DestroyGameObjGracefully();
                }              
            }
        }
    }
}
