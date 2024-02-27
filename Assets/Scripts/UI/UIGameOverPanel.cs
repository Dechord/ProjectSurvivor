using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectSurvivor
{
	public class UIGameOverPanelData : UIPanelData
	{
	}
	public partial class UIGameOverPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			Time.timeScale = 0;
			mData = uiData as UIGameOverPanelData ?? new UIGameOverPanelData();
			// please add init code here
			ActionKit.OnUpdate.Register(() =>
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					Global.ResetData();
					this.CloseSelf();
					SceneManager.LoadScene("Game");
				}
			}).UnRegisterWhenGameObjectDestroyed(this);


			BtnBackToStart.onClick.AddListener(()=> 
			{
				this.CloseSelf();
				SceneManager.LoadScene("GameStart");
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
			Time.timeScale = 1.0f;
		}
	}
}
