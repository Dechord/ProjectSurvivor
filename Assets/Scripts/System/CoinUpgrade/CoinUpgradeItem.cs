using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSurvivor
{
    public class CoinUpgradeItem
    {
        public bool UpgradeFinished { get; set; } = false;
        public string Key { get; private set; }

        public string Desciption { get; private set; } // ÃèÊö

        public int Price { get; private set; } //¼Û¸ñ

        public bool IsShow => !UpgradeFinished && ConditionCheck();

        public void Upgrade()
        {
            mOnUpdate?.Invoke(this);
            UpgradeFinished = true;
            CoinUpgradeSystem.OnCoinUpgradeSystemChanged.Trigger();
        }

        public bool ConditionCheck()
        {
            if (mConditionCheck != null)
            {
                return !UpgradeFinished && mConditionCheck.Invoke(this);
            }
            return !UpgradeFinished;
        }

        private Action<CoinUpgradeItem> mOnUpdate;
        private Func<CoinUpgradeItem, bool> mConditionCheck;
        

        public CoinUpgradeItem WithKey(string key)
        {
            Key = key;
            return this;
        }

        public CoinUpgradeItem WithDesciption(string des)
        {
            Desciption = des;
            return this;
        }

        public CoinUpgradeItem WithPrice(int price)
        {
            Price = price;
            return this;
        }

        public CoinUpgradeItem WithOnUpgrade(Action<CoinUpgradeItem> onUpgrade)
        {
            mOnUpdate = onUpgrade;
            return this;
        }

        public CoinUpgradeItem WithConditionCheck(Func<CoinUpgradeItem, bool> conditionCheck)
        {
            mConditionCheck = conditionCheck;
            return this;
        }
    }
}
