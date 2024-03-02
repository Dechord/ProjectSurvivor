using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectSurvivor
{
    public class ExpUpgradeSystem : AbstractSystem
    {
        public List<ExpUpgradeItem> Items { get; } = new List<ExpUpgradeItem>();

        ExpUpgradeItem Add(ExpUpgradeItem item)
        {
            Items.Add(item);
            return item;
        }

        protected override void OnInit()
        {
            ResetData();
            //
            Global.Level.Register((_) =>
            {
                Roll();
            });        
        }

        public void ResetData()
        {
            Items.Clear();
            var simpleDamageLv1 = Add(new ExpUpgradeItem()
              .WithKey("simple_damage")
              .WithDesciptionFactory(item => $"»ù´¡¹¥»÷Á¦ÌáÉýLv{item.CurrentLevel.Value}")
              .WithMaxLevel(10)
              .WithOnUpgrade((item, level) =>
              {
                  Global.SimpleAbilityDamage.Value *= 1.5f;

              }));

            var simpledurationLv1 = Add(new ExpUpgradeItem()
               .WithKey("simple_duration")
               .WithDesciptionFactory(item => $"»ù´¡¹¥»÷¼ä¸ô¼õÉÙLv{item.CurrentLevel.Value}")
               .WithMaxLevel(10)
               .WithOnUpgrade((item, level) =>
               {
                   Global.SimpleAbilityDuration.Value *= 0.8f;
               }));
        }

        void Roll()
        {
            foreach (var item in Items)
            {
                item.Visible.Value = false;
            }

            var showItem = Items.Where(item => !item.UpgradeFinished).ToList().GetRandomItem();
            if (showItem != null)
            {
                showItem.Visible.Value = true;
            }           
        }
    }
}
