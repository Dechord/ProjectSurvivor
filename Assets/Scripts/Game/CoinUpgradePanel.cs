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

            if (Global.Coin.Value >= -500)
            {
                BtnCoinPercentUpgrade.Show();
                BtnExpPercentUpgrade.Show();
                BtnMaxHpUpgrade.Show();
            }
            else
            {
                BtnCoinPercentUpgrade.Hide();
                BtnExpPercentUpgrade.Hide();
                BtnMaxHpUpgrade.Hide();
            }

            BtnCoinPercentUpgrade.onClick.AddListener(() => {
                Global.Coin.Value -= 5;
                Global.CoinPercent.Value += 0.1f;
                //
                AudioKit.PlaySound("AbilityLevelUp");

            });

            BtnExpPercentUpgrade.onClick.AddListener(() => {
                Global.Coin.Value -= 5;
                Global.ExpPercent.Value += 0.1f;
                //
                AudioKit.PlaySound("AbilityLevelUp");
            });

            BtnMaxHpUpgrade.onClick.AddListener(() => {
                Global.Coin.Value -= 30;
                Global.MaxHP.Value ++;
                //
                AudioKit.PlaySound("AbilityLevelUp");
            });

            Global.Coin.RegisterWithInitValue((coin)=> 
            {
                CoinText.text = $"½ð±Ò£º{coin}";
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}
