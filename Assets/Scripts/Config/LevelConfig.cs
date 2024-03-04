using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSurvivor
{
    [CreateAssetMenu]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField]
        public List<EnemyWaveGroup> EnemyWaveGroups = new List<EnemyWaveGroup>();
    }

    [Serializable]
    public class EnemyWaveGroup    
    {
        public string Name = string.Empty;
        [TextArea]
        public string Description = string.Empty;
        [SerializeField]
        public List<EnemyWave> Waves = new List<EnemyWave>();
    }


    [Serializable]
    public class EnemyWave
    {
        public string Name = string.Empty;
        public bool Active = true;
        public GameObject EnemyPrefab;
        public float GenerateWaveSeconds = 10;
        public float GenerateDuration = 1;
        public float SpeedScale = 1.0f;
        public float HPScale = 1.0f;
    }
}
