using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:9a6ac79e-b531-4403-98fd-bb1a27d01137
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
		public UnityEngine.UI.Button BtnCoinPanelClose;
		[SerializeField]
		public UnityEngine.UI.Text CoinText;
		[SerializeField]
		public RectTransform CoinUpgradeItemRoot;
		[SerializeField]
		public UnityEngine.UI.Button CoinUpgradeItemPrefab;
		
		private UIGameStartOnlyPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnCoinUpgrade = null;
			BtnGameStart = null;
			CoinUpgradePanel = null;
			BtnCoinPanelClose = null;
			CoinText = null;
			CoinUpgradeItemRoot = null;
			CoinUpgradeItemPrefab = null;
			
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
