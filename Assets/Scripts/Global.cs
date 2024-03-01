using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public class Global:Architecture<Global>
    {
        public static BindableProperty<int> Exp = new BindableProperty<int>(0);

        public static BindableProperty<int> Coin = new BindableProperty<int>(0);

        public static BindableProperty<int> Level = new BindableProperty<int>(1);

        public static BindableProperty<float> SimpleAbilityDamage = new BindableProperty<float>(1.0f);
        public static BindableProperty<float> SimpleAbilityDuration = new BindableProperty<float>(1.5f);

        public static BindableProperty<float> CurrentSeconds = new BindableProperty<float>(0);

        public static BindableProperty<float> ExpPercent = new BindableProperty<float>(0.3f);
        public static BindableProperty<float> CoinPercent = new BindableProperty<float>(0.1f);

        public static BindableProperty<float> HP = new BindableProperty<float>(1);
        public static BindableProperty<float> MaxHP = new BindableProperty<float>(3);

        public static void ResetData()
        {
            HP.Value = MaxHP.Value;
            Exp.Value = 0;
            Level.Value = 1;
            SimpleAbilityDamage.Value = 1.0f;
            SimpleAbilityDuration.Value = 1.5f;
            CurrentSeconds.Value = 0;
        }

        [RuntimeInitializeOnLoadMethod]
        public static void AutoInit()
        {
            ResKit.Init();
            UIKit.Root.SetResolution(1920, 1080, 1);

            MaxHP.Value = PlayerPrefs.GetFloat("maxHP", 3.0f);
            HP.Value = MaxHP.Value;
            MaxHP.Register((maxHp) =>
            {
                PlayerPrefs.SetFloat("maxHP", maxHp);
            });

            Coin.Value = PlayerPrefs.GetInt("coin", 0);
            Coin.Register((coin) =>
            {
                PlayerPrefs.SetInt(nameof(coin), coin);
            });

            ExpPercent.Value = PlayerPrefs.GetFloat("expPercent", 0.3f);
            ExpPercent.Register((expPercent) =>
            {
                PlayerPrefs.SetFloat(nameof(expPercent), expPercent);
            });

            CoinPercent.Value = PlayerPrefs.GetFloat("coinPercent", 0.1f);
            CoinPercent.Register((coinPercent) =>
            {
                PlayerPrefs.SetFloat(nameof(coinPercent), coinPercent);
            });
        }

        public static void GeneratePowUp(GameObject gameObject)
        {
            var percent = Random.Range(0, 1.0f);
            if (percent <= ExpPercent.Value)
            {
                //生成经验值
                PowerUpManager.Default.Exp.Instantiate().Position(gameObject.Position()).Show();
                return;
            }

            percent = Random.Range(0, 1.0f);
            if (percent <= CoinPercent.Value)
            {
                //生成金币
                PowerUpManager.Default.Coin.Instantiate().Position(gameObject.Position()).Show();
                return;
            }

            percent = Random.Range(0, 1.0f);
            if (percent <= 0.3f)
            {
                //生成鸡腿
                PowerUpManager.Default.HP.Instantiate().Position(gameObject.Position()).Show();
                return;
            }

            percent = Random.Range(0, 1.0f);
            if (percent <= 0.1f)
            {
                //生成炸弹
                PowerUpManager.Default.Bomb.Instantiate().Position(gameObject.Position()).Show();
                return;
            }

            percent = Random.Range(0, 1.0f);
            if (percent <= 0.1f)
            {
                //生成获取所有经验
                PowerUpManager.Default.GetAllExp.Instantiate().Position(gameObject.Position()).Show();
                return;
            }
        }

        protected override void Init()
        {
            //注册模块
            this.RegisterSystem(new SaveSystem());
            this.RegisterSystem(new CoinUpgradeSystem());
        }

        public static int ExpToNextLv => Level.Value * 5;
    }
}
