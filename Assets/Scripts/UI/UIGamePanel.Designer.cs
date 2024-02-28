using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:e2d3b7fc-4475-4c7f-bfa1-509197e0f10f
	public partial class UIGamePanel
	{
		public const string Name = "UIGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text HPText;
		[SerializeField]
		public UnityEngine.UI.Text ExpText;
		[SerializeField]
		public UnityEngine.UI.Text LevelText;
		[SerializeField]
		public UnityEngine.UI.Text TimeText;
		[SerializeField]
		public UnityEngine.UI.Text EnemyCountText;
		[SerializeField]
		public UnityEngine.UI.Text CoinText;
		[SerializeField]
		public Transform UpgradeRoot;
		[SerializeField]
		public UnityEngine.UI.Button BtnUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnSimpleAbilityDurationUpgrade;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			HPText = null;
			ExpText = null;
			LevelText = null;
			TimeText = null;
			EnemyCountText = null;
			CoinText = null;
			UpgradeRoot = null;
			BtnUpgrade = null;
			BtnSimpleAbilityDurationUpgrade = null;
			
			mData = null;
		}
		
		public UIGamePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGamePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGamePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
