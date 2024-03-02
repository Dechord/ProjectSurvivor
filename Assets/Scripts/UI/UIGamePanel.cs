using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public class UIGamePanelData : UIPanelData
	{
	}
	public partial class UIGamePanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGamePanelData ?? new UIGamePanelData();

			EnemyGenerator.EnemyCount.RegisterWithInitValue((enemyCount)=> 
			{
				EnemyCountText.text = $"敌人：{enemyCount}";
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			//
			Global.HP.RegisterWithInitValue((hp) => {
				HPText.text = $"HP：({Global.HP.Value}/{Global.MaxHP.Value})";
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			//
			Global.MaxHP.RegisterWithInitValue((hp) => {
				HPText.text = $"HP：({Global.HP.Value}/{Global.MaxHP.Value})";
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			// please add init code here
			Global.Exp.RegisterWithInitValue((exp) => {
				ExpText.text = $"经验：({exp}/{Global.ExpToNextLv})";
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			//
			Global.Level.RegisterWithInitValue((lv) => {
				LevelText.text = $"等级：{lv}";
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			//
			Global.CurrentSeconds.RegisterWithInitValue((currentSeconds) => 
			{
				if (Time.frameCount % 30 == 0)
				{
					var secondsToInt = Mathf.FloorToInt(currentSeconds);
					var seconds = secondsToInt % 60;
					int mins = secondsToInt / 60;
					TimeText.text = $"时间：{mins:00}:{seconds:00}";
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			//
			Global.Level.Register((lv) =>
			{
				ExpUpgradePanel.Show();
				Time.timeScale = 0;
				AudioKit.PlaySound("LevelUp");
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			//
			Global.Coin.RegisterWithInitValue((coin)=> 
			{
				PlayerPrefs.SetInt(nameof(coin), coin);
				CoinText.text = $"金币：{coin}";
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			//
			Global.Exp.RegisterWithInitValue((exp) => {
				if (exp >= Global.ExpToNextLv)
				{
					var costExp = Global.ExpToNextLv;
					Global.Level.Value++;
					Global.Exp.Value -= costExp;					
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			//
			ExpUpgradePanel.Hide();			
			//
			var enemyGenerator = FindObjectOfType<EnemyGenerator>();
			ActionKit.OnUpdate.Register(() =>
			{
				Global.CurrentSeconds.Value += Time.deltaTime;
				//
				if (enemyGenerator.LastWave && enemyGenerator.CurrentWave == null && EnemyGenerator.EnemyCount.Value == 0)
				{
					//
					this.CloseSelf();

					UpgradeRoot.Hide();
					UIKit.OpenPanel<UIGamePassPanel>();					
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
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
