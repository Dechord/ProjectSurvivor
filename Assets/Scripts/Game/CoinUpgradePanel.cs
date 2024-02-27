using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class CoinUpgradePanel : ViewController
	{
        private void Awake()
        {
            BtnCoinPanelClose.onClick.AddListener(()=> 
            {
                this.Hide();
            });

            if (Global.Coin.Value >= 5)
            {
                BtnCoinPercentUpgrade.Show();
                BtnExpPercentUpgrade.Show();
            }
            else
            {
                BtnCoinPercentUpgrade.Hide();
                BtnExpPercentUpgrade.Hide();
            }

            BtnCoinPercentUpgrade.onClick.AddListener(() => {
                Global.Coin.Value -= 5;
                Global.CoinPercent.Value += 0.1f;

            });

            BtnExpPercentUpgrade.onClick.AddListener(() => {
                Global.Coin.Value -= 5;
                Global.ExpPercent.Value += 0.1f;
            });

            Global.Coin.RegisterWithInitValue((coin)=> 
            {
                CoinText.text = $"½ð±Ò£º{coin}";
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}
