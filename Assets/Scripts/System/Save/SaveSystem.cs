using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSurvivor
{
    public class SaveSystem : AbstractSystem
    {
        void Save()
        {

        }

        void Load()
        {

        }


        public void SaveBool(string key,int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public bool LoadBool(string key, int defaultValue)
        {
            return PlayerPrefs.GetInt(key, defaultValue) == 1;
        }

        public void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public int LoadInt(string key, int defaultValue)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public string LoadString(string key, string defaultValue)
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        protected override void OnInit()
        {
            ActionKit.OnGUI.Register(() =>
            {

            });
        }
        
    }
}
