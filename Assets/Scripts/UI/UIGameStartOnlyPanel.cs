using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectSurvivor
{
	public class UIGameStartOnlyPanelData : UIPanelData
	{
	}
	public partial class UIGameStartOnlyPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGameStartOnlyPanelData ?? new UIGameStartOnlyPanelData();
			// please add init code here

			BtnCoinUpgrade.onClick.AddListener(()=> 
			{
				CoinUpgradePanel.Show();
			});

			BtnGameStart.onClick.AddListener(() =>
			{
				Global.ResetData();
				//
				this.CloseSelf();
				//
				SceneManager.LoadScene("Game");
			});
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
