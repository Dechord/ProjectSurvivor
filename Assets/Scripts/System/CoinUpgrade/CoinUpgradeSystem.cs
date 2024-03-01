using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjectSurvivor
{
    public class CoinUpgradeSystem : AbstractSystem,ICanSave
    {
        public List<CoinUpgradeItem> Items { get; } = new List<CoinUpgradeItem>();

        public static EasyEvent OnCoinUpgradeSystemChanged = new EasyEvent();
        
        CoinUpgradeItem Add(CoinUpgradeItem item)
        {
            Items.Add(item);
            return item;
        }

        protected override void OnInit()
        {
            var coinUpgradeLv1 = Add(
                new CoinUpgradeItem()
                .WithKey("coin_percentLv1")
                .WithDesciption("��ҵĵ������Lv1��")
                .WithPrice(5)
                .WithUpdateAction((item) =>
                {
                    Global.CoinPercent.Value += 0.1f;
                    Global.Coin.Value -= item.Price;
                }));

            var coinUpgradeLv2 = Add(
                new CoinUpgradeItem()
                .WithKey("coin_percentLv2")
                .WithDesciption("��ҵĵ������Lv2��")
                .WithPrice(7)
                .WithConditionCheck((_) =>
                {
                    return coinUpgradeLv1.UpgradeFinished;
                })
                .WithUpdateAction((item) =>
                {
                    Global.CoinPercent.Value += 0.1f;
                    Global.Coin.Value -= item.Price;
                }));

            var coinUpgradeLv3 = Add(
                new CoinUpgradeItem()
                .WithKey("coin_percentLv3")
                .WithDesciption("��ҵĵ������Lv3��")
                .WithPrice(10)
                .WithConditionCheck((_) =>
                {
                    return coinUpgradeLv2.UpgradeFinished;
                })
                .WithUpdateAction((item) =>
                {
                    Global.CoinPercent.Value += 0.1f;
                    Global.Coin.Value -= item.Price;
                }));

            Items.Add(
                new CoinUpgradeItem()
                .WithKey("exp_percent")
                .WithDesciption("����ĵ������������")
                .WithPrice(5)
                .WithUpdateAction((item) =>
                {
                    Global.ExpPercent.Value += 0.1f;
                    Global.Coin.Value -= item.Price;
                }));


            Items.Add(
                new CoinUpgradeItem()
                .WithKey("max_hp")
                .WithDesciption("�������Ѫ��+1��")
                .WithPrice(30)
                .WithUpdateAction((item) =>
                {
                    Global.MaxHP.Value++;
                    Global.Coin.Value -= item.Price;
                }));

            Load();

            OnCoinUpgradeSystemChanged.Register(() =>
            {
                Save();
            });
        }

        public void Load()
        {
            var saveSystem = this.GetSystem<SaveSystem>();
            foreach (var item in Items)
            {
                item.UpgradeFinished = saveSystem.LoadBool(item.Key, 0);
            }
        }

        public void Save()
        {
            var saveSystem = this.GetSystem<SaveSystem>();
            foreach (var item in Items)
            {
                saveSystem.SaveBool(item.Key, item.UpgradeFinished? 1:0);
            }
        }
    }
}
