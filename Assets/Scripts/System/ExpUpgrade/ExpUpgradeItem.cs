using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSurvivor
{
    public class ExpUpgradeItem
    {
        public bool UpgradeFinished 
        {
            get { return CurrentLevel.Value > MaxLevel; }
            set { UpgradeFinished = value; }
        }

        public string Key { get; private set; }

        public string Desciption => mDesciptionFactory(this);

        public int MaxLevel { get; private set; }

        public BindableProperty<int> CurrentLevel { get; private set; } = new BindableProperty<int>(1);

        public BindableProperty<bool> Visible { get; set; } = new BindableProperty<bool>();

        private Func<ExpUpgradeItem, string> mDesciptionFactory;

        public void Upgrade()
        {          
            CurrentLevel.Value++;
            mOnUpdate?.Invoke(this, CurrentLevel.Value);
        }

        private Action<ExpUpgradeItem,int> mOnUpdate;


        public ExpUpgradeItem WithKey(string key)
        {
            Key = key;
            return this;
        }

        public ExpUpgradeItem WithDesciptionFactory(Func<ExpUpgradeItem, string> DesciptionFactory)
        {
            mDesciptionFactory = DesciptionFactory;
            return this;
        }

        public ExpUpgradeItem WithOnUpgrade(Action<ExpUpgradeItem,int> onUpgrade)
        {
            mOnUpdate = onUpgrade;
            return this;
        }

        public ExpUpgradeItem WithMaxLevel(int maxLevel)
        {
            MaxLevel = maxLevel;
            return this;
        }
    }
}
