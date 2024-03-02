using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace ProjectSurvivor
{
	public partial class ExpUpgradePanel : ViewController,IController
	{
        public IArchitecture GetArchitecture()
        {
			return Global.Interface;
        }

        void Start()
		{
            foreach (var expUpgradeItem in this.GetSystem<ExpUpgradeSystem>().Items)
            {
                ExpUpgradeItemPrefab.InstantiateWithParent(UpgradeRoot)
                    .Self((prefabItem) =>
                    {
                        var itemCache = expUpgradeItem;
                        var selfCache = prefabItem;
                        prefabItem.onClick.AddListener(() =>
                        {
                            itemCache.Upgrade();
                            Time.timeScale = 1.0f;
                            //
                            this.Hide();
                            //
                            AudioKit.PlaySound("AbilityLevelUp");
                        });

                        //¼ì²âÊÇ·ñÏÔÊ¾
                        itemCache.Visible.RegisterWithInitValue((visible) =>
                        {
                            if (!visible)
                            {
                                selfCache.Hide();
                            }
                            else
                            {
                                selfCache.Show();
                            }
                        }).UnRegisterWhenGameObjectDestroyed(selfCache);

                        //
                        itemCache.CurrentLevel.RegisterWithInitValue((lv) =>
                        {
                            selfCache.GetComponentInChildren<Text>().text = itemCache.Desciption;
                        }).UnRegisterWhenGameObjectDestroyed(selfCache);
                    });
            }
		}       
	}
}
