using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:6a8101a4-50f4-4e41-a710-1abf59445a40
	public partial class UIGameStartOnlyPanel
	{
		public const string Name = "UIGameStartOnlyPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnCoinUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnGameStart;
		[SerializeField]
		public RectTransform CoinUpgradePanel;
		[SerializeField]
		public UnityEngine.UI.Button BtnExpPercentUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnCoinPercentUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnCoinPanelClose;
		[SerializeField]
		public UnityEngine.UI.Text CoinText;
		
		private UIGameStartOnlyPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnCoinUpgrade = null;
			BtnGameStart = null;
			CoinUpgradePanel = null;
			BtnExpPercentUpgrade = null;
			BtnCoinPercentUpgrade = null;
			BtnCoinPanelClose = null;
			CoinText = null;
			
			mData = null;
		}
		
		public UIGameStartOnlyPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameStartOnlyPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameStartOnlyPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
