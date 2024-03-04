using UnityEngine;
using QFramework;
using System.Collections.Generic;
using System;

namespace ProjectSurvivor
{
	

	public partial class EnemyGenerator : ViewController
	{
		[SerializeField]
		public LevelConfig Config;
		private Transform playerTrans;
		private float mGenerateDistance = 10.0f;

		public static BindableProperty<int> EnemyCount = new BindableProperty<int>(0);

		Queue<EnemyWave> mEnemyWaveQueue = new Queue<EnemyWave>();

		private float mCurrentWaveDurationSeconds = 0;
		private float mCurrentWaveSeconds = 0;
		private EnemyWave mCurrentWave;

		public int WaveCount = 0;

		private int mTotalCount = 0;
		public bool LastWave => WaveCount == mTotalCount;

		public EnemyWave CurrentWave => mCurrentWave;

		void Start()
		{
			// Code Here
			playerTrans = GameObject.Find("Player").transform;
            //
            foreach (var waveGroup in Config.EnemyWaveGroups)
            {
                foreach (var wave in waveGroup.Waves)
                {
					mTotalCount++;
					mEnemyWaveQueue.Enqueue(wave);
				}				
			}
		}

        private void Update()
        {
			if (mCurrentWave == null)
			{
				if (mEnemyWaveQueue.Count > 0)
				{
					WaveCount++;
					mCurrentWave = mEnemyWaveQueue.Dequeue();
					mCurrentWaveDurationSeconds = 0;
					mCurrentWaveSeconds = 0;
				}
			}

			if (mCurrentWave != null)
			{
				mCurrentWaveSeconds += Time.deltaTime;
				mCurrentWaveDurationSeconds+= Time.deltaTime;

				if (mCurrentWaveDurationSeconds >= mCurrentWave.GenerateDuration && playerTrans != null)
				{
					var randomAngle = UnityEngine.Random.Range(0, 360f);
					var randomRadius = randomAngle * Mathf.Deg2Rad;
					var direction = new Vector3(Mathf.Cos(randomRadius), Mathf.Sin(randomRadius));
					var generatePos = playerTrans.position + direction * mGenerateDistance;

					mCurrentWaveDurationSeconds = 0;
					mCurrentWave.EnemyPrefab.Instantiate().Position(generatePos).Show();
				}

				//
				if (mCurrentWaveSeconds >= mCurrentWave.GenerateWaveSeconds)
				{
					mCurrentWave = null;
				}
			}			
		}
    }
}
