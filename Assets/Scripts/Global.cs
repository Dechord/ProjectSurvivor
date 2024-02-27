using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public class Global
    {
        public static BindableProperty<int> Exp = new BindableProperty<int>(0);

        public static BindableProperty<int> Coin = new BindableProperty<int>(0);

        public static BindableProperty<int> Level = new BindableProperty<int>(1);

        public static BindableProperty<float> SimpleAbilityDamage = new BindableProperty<float>(1.0f);
        public static BindableProperty<float> SimpleAbilityDuration = new BindableProperty<float>(1.5f);

        public static BindableProperty<float> CurrentSeconds = new BindableProperty<float>(0);

        public static BindableProperty<float> ExpPercent = new BindableProperty<float>(0.3f);
        public static BindableProperty<float> CoinPercent = new BindableProperty<float>(0.1f);

        public static void ResetData()
        {
            Exp.Value = 0;
            Level.Value = 1;
            SimpleAbilityDamage.Value = 1.0f;
            SimpleAbilityDuration.Value = 1.5f;
            CurrentSeconds.Value = 0;
        }

        [RuntimeInitializeOnLoadMethod]
        public static void AutoInit()
        {
            Global.Coin.Value = PlayerPrefs.GetInt("coin", 0);
            Global.Coin.Register((coin) =>
            {
                PlayerPrefs.SetInt(nameof(coin), coin);
            });

            Global.ExpPercent.Value = PlayerPrefs.GetFloat("expPercent", 0.3f);
            Global.ExpPercent.Register((expPercent) =>
            {
                PlayerPrefs.SetFloat(nameof(expPercent), expPercent);
            });

            Global.CoinPercent.Value = PlayerPrefs.GetFloat("coinPercent", 0.1f);
            Global.CoinPercent.Register((coinPercent) =>
            {
                PlayerPrefs.SetFloat(nameof(coinPercent), coinPercent);
            });
        }

        public static void GeneratePowUp(GameObject gameObject)
        {
            var percent = Random.Range(0, 1.0f);
            if (percent <= Global.ExpPercent.Value)
            {
                //生成经验值
                PowerUpManager.Default.Exp.Instantiate().Position(gameObject.Position()).Show();
                return;
            }

            percent = Random.Range(0, 1.0f);
            if (percent <= Global.CoinPercent.Value)
            {
                //生成金币
                PowerUpManager.Default.Coin.Instantiate().Position(gameObject.Position()).Show();
            }
        }

        public static int ExpToNextLv => Level.Value * 5;
    }
}
