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
				UpgradeRoot.Show();
				Time.timeScale = 0;
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
			UpgradeRoot.Hide();
			BtnUpgrade.onClick.AddListener(() =>
			{
				Global.SimpleAbilityDamage.Value *= 1.5f;
				UpgradeRoot.Hide();
				Time.timeScale = 1.0f;
			});

			BtnSimpleAbilityDurationUpgrade.onClick.AddListener(() =>
			{
				Global.SimpleAbilityDuration.Value *= 0.8f;
				UpgradeRoot.Hide();
				Time.timeScale = 1.0f;
			});

			//
			var enemyGenerator = FindObjectOfType<EnemyGenerator>();
			ActionKit.OnUpdate.Register(() =>
			{
				Global.CurrentSeconds.Value += Time.deltaTime;
				//
				if (enemyGenerator.LastWave && enemyGenerator.CurrentWave == null && EnemyGenerator.EnemyCount.Value == 0)
				{
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
