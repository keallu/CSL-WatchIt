using System;
using UnityEngine;

namespace WatchIt
{
    public class WatchManager
    {
        public float OnOffButtonDefaultPositionX;
        public float OnOffButtonDefaultPositionY;
        public float DefaultPositionX;
        public float DefaultPositionY;

        private static WatchManager instance;

        public static WatchManager Instance
        {
            get
            {
                return instance ?? (instance = new WatchManager());
            }
        }

        public void ResetOnOffButtonPosition()
        {
            try
            {
                ModConfig.Instance.OnOffButtonPositionX = OnOffButtonDefaultPositionX;
                ModConfig.Instance.OnOffButtonPositionY = OnOffButtonDefaultPositionY;
                ModConfig.Instance.Save();
            }
            catch (Exception e)
            {
                Debug.Log("[Hide It!] WatchManager:ResetOnOffButtonPosition -> Exception: " + e.Message);
            }
        }

        public void ResetPosition()
        {
            try
            {
                ModConfig.Instance.PositionX = DefaultPositionX;
                ModConfig.Instance.PositionY = DefaultPositionY;
                ModConfig.Instance.Save();
            }
            catch (Exception e)
            {
                Debug.Log("[Hide It!] WatchManager:ResetPosition -> Exception: " + e.Message);
            }
        }
    }
}
