using UnityEngine;
using QFramework;
using UnityEngine.UI;
using System.Linq;

namespace ProjectSurvivor
{
	public partial class CoinUpgradePanel : ViewController, IController
	{
        

        private void Awake()
        {
            BtnCoinPanelClose.onClick.AddListener(()=> 
            {
                this.Hide();
            });
          
            CoinUpgradeItemPrefab.Hide();

            Global.Coin.RegisterWithInitValue((coin) =>
            {
                CoinText.text = $"��ң�{coin}";
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            foreach (var coinUpgradeItem in this.GetSystem<CoinUpgradeSystem>().Items)
            {
                CoinUpgradeItemPrefab.InstantiateWithParent(CoinUpgradeItemRoot)
                    .Self((prefabItem) =>
                    {
                        var itemCache = coinUpgradeItem;
                        var selfCache = prefabItem;
                        prefabItem.GetComponentInChildren<Text>().text = $"{coinUpgradeItem.Desciption}{coinUpgradeItem.Price} ���";
                        prefabItem.onClick.AddListener(() =>
                        {
                            itemCache.Upgrade();
                            //
                            AudioKit.PlaySound("AbilityLevelUp");
                        });

                        //����Ƿ���ʾ
                        if (itemCache.IsShow)
                        {
                            selfCache.Show();
                        }
                        else 
                        {
                            selfCache.Hide();
                        }

                        //
                        CoinUpgradeSystem.OnCoinUpgradeSystemChanged.Register(() =>
                        {
                            //����Ƿ���ʾ
                            if (itemCache.IsShow)
                            {
                                selfCache.Show();
                            }
                            else
                            {
                                selfCache.Hide();
                            }
                        }).UnRegisterWhenGameObjectDestroyed(selfCache);

                        //
                        Global.Coin.RegisterWithInitValue((coin) =>
                        {
                            selfCache.interactable = coin >= itemCache.Price;
                        }).UnRegisterWhenGameObjectDestroyed(selfCache);
                    });
            }
        }

        void Start()
        {
            
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}
