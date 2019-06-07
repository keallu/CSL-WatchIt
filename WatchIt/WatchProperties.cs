using System;
using UnityEngine;

namespace WatchIt
{
    public class WatchProperties
    {
        public float WarningPanelDefaultPositionX;
        public float WarningPanelDefaultPositionY;
        public float PanelDefaultPositionX;
        public float PanelDefaultPositionY;

        private static WatchProperties instance;

        public static WatchProperties Instance
        {
            get
            {
                return instance ?? (instance = new WatchProperties());
            }
        }

        public void ResetWarningPanelPosition()
        {
            try
            {
                ModConfig.Instance.WarningPositionX = WarningPanelDefaultPositionX;
                ModConfig.Instance.WarningPositionY = WarningPanelDefaultPositionY;
                ModConfig.Instance.Save();
            }
            catch (Exception e)
            {
                Debug.Log("[Hide It!] WatchProperties:ResetWarningPanelPosition -> Exception: " + e.Message);
            }
        }

        public void ResetPanelPosition()
        {
            try
            {
                ModConfig.Instance.PositionX = PanelDefaultPositionX;
                ModConfig.Instance.PositionY = PanelDefaultPositionY;
                ModConfig.Instance.Save();
            }
            catch (Exception e)
            {
                Debug.Log("[Hide It!] WatchProperties:ResetPanelPosition -> Exception: " + e.Message);
            }
        }
    }
}
